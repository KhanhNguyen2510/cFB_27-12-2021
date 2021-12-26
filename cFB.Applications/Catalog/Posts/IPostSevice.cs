using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.ViewModels.Catalog.Posts;
using cFB.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cFB.Applications.Catalog.Posts
{
    public interface IPostSevice
    {
        //Check
        Task<string> CheckTimeCrawl(string faceBookId, string administrativeDivisionId);
        //Count
        Task<int> GetCountById(string administrativeDivisionId, string facebookID);
        Task<int> GetCountPost(string administrativeDivisionId);
        Task<int> GetCountPostSentimentLabel(string administrativeDivisionId, string sentimentLabelId);
        Task<int> GetCountReport(Reported? report, string administrativeDivisionId);
        Task<int> GetCountSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId);
        Task<int> GetCountNewsLabelAndSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId, string sentimentLabelId);
        Task<int> GetCountByKeyword(string administrativeDivisionId, string keyword, string sentimentLabelId);
        //Create
        Task<bool> AddNewOrUpdateListPost(GetPostCreateRequest request);
        Task<bool> AddNewListPost(List<GetPostCreateRequest> requests);
        //Update
        Task<bool> Reported(string postId);
        Task<bool> ChangeSentimentLabelBy(string postId, string sentimentLabel_Id);
        Task<bool> UploadfilePDF(string PostId, string filePDF);
        Task<bool> UnReported(string postId);
        //Delete
        Task<bool> RemovePostStatustId(string postId);
        Task<bool> RemovePostStatusList(List<string> listPostId);
        //GetList
        Task<List<GetPostFilePDFInPython>> GetAllPostUserPrintPDF(string administrativeDivisionId);
        Task<List<Post>> GetListWatchListReportByFacebookID(string administrativeDivisionId, Reported report);
        Task<List<Post>> GetListWatchListByFacebookID(string administrativeDivisionId, string facebookID);
        Task<PagedResult<GetPostViewModel>> GetAllPostStatus(GetManagePostRequest request);
        Task<GetPostViewModel> GetPostStatusById(string postId);
        Task<List<Post>> GetListPostByKeyword(string administrativeDivisionId, string keyword);
    }
}
