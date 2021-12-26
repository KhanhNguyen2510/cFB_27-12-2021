using cFB.Applications.Catalog.Historys;
using cFB.Data.EFs;
using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.Utilities.AutoStrings;
using cFB.Utilities.Constants;
using cFB.ViewModels.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cFB.Applications.System.Users
{
    public class UserSevice : IUserSevice
    {
        private readonly cFBDbContext _context;
        private readonly IHistorySevice _historySevice;

        private readonly IConfiguration _config;
        private static string administrativeDivisionId = "";
        public UserSevice(cFBDbContext context, IHistorySevice historySevice, IConfiguration configuration)
        {
            _context = context;
            _historySevice = historySevice;
            _config = configuration;
        }
        //Check
        public async Task<GetCheckRole> CheckRole(string administrativeDivision_Id)
        {
            var user = await _context.AdministrativeDivisions.SingleOrDefaultAsync(x => x.AdministrativeDivisionId == administrativeDivision_Id);
            if (user == null) return null;

            var userSeviceViewModel = new GetCheckRole()
            {
                ManagerID = user.ManagerId
            };

            return userSeviceViewModel;
        }
        public async Task<string> CheckNumber(string NumberPhone)
        {
            var check = await _context.AdministrativeDivisions.FirstOrDefaultAsync(x => x.NumberPhone == NumberPhone);
            if (check == null) return null;
            return check.NumberPhone;
        }
        //Login
        public async Task<bool> Authencate(GetLoginRequest request)
        {
            try
            {
                var user = await _context.AdministrativeDivisions.FirstOrDefaultAsync(x => x.AdministrativeDivisionId == request.UserName && x.Password == request.Password);
                if (user != null)
                {
                    await UpdateTimeOnline(request.UserName);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string> LoginInWed(GetLoginRequest request)
        {
            var user = await Authencate(request);
            if (user == true)
            {
                await UpdateTimeOnline(request.UserName);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, request.UserName));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            return null;
        }
        //Create
        public async Task<bool> Register(GetRegisterRequest request)
        {
            try
            {
                administrativeDivisionId = AutoGenerate.UserRandomID();
                var data = new AdministrativeDivision()
                {
                    AdministrativeDivisionId = administrativeDivisionId,
                    AdministrativeDivisionName = (request.AdministrativeDivisionName != null) ? request.AdministrativeDivisionName : "",
                    Addrees = (request.Addrees != null) ? request.Addrees : "",
                    NumberPhone = (request.NumberPhone != null) ? request.NumberPhone : "",
                    Password = (request.Password != null) ? request.Password : administrativeDivisionId,// khi mà không tạo password thì sẽ lấy tên người đó làm mật khẫu 
                    TimeOnline = DateTime.Now,
                    ManagerId = "User"
                };

                _context.AdministrativeDivisions.Add(data);
                await _context.SaveChangesAsync();

                await _historySevice.CreateInHistory(request.AdministrativeDivision_Admin, Event.Create, $"Tài khoản {request.AdministrativeDivision_Admin} đã thêm một tài khoản với tên là {administrativeDivisionId}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Update
        public async Task UpdateTimeOnline(string administrativeDivisionId)
        {
            var data = await _context.AdministrativeDivisions.FirstOrDefaultAsync(x => x.AdministrativeDivisionId == administrativeDivisionId);

            if (data == null) return;

            data.TimeOnline = DateTime.Now;

            await _context.SaveChangesAsync();
        }
        public async Task<bool> Update(GetUserUpdateRequest request)
        {
            try
            {
                var user = await _context.AdministrativeDivisions.FirstOrDefaultAsync(x => x.NumberPhone == request.NumberPhone);

                if (user == null)
                    return false;

                user.AdministrativeDivisionName = (request.AdministrativeDivisionName != null) ? request.AdministrativeDivisionName : user.AdministrativeDivisionName;
                user.NumberPhone = (request.NumberPhone != null) ? request.NumberPhone : user.NumberPhone;
                user.Addrees = (request.Addrees != null) ? request.Addrees : user.Addrees;
                user.Password = (request.Password != null) ? request.Password : user.Password;

                await _context.SaveChangesAsync();
                await _historySevice.CreateInHistory(user.AdministrativeDivisionId, Event.Update, $"Tài khoản {user.AdministrativeDivisionId} đã cập nhật lại thông tin của mình");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateRole(GetUserUpdateRoleRequest request)  /// chỉ dành cho admin cấp phát( Cần nhập thêm thông tin người thêm vào )
        {
            try
            {
                var user = await _context.AdministrativeDivisions.SingleOrDefaultAsync(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID);
                if (user == null) return false;
                if (user.ManagerId == "Admin")
                {
                    return false;
                }
                else
                {
                    user.ManagerId = "Admin";
                    await _context.SaveChangesAsync();
                    await _historySevice.CreateInHistory(request.AdministrativeDivision_Admin, Event.Update, $"Tài khoản {request.AdministrativeDivision_Admin}" +
                        $"đã cấp quyền cho tài khoản {user.AdministrativeDivisionId} vào lúc {DateTime.Now}");
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Delete
        public async Task<bool> DeleteUser(string administrativeDivision_Id)
        {
            try
            {
                var user = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivision_Id).FirstOrDefaultAsync();
                if (user == null) return false;

                _context.AdministrativeDivisions.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //List
        public async Task<List<GetUserSeviceViewModel>> GetUsersPaging(GetUserRequest request)
        {
            var query = from u in _context.AdministrativeDivisions
                        select new { u };

            if (!string.IsNullOrEmpty(request.ManagerID))
            {
                query = query.Where(x => x.u.ManagerId == request.ManagerID);
            }

            if (!string.IsNullOrEmpty(request.AdministrativeDivisionID))
            {
                var rs = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (rs == null && request.AdministrativeDivisionID != ShareContants.UserAdmin)
                {
                    return null;
                }
                if (rs != ShareContants.UserAdmin || request.AdministrativeDivisionID != ShareContants.UserAdmin)
                {
                    query = query.Where(x => x.u.AdministrativeDivisionId == request.AdministrativeDivisionID);
                }
            }

            var data = await query.Select(x => new GetUserSeviceViewModel()
            {
                AdministrativeDivisionID = x.u.AdministrativeDivisionId,
                AdministrativeDivisionName = x.u.AdministrativeDivisionName,
                Addrees = x.u.Addrees,
                NumberPhone = x.u.NumberPhone,
                TimeOnline = x.u.TimeOnline,
                Password = x.u.Password,
                ManagerID = x.u.ManagerId

            }).ToListAsync();

            return data;
        }

        public async Task<GetUserSeviceViewModel> GetUsersById(string administrativeDivisionId)
        {
            try
            {
                var query = await (from u in _context.AdministrativeDivisions
                                   where u.AdministrativeDivisionId == administrativeDivisionId
                                   select new { u }).FirstOrDefaultAsync();

                var data = new GetUserSeviceViewModel()
                {
                    AdministrativeDivisionID = query.u.AdministrativeDivisionId,
                    AdministrativeDivisionName = query.u.AdministrativeDivisionName,
                    Addrees = query.u.Addrees,
                    NumberPhone = query.u.NumberPhone,
                    TimeOnline = query.u.TimeOnline,
                    Password = query.u.Password,
                    ManagerID = query.u.ManagerId
                };

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<GetUserViewModel>> GetUsersPagingList(GetUserRequest request)
        {
            var query = from u in _context.AdministrativeDivisions
                        select new { u };

            if (!string.IsNullOrEmpty(request.ManagerID))
            {
                query = query.Where(x => x.u.ManagerId == request.ManagerID);
            }

            if (!string.IsNullOrEmpty(request.AdministrativeDivisionID))
            {
                var rs = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (rs == null && request.AdministrativeDivisionID != ShareContants.UserAdmin)
                {
                    return null;
                }
                if (rs != ShareContants.UserAdmin || request.AdministrativeDivisionID != ShareContants.UserAdmin)
                {
                    query = query.Where(x => x.u.AdministrativeDivisionId == request.AdministrativeDivisionID);
                }
            }

            var data = await query.Select(x => new GetUserViewModel()
            {
                AdministrativeDivisionID = x.u.AdministrativeDivisionId,
                AdministrativeDivisionName = x.u.AdministrativeDivisionName,
                Addrees = x.u.Addrees,
                NumberPhone = x.u.NumberPhone,
                TimeOnline = ShareContants.Checkday(x.u.TimeOnline, "user"),
                Password = x.u.Password,
                ManagerId = x.u.ManagerId
            }).ToListAsync();

            return data;
        }
    }
}
