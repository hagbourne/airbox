using System;

namespace Airbox.Api.Domain.Models
{
    /// <summary>
    /// Data object for a single user location entry.
    /// </summary>
    public class UserLocation
    {
        public string Username { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
