using Airbox.Api.Domain.Models;
using Airbox.Api.Domain.Services;
using Airbox.Api.Extensions;
using Airbox.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Controllers
{
    /// <summary>
    /// Read current and historic user locations, and add new user locations.
    /// </summary>
    [ApiController]
//    [Route("api/v1/users/{username}/locations")]
//    [Route("api/v1/users/locations")]
    [Route("api/v1/users")]
    public class UsersLocationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserLocationService _userLocationService;

        public UsersLocationsController(IMapper mapper, IUserLocationService userLocationService)
        {
            _mapper = mapper;
            _userLocationService = userLocationService;
        }

        /// <summary>
        /// Get a list of historic locations for a given user or get the current location of either the given user or for all users. Note that full history for all users is not supported so an empty list is returned.
        /// </summary>
        /// <returns>One or more users with a list of locations for each. Current locations are represented as a list with one entry.</returns>
        [HttpGet]
        [Route("locations")]
        public async Task<IEnumerable<UserLocationResource>> GetAsync()
        {
            return await GetAsync(null, 1);
        }

        /// <summary>
        /// Get a list of historic locations for a given user or get the current location of either the given user or for all users. Note that full history for all users is not supported so an empty list is returned.
        /// </summary>
        /// <param name="username">A username matched as a case-sensitive string identifier.</param>
        /// <param name="current">An integer which when set to the value 1 causes only the current location to be returned. Otherwise, an ordered history is returned but only when a username is given.</param>
        /// <returns>One or more users with a list of locations for each. Current locations are represented as a list with one entry.</returns>
        [HttpGet]
        [Route("{username}/locations")]
        public async Task<IEnumerable<UserLocationResource>> GetAsync([FromRoute] string username, [FromQuery] int current = 0)
        {
            IEnumerable<UserLocation> userLocations;

            if (null == username)
            {
                // Matches route api/users/locations
                if (0 == current)
                {
                    userLocations = new List<UserLocation>();
                }
                else
                {
                    userLocations = await _userLocationService.ListCurrentAsync(username);
                }
            }
            else
            {
                // Matches route api/users/{username}/locations
                if (0 == current)
                {
                    userLocations = await _userLocationService.ListHistoryAsync(username);
                }
                else
                {
                    userLocations = await _userLocationService.ListCurrentAsync(username);
                }
            }

            var userLocationResources = userLocations.GroupBy(u => u.Username, (k, g) => new UserLocationResource()
            {
                Username = k,
                Locations = _mapper.Map<IEnumerable<UserLocation>, IEnumerable<LocationResource>>(g)
            });

            return userLocationResources;
        }

        /// <summary>
        /// Add a single location for the given user.
        /// </summary>
        /// <param name="username">Required user name string.</param>
        /// <param name="location">A location object comprising a latitude, longitude and timestamp which are all required. </param>
        /// <returns>A success/error response with the user/location in same format as it will be returned in get requests.</returns>
        [HttpPost]
        [Route("{username}/locations")]
        public async Task<IActionResult> PostAsync([FromRoute] string username, [FromBody] LocationResource location)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("No username supplied.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userLocation = _mapper.Map<LocationResource, UserLocation>(location);
            userLocation.Username = username;

            var response = await _userLocationService.AddUserLocationAsync(userLocation);

            if (!response.Success)
                return BadRequest(response.Message);

            var userLocationResource = new UserLocationResource()
            {
                Username = response.UserLocation.Username,
                Locations = new List<LocationResource>() { _mapper.Map<UserLocation, LocationResource>(response.UserLocation) }
            };

            return Ok(userLocationResource);
        }
    }
}
