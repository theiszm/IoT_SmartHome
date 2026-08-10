using IoT_SmartHome.Api.Controllers;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IoT_SmartHome.Tests
{
    public class SmartSpeakersControllerTests
    {
        private HomeDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<HomeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new HomeDbContext(options);
        }

        [Fact]
        public async Task Get_ReturnsAllSmartSpeakers()
        {
            using var context = GetInMemoryDbContext();
            context.SmartSpeakers.AddRange(
                new SmartSpeaker { Id = 1, Room = "Living Room", Volume = 50, CurrentTrack = "Song A", IsMuted = false },
                new SmartSpeaker { Id = 2, Room = "Office", Volume = 20, CurrentTrack = "Song B", IsMuted = true }
                );
            await context.SaveChangesAsync();

            var controller = new SmartSpeakersController(context);

            var result = await controller.Get();

            var speakers = result.Value;
            Assert.NotNull(speakers);
            Assert.Equal(2, speakers.Count());
        
        }
    }
}
