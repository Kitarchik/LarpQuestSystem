﻿using System.Collections.Generic;
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
    public class QuestPlayersController : ControllerBase
    {
        readonly LarpSystemContext _db;

        public QuestPlayersController(LarpSystemContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestPlayer>>> Get()
        {
            return await _db.QuestPlayers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestPlayer>> Get(int id)
        {
            QuestPlayer questPlayer = await _db.QuestPlayers.FirstOrDefaultAsync(x => x.Id == id);
            if (questPlayer == null)
                return NotFound();
            return new ObjectResult(questPlayer);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpPost]
        public async Task<ActionResult<QuestPlayer>> Post(QuestPlayer questPlayer)
        {
            if (questPlayer == null)
            {
                return BadRequest();
            }

            _db.QuestPlayers.Add(questPlayer);
            await _db.SaveChangesAsync();
            return Ok(questPlayer);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpPut]
        public async Task<ActionResult<QuestPlayer>> Put(QuestPlayer questPlayer)
        {
            if (questPlayer == null)
            {
                return BadRequest();
            }
            if (!_db.QuestPlayers.Any(x => x.Id == questPlayer.Id))
            {
                return NotFound();
            }

            _db.Update(questPlayer);
            await _db.SaveChangesAsync();
            return Ok(questPlayer);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestPlayer>> Delete(int id)
        {
            QuestPlayer questPlayer = _db.QuestPlayers.FirstOrDefault(x => x.Id == id);
            if (questPlayer == null)
            {
                return NotFound();
            }
            _db.QuestPlayers.Remove(questPlayer);
            await _db.SaveChangesAsync();
            return Ok(questPlayer);
        }
    }
}