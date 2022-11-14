using FXProject.Client.Pages.Admin.Services;
using FXProject.Client.Services;
using FXProject.Shared.DbTables;
using Microsoft.AspNetCore.Components;


namespace FXProject.Client.Pages.Admin
{
    public class UserBase : ComponentBase
    {

        [Inject]
        public NavigationManager? NavigationManager { get; set; }


        [Inject]
        IUserService_Web UserService_Web { get; set; } = null!;

        public IEnumerable<FX_UserList> UserList { get; set; } = new List<FX_UserList>();
        public List<FX_UserList> searchUserList = new();            

        protected override async Task OnInitializedAsync()
        {
            await GetAllUsers();
        }


        public async Task GetAllUsers()
        {
            UserList = await UserService_Web.GetAllUsers();


            searchUserList = UserList.ToList();
        }


        /*This is used for searching through the list --- START OF CODE ---*/

        protected string SearchUser { get; set; } = string.Empty;

        protected void FilterUser()
        {
            if (!string.IsNullOrEmpty(SearchUser))
            {
                UserList = searchUserList
                    .Where(x => x.UserEmail.IndexOf(SearchUser, StringComparison.OrdinalIgnoreCase) != -1)
                    .ToList();
            }
            else
            {
                UserList = searchUserList;
            }
        }

        public void ResetUserSearch()
        {
            SearchUser = string.Empty;
            UserList = searchUserList;
        }


        /*This is used for searching through the list --- END OF CODE ---*/
    }
}
