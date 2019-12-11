using Airbox.Api.Domain.Models;
using Airbox.Api.Domain.Repositories;
using Airbox.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Services
{
    /// <summary>
    /// Service to provide operations to user location objects.
    /// </summary>
    public class UserLocationService : IUserLocationService
    {
        private readonly IUserLocationRepository _locationRepository;

        public UserLocationService(IUserLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Add a new populated user location object to a back-end repository.
        /// </summary>
        /// <param name="userLocation">A populated user location object.</param>
        /// <returns>A wrapped response with success/error and the added user location object.</returns>
        public async Task<AddUserLocationResponse> AddUserLocationAsync(UserLocation userLocation)
        {
            UserLocation userLocationResult;

            try
            {
                userLocationResult = await _locationRepository.AddUserLocationAsync(userLocation);
            }
            catch (Exception ex)
            {
                return new AddUserLocationResponse($"An error occurred when saving the user location: {ex.Message}");
            }

            return new AddUserLocationResponse(userLocationResult);
        }

        /// <summary>
        /// Get from the back-end repository current user locations for all users.
        /// </summary>
        /// <returns>List of current locations or an empty list.</returns>
        public async Task<IEnumerable<UserLocation>> ListAllCurrentAsync()
        {
            return await _locationRepository.ListCurrentAsync(null);
        }

        /// <summary>
        /// Get from the back-end repository current user locations for a matching user.
        /// </summary>
        /// <param name="username">A username to match.</param>
        /// <returns>List of current locations or an empty list.</returns>
        public async Task<IEnumerable<UserLocation>> ListCurrentAsync(string username)
        {
            return await _locationRepository.ListCurrentAsync(username);
        }

        /// <summary>
        /// Get from the back-end repository all user locations for a matching user.
        /// </summary>
        /// <param name="username">A username to match.</param>
        /// <returns>List of all locations or an empty list.</returns>
        public async Task<IEnumerable<UserLocation>> ListHistoryAsync(string username)
        {
            return await _locationRepository.ListHistoryAsync(username);
        }
    }
}
