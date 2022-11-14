using FXProject.Shared.DbTables;

namespace FXProject.Server.Services.IService
{
    public interface ILocationService
    {
        // Interfaces were used to manage ERRORS and ensure EFFICIENT coding of all METHODS for the API

        Task<IEnumerable<FX_Location>> FindAllLocations();
        Task<FX_Location> FindLocation(int fxLocationID);


    }
}
