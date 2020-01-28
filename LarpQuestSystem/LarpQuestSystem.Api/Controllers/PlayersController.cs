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
    public class PlayersController : ControllerBase
    {
        readonly QuestContext _db;

        public PlayersController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return await _db.Players.ToListAsync();
        }

        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Player>>> GetFull()
        {
            return await _db.Players
                .Include(x => x.Location)
                .Include(x => x.QuestPlayers)
                .ThenInclude(x => x.Quest)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            Player player = await _db.Players
                .Include(x => x.Location)
                .Include(x=>x.QuestPlayers)
                .ThenInclude(x=>x.Quest)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (player == null)
                return NotFound();
            return new ObjectResult(player);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> Post(Player player)
        {
            if (player == null)
            {
                return BadRequest();
            }

            if (_db.Players.Any(x => x.Name == player.Name))
            {
                return Conflict();
            }

            _db.Players.Add(player);
            await _db.SaveChangesAsync();
            return Ok(player);
        }

        [HttpPut]
        public async Task<ActionResult<Player>> Put(Player player)
        {
            if (player == null)
            {
                return BadRequest();
            }
            if (!_db.Players.Any(x => x.Id == player.Id))
            {
                return NotFound();
            }

            _db.Update(player);
            await _db.SaveChangesAsync();
            return Ok(player);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> Delete(int id)
        {
            Player player = _db.Players.FirstOrDefault(x => x.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            _db.Players.Remove(player);
            await _db.SaveChangesAsync();
            return Ok(player);
        }
    }
}