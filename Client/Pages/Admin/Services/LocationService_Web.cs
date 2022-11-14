using FXProject.Shared.DbTables;
using System.Net.Http.Json;

namespace FXProject.Client.Pages.Admin.Services
{
    public class LocationService_Web : ILocationService_Web
    {

        private readonly HttpClient httpClient;


        public LocationService_Web (HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        // http services used on the client side through Dependency Injection to GET ALL LOCATIONS
        public async Task<IEnumerable<FX_Location>> GetAllLocations()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<FX_Location>>("api/Location/GetLocations");
        
        }

        // http services used on the client side through Dependency Injection to GET A SPECIFIC LOCATION
        public async Task<FX_Location> GetLocation(int locationID)
        {
            return await httpClient.GetFromJsonAsync<FX_Location>($"api/Location/GetLocation/{locationID}");
        }
            

    }
}
