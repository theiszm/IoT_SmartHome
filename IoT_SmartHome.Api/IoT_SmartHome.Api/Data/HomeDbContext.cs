using Microsoft.EntityFrameworkCore;
using IoT_SmartHome.Api.Models;

namespace IoT_SmartHome.Api.Data
{
    public class HomeDbContext : DbContext
    {
        public HomeDbContext(DbContextOptions<HomeDbContext> options)
            : base(options) { }

        // Creates the model tables in the database
        public DbSet<SmartLight> SmartLights { get; set; }
        public DbSet<SmartSecurityCamera> SmartSecurityCameras { get; set; }
        public DbSet<SmartSpeaker> SmartSpeakers { get; set; }
        public DbSet<SmartThermostat> SmartThermostats { get; set; }

    }
}
