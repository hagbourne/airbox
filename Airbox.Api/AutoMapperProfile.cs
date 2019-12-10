using Airbox.Api.Domain.Models;
using Airbox.Api.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserLocation, LocationResource>();
            CreateMap<LocationResource, UserLocation>();
        }
    }
}
