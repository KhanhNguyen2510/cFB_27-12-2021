using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.IntergrationAPI.WatchLists;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.WatchLists;
using cFB.Wedsite.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Wedsite.Controllers
{
    [Authorize]
    public class WatchListController : Controller
    {
        private readonly IWatchListApiClient _watchListApiClient;

        public WatchListController(IWatchListApiClient watchListApiClient)
        {
            _watchListApiClient = watchListApiClient;
        }

        public string LoadRoleUser()
        {
            TempData.Keep("name");
            return ShareContants.RoleOfUser = TempData["name"].ToString(); ;
        }

        public async Task<IActionResult> Index(string facebookTypeID, Status? Status, int pageIndex = 1, int pageSize = 100)
        {
           /* Response.Headers.Add("Refresh", "15");*/ // reset sau 15 phút
            ShareContants.NumberPageVisits = 0;
            if (pageSize == ShareContants.PageSizeErro) pageSize = 1;
            ViewBag.PageSize = pageSize;

            var request = new GetManageWatchListRequest()
            {
                AdministrativeDivisionID = LoadRoleUser(),
                FacebookTypeID = facebookTypeID,
                Status = Status,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var watchlist = await _watchListApiClient.GetAllWatchListPagedResult(request);

            var faceBookType = new List<FaceBookType>();
            faceBookType.Add(new FaceBookType() { FaceBookTypeId = "PAGE", FaceBookTypeName = "Trang" });
            faceBookType.Add(new FaceBookType() { FaceBookTypeId = "GR", FaceBookTypeName = "Nhóm công khai" });
            faceBookType.Add(new FaceBookType() { FaceBookTypeId = "PGR", FaceBookTypeName = "Nhóm kín" });
            faceBookType.Add(new FaceBookType() { FaceBookTypeId = "USER", FaceBookTypeName = "Cá nhân" });
            ViewBag.FaceBookTypeID = faceBookType.Select(x => new SelectListItem()
            {
                Text = x.FaceBookTypeName,
                Value = x.FaceBookTypeId,
                Selected = facebookTypeID == x.FaceBookTypeId
            });

            var status = new List<StatusEnvent>();
            status.Add(new StatusEnvent() { Id = Data.Enums.Status.Activate, Name = "Đang theo dõi" });
            status.Add(new StatusEnvent() { Id = Data.Enums.Status.Inactive, Name = "Chưa theo dõi" });
            ViewBag.Status = status.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = Status == x.Id
            });

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];

            }
            if (TempData["resultErro"] != null)
            {
                ViewBag.SuccessMsgErro = TempData["resultErro"];
            }
            return View(watchlist);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] GetWatchListCreateRequest request)
        {
            var result = await _watchListApiClient.AddNewOrUpdateWatchList(request);
            if (result == true)
            {
                TempData["result"] = ShowMessage.AddItemSuccessful();
            }
            else
            {
                TempData["resultErro"] = ShowMessage.AddItemFaled();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] GetWatchListUpdateRequest request)
        {
            var result = await _watchListApiClient.Update(request.FaceBookID, request.FaceBookName, request.FaceBookTypeID);
            if (result == true)
            {
                TempData["result"] = ShowMessage.UpdateItemSuccessful(); ;
            }
            else
            {
                TempData["resultErro"] = ShowMessage.UpdateItemFaled();
            }
            return RedirectToAction("Index");
        }
    }
}
