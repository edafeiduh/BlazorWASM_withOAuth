using FXProject.Shared.DbTables;

namespace FXProject.Server.Services.IService
{
    public interface IUserService
    {

        // Interfaces were used to manage ERRORS and ensure EFFICIENT coding of all METHODS for the API
        Task<IEnumerable<FX_UserList>> FindAllUsers();
        Task<FX_UserList> FindUser(int fxUserID);
        Task<FX_UserList> UpdateUser(FX_UserList fxUser);


    }
}
