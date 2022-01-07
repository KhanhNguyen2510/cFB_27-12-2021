using cFB.IntergrationAPI.Systems.Users;
using cFB.Utilities.Constants;
using cFB.ViewModels.System;
using cFB.Wedsite.Messages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace cFB.Wedsite.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserApiSevice _userApiClient;
        private readonly IConfiguration _configuration;

        public UserController(IUserApiSevice userApiSevice
            , IConfiguration configuration)
        {
            _configuration = configuration;
            _userApiClient = userApiSevice;
        }
        public async Task<IActionResult> Index(string Keyword)
        {
           Response.Headers.Add("Refresh", "90000"); // reset sau 15 phút
            ShareContants.NumberPageVisits = 0;
            if (!ModelState.IsValid)
                return View();
            string role = "";
            TempData.Keep("name");
            TempData.Keep("Role");

            if (TempData["Role"].ToString() == "Admin")
                role = "User";

            var request = new GetUserRequest()
            {
                ManagerID = role
            };

            var data = await _userApiClient.GetUsersPagingList(request);

            if (TempData["Role"].ToString() == "Admin")
            {
                if (TempData["result"] != null)
                {
                    ViewBag.SuccessMsg = TempData["result"].ToString();
                }

                return View(data);
            }
            else
            {
                TempData["result"] = ShowMessage.CheckUser(User.Identity.Name);
                return RedirectToAction("Home", "Home");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(GetRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            var checkPhone = _userApiClient.CheckPhone(request.NumberPhone).Result;
            if(checkPhone != null)
            {
                TempData["result"] = ShowMessage.NumberPhone();
                if (TempData["result"] != null)
                {
                    ViewBag.SuccessMsg = TempData["result"].ToString();
                }
                return View();
            }

            var result = await _userApiClient.Create(request);

            if (result == false)
            {
                TempData["result"] = ShowMessage.AddUserSuccessful();
            }
            else
            {
                TempData["result"] = ShowMessage.AddUserFaled();
            }

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"].ToString();
            }
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(GetUserUpdateRequest request)
        {

            request.AdministrativeDivisionID = "Admin";// hiện tại không có thời gian viết js cho hàm này 
           
            var result = await _userApiClient.Update(request);
            if (result == false)
            {
                TempData["result"] = ShowMessage.UpdateUserFaled();
            }
            else
            {
                TempData["result"] = ShowMessage.UpdateUserSuccessful();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateRole(GetUserUpdateRoleRequest request)
        {
            var result = await _userApiClient.UpdateRole(request);

            if (result == false)
            {
                TempData["result"] = ShowMessage.UpdateUserFaled();
            }
            else
            {
                TempData["result"] = ShowMessage.UpdateUserSuccessful();
            }
            return RedirectToAction("Index");
        }

    }
}
