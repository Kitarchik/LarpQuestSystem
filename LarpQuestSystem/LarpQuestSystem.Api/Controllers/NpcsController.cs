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
        public async Task<ActionResult<NpcInfoView>> Get(int id)
        {
            Npc npc = await _db.Npcs
                .Include(x => x.Location)
                .Include(x => x.StartingQuests)
                .Include(x => x.EndingQuests)
                .Include(x => x.ItemsOnStart)
                .ThenInclude(x => x.Item)
                .Include(x => x.ItemsOnStart)
                .ThenInclude(x => x.Quest)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (npc == null)
                return NotFound();

            var npcInfo = new NpcInfoView
            {
                Npc = new Npc
                {
                    Description = npc.Description,
                    Id = npc.Id,
                    Name = npc.Name,
                },
                Location = new Location
                {
                    Description = npc.Location.Description,
                    Id = npc.Location.Id,
                    Name = npc.Location.Name,
                },
                StartingQuests = npc.StartingQuests.Select(q => new Quest
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
                EndingQuests = npc.EndingQuests.Select(q => new Quest
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
                QuestItems = npc.ItemsOnStart.Select(x => new QuestItem
                {
                    AmountNeeded = x.AmountNeeded,
                    Id = x.Id,
                    IsReady = x.IsReady,
                    IsTechnicalDocumentReady = x.IsTechnicalDocumentReady,
                    TechnicalDocumentForNpc = x.TechnicalDocumentForNpc,
                    Quest = new Quest
                    {
                        Id = x.QuestId,
                        Name = x.Quest.Name,
                    },
                    Item = new Item
                    {
                        Id = x.ItemId,
                        Name = x.Item.Name,
                    },
                    StartingNpc = new Npc
                    {
                        Id=x.StartingNpcId,
                        Name=x.StartingNpc.Name,
                    },
                }).ToList()
            };

            return new ObjectResult(npcInfo);
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