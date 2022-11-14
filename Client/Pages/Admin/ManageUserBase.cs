using FXProject.Client.Pages.Admin.Services;
using FXProject.Client.Services;
using FXProject.Shared.DbTables;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FXProject.Client.Pages.Admin
{
    public class ManageUserBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationStateTask { get; set; }

        public string? PageHeaderText { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public ILocationService_Web LocationService_Web { get; set; } = null!;
        public List<FX_Location> Locations { get; set; } = new List<FX_Location>();

      
       
        [Inject]
        public IUserService_Web? UserService_Web { get; set; }
        public FX_UserList FXUser { get; set; } = new FX_UserList();

        [Parameter]
        public string? userID { get; set; }


        protected override async Task OnInitializedAsync()
        {

            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)

            {
                //string returnUrl = WebUtility.UrlEncode($"/edituserdata /{userID}");
                NavigationManager.NavigateTo("/");
            }


            else
            {

                int.TryParse(userID, out int fxUserID);

                if (fxUserID != 0)
                {
                    PageHeaderText = "Edit User Profile";
                    FXUser = await UserService_Web.GetUser(int.Parse(userID));
                }

                else
                {
                    PageHeaderText = "Create a New User Profile";
                    FXUser = new FX_UserList
                    {
                        UserFirstName = "James",

                        UserLastName = "Scott",

                        UserEmail = "@niu.edu",

                        UserIsActive = true,

                    };
                }


               Locations = (await LocationService_Web.GetAllLocations()).ToList();

            }

        }


        protected async Task HandleValidUserEdit()
        {
            FX_UserList? result = null;

            if (FXUser.UserID != 0)
            {

                result = await UserService_Web.UpdateUserInfo(FXUser);

            }

          
            if (result != null)
            {
                NavigationManager.NavigateTo("/userdirectory");
            }
        }


        protected async Task HandleInvalidUserEdit()
        {

            NavigationManager.NavigateTo("/userdirectory", true);

        }


    }

}
