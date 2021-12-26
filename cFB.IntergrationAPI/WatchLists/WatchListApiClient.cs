using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.WatchLists;
using cFB.ViewModels.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.WatchLists
{
    public class WatchListApiClient : BaseApiClient, IWatchListApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public WatchListApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> GetCountWatchList(string administrativeDivision_Id)
        {
            var data = await GetAsync<int>($"/api/WatchList/GetCountWatchList?administrativeDivision_Id={administrativeDivision_Id}");
            return data;
        }

        public async Task<bool> AddNewOrUpdateWatchList(GetWatchListCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            //requestContent.Add(new StringContent(request.FaceBookID.ToString()), "faceBookID");
            requestContent.Add(new StringContent(request.FaceBookName.ToString()), "FaceBookName");
            requestContent.Add(new StringContent(request.FaceBookUrl.ToString()), "FaceBookUrl");
            requestContent.Add(new StringContent(Status.Activate.ToString()), "Status");///// chổ này tại không kiếm cách lấy ra 1 list dữ liệu của lớp 
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AdministrativeDivisionID) ? "Admin" : request.AdministrativeDivisionID.ToString()), "AdministrativeDivisionID"); /// giá trị khi cột đó có giá trị null thi mặc định là quản trị viên
            requestContent.Add(new StringContent(request.FaceBookTypeID.ToString()), "FaceBookTypeID");
            var response = await client.PostAsync($"api/WatchList", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(string FaceBookID, string FaceBookName, string FaceBookTypeId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(FaceBookID.ToString()), "FaceBookID");
            requestContent.Add(new StringContent(FaceBookName.ToString()), "FaceBookName");
            requestContent.Add(new StringContent(FaceBookTypeId.ToString()), "FaceBookTypeId");

            var response = await client.PostAsync($"/api/WatchList/UpdateToWatchList/{FaceBookID}?FaceBookName={FaceBookName}&FaceBookTypeId={FaceBookTypeId}", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<WatchList>> GetAllWatchList(GetManageListWatchListPagingRequest request)
        {
            var data = await GetAsync<List<WatchList>>($"/api/WatchList/GetAllWatchList?AdministrativeDivisionID={request.AdministrativeDivisionID}");
            return data;
        }

        public async Task<PagedResult<GetWatchListViewModel>> GetAllWatchListPagedResult(GetManageWatchListRequest request)
        {
            var data = await GetAsync<PagedResult<GetWatchListViewModel>>($"/api/WatchList/GetAllWatchListPagedResult" +
                $"?AdministrativeDivisionID={request.AdministrativeDivisionID}" +
                $"&FacebookTypeID={request.FacebookTypeID}" +
                $"&Status={request.Status}" +
                $"&PageIndex={request.PageIndex}&PageSize={request.PageSize}");
            return data;
        }
    }
}
