using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.ViewModels.Catalog.Posts;
using cFB.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Posts
{
    public interface IPostApiClient
    {
        Task<int> GetCountPost(string administrativeDivisionId);
        Task<int> GetCountByKeyword(string administrativeDivisionId, string keyword, string sentimentLabelId);
        Task<int> GetCountById(string administrativeDivisionId, string facebookID);
        Task<int> GetCountPostSentimentLabel(string administrativeDivisionId, string sentimentLabelId);
        Task<int> GetCountReport(Reported report, string administrativeDivisionId);
        Task<int> GetCountSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId);
        Task<int> GetCountNewsLabelAndSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId, string sentimentLabelId);
        Task<List<Post>> GetListWatchListReportByFacebookID(string administrativeDivisionId, Reported report);
        Task<List<Post>> GetListWatchListByFacebookID(string administrativeDivisionId, string facebookID);
        Task<PagedResult<GetPostViewModel>> GetAllPostStatus(GetManagePostRequest request);
        Task<List<Post>> GetListPostByKeyword(string administrativeDivisionId, string keyword);
    }
}
