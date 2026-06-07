using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;

namespace IoT_SmartHome.Api.Controllers
{
    [Route("api/[controller]")] // api/smartlocks
    [ApiController]
    public class SmartLocksController : Controller
    {
        private readonly HomeDbContext _context;

        public SmartLocksController(HomeDbContext context)
        {
            _context = context;
        }

        // GET api/smartlocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartLock>>> Get()
        {
            return await _context.SmartLocks.ToListAsync();
        }

        // POST api/smartlocks
        [HttpPost]
        public async Task<ActionResult<SmartLock>> Post([FromBody] SmartLock device)
        {
            device.LastUpdated = DateTime.UtcNow;

            _context.SmartLocks.Add(device);

            await _context.SaveChangesAsync();
            return Ok(device);
        }
    }
}
