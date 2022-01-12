using cFB.IntergrationAPI.Posts;
using cFB.IntergrationAPI.Systems.Users;
using cFB.IntergrationAPI.WatchLists;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.WatchLists;
using cFB.ViewModels.System;
using cFB.Wedsite.Messages;
using cFB.Wedsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Wedsite.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly IWatchListApiClient _watchListApiClient;
        private readonly IPostApiClient _postApiClient;
        private readonly IUserApiSevice _userApiSevice;

        public AnalysisController(IWatchListApiClient watchListApiClient, IPostApiClient postApiClient, IUserApiSevice userApiSevice)
        {
            _watchListApiClient = watchListApiClient;
            _postApiClient = postApiClient;
            _userApiSevice = userApiSevice;
        }

        public string LoadRoleUser()
        {
            TempData.Keep("name");
            return ShareContants.RoleOfUser = TempData["name"].ToString(); ;
        }

        public async Task<IActionResult> AnalysisAll(string facebookID, string userId, string keyword)
        {
           /* Response.Headers.Add("Refresh", "60000");*/ // reset sau 15 phút
            ShareContants.NumberPageVisits = 0;

            var sentimentLabel = new[] { ShareContants.SentimentLabelName.Positive, ShareContants.SentimentLabelName.Normal, ShareContants.SentimentLabelName.Negative };

            var listCount = new List<int>();

            var countPost = await _postApiClient.GetCountPost(LoadRoleUser());
            var countWatchList = await _watchListApiClient.GetCountWatchList(LoadRoleUser());

            var getCountReported = await _postApiClient.GetCountReport(Data.Enums.Reported.Reported, LoadRoleUser());
            var getCountUnReported = await _postApiClient.GetCountReport(Data.Enums.Reported.UnReported, LoadRoleUser());

            foreach (var i in sentimentLabel)
            {
                listCount.Add(await _postApiClient.GetCountPostSentimentLabel(LoadRoleUser(), i));
            }

            var requestWatchList = new GetManageListWatchListPagingRequest()
            {
                AdministrativeDivisionID = LoadRoleUser()
            };
            var watchList = await _watchListApiClient.GetAllWatchList(requestWatchList);

            ViewBag.WatchList = watchList.Select(x => new SelectListItem()
            {
                Text = $"({x.AdministrativeDivisionId}) -- " + x.FaceBookName,
                Value = x.FaceBookId,
                Selected = facebookID == x.FaceBookId
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
            var analysis = new Analysis()
            {
                NumberOfWatchList = countWatchList,
                NumberOfPost = countPost,
                NumberOfPositivePost = listCount[0],
                NumberOfNegativePost = listCount[2],
                NumberOfneuativePost = listCount[1],
                NumberOfReported = getCountReported,
                NumberOfUnReported = getCountUnReported
            };

            if (keyword != null)
            {
                ViewBag.Keyword = keyword;
                var listkeyword = new List<int>();
                foreach (var item in sentimentLabel)
                {
                    listkeyword.Add(await _postApiClient.GetCountByKeyword(LoadRoleUser(), keyword, item));
                }

                ViewBag.KeyWord = keyword;
                ViewBag.KeyWordPOS = listkeyword[0];
                ViewBag.KeyWordNEU = listkeyword[1];
                ViewBag.KeyWordNEG = listkeyword[2];

                ViewBag.ListPostByKeyWord = await _postApiClient.GetListPostByKeyword(LoadRoleUser(), keyword);
            }

            if (userId != null)
            {
                var getCountReportedByUser = await _postApiClient.GetCountReport(Data.Enums.Reported.Reported, userId);
                var getCountUnReportedByUser = await _postApiClient.GetCountReport(Data.Enums.Reported.UnReported, userId);

                ViewBag.UserId = userId;
                ViewBag.GetCountReportedByUser = getCountReportedByUser;
                ViewBag.GetCountUnReportedByUser = getCountUnReportedByUser;

                ViewBag.TopReport = await _postApiClient.GetListWatchListReportByFacebookID(userId, Data.Enums.Reported.Reported);
                ViewBag.AdministrativeDivisionName = administrativeDivision.Where(x => x.AdministrativeDivisionID == userId).Select(x => x.AdministrativeDivisionName).FirstOrDefault();
            }

            if (facebookID != null)
            {
                var listPostByID = await _postApiClient.GetCountById(LoadRoleUser(), facebookID);
                ViewBag.ListPostByID = listPostByID;

                if (listPostByID > 0)
                {
                    var listCountFacebook = new List<int>();
                    var listCountFacebookANTT = new List<int>();
                    var listCountFacebookANQG = new List<int>();

                    foreach (var i in sentimentLabel)
                    {
                        listCountFacebook.Add(await _postApiClient.GetCountSentimentLabel(facebookID, LoadRoleUser(), i));
                        listCountFacebookANTT.Add(await _postApiClient.GetCountNewsLabelAndSentimentLabel(facebookID, LoadRoleUser(), "ANTT", i));
                        listCountFacebookANQG.Add(await _postApiClient.GetCountNewsLabelAndSentimentLabel(facebookID, LoadRoleUser(), "ANQG", i));
                    }

                    ViewBag.TopInteractive = await _postApiClient.GetListWatchListByFacebookID(LoadRoleUser(), facebookID);
                    ViewBag.FacebookName = watchList.Where(x => x.FaceBookId == facebookID).Select(x => x.FaceBookName).FirstOrDefault();

                    ViewBag.ANQGPOS = listCountFacebookANQG[0];
                    ViewBag.ANQGNEU = listCountFacebookANQG[1];
                    ViewBag.ANQGNEG = listCountFacebookANQG[2];

                    ViewBag.ANTTPOS = listCountFacebookANTT[0];
                    ViewBag.ANTTNEU = listCountFacebookANTT[1];
                    ViewBag.ANTTNEG = listCountFacebookANTT[2];

                    ViewBag.ListPostByID = listPostByID;
                    ViewBag.POS = listCountFacebook[0];
                    ViewBag.NEU = listCountFacebook[1];
                    ViewBag.NEG = listCountFacebook[2];
                }
                else
                {
                    TempData["WarningMessage"] = ShowMessage.Nulldata();

                }
            }
            return View(analysis);
        }
    }
}
