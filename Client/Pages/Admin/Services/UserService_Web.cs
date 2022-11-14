using FXProject.Shared.DbTables;
using System.Net.Http.Json;

namespace FXProject.Client.Services
{
    public class UserService_Web : IUserService_Web
    {
        private readonly HttpClient httpClient;

        public UserService_Web (HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }


        // http services used on the client side through Dependency Injection to GET ALL USERS
        public async Task<IEnumerable<FX_UserList>> GetAllUsers()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<FX_UserList>>("api/Users/GetUsers");
        }


        // http services used on the client side through Dependency Injection to GET A SPECIFIC USER
        public async Task<FX_UserList> GetUser(int fxUserID)
        {
            return await httpClient.GetFromJsonAsync<FX_UserList>($"api/Users/GetUser/{fxUserID}");
        }


        // http services used on the client side through Dependency Injection to UPDATE A SPECIFIC User Data Profile
        public async Task<FX_UserList> UpdateUserInfo (FX_UserList fxUser)
        {
            var response = await httpClient.PutAsJsonAsync<FX_UserList>($"api/Users/Update/{fxUser.UserID}", fxUser);

            return await response.Content.ReadFromJsonAsync<FX_UserList>();
        }


    }
}
