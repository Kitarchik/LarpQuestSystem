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
    public class QuestItemsController : ControllerBase
    {
        readonly QuestContext _db;

        public QuestItemsController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestItem>>> Get()
        {
            return await _db.QuestItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestItem>> Get(int id)
        {
            QuestItem item = await _db.QuestItems
                .Include(x => x.Item)
                .Include(x => x.Quest)
                .Include(x => x.StartingNpc)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [Authorize(Policy = Policies.IsAdmin)]
        [HttpPost]
        public async Task<ActionResult<QuestItem>> Post(QuestItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var freeAmount = _db.Items.AsNoTracking().First(x => x.Id == item.ItemId).AmountReady - _db.QuestItems.AsNoTracking().Where(x => x.ItemId == item.ItemId && x.IsReady).Sum(x => x.AmountNeeded);
            if (freeAmount > item.AmountNeeded)
            {
                item.IsReady = true;
            }
            _db.QuestItems.Add(item);

            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [Authorize(Policy = Policies.IsAdmin)]
        [HttpPut]
        public async Task<ActionResult<QuestItem>> Put(QuestItem questItem)
        {
            if (questItem == null)
            {
                return BadRequest();
            }
            if (!_db.QuestItems.Any(x => x.Id == questItem.Id))
            {
                return NotFound();
            }

            var itemId = _db.QuestItems.AsNoTracking().First(x => x.Id == questItem.Id).ItemId;
            var amountReady = _db.Items.AsNoTracking().First(x => x.Id == itemId).AmountReady;
            var amountReserved = _db.QuestItems
                .AsNoTracking()
                .Where(x => x.ItemId == itemId && x.Id != questItem.Id && x.IsReady)
                .Sum(x => x.AmountNeeded);
            questItem.IsReady = amountReady - amountReserved >= questItem.AmountNeeded;
            questItem.Quest = null;
            questItem.StartingNpc = null;
            questItem.Item = null;

            _db.Update(questItem);
            await _db.SaveChangesAsync();
            return Ok(questItem);
        }

        [Authorize(Policy = Policies.IsAdmin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestItem>> Delete(int id)
        {
            QuestItem item = _db.QuestItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _db.QuestItems.Remove(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }
    }
}