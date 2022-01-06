using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Historys;
using cFB.ViewModels.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Historys
{
    public class HistoryApiClient : BaseApiClient, IHistoryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HistoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<PagedResult<GetHistoryViewModel>> GetAllHistory(GetManagerHistoryRequest request)
        {
            var data = await GetAsync<PagedResult<GetHistoryViewModel>>($"/api/Historys/GetAllHistory" +
                $"?AdministrativeDivision_Id={request.AdministrativeDivision_Id}" +
                $"&Event={request.Event}" +
                $"&StartDate={request.StartDate}" +
                $"&EndDate={request.EndDate}" +
                $"&PageIndex={request.PageIndex}" +
                $"&PageSize={request.PageSize}");
            return data;
        }

        public async Task<PagedResult<GetHistoryClientViewModel>> GetAllHistoryClient(GetManagerHistoryClientRequest request)
        {
            var data = await GetAsync<PagedResult<GetHistoryClientViewModel>>($"/api/Historys/GetAllHistoryClient" +
                $"?AdministrativeDivision_Id={request.AdministrativeDivision_Id}" +
                $"&StartDate={request.StartDate}" +
                $"&EndDate={request.EndDate}" +
                $"&IPAdress={request.IPAdress}" +
                $"&PageIndex={request.PageIndex}" +
                $"&PageSize={request.PageSize}");
            return data;
        }

        public async Task<bool> CreateHistoryClient(string AdministrativeDivisionID)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(AdministrativeDivisionID.ToString()), "AdministrativeDivisionID");

            var response = await client.PostAsync($"/api/Historys?AdministrativeDivisionID={AdministrativeDivisionID}", requestContent);

            return response.IsSuccessStatusCode;
        }


    }
}
