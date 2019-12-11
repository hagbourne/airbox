using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Resources
{
    /// <summary>
    /// Data transfer object for a single user with a list of any recorded locations.
    /// </summary>
    public class UserLocationResource
    {
        [Required]
        public string Username { get; set; }

        public IEnumerable<LocationResource> Locations { get; set; }
    }
}
