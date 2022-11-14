
using FXProject.Server.Services.IService;
using FXProject.Shared.DbTables;
using Microsoft.AspNetCore.Mvc;

namespace FXProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _location;

        public LocationController(ILocationService iLocation)
        {
            _location = iLocation;
        }

        //To GET the list of all available Locations for the users

        [HttpGet("GetLocations")]
        public async Task<ActionResult<IEnumerable<FX_Location>>> GetLocations()
        {
            try
            {
                return Ok(await _location.FindAllLocations());
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from server");
            }
             
        }

        //To GET the list of a specific Location by the locationID
        [HttpGet("GetLocation/{locationID:int}")]
        public async Task<ActionResult<FX_Location>> GetLocation(int locationID)
        {
            var fxLocation = await _location.FindLocation(locationID);

            if (fxLocation != null)
            {
                return Ok(fxLocation);
            }

            return NotFound();
        }
    }
}
