using Airbox.Api.Domain.Models;
using Airbox.Api.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api
{
    /// <summary>
    /// Create maps for automapper. Discovered and instantiated automatically.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserLocation, LocationResource>();
            CreateMap<LocationResource, UserLocation>();
        }
    }
}
