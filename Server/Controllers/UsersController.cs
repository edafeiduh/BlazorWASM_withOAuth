using FXProject.Server.Services.IService;
using FXProject.Shared.DbTables;
using Microsoft.AspNetCore.Mvc;

namespace FXProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase // Derive from ControllerBase as this is a WebAPI that supports other Non-Web Applications 
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        //To GET the list of all available users in the application
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<FX_UserList>>> GetUsers()
        {
            try
            {
                return Ok(await _userService.FindAllUsers());
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from server");
            }
        }

        //To GET the list of a specific user in the application by the UserID
        [HttpGet("GetUser/{fxUserID:int}")]
        public async Task<ActionResult<FX_UserList>> GetUser(int fxUserID)
        {
            try
            {
                var result = await _userService.FindUser(fxUserID);
                if(result == null)
                {
                    return NotFound();
                }
                else
                {

                    return result;
                }
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error reaching Server");
            }
        }


        //To Update a specific user data profile in the application by the UserID
        [HttpPut("Update/{fxUserID:int}")]

        public async Task<ActionResult<FX_UserList>> UpdateUser(int fxUserID, FX_UserList fxUser)
        {
            try
            {
                if (fxUserID != fxUser.UserID)
                {
                    return BadRequest();
                }
                
                else
                {
                    var fxUserToUpdate = await _userService.FindUser(fxUserID);


                    if(fxUserToUpdate == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return await _userService.UpdateUser(fxUser);
                    }


                }


            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error reaching Server");
            }
        }


    }
}
