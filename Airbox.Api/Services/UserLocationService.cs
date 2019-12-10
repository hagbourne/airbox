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

        public Task<UserLocation> AddUserLocationAsync(UserLocation userLocation)
        {
            return _locationRepository.AddUserLocationAsync(userLocation);
        }

        public Task<IEnumerable<UserLocation>> ListCurrentAsync(string username)
        {
            return _locationRepository.ListCurrentAsync(username);
        }

        public Task<IEnumerable<UserLocation>> ListHistoryAsync(string username)
        {
            return _locationRepository.ListHistoryAsync(username);
        }
    }
}
