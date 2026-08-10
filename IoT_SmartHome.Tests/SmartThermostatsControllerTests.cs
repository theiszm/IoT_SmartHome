using IoT_SmartHome.Api.Controllers;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IoT_SmartHome.Tests
{
    public class SmartThermostatsControllerTests
    {
        private HomeDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<HomeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new HomeDbContext(options);
        }

        [Fact]
        public async Task Get_ReturnsAllSmartThermostats()
        {
            using var context = GetInMemoryDbContext();
            context.SmartThermostats.AddRange(
                new SmartThermostat { Id = 1, Room = "Living Room", CurrentTemperature = 21.0, TargetTemperature = 22.0, SystemMode = "Heating" }, 
                new SmartThermostat { Id = 2, Room = "Bedroom", CurrentTemperature = 24.0, TargetTemperature = 20.0, SystemMode = "Cooling" }   
            );
            await context.SaveChangesAsync();

            var controller = new SmartThermostatsController(context); 

            var result = await controller.Get(); 

            var thermostats = result.Value;
            Assert.NotNull(thermostats);
            Assert.Equal(2, thermostats.Count());
        }
    }
}
