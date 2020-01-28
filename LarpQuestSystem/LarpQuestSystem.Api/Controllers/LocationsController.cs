using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarpQuestSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        readonly QuestContext _db;

        public LocationsController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> Get()
        {
            return await _db.Locations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> Get(int id)
        {
            Location location = await _db.Locations
                .Include(x => x.Npcs)
                .Include(x=>x.Players)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (location == null)
                return NotFound();
            return new ObjectResult(location);
        }

        [HttpPost]
        public async Task<ActionResult<Location>> Post(Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }

            if (_db.Locations.Any(x => x.Name == location.Name))
            {
                return Conflict();
            }

            _db.Locations.Add(location);
            await _db.SaveChangesAsync();
            return Ok(location);
        }

        [HttpPut]
        public async Task<ActionResult<Location>> Put(Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }
            if (!_db.Locations.Any(x => x.Id == location.Id))
            {
                return NotFound();
            }

            _db.Update(location);
            await _db.SaveChangesAsync();
            return Ok(location);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> Delete(int id)
        {
            Location location = _db.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            _db.Locations.Remove(location);
            await _db.SaveChangesAsync();
            return Ok(location);
        }
    }
}