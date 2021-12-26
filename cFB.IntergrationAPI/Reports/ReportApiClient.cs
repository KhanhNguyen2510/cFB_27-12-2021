using cFB.ViewModels.Catalog.Reports;
using cFB.ViewModels.Common;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Reports
{
    public class ReportApiClient : BaseApiClient, IReportApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ReportApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<PagedResult<GetReportViewModel>> GetAllReport(GetReportRequest request)
        {
            var data = await GetAsync<PagedResult<GetReportViewModel>>($"/api/Reports/GetAllReport" +
                $"?AdministrativeDivisionID={request.AdministrativeDivisionID}" +
                $"&DateReport={request.DateReport}" +
                $"&ReportID={request.ReportID}" +
                $"&PostID={request.PostID}" +
                $"&PageIndex={request.PageIndex}" +
                $"&PageSize={request.PageSize}");
            return data;
        }
    }
}
