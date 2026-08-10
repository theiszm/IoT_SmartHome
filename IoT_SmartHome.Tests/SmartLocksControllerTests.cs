using IoT_SmartHome.Api.Controllers;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IoT_SmartHome.Tests
{
    public class SmartLocksControllerTests
    {
        private HomeDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<HomeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new HomeDbContext(options);
        }
        [Fact]
        public async Task Get_ReturnsAllSmartLocks()
        {
            using var context = GetInMemoryDbContext();
            context.SmartLocks.AddRange(
                new SmartLock { Id = 1, Room = "Front Door", IsLocked = true, BatteryPercentage = 95 },
                new SmartLock { Id = 2, Room = "Back Door", IsLocked = false, BatteryPercentage = 80 }
                );
            await context.SaveChangesAsync();

            var controller = new SmartLocksController(context);
            
            var result = await controller.Get();

            var locks = result.Value;
            Assert.NotNull(locks);
            Assert.Equal(2, locks.Count());
        }
    }
}
