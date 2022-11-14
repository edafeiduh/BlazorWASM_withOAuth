using FXProject.Shared.DbTables;

namespace FXProject.Client.Pages.Admin.Services
{
    public interface ILocationService_Web
    {

        Task<IEnumerable<FX_Location>> GetAllLocations();

        Task<FX_Location> GetLocation(int locationID);

    }
}