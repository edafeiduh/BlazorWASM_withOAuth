using FXProject.Shared.DbTables;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Net.Http.Json;
using System.Security.Claims;


// This is used to create custom claims and add it to OKTA authentication which can be used for Role Base Authorization

public class OktaCustomClaims : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    private readonly IHttpClientFactory _httpClientFactory;
    public OktaCustomClaims(IAccessTokenProviderAccessor tokenaccessor, IHttpClientFactory httpClientFactory) : base(tokenaccessor)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount useraccount, RemoteAuthenticationUserOptions userOptions)
    {
        var initialUser = await base.CreateUserAsync(useraccount, userOptions);

        // This verifies if the User is Authenticated by OKTA with the required claims

        if (initialUser.Identity.IsAuthenticated)
        {
            var userIdentity = (ClaimsIdentity)initialUser.Identity;

            // This gets the registered User email authenticated by Okta

            var authorizedEmailAddress = userIdentity.Claims.Where(e => e.Type == "preferred_username")
                .Select(e => e.Value).SingleOrDefault();


            // Connect to the Database and get the User ID and Role

            using (var httpclient = _httpClientFactory.CreateClient("FXProject.ServerAPI"))
            {
                var webresponse = await httpclient.GetFromJsonAsync<FX_UserRoleMap>($"api/userlist/{authorizedEmailAddress}");

                if(webresponse != null)
                {
                    int rolecount = webresponse.RoleID_FK;

                    string? userrole = null;

                    switch(webresponse.RoleID_FK)  
                    {
                        case 1:
                            userrole = "Admin User";
                            break;


                        case 2:
                            userrole = "Silver Customer";
                            break;

                        case 3:
                            userrole = "Bronze Member";
                            break;

                        default:
                            break;

                    }

                    // Add the selected Role to the OKTA claims
                    userIdentity.AddClaim(new Claim(ClaimTypes.Role, userrole));
                }
            }
       

        }

        return initialUser;
    }
}