using FXProject.Server.ForexDbContext;
using FXProject.Shared.DbTables;
using Microsoft.EntityFrameworkCore;

namespace FXProject.Server.Services.IService
{
    public class LocationService : ILocationService
    {
        // This CODE Implements all the METHOD declarations in the ILocationService Interface

        private readonly FXDbContext _dbContext;

        public LocationService(FXDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //To READ the list of all Application User Type created and approved in CB_UserType table
        public async Task<IEnumerable<FX_Location>> FindAllLocations()
        {
            try
            {
                return await _dbContext.FX_Location.ToListAsync();
            }

            catch
            {
                throw;
            }

        }


        //To READ a *SPECIFIC* User Type by *usertypeID* created and approved in CB_UserType table
        public async Task<FX_Location> FindLocation(int locationID)
        {
            try
            {
                var Location_ToFind = await _dbContext.FX_Location.FindAsync(locationID);

                if (Location_ToFind != null)
                {
                    return Location_ToFind;
                }

                else
                {
                    throw new ArgumentNullException();
                }
            }


            catch (Exception)
            {
                throw;
            }


        }
    }
}
