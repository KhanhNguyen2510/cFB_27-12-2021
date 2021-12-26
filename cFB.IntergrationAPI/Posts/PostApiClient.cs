using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.ViewModels.Catalog.Posts;
using cFB.ViewModels.Common;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Posts
{
    public class PostApiClient : BaseApiClient, IPostApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PostApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<PagedResult<GetPostViewModel>> GetAllPostStatus(GetManagePostRequest request)
        {
            var data = await GetAsync<PagedResult<GetPostViewModel>>($"/api/Posts/GetAllPost" +
                $"?Search={request.Search}" +
                $"&AdministrativeDivisionID={request.AdministrativeDivisionID}" +
                $"&NewsLabelID={request.NewsLabelID}" +
                $"&SentimentLabelID={request.SentimentLabelID}" +
                $"&StartDate={request.StartDate}" +
                $"&EndDate={request.EndDate}" +
                $"&TimeCrawl={request.TimeCrawl}" +
                $"&Report={request.Report}" +
                $"&WatchListID={request.WatchListID}" +
                $"&PageIndex={request.PageIndex}" +
                $"&PageSize={request.PageSize}");
            return data;
        }

        public async Task<List<Post>> GetListWatchListReportByFacebookID(string administrativeDivisionId, Reported report)
        {
            var data = await GetAsync<List<Post>>($"/api/Posts/GetListWatchListReportByFacebookID?administrativeDivisionId={administrativeDivisionId}&report={report}");
            return data;
        }

        public async Task<List<Post>> GetListPostByKeyword(string administrativeDivisionId, string keyword)
        {
            var data = await GetAsync<List<Post>>($"/api/Posts/GetListPostByKeyword?administrativeDivisionId={administrativeDivisionId}&keyword={keyword}");
            return data;
        }

        public async Task<List<Post>> GetListWatchListByFacebookID(string administrativeDivisionId, string facebookID)
        {
            var data = await GetAsync<List<Post>>($"/api/Posts/GetListWatchListByFacebookID?administrativeDivisionId={administrativeDivisionId}&facebookID={facebookID}");
            return data;
        }

        public async Task<int> GetCountPost(string administrativeDivisionId)
        {
            var data = await GetAsync<int>($"/api/Posts/GetCountPost?administrativeDivisionId={administrativeDivisionId}");
            return data;
        }

        public async Task<int> GetCountByKeyword(string administrativeDivisionId, string keyword, string sentimentLabelId)
        {
            var data = await GetAsync<int>($"/api/Posts/GetCountByKeyword?administrativeDivisionId={administrativeDivisionId}&keyword={keyword}&sentimentLabelId={sentimentLabelId}");
            return data;
        }

        public async Task<int> GetCountReport(Reported report, string administrativeDivisionId)
        {
            var data = await GetAsync<int>($"/api/Posts/GetCountReport?report={report}&administrativeDivisionId={administrativeDivisionId}");
            return data;
        }

        public async Task<int> GetCountSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId)
        {
            var data = await GetAsync<int>($"/api/Posts/GetCountSentimentLabel?facebookID={facebookID}&administrativeDivisionId={administrativeDivisionId}&newsLabelId={newsLabelId}");
            return data;
        }

        public async Task<int> GetCountNewsLabelAndSentimentLabel(string facebookID, string administrativeDivisionId, string newsLabelId, string sentimentLabelId)
        {
            var data = await GetAsync<int>($"/api/Posts/GetCountNewsLabelAndSentimentLabel?facebookID={facebookID}&administrativeDivisionId={administrativeDivisionId}&newsLabelId={newsLabelId}&sentimentLabelId={sentimentLabelId}");
            return data;
        }
        public async Task<int> GetCountPostSentimentLabel(string administrativeDivisionId, string sentimentLabelId)
        {
            var data = await GetAsync<int>($"/api/Posts/GetCountPostSentimentLabel?administrativeDivisionId={administrativeDivisionId}&sentimentLabelId={sentimentLabelId}");
            return data;
        }

        public async Task<int> GetCountById(string administrativeDivisionId, string facebookID)
        {
            var data = await GetAsync<int>($"/api/Posts/GetCountById?administrativeDivisionId={administrativeDivisionId}&facebookID={facebookID}");
            return data;
        }
    }
}
