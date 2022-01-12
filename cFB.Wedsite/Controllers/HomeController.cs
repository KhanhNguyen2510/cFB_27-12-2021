using cFB.IntergrationAPI.Historys;
using cFB.IntergrationAPI.Systems.Users;
using cFB.Utilities.Constants;
using cFB.ViewModels.System;
using cFB.Wedsite.Messages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cFB.Wedsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserApiSevice _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IHistoryApiClient _historyApiClient;
        private readonly IHttpContextAccessor _accessor;


        public HomeController(IUserApiSevice userApiSevice, IConfiguration configuration,IHistoryApiClient historyApiClient,IHttpContextAccessor httpContextAccessor)
        {
            _userApiClient = userApiSevice;
            _configuration = configuration;
            _historyApiClient = historyApiClient;
            _accessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetLoginRequest request)
        {
            ShareContants.NumberPageVisits = 0;
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var result = await _userApiClient.LoginInWed(request);

                var userPrincipal = this.ValidateToken(result);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);

                if (result == null)
                {
                    ViewBag.SuccessMsg = ShowMessage.AuthenticateFailed();
                    return View();
                }

                var userAgent = Request.Headers["User-Agent"];
                var ipAdress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                var check = await _userApiClient.CheckRole(request.UserName);
                await _historyApiClient.CreateHistoryClient(request.UserName, userAgent,ipAdress);
                TempData["Role"] = check.ManagerID;
                TempData["name"] = request.UserName;


                return RedirectToAction("Home", "Home");
            }
            catch (Exception)
            {
                ViewBag.SuccessMsg = ShowMessage.AuthenticateFailed();
                return View();
            }
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var user = User.Identity.Name;
            var data = await _userApiClient.GetUserById(user);

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"].ToString();
            }

            return View(data);
        }
    }
}
