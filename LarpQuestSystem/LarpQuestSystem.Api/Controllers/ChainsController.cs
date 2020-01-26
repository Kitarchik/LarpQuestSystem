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
    public class ChainsController : ControllerBase
    {
        readonly QuestContext _db;

        public ChainsController(QuestContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chain>>> Get()
        {
            return await _db.Chains.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chain>> Get(int id)
        {
            Chain chain = await _db.Chains.FirstOrDefaultAsync(x => x.Id == id);
            if (chain == null)
                return NotFound();
            return new ObjectResult(chain);
        }

        [HttpGet("forQuest/{questId}")]
        public async Task<ActionResult<IEnumerable<QuestChain>>> GetChainsForQuest(int questId)
        {
            return await _db.QuestChains.Where(x => x.QuestId==questId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Chain>> Post(Chain chain)
        {
            if (chain == null)
            {
                return BadRequest();
            }

            if (_db.Chains.Any(x => x.Name == chain.Name))
            {
                return Conflict();
            }

            _db.Chains.Add(chain);
            await _db.SaveChangesAsync();
            return Ok(chain);
        }

        [HttpPut]
        public async Task<ActionResult<Chain>> Put(Chain chain)
        {
            if (chain == null)
            {
                return BadRequest();
            }
            if (!_db.Chains.Any(x => x.Id == chain.Id))
            {
                return NotFound();
            }

            _db.Update(chain);
            await _db.SaveChangesAsync();
            return Ok(chain);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Chain>> Delete(int id)
        {
            Chain chain = _db.Chains.FirstOrDefault(x => x.Id == id);
            if (chain == null)
            {
                return NotFound();
            }
            _db.Chains.Remove(chain);
            await _db.SaveChangesAsync();
            return Ok(chain);
        }
    }
}