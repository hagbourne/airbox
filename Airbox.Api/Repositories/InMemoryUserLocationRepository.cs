using Airbox.Api.Domain.Models;
using Airbox.Api.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Repositories
{
    /// <summary>
    /// An in-memory implementation of a user location repository to test the API in the absense of a true persitence layer.
    /// </summary>
    public class InMemoryUserLocationRepository : IUserLocationRepository
    {
        private readonly IList<UserLocation> _userLocations = new List<UserLocation>();

         /// <summary>
         /// Get a list of current (highest timestamp) locations for the given user, or for all users, from the repository.
         /// </summary>
         /// <param name="username">A username to match case-insensitively, or a null string to return all users.</param>
         /// <returns>A list of user locations or an empty list.</returns>
         public async Task<IEnumerable<UserLocation>> ListCurrentAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                // Return a location for all users grouped by username returning the first of each group reverse sorted by timestamp.
                return _userLocations.OrderBy(u => u.Username).GroupBy(u => u.Username, (k, g) => g.OrderByDescending(u => u.Timestamp).First());
            }
            else
            {
                // Return a location for the given user, grouped by username returning the first of each group reverse sorted by timestamp.
                return _userLocations.Where(u => u.Username == username).GroupBy(u => u.Username, (k, g) => g.OrderByDescending(u => u.Timestamp).First());
            }
        }

        /// <summary>
        /// Get a list of all locations, ordered by timestamp for the given user, from the repository.
        /// </summary>
        /// <param name="username">A username to match case-insensitively.</param>
        /// <returns>A list of user locations or an empty list.</returns>
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

        /// <summary>
        /// Add a populated user location to the repository.
        /// </summary>
        /// <param name="userLocation">A populated user location object.</param>
        /// <returns>The user location object as added.</returns>
        public async Task<UserLocation> AddUserLocationAsync(UserLocation userLocation)
        {
            if (string.IsNullOrWhiteSpace(userLocation.Username))
                throw new ArgumentException("Property 'Username' is a required field.", "userLocation");

            if (null == userLocation.Timestamp)
                throw new ArgumentException("Property 'Timestamp' is a required field.", "userLocation");

            _userLocations.Add(userLocation);

            return userLocation;
        }
    }
}
