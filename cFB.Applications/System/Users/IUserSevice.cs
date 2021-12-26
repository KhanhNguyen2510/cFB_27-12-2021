using cFB.ViewModels.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cFB.Applications.System.Users
{
    public interface IUserSevice
    {
        //Check
        Task<GetCheckRole> CheckRole(string administrativeDivision_Id);
        Task<string> CheckNumber(string NumberPhone);
        //Login
        Task<string> LoginInWed(GetLoginRequest request);
        Task<bool> Authencate(GetLoginRequest request);
        //Create
        Task<bool> Register(GetRegisterRequest request);
        //Update
        Task<bool> Update(GetUserUpdateRequest request);
        Task<bool> UpdateRole(GetUserUpdateRoleRequest request);
        //Delete
        Task<bool> DeleteUser(string administrativeDivision_Id);
        //List
        Task<List<GetUserSeviceViewModel>> GetUsersPaging(GetUserRequest request);
        Task<GetUserSeviceViewModel> GetUsersById(string administrativeDivisionId);
        Task<List<GetUserViewModel>> GetUsersPagingList(GetUserRequest request);
    }
}
