using Microsoft.EntityFrameworkCore;
using IoT_SmartHome.Api.Models;

namespace IoT_SmartHome.Api.Data
{
    public class HomeDbContext : DbContext
    {
        public HomeDbContext(DbContextOptions<HomeDbContext> options)
            : base(options) { }

        // Creates a SmartLights table in the database
        public DbSet<SmartLight> SmartLights { get; set; }
    
    }
}
