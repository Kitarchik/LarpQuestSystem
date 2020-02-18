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
    public class ChainsController : ControllerBase
    {
        readonly LarpSystemContext _db;

        public ChainsController(LarpSystemContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chain>>> Get()
        {
            return await _db.Chains.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChainInfoView>> Get(int id)
        {
            Chain chain = await _db.Chains
                .Include(x => x.QuestChains)
                .ThenInclude(x => x.Quest)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (chain == null)
                return NotFound();

            var chainInfo = new ChainInfoView
            {
                Chain = new Chain
                {
                    Description = chain.Description,
                    Id = chain.Id,
                    Name = chain.Name,
                },
                QuestChains = chain.QuestChains.Select(x => new QuestChain
                {
                    ChainId = x.ChainId,
                    Id = x.Id,
                    QuestId = x.QuestId,
                    StepNumber = x.StepNumber,
                    Quest = new Quest
                    {
                        Id = x.QuestId,
                        Name = x.Quest.Name,
                    },
                }).ToList(),
            };
            return new ObjectResult(chainInfo);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
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

        [Authorize(Policy = Policies.IsScriptManager)]
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

        [Authorize(Policy = Policies.IsScriptManager)]
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