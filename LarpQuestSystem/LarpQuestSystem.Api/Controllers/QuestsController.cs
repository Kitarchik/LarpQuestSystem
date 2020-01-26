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
    public class QuestsController : ControllerBase
    {
        readonly QuestContext _db;

        public QuestsController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> Get()
        {
            return await _db.Quests.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> Get(int id)
        {
            Quest quest = await _db.Quests.FirstOrDefaultAsync(x => x.Id == id);
            if (quest == null)
                return NotFound();
            return new ObjectResult(quest);
        }

        [HttpGet("forNpc/{id}")]
        public async Task<ActionResult<IEnumerable<Quest>>> GetQuestsForNpc(int id)
        {
            return await _db.Quests.Where(x=>x.QuestEndingId==id || x.QuestGiverId==id).ToListAsync();
        }

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