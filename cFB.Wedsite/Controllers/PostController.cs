using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.IntergrationAPI.Posts;
using cFB.IntergrationAPI.Systems.Users;
using cFB.IntergrationAPI.WatchLists;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Posts;
using cFB.ViewModels.Catalog.WatchLists;
using cFB.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Wedsite.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostApiClient _postApiClient;
        private readonly IConfiguration _configuration;
        private readonly IWatchListApiClient _watchListApiClient;
        private readonly IUserApiSevice _userApiSevice;

        public PostController(IPostApiClient postApiClient, IConfiguration configuration, IWatchListApiClient watchListApiClient, IUserApiSevice userApiSevice)
        {
            _configuration = configuration;
            _postApiClient = postApiClient;
            _watchListApiClient = watchListApiClient;
            _userApiSevice = userApiSevice;
        }

        public string LoadRoleUser()
        {
            TempData.Keep("name");
            return ShareContants.RoleOfUser = TempData["name"].ToString(); ;
        }

        public async Task<IActionResult> Index(string userId, string search, string newsLabelID, string sentimentLabelID, string watchlistID, Reported? report, DateTime? timeCrawl, DateTime? startDate, DateTime? endDate, int pageIndex = 1, int pageSize = 100)
        {
           /* Response.Headers.Add("Refresh", "15");*/ // reset sau 15 phút

            ShareContants.NumberPageVisits++;
            ViewBag.RoleOfUser = LoadRoleUser();

            if (pageSize == ShareContants.PageSizeErro) pageSize = 1;

            if (endDate != null && startDate == null) startDate = endDate;

            if (ShareContants.NumberPageVisits == 1) timeCrawl = DateTime.Now.AddHours(-8);

            var request = new GetManagePostRequest()
            {
                Search = search,
                AdministrativeDivisionID = userId == null ? LoadRoleUser() : userId,
                NewsLabelID = newsLabelID,
                SentimentLabelID = sentimentLabelID,
                StartDate = startDate?.Date,
                EndDate = endDate?.Date,
                TimeCrawl = timeCrawl?.Date,
                Report = report,
                WatchListID = watchlistID,
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var post = await _postApiClient.GetAllPostStatus(request);

            ViewBag.Search = search;
            ViewBag.StartDate = startDate?.Date;
            ViewBag.EndDate = endDate?.Date;
            ViewBag.TimeCrawl = timeCrawl?.Date;
            ViewBag.PageSize = pageSize;

            var reports = new List<ReportsEnvent>();
            reports.Add(new ReportsEnvent() { Id = Reported.Reported, Name = "Đã lập" });
            reports.Add(new ReportsEnvent() { Id = Reported.UnReported, Name = "Chưa lập" });
            ViewBag.Report = reports.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = report == x.Id
            });

            var newsLabel = new List<NewsLabel>();
            newsLabel.Add(new NewsLabel() { NewsLabelId = "ANQG", NewsLabelName = "An ninh quốc gia" });
            newsLabel.Add(new NewsLabel() { NewsLabelId = "ANTT", NewsLabelName = "An ninh trật tự" });
            ViewBag.NewsLabelID = newsLabel.Select(x => new SelectListItem()
            {
                Text = x.NewsLabelName,
                Value = x.NewsLabelId,
                Selected = newsLabelID == x.NewsLabelId
            });

            var sentimentLabel = new List<SentimentLabel>();
            sentimentLabel.Add(new SentimentLabel() { SentimentLabelId = ShareContants.SentimentLabelName.Positive, SentimentLabelName = "Tích cực" });
            sentimentLabel.Add(new SentimentLabel() { SentimentLabelId = ShareContants.SentimentLabelName.Normal, SentimentLabelName = "Bình thường" });
            sentimentLabel.Add(new SentimentLabel() { SentimentLabelId = ShareContants.SentimentLabelName.Negative, SentimentLabelName = "Tiêu cực" });
            ViewBag.SentimentLabelID = sentimentLabel.Select(x => new SelectListItem()
            {
                Text = x.SentimentLabelName,
                Value = x.SentimentLabelId,
                Selected = sentimentLabelID == x.SentimentLabelId
            });

            var requestWatchList = new GetManageListWatchListPagingRequest()
            {
                AdministrativeDivisionID = LoadRoleUser()
            };
            var watchList = await _watchListApiClient.GetAllWatchList(requestWatchList);

            ViewBag.WatchList = watchList.Select(x => new SelectListItem()
            {
                Text = $"({x.AdministrativeDivisionId}) -- " + x.FaceBookName,
                Value = x.FaceBookId,
                Selected = watchlistID == x.FaceBookId
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

            return View(post);
        }

        public async Task<IActionResult> Export(string NameFileExcel, string NewsLabelID, string SentimentLabelID, DateTime? StartDate, DateTime? EndDate, DateTime? TimeCrawl, Reported? report)
        {
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new GetManagePostRequest()
            {
                AdministrativeDivisionID = LoadRoleUser(),
                NewsLabelID = NewsLabelID,
                SentimentLabelID = SentimentLabelID,
                StartDate = StartDate?.Date,
                EndDate = EndDate?.Date,
                TimeCrawl = TimeCrawl?.Date,
                Report = report,
                PageSize = 1048576,
                PageIndex = 1
            };
            var post = await _postApiClient.GetAllPostStatus(request);

            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add($"File thông tin ngày({DateTime.Now.ToString("dd/MM/yyyy")})");
                sheet.Cells.Style.Font.Name = "Times New Roman";
                using (ExcelRange Rng = sheet.Cells[1, 1, 1, 12])
                {
                    Rng.Value = $"Thông tin bài viết (ngày {DateTime.Now.ToString("dd/MM/yyyy")}) ";
                    Rng.Merge = true;
                    Rng.Style.Font.Size = 23;
                    Rng.Style.Font.Bold = true;
                    Rng.Style.Font.Color.SetColor(Color.Blue);
                    Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Rng.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                }
                sheet.Row(2).Height = 25;
                for (int i = 1; i <= 12; i++)
                {
                    sheet.Column(i).Width = 23;
                    
                    sheet.Cells[2, i].Style.Font.Size = 13;
                    sheet.Cells[2,i].Style.Font.Bold = true;
                    sheet.Cells[2,i].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                }
                sheet.Cells[2, 1].Value = "Mã bài viết";
                sheet.Cells[2, 2].Value = "Mã đối tượng";
                sheet.Cells[2, 3].Value = "Địa phương";
                sheet.Cells[2, 4].Value = "Nội dung";
                sheet.Cells[2, 5].Value = "Thời gian đăng";
                sheet.Cells[2, 6].Value = "Số lượng bình luận";
                sheet.Cells[2, 7].Value = "Số lượng thích";
                sheet.Cells[2, 8].Value = "Số lượng chia sẽ";
                sheet.Cells[2, 9].Value = "Thuộc nội dung";
                sheet.Cells[2, 10].Value = "Đánh giá"; 
                sheet.Cells[2, 11].Value = "PDF";
                sheet.Cells[2, 12].Value = "URL";

                int rowIndex = 3;
                foreach (var l in post.Items)
                {
                    sheet.Cells[rowIndex, 1].Value = l.PostID;
                    sheet.Cells[rowIndex, 2].Value = l.Facebook_ID;
                    sheet.Cells[rowIndex, 3].Value = l.AdministrativeDivisionName;
                    sheet.Cells[rowIndex, 4].Value = l.PostContent;
                    sheet.Cells[rowIndex, 5].Value = l.UploadTime.ToString("dd/MM/yyyy HH:mm:ss");
                    sheet.Cells[rowIndex, 6].Value = l.TotalComment;
                    sheet.Cells[rowIndex, 7].Value = l.TotalLikes;
                    sheet.Cells[rowIndex, 8].Value = l.TotalShare;
                    sheet.Cells[rowIndex, 9].Value = l.NewsLabelName;
                    sheet.Cells[rowIndex, 10].Value = l.SentimentLabelName;
                    sheet.Cells[rowIndex, 11].Value = l.FilePDF;
                    sheet.Cells[rowIndex, 12].Value = l.PostUrl;
                    for (int i = 1; i <= 12; i++)
                    {
                        if (i == 4 || i == 11 || i == 12)
                        {
                            continue;
                        }
                        else
                        {
                            sheet.Cells[rowIndex, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        }
                    }
                        rowIndex++;
                }
                package.Save();
            }

            stream.Position = 0;

            var filename = $"{(!string.IsNullOrEmpty(NameFileExcel) ? NameFileExcel : $"Thông tin bài đăng ngày({DateTime.Now.ToString("dd/MM/yyyy")})")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }


    }
}
