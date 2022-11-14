using FXProject.Shared.DbTables;


namespace FXProject.Client.Services
{
    public interface IUserService_Web
    {
        Task<IEnumerable<FX_UserList>> GetAllUsers();

        Task <FX_UserList> GetUser(int fxUserID);

        Task<FX_UserList> UpdateUserInfo(FX_UserList fxUser);
    }
}
