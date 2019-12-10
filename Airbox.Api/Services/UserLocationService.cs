using Airbox.Api.Domain.Models;
using Airbox.Api.Domain.Repositories;
using Airbox.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Services
{
    public class UserLocationService : IUserLocationService
    {
        private readonly IUserLocationRepository _locationRepository;

        public UserLocationService(IUserLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

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

        public async Task<IEnumerable<UserLocation>> ListAllCurrentAsync()
        {
            return await _locationRepository.ListCurrentAsync(null);
        }

        public async Task<IEnumerable<UserLocation>> ListCurrentAsync(string username)
        {
            return await _locationRepository.ListCurrentAsync(username);
        }

        public async Task<IEnumerable<UserLocation>> ListHistoryAsync(string username)
        {
            return await _locationRepository.ListHistoryAsync(username);
        }
    }
}
