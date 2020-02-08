using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LarpQuestSystem.Api.Controllers
{
    [Authorize(Policy = Policies.IsScriptWriter)]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestChainsController : ControllerBase
    {
        readonly QuestContext _db;

        public QuestChainsController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestChain>>> Get()
        {
            return await _db.QuestChains.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestChain>> Get(int id)
        {
            QuestChain questChain = await _db.QuestChains.FirstOrDefaultAsync(x => x.Id == id);
            if (questChain == null)
                return NotFound();
            return new ObjectResult(questChain);
        }

        [Authorize(Policy = Policies.IsAdmin)]
        [HttpPost]
        public async Task<ActionResult<QuestChain>> Post(QuestChain questChain)
        {
            if (questChain == null)
            {
                return BadRequest();
            }

            _db.QuestChains.Add(questChain);
            await _db.SaveChangesAsync();
            return Ok(questChain);
        }

        [Authorize(Policy = Policies.IsAdmin)]
        [HttpPut]
        public async Task<ActionResult<QuestChain>> Put(QuestChain questChain)
        {
            if (questChain == null)
            {
                return BadRequest();
            }
            if (!_db.QuestChains.Any(x => x.Id == questChain.Id))
            {
                return NotFound();
            }

            _db.Update(questChain);
            await _db.SaveChangesAsync();
            return Ok(questChain);
        }

        [Authorize(Policy = Policies.IsAdmin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestChain>> Delete(int id)
        {
            QuestChain questChain = _db.QuestChains.FirstOrDefault(x => x.Id == id);
            if (questChain == null)
            {
                return NotFound();
            }
            _db.QuestChains.Remove(questChain);
            await _db.SaveChangesAsync();
            return Ok(questChain);
        }
    }
}