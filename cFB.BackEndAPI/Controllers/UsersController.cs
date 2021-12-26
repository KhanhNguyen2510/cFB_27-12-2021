using cFB.Applications.System.Users;
using cFB.ViewModels.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace cFB.BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserSevice _userSevice;
        private readonly IConfiguration _config;

        public UserController(IUserSevice userSevice, IConfiguration configuration)
        {
            _userSevice = userSevice;
            _config = configuration;
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromForm] GetRegisterRequest request)
        {
            var user = await _userSevice.Register(request);
            return Json(user);
        }

        [HttpPost("UpdateToUser")]
        public async Task<JsonResult> Update([FromForm] GetUserUpdateRequest request)
        {
            var user = await _userSevice.Update(request);
            return Json(user);
        }

        [HttpPost("LoginWed")]
        public async Task<IActionResult> LoginInWed([FromForm] GetLoginRequest request)
        {
            var user = await _userSevice.LoginInWed(request);
            if (user != null)
            {
                return Json(user);

            }
            return Json(user);
        }

        [HttpPost("Login")]
        public async Task<JsonResult> Login([FromQuery] GetLoginRequest request)
        {
            var watchList = await _userSevice.Authencate(request);
            return Json(watchList);
        }

        [HttpPost("UpdateRole")]
        public async Task<JsonResult> UpdateRole([FromForm] GetUserUpdateRoleRequest request)
        {
            var user = await _userSevice.UpdateRole(request);
            return Json(user);
        }

        [HttpGet("CheckRoleUser")]
        public async Task<JsonResult> CheckRole(string administrativeDivision_Id)
        {
            var user = await _userSevice.CheckRole(administrativeDivision_Id);
            return Json(user);
        }

        [HttpDelete("Delete/{administrativeDivision_Id}")]
        public async Task<JsonResult> DeleteUser(string administrativeDivision_Id)
        {
            var user = await _userSevice.DeleteUser(administrativeDivision_Id);

            return Json(user);
        }

        [HttpGet("GetUserById/{administrativeDivisionId}")]
        public async Task<JsonResult> GetUsersById(string administrativeDivisionId)
        {
            var user = await _userSevice.GetUsersById(administrativeDivisionId);

            return Json(user);
        }

        [HttpGet("GetUsersAll")]
        public async Task<JsonResult> GetUsersPaging([FromQuery] GetUserRequest request)
        {
            var user = await _userSevice.GetUsersPaging(request);
            return Json(user);
        }

        [HttpGet("GetUsersAllList")]
        public async Task<JsonResult> GetUsersPagingList([FromQuery] GetUserRequest request)
        {
            var user = await _userSevice.GetUsersPagingList(request);
            return Json(user);
        }
        [HttpGet("CheckPhone")]
        public async Task<JsonResult> CheckPhone(string NumberPhone)
        {
            var user = await _userSevice.CheckNumber(NumberPhone);
            return Json(user);
        }
    }
}
