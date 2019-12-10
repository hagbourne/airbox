using Airbox.Api.Domain.Models;
using Airbox.Api.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Repositories
{
    public class InMemoryUserLocationRepository : IUserLocationRepository
    {
        private readonly IList<UserLocation> _userLocations = new List<UserLocation>();

        public InMemoryUserLocationRepository()
        {
            Seed();
        }

         public async Task<IEnumerable<UserLocation>> ListCurrentAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                // Return a location for all users grouped by username returning the first of each group reverse sorted by timestamp.
                return _userLocations.GroupBy(u => u.Username, (k, g) => g.OrderByDescending(u => u.Timestamp).First());
            }
            else
            {
                // Return a location for the given user, grouped by username returning the first of each group reverse sorted by timestamp.
                return _userLocations.Where(u => u.Username == username).GroupBy(u => u.Username, (k, g) => g.OrderByDescending(u => u.Timestamp).First());
            }
        }

        public async Task<IEnumerable<UserLocation>> ListHistoryAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ApplicationException($"A full list of locations for all users is not supported.");
            }
            else
            {
                // Return an ordered list of locations for the given user.
                return (IEnumerable<UserLocation>)_userLocations.Where(u => u.Username == username).OrderBy(u => u.Timestamp);
            }
        }

        public async Task<UserLocation> AddUserLocationAsync(UserLocation userLocation)
        {
            _userLocations.Add(userLocation);

            return userLocation;
        }

        private void Seed()
        {
            _userLocations.Add(new UserLocation() { Username = "Test", Latitude = 123.45, Longitude = -12.34, Timestamp = DateTime.Now });
        }
    }
}
