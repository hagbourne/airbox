using Airbox.Api.Domain.Models;
using Airbox.Api.Repositories;
using Airbox.Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Tests
{
    [TestClass]
    public class WithUserLocationService
    {
        private UserLocationService _service = new UserLocationService(new InMemoryUserLocationRepository());

        [TestInitialize]
        public void SetUp()
        {
            _service.AddUserLocationAsync(new UserLocation() { Username = "test", Latitude = 101.0, Longitude = 51.0, Timestamp = new DateTime(2019, 1, 1) });
            _service.AddUserLocationAsync(new UserLocation() { Username = "test", Latitude = 102.0, Longitude = 52.0, Timestamp = new DateTime(2019, 1, 2) });
            _service.AddUserLocationAsync(new UserLocation() { Username = "test", Latitude = 103.0, Longitude = 53.0, Timestamp = new DateTime(2019, 1, 3) });

            _service.AddUserLocationAsync(new UserLocation() { Username = "other", Latitude = 21.0, Longitude = -21.0, Timestamp = new DateTime(2019, 2, 1) });
            _service.AddUserLocationAsync(new UserLocation() { Username = "other", Latitude = 22.0, Longitude = -22.0, Timestamp = new DateTime(2019, 2, 2) });
        }

        [TestMethod]
        public async Task WhenAddingUserLocation()
        {
            var newLocation = new UserLocation()
            {
                Username = "new",
                Latitude = 123.456,
                Longitude = -43.21,
                Timestamp = new DateTime(2019, 12, 12, 9, 30, 45)
            };

            var response = await _service.AddUserLocationAsync(newLocation);

            Assert.IsTrue(response.Success, "Then add succeeds.");

            Assert.AreEqual(newLocation.Username, response.UserLocation.Username, "Then username is returned unmodified.");
            Assert.AreEqual(newLocation.Latitude, response.UserLocation.Latitude, "Then latitude is returned unmodified.");
            Assert.AreEqual(newLocation.Longitude, response.UserLocation.Longitude, "Then longitude is returned unmodified.");
            Assert.AreEqual(newLocation.Timestamp, response.UserLocation.Timestamp, "Then timestamp is returned unmodified.");

            var badResponse = await _service.AddUserLocationAsync(new UserLocation());

            Assert.IsFalse(badResponse.Success, "Then add fails.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(badResponse.Message), "Then an error message is returned");
        }

        [TestMethod]
        public async Task WhenListingUserLocationHistory()
        {
            var getLocations = (await _service.ListHistoryAsync("test")).ToList();

            Assert.AreEqual(3, getLocations.Count, "Then three user locations are returned.");

            Assert.AreEqual(101.0, getLocations[0].Latitude, "Then first latitude is lowest.");
            Assert.AreEqual(103.0, getLocations[2].Latitude, "Then last latitude is highest.");

            var getNoLocations = (await _service.ListHistoryAsync("ZZZZZ")).ToList();

            Assert.IsNotNull(getNoLocations, "Then a list is returned.");
            Assert.AreEqual(0, getNoLocations.Count(), "Then an empty list is returned.");

            try
            {
                var getAllLocations = (await _service.ListHistoryAsync(null)).ToList();
            }
            catch (ApplicationException)
            {
                return;
            }

            Assert.Fail("Then an application exception is thrown for history of all users.");
        }

        [TestMethod]
        public async Task WhenListingCurrentUserLocation()
        {
            var getLocations = (await _service.ListCurrentAsync("test")).ToList();

            Assert.AreEqual(1, getLocations.Count, "Then a single user location is returned.");

            Assert.AreEqual(103.0, getLocations[0].Latitude, "Then latitude is highest.");

            var getNoLocations = (await _service.ListCurrentAsync("ZZZZZ")).ToList();

            Assert.IsNotNull(getNoLocations, "Then a list is returned.");
            Assert.AreEqual(0, getNoLocations.Count(), "Then an empty list is returned.");

            var getAllLocations = (await _service.ListCurrentAsync(null)).ToList();

            Assert.AreEqual(2, getAllLocations.Count, "Then two user locations are returned.");

            Assert.AreEqual("other", getAllLocations[0].Username, "Then first user is other (alpha).");
            Assert.AreEqual("test", getAllLocations[1].Username, "Then second user is test (alpha).");

            Assert.AreEqual(22.0, getAllLocations[0].Latitude, "Then first latitude is other/highest.");
            Assert.AreEqual(103.0, getAllLocations[1].Latitude, "Then second latitude is test/highest.");
        }
    }
}
