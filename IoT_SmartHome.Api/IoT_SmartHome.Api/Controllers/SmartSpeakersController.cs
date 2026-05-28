using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;    
using IoT_SmartHome.Api.Data;   
using IoT_SmartHome.Api.Models; 

namespace IoT_SmartHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/smartspeakers
    public class SmartSpeakersController : ControllerBase
    {
        private readonly HomeDbContext _context;
        public SmartSpeakersController(HomeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartSpeaker>>> Get()
        { 
            return await _context.SmartSpeakers.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SmartSpeaker device)
        {
            device.LastUpdated = DateTime.UtcNow;
            _context.SmartSpeakers.Add(device);
            await _context.SaveChangesAsync();
            return Ok(device);
        }

    }
}
