using cFB.Applications.Catalog.Historys;
using cFB.Data.EFs;
using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.Utilities.AutoStrings;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cFB.ViewModels.Catalog.Reports;
using cFB.ViewModels.Common;
using static cFB.Utilities.Constants.ShareContants;
using cFB.Applications.Catalog.Reports;

namespace cFB.Applications.Catalog.Posts
{
    public class PostSevice : IPostSevice
    {
        private readonly cFBDbContext _context;

        private readonly IHistorySevice _historySevice;
        private readonly IReportSevice _reportsSevice;

        public PostSevice(cFBDbContext context, IHistorySevice historySevice, IReportSevice reportsSevice)
        {
            _context = context;
            _historySevice = historySevice;
            _reportsSevice = reportsSevice;
        }
        //Check
        public async Task<bool> CheckExistPostStatus(string postUrl, string sentimentLabel_Id, string administrativeDivisionId) 
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostUrl == postUrl && x.AdministrativeDivisionId == administrativeDivisionId);
                if (post != null)
                {
                    if (sentimentLabel_Id == ShareContants.SentimentLabelName.Negative && post.SentimentLabelId != ShareContants.SentimentLabelName.Negative) return false;

                    else return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> CheckWatchList(string faceBookID)
        {
            try
            {
                var watchlist = await _context.WatchLists.FirstOrDefaultAsync(x => x.FaceBookId == faceBookID);

                if (watchlist == null) return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string> CheckTimeCrawl(string faceBookId, string administrativeDivisionId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return null;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    var post = await _context.Posts.Where(x => x.FaceBookId == faceBookId).OrderByDescending(x => x.UploadTime).Select(x => x.UploadTime).FirstOrDefaultAsync();
                    return post.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    var post = await _context.Posts.Where(x => x.FaceBookId == faceBookId && x.AdministrativeDivisionId == administrativeDivisionId).OrderByDescending(x => x.UploadTime).Select(x => x.UploadTime).FirstOrDefaultAsync();
                    return post.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Count
        public async Task<int> GetCountById(string administrativeDivisionId, string facebookID)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID).CountAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID && x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> GetCountByKeyword(string administrativeDivisionId, string keyword, string sentimentLabelId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    var watchlist = await _context.WatchLists.Where(x => x.FaceBookUrl.Contains(keyword)).Select(x => x.FaceBookId).FirstOrDefaultAsync();
                    var userurl = await _context.Posts.Where(x => x.UserUrl.Contains(keyword)).Select(x => x.UserUrl).FirstOrDefaultAsync();
                    if (watchlist != null)
                    {
                        return await _context.Posts.Where(x => x.PostId.Contains(watchlist) && x.SentimentLabelId == sentimentLabelId).CountAsync();
                    }
                    else if (userurl != null)
                    {
                        return await _context.Posts.Where(x => x.UserUrl.Contains(userurl) && x.SentimentLabelId == sentimentLabelId).CountAsync();
                    }
                    else
                    {
                        return await _context.Posts.Where(x => x.PostContent.Contains(keyword) && x.SentimentLabelId == sentimentLabelId).CountAsync();
                    }
                }
                else
                {
                    var watchlist = await _context.WatchLists.Where(x => x.FaceBookUrl.Contains(keyword) && x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.FaceBookId).FirstOrDefaultAsync();
                    var userurl = await _context.Posts.Where(x => x.UserUrl.Contains(keyword) && x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.UserUrl).FirstOrDefaultAsync();
                    if (watchlist != null)
                    {
                        return await _context.Posts.Where(x => x.PostId.Contains(watchlist) && x.SentimentLabelId == sentimentLabelId && x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                    }
                    else if (userurl != null)
                    {
                        return await _context.Posts.Where(x => x.UserUrl.Contains(userurl) && x.SentimentLabelId == sentimentLabelId && x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                    }
                    else
                    {
                        return await _context.Posts.Where(x => x.PostContent.Contains(keyword) && x.SentimentLabelId == sentimentLabelId && x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> GetCountPostSentimentLabel(string administrativeDivisionId, string sentimentLabelId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.Where(x => x.SentimentLabelId == sentimentLabelId).CountAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.SentimentLabelId == sentimentLabelId && x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> GetCountPost(string administrativeDivisionId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.CountAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> GetCountReport(Reported? report, string administrativeDivisionId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null || report == null)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.Where(x => x.Report == report && x.SentimentLabelId == ShareContants.SentimentLabelName.Negative).CountAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.Report == report && x.AdministrativeDivisionId == administrativeDivisionId && x.SentimentLabelId == ShareContants.SentimentLabelName.Negative).CountAsync();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> GetCountSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID && x.SentimentLabelId == newsLabelId).CountAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID && x.SentimentLabelId == newsLabelId && x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> GetCountNewsLabelAndSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId, string sentimentLabelId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID && x.SentimentLabelId == sentimentLabelId && x.NewsLabelId == newsLabelId).CountAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID && x.SentimentLabelId == sentimentLabelId && x.NewsLabelId == newsLabelId && x.AdministrativeDivisionId == administrativeDivisionId).CountAsync();
                }
            }
            catch
            {
                return 0;
            }
        }
        //Create
        public async Task<bool> CreateToPostStatus(GetPostCreateRequest request)
        {
            try
            {
                var data = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == request.PostId);
                var post = new Post()
                {
                    PostId = data != null ? AutoGenerate.PostRandomID(request.AdministrativeDivisionId) : request.PostId,
                    UserUrl = request.UserUrl,
                    PostUrl = request.PostUrl,
                    PostContent = (request.PostContent == null || request.PostContent == "None") ? "" : request.PostContent,
                    Image = (request.Image == null || request.Image == "None") ? "" : request.Image,
                    UploadTime = (DateTime)(request.UploadTime == null ? DateTime.Now : request.UploadTime),
                    CrawledTime = (DateTime)(request.CrawledTime == null ? DateTime.Now : request.CrawledTime),
                    TotalLikes = (int)(request.TotalLikes == null ? 0 : request.TotalLikes),
                    TotalComment = (int)(request.TotalComment == null ? 0 : request.TotalComment),
                    TotalShare = (int)(request.TotalShare == null ? 0 : request.TotalShare),
                    NewsLabelId = request.NewsLabelID,
                    FaceBookId = request.FacebookID,
                    SentimentLabelId = request.SentimentLabelID,
                    AdministrativeDivisionId = request.AdministrativeDivisionId != null ? request.AdministrativeDivisionId : "",
                    Report = Data.Enums.Reported.UnReported,
                    FilePDF = request.FilePDF == null || request.FilePDF == "string" ? "" : request.FilePDF
                };

                _context.Posts.Add(post);

                await _context.SaveChangesAsync();

                await _historySevice.CreateInHistory(request.AdministrativeDivisionId, Event.Create, $"Đã thêm {request.PostUrl}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> AddNewOrUpdateListPost(GetPostCreateRequest request)
        {
            //step 1: check ID exits in Post -> then update / add new
            //step 2: if add new -> check FacebookID exits in WatchList -> if not then add facebookID before add post
            try
            {
                if (await CheckWatchList(request.FacebookID))
                {
                    if (await CheckExistPostStatus(request.PostUrl, request.SentimentLabelID, request.AdministrativeDivisionId))
                    {
                        var data = await _context.Posts.FirstOrDefaultAsync(x => x.PostUrl == request.PostUrl);

                        var update = await UpdateToPost(data.PostId, request.PostContent, (DateTime)request.CrawledTime, (int)request.TotalLikes, (int)request.TotalComment, (int)request.TotalShare, request.NewsLabelID, request.SentimentLabelID);

                        if (update == false) return false;

                        return true;
                    }
                    else
                    {
                        var data = await CreateToPostStatus(request);

                        if (data == true) return true;

                        return false;
                    }
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> AddNewListPost(List<GetPostCreateRequest> requests)
        {
            try
            {
                foreach (var item in requests)
                {
                    if (await CheckWatchList(item.FacebookID))
                    {
                        if (await CheckExistPostStatus(item.PostUrl, item.SentimentLabelID, item.AdministrativeDivisionId))
                        {
                            var data = await _context.Posts.FirstOrDefaultAsync(x => x.PostUrl == item.PostUrl);
                            var update = await UpdateToPost(data.PostId, item.PostContent, (DateTime)item.CrawledTime, (int)item.TotalLikes, (int)item.TotalComment, (int)item.TotalShare, item.NewsLabelID, item.SentimentLabelID);
                        }
                        else
                        {
                            var data = await CreateToPostStatus(item);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Update
        public async Task<bool> UpdateToPost(string PostId, string PostContent, DateTime? CrawledTime, int? TotalLikes, int? TotalComment, int? TotalShare, string NewsLabel_Id, string SentimentLabel_Id)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == PostId);

                if (post == null) return false;

                post.PostContent = PostContent;
                post.CrawledTime = (DateTime)(CrawledTime == null ? DateTime.Now : CrawledTime);
                post.TotalLikes = (int)(TotalLikes == null ? 0 : TotalLikes);
                post.TotalComment = (int)(TotalComment == null ? 0 : TotalComment);
                post.TotalShare = (int)(TotalShare == null ? 0 : TotalShare);
                post.NewsLabelId = !string.IsNullOrEmpty(NewsLabel_Id) ? NewsLabel_Id : post.NewsLabelId;
                post.SentimentLabelId = !string.IsNullOrEmpty(SentimentLabel_Id) ? SentimentLabel_Id : post.SentimentLabelId;

                await _context.SaveChangesAsync();

                await _historySevice.CreateInHistory(post.AdministrativeDivisionId, Event.Update, $"Cập nhật bài đăng {post.PostId}");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UploadfilePDF(string PostId, string filePDF)
        {
            try
            {
                if (filePDF == null || filePDF == "" || filePDF == "string") return false;

                var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == PostId);

                if (post == null) return false;

                post.FilePDF = filePDF;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UnReported(string postId) // chức năng huỷ duyệt bài 
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postId && x.Report == Data.Enums.Reported.Reported);
                var report = await _context.Reports.FirstOrDefaultAsync(x => x.PostId == postId);

                if (post == null || report == null) return false;

                post.Report = Data.Enums.Reported.UnReported;

                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
                await _historySevice.CreateInHistory(post.AdministrativeDivisionId, Event.Report, $"Đã hủy báo cáo bài viết {post.FaceBookId}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Reported(string postId) // chức năng check duyệt bài
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

                if (post == null) return false;

                post.Report = Data.Enums.Reported.Reported;

                await _context.SaveChangesAsync();

                var report = new GetManageReportRequest()
                {
                    AdministrativeDivisionID = post.AdministrativeDivisionId,
                    DateReport = DateTime.Now,
                    FileReport = post.FilePDF,
                    ReportID = post.PostId,
                    PostID = post.PostId
                };

                await _reportsSevice.CreateReport(report);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> ChangeSentimentLabelBy(string postId, string sentimentLabel_Id)
        {
            try
            {
                var data = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

                if (data == null) return false;

                data.SentimentLabelId = !string.IsNullOrEmpty(sentimentLabel_Id) ? sentimentLabel_Id : data.SentimentLabelId;

                await _context.SaveChangesAsync();

                await _historySevice.CreateInHistory(data.AdministrativeDivisionId, Event.Update, $"Cập nhật loại đánh gia với  {data.PostId} từ {data.SentimentLabelId} thành {sentimentLabel_Id}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        //Delete
        public async Task<bool> RemovePostStatusList(List<string> listPostId)
        {
            try
            {
                var rs = _context.Posts.Where(x => listPostId.Contains(x.PostId));

                _context.Posts.RemoveRange(rs);

                var data = rs.Select(x => x.AdministrativeDivisionId).FirstOrDefault();


                await _historySevice.CreateInHistory(data, Event.Delete, $"Đã xóa {rs.Count()} bài viết");


                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> RemovePostStatustId(string postId)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

                if (post == null) return false;

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                await _historySevice.CreateInHistory(post.AdministrativeDivisionId, Event.Delete, $"Đã xóa {post.PostUrl}");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //List
        public async Task<GetPostViewModel> GetPostStatusById(string postId)
        {
            try
            {
                var query = await (from p in _context.Posts
                                   join wl in _context.WatchLists
                                   on p.FaceBookId equals wl.FaceBookId
                                   join nl in _context.NewsLabels
                                   on p.NewsLabelId equals nl.NewsLabelId
                                   join sl in _context.SentimentLabels
                                   on p.SentimentLabelId equals sl.SentimentLabelId
                                   join ad in _context.AdministrativeDivisions
                                   on p.AdministrativeDivisionId equals ad.AdministrativeDivisionId
                                   where p.PostId == postId
                                   select new
                                   {
                                       p.PostId,
                                       p.PostUrl,
                                       p.UserUrl,
                                       p.PostContent,
                                       p.Image,
                                       p.UploadTime,
                                       p.CrawledTime,
                                       p.TotalLikes,
                                       p.TotalComment,
                                       p.TotalShare,
                                       p.FaceBookId,
                                       wl.FaceBookName,
                                       wl.FaceBookUrl,
                                       p.NewsLabelId,
                                       nl.NewsLabelName,
                                       p.SentimentLabelId,
                                       sl.SentimentLabelName,
                                       ad.AdministrativeDivisionName,
                                       ad.AdministrativeDivisionId,
                                       p.FilePDF,
                                       p.Report
                                   }).FirstOrDefaultAsync();

                if (query == null) return null;


                var data = new GetPostViewModel()
                {
                    PostID = query.PostId,
                    PostUrl = query.PostUrl,
                    UserUrl = query.UserUrl,
                    PostContent = query.PostContent,
                    Image = query.Image,
                    UploadTime = query.UploadTime,
                    CrawledTime = query.CrawledTime,
                    TotalLikes = query.TotalLikes,
                    TotalComment = query.TotalComment,
                    TotalShare = query.TotalShare,
                    Facebook_ID = query.FaceBookId,
                    FacebookName = query.FaceBookName,
                    FacebookUrl = query.FaceBookUrl,
                    NewsLabelID = query.NewsLabelId,
                    NewsLabelName = query.NewsLabelName,
                    SentimentLabelID = query.SentimentLabelId,
                    SentimentLabelName = query.SentimentLabelName,
                    AdministrativeDivisionID = query.AdministrativeDivisionId,
                    AdministrativeDivisionName = query.AdministrativeDivisionName,
                    FilePDF = query.FilePDF,
                    Report = query.Report
                };

                return data;
            }
            catch (Exception)
            {
                return null;
            }
           

        }
        public async Task<PagedResult<GetPostViewModel>> GetAllPostStatus(GetManagePostRequest request)
        {
            try
            {
                var query = from p in _context.Posts
                            join wl in _context.WatchLists
                            on p.FaceBookId equals wl.FaceBookId
                            join nl in _context.NewsLabels
                            on p.NewsLabelId equals nl.NewsLabelId
                            join sl in _context.SentimentLabels
                            on p.SentimentLabelId equals sl.SentimentLabelId
                            join ad in _context.AdministrativeDivisions
                            on p.AdministrativeDivisionId equals ad.AdministrativeDivisionId
                            orderby p.CrawledTime descending
                            select new
                            {
                                p.PostId,
                                p.PostUrl,
                                p.UserUrl,
                                p.PostContent,
                                p.Image,
                                p.UploadTime,
                                p.CrawledTime,
                                p.TotalLikes,
                                p.TotalComment,
                                p.TotalShare,
                                p.FaceBookId,
                                wl.FaceBookName,
                                wl.FaceBookUrl,
                                p.NewsLabelId,
                                nl.NewsLabelName,
                                p.SentimentLabelId,
                                sl.SentimentLabelName,
                                ad.AdministrativeDivisionName,
                                ad.AdministrativeDivisionId,
                                p.FilePDF,
                                p.Report
                            };

                if (!string.IsNullOrEmpty(request.NewsLabelID))
                    query = query.Where(x => x.NewsLabelId == request.NewsLabelID);

                if (!string.IsNullOrEmpty(request.AdministrativeDivisionID))
                {
                    var rs = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID).Select(x => x.ManagerId).FirstOrDefaultAsync();
                    if (rs == null && request.AdministrativeDivisionID != ShareContants.UserAdmin)
                    {
                        return null;
                    }
                    if (rs != ShareContants.UserAdmin)
                    {
                        query = query.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID);
                    }
                }

                if (!string.IsNullOrEmpty(request.WatchListID))
                    query = query.Where(x => x.FaceBookId == request.WatchListID);

                if (!string.IsNullOrEmpty(request.SentimentLabelID))
                    query = query.Where(x => x.SentimentLabelId == request.SentimentLabelID);

                if (request.TimeCrawl != null)
                    query = query.Where(x => x.CrawledTime.Date == request.TimeCrawl.Value.Date);

                if (request.Report != null)
                    query = query.Where(x => x.Report == request.Report);

                if ((request.StartDate != null || request.EndDate != null))
                    query = query.Where(x => x.UploadTime.Date >= request.StartDate.Value.Date && x.UploadTime.Date <= request.EndDate.Value.Date);

                if (!string.IsNullOrEmpty(request.Search))
                    query = query.Where(x => x.PostId.Contains(request.Search) || x.PostUrl.Contains(request.Search) || x.PostContent.Contains(request.Search));

                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize).Select(x => new GetPostViewModel()
                    {
                        PostID = x.PostId,
                        PostUrl = x.PostUrl,
                        UserUrl = x.UserUrl,
                        PostContent = x.PostContent,
                        Image = x.Image,
                        UploadTime = x.UploadTime,
                        CrawledTime = x.CrawledTime,
                        TotalLikes = x.TotalLikes,
                        TotalComment = x.TotalComment,
                        TotalShare = x.TotalShare,
                        Facebook_ID = x.FaceBookId,
                        FacebookName = x.FaceBookName,
                        FacebookUrl = x.FaceBookUrl,
                        NewsLabelID = x.NewsLabelId,
                        NewsLabelName = x.NewsLabelName,
                        SentimentLabelID = x.SentimentLabelId,
                        SentimentLabelName = x.SentimentLabelName,
                        AdministrativeDivisionID = x.AdministrativeDivisionId,
                        AdministrativeDivisionName = x.AdministrativeDivisionName,
                        FilePDF = x.FilePDF,
                        Report = x.Report
                    }).ToListAsync();

                var pagedResult = new PagedResult<GetPostViewModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };

                return pagedResult;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Post>> GetListWatchListReportByFacebookID(string administrativeDivisionId, Reported report)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return null;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.Where(x => x.Report == Data.Enums.Reported.Reported && x.SentimentLabelId == ShareContants.SentimentLabelName.Negative).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.Report == Data.Enums.Reported.Reported && x.AdministrativeDivisionId == administrativeDivisionId && x.SentimentLabelId == ShareContants.SentimentLabelName.Negative).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Post>> GetListPostByKeyword(string administrativeDivisionId, string keyword)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return null;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    var watchlist = await _context.WatchLists.Where(x => x.FaceBookUrl.Contains(keyword)).Select(x => x.FaceBookId).FirstOrDefaultAsync();
                    var userurl = await _context.Posts.Where(x => x.UserUrl.Contains(keyword)).Select(x => x.UserUrl).FirstOrDefaultAsync();
                    if (watchlist != null)
                    {
                        return await _context.Posts.Where(x => x.PostId.Contains(watchlist) && x.SentimentLabelId == SentimentLabelName.Negative).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                    }
                    else if (userurl != null)
                    {
                        return await _context.Posts.Where(x => x.UserUrl.Contains(userurl) && x.SentimentLabelId == SentimentLabelName.Negative).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                    }
                    else
                    {
                        return await _context.Posts.Where(x => x.PostContent.Contains(keyword) && x.SentimentLabelId == SentimentLabelName.Negative).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                    }
                }
                else
                {
                    var watchlist = await _context.WatchLists.Where(x => x.FaceBookUrl.Contains(keyword) && x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.FaceBookId).FirstOrDefaultAsync();
                    var userurl = await _context.Posts.Where(x => x.UserUrl.Contains(keyword) && x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.UserUrl).FirstOrDefaultAsync();
                    if (watchlist != null)
                    {
                        return await _context.Posts.Where(x => x.PostId.Contains(watchlist) && x.SentimentLabelId == SentimentLabelName.Negative && x.AdministrativeDivisionId == administrativeDivisionId).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                    }
                    else if (userurl != null)
                    {
                        return await _context.Posts.Where(x => x.UserUrl.Contains(userurl) && x.SentimentLabelId == SentimentLabelName.Negative && x.AdministrativeDivisionId == administrativeDivisionId).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                    }
                    else
                    {
                        return await _context.Posts.Where(x => x.PostContent.Contains(keyword) && x.SentimentLabelId == SentimentLabelName.Negative && x.AdministrativeDivisionId == administrativeDivisionId).OrderByDescending(x => x.CrawledTime).Take(10).ToListAsync();
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Post>> GetListWatchListByFacebookID(string administrativeDivisionId, string facebookID)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return null;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID).OrderByDescending(po => po.TotalLikes + po.TotalComment + po.TotalShare).Take(10).ToListAsync();
                }
                else
                {
                    return await _context.Posts.Where(x => x.FaceBookId == facebookID && x.AdministrativeDivisionId == administrativeDivisionId).OrderByDescending(po => po.TotalLikes + po.TotalComment + po.TotalShare).Take(10).ToListAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<GetPostFilePDFInPython>> GetAllPostUserPrintPDF(string administrativeDivisionId)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivisionId).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivisionId != ShareContants.UserAdmin)
                {
                    return null;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivisionId == ShareContants.UserAdmin)
                {
                    var query = await (from p in _context.Posts
                                       join sl in _context.SentimentLabels
                                       on p.SentimentLabelId equals sl.SentimentLabelId
                                       where p.SentimentLabelId == SentimentLabelName.Negative && (p.FilePDF == "" || p.FilePDF == null || p.FilePDF == "string")
                                       select new GetPostFilePDFInPython()
                                       {
                                           PostID = p.PostId,
                                           PostURL = p.PostUrl
                                       }).ToListAsync();

                    return query;
                } 
                else
                {
                    var query = await (from p in _context.Posts
                                       join sl in _context.SentimentLabels
                                       on p.SentimentLabelId equals sl.SentimentLabelId
                                       where p.SentimentLabelId == SentimentLabelName.Negative && (p.FilePDF == "" || p.FilePDF == null || p.FilePDF == "string")
                                       && p.AdministrativeDivisionId == administrativeDivisionId
                                       select new GetPostFilePDFInPython()
                                       {
                                           PostID = p.PostId,
                                           PostURL = p.PostUrl
                                       }).ToListAsync();

                    return query;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
