using cFB.Utilities.Constants;
using cFB.ViewModels.System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Systems.Users
{
    public class UserApiClient : BaseApiClient, IUserApiSevice
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserApiClient(IHttpClientFactory httpClientFactory,
                    IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> LoginInWed(GetLoginRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.UserName.ToString()), "UserName");
            requestContent.Add(new StringContent(request.Password.ToString()), "Password");
            var response = await client.PostAsync("/api/User/LoginWed", requestContent);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> Authencate(GetLoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);
            var response = await client.PostAsync($"/api/User/Login?UserName={request.UserName}&Password={request.Password}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<GetCheckRole> CheckRole(string administrativeDivision_Id)
        {
            var response = await GetAsync<GetCheckRole>($"/api/User/CheckRoleUser?administrativeDivision_Id={administrativeDivision_Id}");
            return response;
        }

        public async Task<GetUserSeviceViewModel> GetUserById(string administrativeDivision_Id)
        {
            var response = await GetAsync<GetUserSeviceViewModel>($"/api/User/GetUserById/{administrativeDivision_Id}");
            return response;
        }


        public async Task<bool> Create(GetRegisterRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.AdministrativeDivision_Admin.ToString()), "AdministrativeDivision_Admin");
            requestContent.Add(new StringContent(request.AdministrativeDivisionName.ToString()), "AdministrativeDivisionName");
            requestContent.Add(new StringContent(request.Addrees.ToString()), "Addrees");
            requestContent.Add(new StringContent(request.NumberPhone.ToString()), "NumberPhone");
            requestContent.Add(new StringContent(request.Password.ToString()), "Password");
            requestContent.Add(new StringContent(request.ConfirmPassword.ToString()), "ConfirmPassword");
            var response = await client.PostAsync($"api/User", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Update(GetUserUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.AdministrativeDivisionID.ToString()), "AdministrativeDivisionID");
            requestContent.Add(new StringContent(request.AdministrativeDivisionName.ToString()), "AdministrativeDivisionName");
            requestContent.Add(new StringContent(request.NumberPhone.ToString()), "NumberPhone");
            requestContent.Add(new StringContent(request.Addrees.ToString()), "Addrees");
            
            requestContent.Add(new StringContent(request.Password == null?"":request.Password.ToString()), "Password");

            var response = await client.PostAsync($"/api/User/UpdateToUser", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<GetUserSeviceViewModel>> GetUsersPaging(GetUserRequest request)
        {
            var data = await GetAsync<List<GetUserSeviceViewModel>>($"/api/User/GetUsersAll?ManagerId={request.ManagerID}&administrativeDivisionId={request.AdministrativeDivisionID}");
            return data;
        }

        public async Task<List<GetUserViewModel>> GetUsersPagingList(GetUserRequest request)
        {
            var response = await GetAsync<List<GetUserViewModel>>($"/api/User/GetUsersAllList?ManagerID={request.ManagerID}&AdministrativeDivisionID={request.AdministrativeDivisionID}");
            return response;
        }

        public async Task<string> CheckPhone(string NumberPhone)
        {
            var response = await GetAsync<string>($"/api/User/CheckPhone?NumberPhone={NumberPhone}");
            return response;
        }


        public async Task<bool> UpdateRole(GetUserUpdateRoleRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.AdministrativeDivisionID.ToString()), "AdministrativeDivisionID");
            requestContent.Add(new StringContent(request.AdministrativeDivision_Admin.ToString()), "AdministrativeDivision_Admin");

            var response = await client.PostAsync($"/api/User/UpdateRole", requestContent);

            return response.IsSuccessStatusCode;
        }
    }
}
