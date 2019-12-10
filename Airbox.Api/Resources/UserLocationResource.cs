using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Resources
{
    public class UserLocationResource
    {
        [Required]
        public string Username { get; set; }

        public IEnumerable<LocationResource> Locations { get; set; }
    }
}
