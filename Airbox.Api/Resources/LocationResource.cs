using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Resources
{
    /// <summary>
    /// Data transfer object for a single location resource.
    /// </summary>
    public class LocationResource
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

    }
}
