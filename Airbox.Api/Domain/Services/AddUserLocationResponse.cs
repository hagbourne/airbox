using Airbox.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Domain.Services
{
    public class AddUserLocationResponse : BaseResponse
    {
        public UserLocation UserLocation { get; private set; }

        private AddUserLocationResponse(bool success, string message, UserLocation userLocation) : base(success, message)
        {
            UserLocation = userLocation;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public AddUserLocationResponse(UserLocation userLocation) : this(true, string.Empty, userLocation) { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AddUserLocationResponse(string message) : this(false, message, null) { }
    }
}
