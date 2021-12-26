using cFB.Applications.Catalog.WatchLists;
using cFB.ViewModels.Catalog.WatchLists;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListController : Controller
    {
        private readonly IWatchListService _watchListService;

        public WatchListController(IWatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        [HttpGet("GetCountWatchList")]
        public async Task<JsonResult> GetCountWatchList(string administrativeDivision_Id)
        {
            var watchList = await _watchListService.GetCountWatchList(administrativeDivision_Id);
            return Json(watchList);
        }

        [HttpGet("CheckExistInWatchList/{faceBookId}/{administrativeDivision_Id}")]
        public async Task<JsonResult> CheckExistInWatchListByID(string faceBookId, string administrativeDivision_Id)
        {
            var watchlist = await _watchListService.CheckExistInWatchListByID(faceBookId, administrativeDivision_Id);
            return Json(watchlist);
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromForm] GetWatchListCreateRequest request)
        {
            var watchlist = await _watchListService.AddNewOrUpdateWatchList(request);
            return Json(watchlist);
        }

        [HttpPost("UpdateToWatchList/{FaceBookID}")]
        public async Task<JsonResult> Update(string FaceBookID, string FaceBookName, string FaceBookTypeId)
        {
            var watchlist = await _watchListService.UpdateToWatchList(FaceBookID, FaceBookName, FaceBookTypeId);
            return Json(watchlist);
        }

        [HttpPost("Follow/{faceBookId}")]
        public async Task<JsonResult> Follow(string faceBookId)
        {
            var query = await _watchListService.Follow(faceBookId);
            return Json(query);
        }

        [HttpPost("Unfollow/{faceBookId}")]
        public async Task<JsonResult> Unfollow(string faceBookId)
        {
            var query = await _watchListService.Unfollow(faceBookId);
            return Json(query);
        }

        [HttpGet("GetWatchListItemByID/{faceBookId}")]
        public async Task<JsonResult> GetById(string faceBookId)
        {
            var watchList = await _watchListService.GetWatchListItemById(faceBookId);

            if (watchList == null) return Json(null);

            return Json(watchList);
        }

        [HttpGet("GetAllWatchList")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetWatchListInPython request)
        {
            var watchList = await _watchListService.GetAllWatchList(request);

            return Ok(watchList);
        }

        [HttpGet("GetAllWatchListPagedResult")]
        public async Task<IActionResult> GetAllWatchListPagedResult([FromQuery] GetManageWatchListRequest request)
        {
            var watchList = await _watchListService.GetAllWatchListPagedResult(request);
            return Json(watchList);
        }
    }
}
