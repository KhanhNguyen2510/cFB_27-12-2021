using cFB.Applications.Catalog.Posts;
using cFB.Data.Enums;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Posts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace cFB.BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly IPostSevice _postSevice;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostsController(IPostSevice postSevice, IWebHostEnvironment webHostEnvironment)
        {
            _postSevice = postSevice;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetCountById")]
        public async Task<JsonResult> GetCountById(string administrativeDivisionId, string facebookID)
        {
            var postStatus = await _postSevice.GetCountById(administrativeDivisionId, facebookID);
            return Json(postStatus);
        }

        [HttpGet("GetCountByKeyword")]
        public async Task<JsonResult> GetCountByKeyword(string administrativeDivisionId, string keyword, string sentimentLabelId)
        {
            var postStatus = await _postSevice.GetCountByKeyword(administrativeDivisionId, keyword, sentimentLabelId);
            return Json(postStatus);
        }

        [HttpGet("GetCountReport")]
        public async Task<JsonResult> GetCountReport(Reported report, string administrativeDivisionId)
        {
            var postStatus = await _postSevice.GetCountReport(report, administrativeDivisionId);
            return Json(postStatus);
        }

        [HttpGet("GetListPostByKeyword")]
        public async Task<JsonResult> GetListPostByKeyword(string administrativeDivisionId, string keyword)
        {
            var postStatus = await _postSevice.GetListPostByKeyword(administrativeDivisionId, keyword);
            return Json(postStatus);
        }

        [HttpGet("GetCountPostSentimentLabel")]
        public async Task<JsonResult> GetCountPostSentimentLabel(string administrativeDivisionId, string sentimentLabelId)
        {
            var postStatus = await _postSevice.GetCountPostSentimentLabel(administrativeDivisionId, sentimentLabelId);
            return Json(postStatus);
        }

        [HttpGet("GetCountPost")]
        public async Task<JsonResult> GetCountPost(string administrativeDivisionId)
        {
            var postStatus = await _postSevice.GetCountPost(administrativeDivisionId);
            return Json(postStatus);
        }
        [HttpGet("GetCountSentimentLabel")]
        public async Task<JsonResult> GetCountSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId)
        {
            var postStatus = await _postSevice.GetCountSentimentLabel(facebookID, administrativeDivisionId, newsLabelId);
            return Json(postStatus);
        }

        [HttpGet("GetCountNewsLabelAndSentimentLabel")]
        public async Task<JsonResult> GetCountNewsLabelAndSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId, string sentimentLabelId)
        {
            var postStatus = await _postSevice.GetCountNewsLabelAndSentimentLabel(facebookID, administrativeDivisionId, newsLabelId, sentimentLabelId);
            return Json(postStatus);
        }

        [HttpPost("AddNewListPost")]
        public async Task<JsonResult> AddNewListPost([FromBody] List<GetPostCreateRequest> requests)
        {
            var data = await _postSevice.AddNewListPost(requests);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromForm] GetPostCreateRequest request)
        {
            var post = await _postSevice.AddNewOrUpdateListPost(request);
            return Json(post);
        }

        [HttpPost("DeletePostId/{postId}")]
        public async Task<JsonResult> RemovePostStatustId(string postId)
        {
            var postStatus = await _postSevice.RemovePostStatustId(postId);
            return Json(postStatus);
        }

        [HttpPost("DeleteListPost")]
        public async Task<JsonResult> RemovePostStatusList(List<string> listPostId)
        {
            var postStatus = await _postSevice.RemovePostStatusList(listPostId);
            return Json(postStatus);
        }

        [HttpPost("UploadPDF")]
        public async Task<bool> UploadImage([FromForm] UploadFilePDFRequest request)
        {
            try
            {
                if (request.FormFile == null) return false;

                else if (request.FormFile.Length > 0)
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Upload\\" + request.FormFile.FileName))
                    {
                        request.FormFile.CopyTo(fileStream);
                        fileStream.Flush();
                        await _postSevice.UploadfilePDF(request.PostId, SystemContants.MainConectionAPI + "/Upload/" + request.FormFile.FileName);
                        return true;
                    }
                }

                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet("CheckTimePost")]
        public async Task<JsonResult> CheckTimeCrawl(string faceBookId, string administrativeDivisionId)
        {
            var post = await _postSevice.CheckTimeCrawl(faceBookId, administrativeDivisionId);
            return Json(post);
        }

        [HttpPost("Reported/{posId}")]
        public async Task<JsonResult> Reported(string posId)
        {
            var post = await _postSevice.Reported(posId);
            return Json(post);
        }
        [HttpPost("UnReported/{posId}")]
        public async Task<JsonResult> UnReported(string posId)
        {
            var post = await _postSevice.UnReported(posId);
            return Json(post);
        }
        [HttpPost("ChangeSentimentLabelBy/{postId}/{sentimentLabel_Id}")]
        public async Task<JsonResult> ChangeSentimentLabelBy(string postId, string sentimentLabel_Id)
        {
            var post = await _postSevice.ChangeSentimentLabelBy(postId, sentimentLabel_Id);
            return Json(post);
        }

        [HttpGet("GetPostByID/{postId}")]
        public async Task<JsonResult> GetById(string postId)
        {
            var postStatus = await _postSevice.GetPostStatusById(postId);

            return Json(postStatus);
        }

        [HttpGet("GetListWatchListByFacebookID")]
        public async Task<JsonResult> GetListWatchListByFacebookID(string administrativeDivisionId, string facebookID)
        {
            var postStatus = await _postSevice.GetListWatchListByFacebookID(administrativeDivisionId, facebookID);
            return Json(postStatus);
        }

        [HttpGet("GetListWatchListReportByFacebookID")]
        public async Task<JsonResult> GetListWatchListReportByFacebookID(string administrativeDivisionId, Reported report)
        {
            var postStatus = await _postSevice.GetListWatchListReportByFacebookID(administrativeDivisionId, report);
            return Json(postStatus);
        }

        [HttpGet("GetAllPost")]
        public async Task<IActionResult> GetAllPostStatus([FromQuery] GetManagePostRequest request)
        {
            var postStatus = await _postSevice.GetAllPostStatus(request);
            return Json(postStatus);
        }
        [HttpGet("GetAllPostUserPrintPDF")]
        public async Task<IActionResult> GetAllPostUserPrintPDF(string administrativeDivisionId)
        {
            var postStatus = await _postSevice.GetAllPostUserPrintPDF(administrativeDivisionId);
            return Json(postStatus);
        }

    }
}
