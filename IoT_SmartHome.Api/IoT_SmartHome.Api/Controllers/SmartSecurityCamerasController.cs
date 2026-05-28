using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoT_SmartHome.Api.Data;   
using IoT_SmartHome.Api.Models;

namespace IoT_SmartHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/smartsecuritycameras
    public class SmartSecurityCamerasController : ControllerBase
    {
        private readonly HomeDbContext _context;
        public SmartSecurityCamerasController(HomeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartSecurityCamera>>> Get()
        { 
            return await _context.SmartSecurityCameras.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SmartSecurityCamera device)
        {
            device.LastUpdated = DateTime.UtcNow;
            _context.SmartSecurityCameras.Add(device);
            await _context.SaveChangesAsync();
            return Ok(device);
        }
    }
}
