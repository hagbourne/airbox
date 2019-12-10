using Airbox.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Domain.Services
{
    public interface IUserLocationService
    {
        Task<IEnumerable<UserLocation>> ListAllCurrentAsync();

        Task<IEnumerable<UserLocation>> ListCurrentAsync(string username);

        Task<IEnumerable<UserLocation>> ListHistoryAsync(string username);

        Task<AddUserLocationResponse> AddUserLocationAsync(UserLocation userLocation);
    }
}
