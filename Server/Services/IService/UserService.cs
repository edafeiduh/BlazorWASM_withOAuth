
using FXProject.Server.ForexDbContext;
using FXProject.Shared.DbTables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FXProject.Server.Services.IService
{
    public class UserService : IUserService
    {
        // This CODE Implements all the METHOD declarations in the IUserService Interface
        // Used LINQ queries to manage data access and filteration
        // Implemented Exception Handling to ensure proper coding and debugging operations

        private readonly FXDbContext forexDbContext;
        public UserService(FXDbContext _forexDbContext)
        {
            forexDbContext = _forexDbContext;

        }

        public async Task<IEnumerable<FX_UserList>> FindAllUsers()
        {
            return await forexDbContext.FX_UserList
                .Include(e => e.MyLocation)
                .ToListAsync();
        }


        public async Task<FX_UserList> FindUser(int fxUserID)
        {
            try
            {
                return await forexDbContext.FX_UserList
                    .Include(e => e.MyLocation)
                    .FirstOrDefaultAsync(e => e.UserID == fxUserID);

            }

            catch
            {
                throw;
            }

        }

        public async Task<FX_UserList> UpdateUser(FX_UserList fxUser)
        {
            var result = await forexDbContext.FX_UserList
                .FirstOrDefaultAsync(e => e.UserID == fxUser.UserID);

            if (result != null)
            {
                result.UserFirstName = fxUser.UserFirstName;
                result.UserLastName = fxUser.UserLastName;
                result.UserEmail = fxUser.UserEmail;
                result.UserLocation = fxUser.UserLocation;
                result.UserIsActive = fxUser.UserIsActive;
            }
            await forexDbContext.SaveChangesAsync();

            return result;
        }


    }
}
