using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LarpQuestSystem.Api.Controllers
{
    [Authorize(Policy = Policies.IsScriptWriter)]
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
        public async Task<ActionResult<LocationInfoView>> Get(int id)
        {
            Location location = await _db.Locations
                .Include(x => x.Npcs)
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (location == null)
                return NotFound();

            var locationInfo = new LocationInfoView
            {
                Location = new Location
                {
                    Id = location.Id,
                    Description = location.Description,
                    Name = location.Name,
                },
                Npcs = location.Npcs.Select(x => new Npc
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                }).ToList(),
                Players = location.Players.Select(x => new Player
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).ToList(),
            };

            return new ObjectResult(locationInfo);
        }

        [Authorize(Policy = Policies.IsAdmin)]
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

        [Authorize(Policy = Policies.IsAdmin)]
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

        [Authorize(Policy = Policies.IsAdmin)]
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