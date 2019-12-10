using Airbox.Api.Domain.Models;
using Airbox.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Controllers
{
    [Route("api/users/{username}/locations")]
    [ApiController]
    public class UsersLocationsController : ControllerBase
    {
        private readonly IUserLocationService _userLocationService;

        public UsersLocationsController(IUserLocationService userLocationService)
        {
            _userLocationService = userLocationService;
        }

        // GET api/values/5
        [HttpGet]
        public async Task<IEnumerable<UserLocation>> GetAsync([FromRoute] string username, [FromQuery] int current = 0)
        {
            if (0 == current)
            {
                return await _userLocationService.ListCurrentAsync(username);
            }
            else
            {
                return await _userLocationService.ListHistoryAsync(username);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] UserLocation location)
        {
        }
    }
}
