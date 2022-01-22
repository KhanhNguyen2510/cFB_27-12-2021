using cFB.Data.Enums;
using cFB.IntergrationAPI.Historys;
using cFB.IntergrationAPI.Systems.Users;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Historys;
using cFB.ViewModels.System;
using cFB.Wedsite.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Wedsite.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {

        private readonly IHistoryApiClient _historyApiClient;
        private readonly IConfiguration _configuration;
        private readonly IUserApiSevice _userApiSevice;

        public HistoryController(IHistoryApiClient historyApiClient, IConfiguration configuration, IUserApiSevice userApiSevice)
        {
            _configuration = configuration;
            _historyApiClient = historyApiClient;
            _userApiSevice = userApiSevice;
        }

        public string LoadRoleUser()
        {
            TempData.Keep("name");
            return ShareContants.RoleOfUser = TempData["name"].ToString(); ;
        }

        public async Task<IActionResult> Index(string userId, Event? Event, DateTime? StartDate, DateTime? EndDate, int pageIndex = 1, int pageSize = 100)
        {
            /*Response.Headers.Add("Refresh", "60000");*/ // reset sau 10 phút
            ShareContants.NumberPageVisits = 0;

            if (pageSize == ShareContants.PageSizeErro) pageSize = 1;

            if (EndDate != null && StartDate == null) StartDate = EndDate;

            var request = new GetManagerHistoryRequest()
            {
                AdministrativeDivision_Id = userId == null ? LoadRoleUser() : userId,
                Event = Event,
                StartDate = StartDate?.Date,
                EndDate = EndDate?.Date,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _historyApiClient.GetAllHistory(request);

            var envent = new List<HistoryEnventsModel>();
            envent.Add(new HistoryEnventsModel() { Id = Data.Enums.Event.Create, Name = "Thao tác thêm" });
            envent.Add(new HistoryEnventsModel() { Id = Data.Enums.Event.Update, Name = "Thao tác cập nhật" });
            envent.Add(new HistoryEnventsModel() { Id = Data.Enums.Event.Delete, Name = "Thao tác xóa" });
            envent.Add(new HistoryEnventsModel() { Id = Data.Enums.Event.Report, Name = "Thao tác lập báo cáo" });
            ViewBag.Event = envent.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = Event == x.Id
            });

            var requestAdministrativeDivision = new GetUserRequest()
            {
                AdministrativeDivisionID = LoadRoleUser()
            };
            var administrativeDivision = await _userApiSevice.GetUsersPaging(requestAdministrativeDivision);
            ViewBag.AdministrativeDivision = administrativeDivision.Select(x => new SelectListItem()
            {
                Text = x.AdministrativeDivisionName,
                Value = x.AdministrativeDivisionID,
                Selected = userId == x.AdministrativeDivisionID
            });

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.PageSize = pageSize;
            ViewBag.RoleOfUser = LoadRoleUser();

            return View(data);
        }
        public async Task<IActionResult> HistoryClient(string userId, string IpAdress, DateTime? StartDate, DateTime? EndDate, int pageIndex = 1, int pageSize = 100)
        {
            TempData.Keep("Role");
            if (TempData["Role"].ToString() != "Admin")
            {
                TempData["result"] = ShowMessage.CheckUser(User.Identity.Name);
                return RedirectToAction("Home", "Home");
            }
            else
            {

                ShareContants.NumberPageVisits = 0;

                if (pageSize == ShareContants.PageSizeErro) pageSize = 1;

                if (EndDate != null && StartDate == null) StartDate = EndDate;


                var request = new GetManagerHistoryClientRequest()
                {
                    AdministrativeDivision_Id = userId == null ? LoadRoleUser() : userId,
                    IPAdress = IpAdress,
                    StartDate = StartDate?.Date,
                    EndDate = EndDate?.Date,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
                var data = await _historyApiClient.GetAllHistoryClient(request);

                var requestAdministrativeDivision = new GetUserRequest()
                {
                    AdministrativeDivisionID = LoadRoleUser()
                };
                var administrativeDivision = await _userApiSevice.GetUsersPaging(requestAdministrativeDivision);
                ViewBag.AdministrativeDivision = administrativeDivision.Select(x => new SelectListItem()
                {
                    Text = x.AdministrativeDivisionName,
                    Value = x.AdministrativeDivisionID,
                    Selected = userId == x.AdministrativeDivisionID
                });

                ViewBag.StartDate = StartDate;
                ViewBag.EndDate = EndDate;
                ViewBag.PageSize = pageSize;
                ViewBag.RoleOfUser = LoadRoleUser();

                return View(data);
            }
        }
    }
}
