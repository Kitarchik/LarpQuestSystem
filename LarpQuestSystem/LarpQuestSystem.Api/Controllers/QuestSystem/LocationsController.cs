using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model.MaterialManagement;
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
    public class LocationsController : ControllerBase
    {
        readonly LarpSystemContext _db;

        public LocationsController(LarpSystemContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> Get()
        {
            return await _db.Locations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationInfoView>> Get(int id)
        {
            Location location = await _db.Locations
                .Include(x => x.Npcs)
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (location == null)
                return NotFound();

            var locationInfo = new LocationInfoView
            {
                Location = new Location
                {
                    Id = location.Id,
                    Description = location.Description,
                    Name = location.Name,
                    RequestsPayed = location.RequestsPayed,
                },
                Npcs = location.Npcs.Select(x => new Npc
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                }).ToList(),
                Players = location.Players.Select(x => new Player
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).ToList(),
            };

            return new ObjectResult(locationInfo);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpPost]
        public async Task<ActionResult<Location>> Post(Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }

            if (_db.Locations.Any(x => x.Name == location.Name))
            {
                return Conflict();
            }

            _db.Locations.Add(location);
            await _db.SaveChangesAsync();
            if (location.RequestsPayed > 0)
            {
                AddMaterialsRequest(location);
            }
            return Ok(location);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpPut]
        public async Task<ActionResult<Location>> Put(Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }
            if (!_db.Locations.Any(x => x.Id == location.Id))
            {
                return NotFound();
            }

            var oldLocation = _db.Locations.AsNoTracking().First(x => x.Id == location.Id);
            if (oldLocation.RequestsPayed == 0 && location.RequestsPayed > 0)
            {
                AddMaterialsRequest(location);
            }

            if (oldLocation.RequestsPayed > 0 && location.RequestsPayed == 0)
            {
                DeleteMaterialsRequest(location);
            }

            _db.Update(location);
            await _db.SaveChangesAsync();

            return Ok(location);
        }

        [Authorize(Policy = Policies.IsScriptManager)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> Delete(int id)
        {
            Location location = _db.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            _db.Locations.Remove(location);
            await _db.SaveChangesAsync();
            return Ok(location);
        }

        private void AddMaterialsRequest(Location location)
        {
            var materialsRequest = new MaterialsRequest
            {
                Customer = "За взносы",
                IsPayed = true,
                LocationId = location.Id
            };
            _db.MaterialsRequests.Add(materialsRequest);
            _db.SaveChanges();
            var materialsForPayedRequests = _db.Materials.Where(x => x.AmountPerPayedRequestInLocation > 0).ToList();
            foreach (var material in materialsForPayedRequests)
            {
                var singleMaterialRequest = new SingleMaterialRequest
                {
                    MaterialId = material.Id,
                    MaterialsRequestId = materialsRequest.Id,
                    QuantityOrdered = location.RequestsPayed * material.AmountPerPayedRequestInLocation,
                    QuantityServed = 0,
                };
                _db.SingleMaterialRequests.Add(singleMaterialRequest);
                _db.SaveChanges();
            }
        }

        private void DeleteMaterialsRequest(Location location)
        {
            var materialsRequest = _db.MaterialsRequests
                .First(x => x.IsPayed &&
                            x.Customer == "За взносы" &&
                            x.LocationId == location.Id);

            foreach (var singleRequest in materialsRequest.SingleMaterialRequests)
            {

                _db.SingleMaterialRequests.Remove(singleRequest);
                _db.SaveChanges();
            }

            _db.MaterialsRequests.Remove(materialsRequest);
        }
    }
}