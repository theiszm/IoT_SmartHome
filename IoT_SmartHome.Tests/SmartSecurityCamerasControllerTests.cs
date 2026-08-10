using IoT_SmartHome.Api.Controllers;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IoT_SmartHome.Tests
{
    public class SmartSecurityCamerasControllerTests
    {
        private HomeDbContext GetInMemoryDbContext()
        {
             var options = new DbContextOptionsBuilder<HomeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new HomeDbContext(options);
        }

        [Fact]
        public async Task Get_ReturnsAllSmartSecurityCameras()
        {
            using var context = GetInMemoryDbContext();
            context.SmartSecurityCameras.AddRange(
                new SmartSecurityCamera { Id = 1, Room = "Front Porch", IsRecording = true, StorageUsagePercentage = 50, MotionDetected = true },
                new SmartSecurityCamera { Id = 2, Room = "Backyard", IsRecording = false, StorageUsagePercentage = 20, MotionDetected = true }
                );
            await context.SaveChangesAsync();

            var controller = new SmartSecurityCamerasController(context);
            
            var result = await controller.Get();
            
            var cameras = result.Value;
            Assert.NotNull(cameras);
            Assert.Equal(2, cameras.Count());
        }
    }
}
