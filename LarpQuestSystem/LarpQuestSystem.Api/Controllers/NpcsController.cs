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
    public class NpcsController : ControllerBase
    {
        readonly QuestContext _db;

        public NpcsController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Npc>>> Get()
        {
            return await _db.Npcs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Npc>> Get(int id)
        {
            Npc npc = await _db.Npcs.FirstOrDefaultAsync(x => x.Id == id);
            if (npc == null)
                return NotFound();
            return new ObjectResult(npc);
        }

        [HttpPost]
        public async Task<ActionResult<Npc>> Post(Npc npc)
        {
            if (npc == null)
            {
                return BadRequest();
            }

            if (_db.Npcs.Any(x => x.Name == npc.Name))
            {
                return Conflict();
            }

            _db.Npcs.Add(npc);
            await _db.SaveChangesAsync();
            return Ok(npc);
        }

        [HttpPut]
        public async Task<ActionResult<Npc>> Put(Npc npc)
        {
            if (npc == null)
            {
                return BadRequest();
            }
            if (!_db.Npcs.Any(x => x.Id == npc.Id))
            {
                return NotFound();
            }

            _db.Update(npc);
            await _db.SaveChangesAsync();
            return Ok(npc);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Npc>> Delete(int id)
        {
            Npc npc = _db.Npcs.FirstOrDefault(x => x.Id == id);
            if (npc == null)
            {
                return NotFound();
            }
            _db.Npcs.Remove(npc);
            await _db.SaveChangesAsync();
            return Ok(npc);
        }
    }
}