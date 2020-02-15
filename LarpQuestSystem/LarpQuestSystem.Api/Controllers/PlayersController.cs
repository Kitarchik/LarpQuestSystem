using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LarpQuestSystem.Data.Model.QuestSystem;
using LarpQuestSystem.Data.Model.Security;
using Microsoft.AspNetCore.Authorization;

namespace LarpQuestSystem.Api.Controllers
{
    [Authorize(Policy = Policies.IsScriptWriter)]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerInfoView>> Get(int id)
        {
            var player = await _db.Players
                    .Include(x => x.Location)
                    .Include(x => x.QuestPlayers)
                    .ThenInclude(x => x.Quest)
                    .FirstOrDefaultAsync(x => x.Id == id);
            if (player == null)
                return NotFound();
            var playerInfo = new PlayerInfoView
            {
                Player = new Player
                {
                    Id = player.Id,
                    Name = player.Name,
                    Description = player.Description,
                    LocationId = player.LocationId,
                    NumberOfPayedAccounts = player.NumberOfPayedAccounts
                },
                Location = new Location
                {
                    Description = player.Location.Description,
                    Id = player.Location.Id,
                    Name = player.Location.Name,
                },
                Quests = player.QuestPlayers.Select(x => x.Quest).Select(q => new Quest
                {
                    AmountToPrint = q.AmountToPrint,
                    ArtisticTextLink = q.ArtisticTextLink,
                    Complexity = q.Complexity,
                    Description = q.Description,
                    Grade = q.Grade,
                    Id = q.Id,
                    IsArtisticTextReady = q.IsArtisticTextReady,
                    Name = q.Name,
                    TechnicalDescriptionLink = q.TechnicalDescriptionLink,
                    IsTechnicalDescriptionReady = q.IsTechnicalDescriptionReady,
                    IsPrinted = q.IsPrinted,
                }).ToList(),
            };
            return new ObjectResult(playerInfo);
        }

        [Authorize(Policy = Policies.IsAdmin)]
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

        [Authorize(Policy = Policies.IsAdmin)]
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

        [Authorize(Policy = Policies.IsAdmin)]
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