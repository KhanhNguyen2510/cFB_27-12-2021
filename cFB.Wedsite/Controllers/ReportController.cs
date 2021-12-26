using cFB.IntergrationAPI.Reports;
using cFB.IntergrationAPI.Systems.Users;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Reports;
using cFB.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Wedsite.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportApiClient _reportApiClient;
        private readonly IUserApiSevice _userApiSevice;

        public ReportController(IReportApiClient reportApiClient, IUserApiSevice userApiSevice)
        {
            _reportApiClient = reportApiClient;
            _userApiSevice = userApiSevice;
        }
        public string LoadRoleUser()
        {
            TempData.Keep("name");
            return ShareContants.RoleOfUser = TempData["name"].ToString(); ;
        }

        public async Task<IActionResult> Index(string userId, string postId, DateTime? dateReport, string reportID, int pageIndex = 1, int pageSize = 50)
        {
            ShareContants.NumberPageVisits = 0;

            if (pageSize == ShareContants.PageSizeErro) pageSize = 1;
            ViewBag.RoleOfUser = LoadRoleUser();
            ViewBag.Search = postId;
            ViewBag.PageSize = pageSize;

            var request = new GetReportRequest()
            {
                AdministrativeDivisionID = userId == null ? LoadRoleUser() : userId,
                DateReport = dateReport,
                PostID = postId,
                ReportID = reportID,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var report = await _reportApiClient.GetAllReport(request);

            

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
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];

            }
            if (TempData["resultErro"] != null)
            {
                ViewBag.SuccessMsgErro = TempData["resultErro"];
            }
            return View(report);
        }
    }
}
