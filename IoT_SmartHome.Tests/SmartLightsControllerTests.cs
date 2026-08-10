using IoT_SmartHome.Api.Controllers;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IoT_SmartHome.Tests
{
    public class SmartLightsControllerTests
    {
        private HomeDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<HomeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new HomeDbContext(options);
        }

        [Fact]
        public async Task Get_ReturnsAllSmartLights()
        {
            using var context = GetInMemoryDbContext();

            context.SmartLights.AddRange(
                new SmartLight { Id = 1, Room = "Living Room", IsOn = true, Brightness = 80 },
                new SmartLight { Id = 2, Room = "Bedroom", IsOn = false, Brightness = 0 }
                );
            await context.SaveChangesAsync();

            var controller = new SmartLightsController(context);

            var result = await controller.Get();

            var lights = result.Value;

            Assert.NotNull(lights);
            Assert.Equal(2, lights.Count());

        }


    }
}
