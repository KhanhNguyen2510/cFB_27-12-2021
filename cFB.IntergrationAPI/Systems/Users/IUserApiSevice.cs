using cFB.ViewModels.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Systems.Users
{
    public interface IUserApiSevice
    {
        Task<string> LoginInWed(GetLoginRequest request);
        Task<bool> Authencate(GetLoginRequest request);
        Task<bool> Create(GetRegisterRequest request);
        Task<bool> Update(GetUserUpdateRequest request);
        Task<bool> UpdateRole(GetUserUpdateRoleRequest request);
        Task<GetCheckRole> CheckRole(string administrativeDivision_Id);
        Task<string> CheckPhone(string NumberPhone);
        Task<List<GetUserSeviceViewModel>> GetUsersPaging(GetUserRequest request);
        Task<GetUserSeviceViewModel> GetUserById(string administrativeDivision_Id);
        Task<List<GetUserViewModel>> GetUsersPagingList(GetUserRequest request);
    }
}
