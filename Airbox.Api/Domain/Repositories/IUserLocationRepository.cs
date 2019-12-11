using Airbox.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Domain.Repositories
{
    /// <summary>
    /// Interface of user location persistence repository class.
    /// </summary>
    public interface IUserLocationRepository
    {
        Task<IEnumerable<UserLocation>> ListCurrentAsync(string username);

        Task<IEnumerable<UserLocation>> ListHistoryAsync(string username);

        Task<UserLocation> AddUserLocationAsync(UserLocation userLocation);
    }
}
