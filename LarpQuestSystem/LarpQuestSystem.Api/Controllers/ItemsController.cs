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
    public class ItemsController : ControllerBase
    {
        readonly QuestContext _db;

        public ItemsController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {
            return await _db.Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemInfoView>> Get(int id)
        {
            Item item = await _db.Items
                .Include(x => x.QuestItems)
                .ThenInclude(x => x.Quest)
                .Include(x => x.QuestItems)
                .ThenInclude(x => x.StartingNpc)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            var itemInfo = new ItemInfoView
            {
                Item = new Item
                {
                    AmountReady = item.AmountReady,
                    Description = item.Description,
                    Id = item.Id,
                    ItemType = item.ItemType,
                    Name = item.Name,
                },
                QuestItems = item.QuestItems.Select(x => new QuestItem
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
                    StartingNpc = new Npc
                    {
                        Id = x.StartingNpcId,
                        Name = x.StartingNpc.Name,
                    },
                    Item = new Item
                    {
                        Id=x.ItemId,
                        Name = x.Item.Name,
                    },
                }).ToList(),
            };
            return new ObjectResult(itemInfo);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Post(Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            if (_db.Items.Any(x => x.Name == item.Name))
            {
                return Conflict();
            }

            _db.Items.Add(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<Item>> Put(Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (!_db.Items.Any(x => x.Id == item.Id))
            {
                return NotFound();
            }

            _db.Update(item);
            await _db.SaveChangesAsync();
            await UpdateQuestItemsReadiness(item.Id, item.AmountReady);
            return Ok(item);
        }

        private async Task UpdateQuestItemsReadiness(int itemId, int newAmountReady)
        {
            var questItemsReady = _db.QuestItems.Where(x => x.IsReady && x.ItemId == itemId).OrderBy(x => x.AmountNeeded).ToList();
            var lastAmountReady = questItemsReady.Sum(x => x.AmountNeeded);

            if (newAmountReady > lastAmountReady)
            {
                var questItemsNotReady = _db.QuestItems.Where(x => !x.IsReady && x.ItemId == itemId).OrderBy(x => x.AmountNeeded).ToList();
                var questItemsToUpdate = new List<QuestItem>();
                var amountToDistribute = newAmountReady - lastAmountReady;
                int amountDistributed = 0;
                foreach (var questItem in questItemsNotReady)
                {
                    if (questItem.AmountNeeded + amountDistributed <= amountToDistribute)
                    {
                        questItemsToUpdate.Add(questItem);
                        amountDistributed += questItem.AmountNeeded;
                    }
                }
                foreach (var questItem in questItemsToUpdate)
                {
                    questItem.IsReady = true;
                    _db.Update(questItem);
                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                var questItemsToUpdate = new List<QuestItem>();
                var amountToDistribute = lastAmountReady - newAmountReady;
                int amountDistributed = 0;
                foreach (var questItem in questItemsReady)
                {
                    if (questItem.AmountNeeded <= amountToDistribute - amountDistributed)
                    {
                        questItemsToUpdate.Add(questItem);
                        amountDistributed += questItem.AmountNeeded;
                    }
                }
                foreach (var questItem in questItemsToUpdate)
                {
                    questItem.IsReady = false;
                    _db.Update(questItem);
                    await _db.SaveChangesAsync();
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(int id)
        {
            Item item = _db.Items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _db.Items.Remove(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }
    }
}