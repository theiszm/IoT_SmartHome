using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;

namespace IoT_SmartHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/smartthermostats
    public class SmartThermostatsController : ControllerBase
    {
        private readonly HomeDbContext _context;
        public SmartThermostatsController(HomeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartThermostat>>> Get()
        { 
            return await _context.SmartThermostats.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SmartThermostat device)
        {
            device.LastUpdated = DateTime.UtcNow;
            _context.SmartThermostats.Add(device);
            await _context.SaveChangesAsync();
            return Ok(device);
        }
        
    }
}
