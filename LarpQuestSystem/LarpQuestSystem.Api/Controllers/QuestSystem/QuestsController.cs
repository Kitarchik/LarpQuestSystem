using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model.QuestSystem;
using LarpQuestSystem.Data.Model.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LarpQuestSystem.Api.Controllers.QuestSystem
{
    [Authorize(Policy = Policies.IsScriptWriter)]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        readonly LarpSystemContext _db;

        public QuestsController(LarpSystemContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> Get()
        {
            return await _db.Quests.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestInfoView>> Get(int id)
        {
            Quest quest = await _db.Quests
                .Include(x => x.QuestGiver)
                .Include(x => x.QuestEnding)
                .Include(x => x.QuestChains)
                .ThenInclude(x => x.Chain)
                .Include(x => x.QuestPlayers)
                .ThenInclude(x => x.Player)
                .Include(x => x.QuestItems)
                .ThenInclude(x => x.Item)
                .Include(x => x.QuestItems)
                .ThenInclude(x => x.StartingNpc)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (quest == null)
                return NotFound();

            var questView = new QuestInfoView
            {
                Quest = new Quest
                {
                    AmountToPrint = quest.AmountToPrint,
                    ArtisticTextLink = quest.ArtisticTextLink,
                    Complexity = quest.Complexity,
                    Description = quest.Description,
                    Grade = quest.Grade,
                    Id = quest.Id,
                    IsArtisticTextReady = quest.IsArtisticTextReady,
                    Name = quest.Name,
                    TechnicalDescriptionLink = quest.TechnicalDescriptionLink,
                    IsTechnicalDescriptionReady = quest.IsTechnicalDescriptionReady,
                    IsPrinted = quest.IsPrinted,
                    QuestEndingId = quest.QuestEndingId,
                    QuestGiverId = quest.QuestGiverId,
                },
                QuestGiver = new Npc
                {
                    Description = quest.QuestGiver.Description,
                    Id = quest.QuestGiver.Id,
                    Name = quest.QuestGiver.Name,
                },
                QuestEnding = new Npc
                {
                    Description = quest.QuestEnding.Description,
                    Id = quest.QuestEnding.Id,
                    Name = quest.QuestEnding.Name,
                },
                Chains = quest.QuestChains.Select(x => x.Chain).Select(c => new Chain
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                }).ToList(),
                Players = quest.QuestPlayers.Select(x => x.Player).Select(p => new Player
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                }).ToList(),
                QuestItems = quest.QuestItems.Select(x => new QuestItem
                {
                    AmountNeeded = x.AmountNeeded,
                    Id = x.Id,
                    IsReady = x.IsReady,
                    IsTechnicalDocumentReady = x.IsTechnicalDocumentReady,
                    TechnicalDocumentForNpc = x.TechnicalDocumentForNpc,
                    StartingNpc = new Npc
                    {
                        Id = x.StartingNpcId,
                        Name = x.StartingNpc.Name,
                    },
                    Item = new Item
                    {
                        Id = x.ItemId,
                        Name = x.Item.Name,
                    },
                    Quest = new Quest
                    {
                        Id=x.QuestId,
                        Name=x.Quest.Name,
                    },
                }).ToList(),
            };

            return new ObjectResult(questView);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpPost]
        public async Task<ActionResult<Quest>> Post(Quest quest)
        {
            if (quest == null)
            {
                return BadRequest();
            }

            if (_db.Quests.Any(x => x.Name == quest.Name))
            {
                return Conflict();
            }

            _db.Quests.Add(quest);
            await _db.SaveChangesAsync();
            return Ok(quest);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpPut]
        public async Task<ActionResult<Quest>> Put(Quest quest)
        {
            if (quest == null)
            {
                return BadRequest();
            }
            if (!_db.Quests.Any(x => x.Id == quest.Id))
            {
                return NotFound();
            }

            _db.Update(quest);
            await _db.SaveChangesAsync();
            return Ok(quest);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Quest>> Delete(int id)
        {
            Quest quest = _db.Quests.FirstOrDefault(x => x.Id == id);
            if (quest == null)
            {
                return NotFound();
            }
            _db.Quests.Remove(quest);
            await _db.SaveChangesAsync();
            return Ok(quest);
        }
    }
}