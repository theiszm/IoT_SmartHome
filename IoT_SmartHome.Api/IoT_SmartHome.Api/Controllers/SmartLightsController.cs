using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;

namespace IoT_SmartHome.Api.Controllers
{
    [Route("api/[controller]")] // api/smartlights
    [ApiController]
    public class SmartLightsController : ControllerBase
    {
        private readonly HomeDbContext _context;

        // Dependency injection of the HomeDbContext to access the database
        public SmartLightsController(HomeDbContext context)
        {
            _context = context;
        }

        // GET api/smartlights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartLight>>> GetLights()
        {
            return await _context.SmartLights.ToListAsync();
        }

        // POST api/smartlights
        [HttpPost]
        public async Task<ActionResult<SmartLight>> PostLight([FromBody] SmartLight light)
        {
            // Add light to the memory HomeDbContext
            _context.SmartLights.Add(light);

            // Save changes to the actual SQLite file
            await _context.SaveChangesAsync();

            // Return the object back to the user to confirm it was created,
            // along with a 201 Created status code.
            return CreatedAtAction(nameof(GetLights), new { id = light.Id }, light);

        }

    }
}
