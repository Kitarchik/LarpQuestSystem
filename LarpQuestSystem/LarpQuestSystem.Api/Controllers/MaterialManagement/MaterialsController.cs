using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model.MaterialManagement;
using LarpQuestSystem.Data.Model.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarpQuestSystem.Api.Controllers.MaterialManagement
{
    [Authorize(Policy = Policies.IsMaterialsManager)]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        readonly LarpSystemContext _db;

        public MaterialsController(LarpSystemContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialWithTotalView>>> Get()
        {
            var materials = await _db.Materials.ToListAsync();
            return materials.Select(x => new MaterialWithTotalView
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                UnitOfMeasure = x.UnitOfMeasure,
                TotalOrdered = _db.SingleMaterialRequests
                    .Where(x1 => x1.MaterialId == x.Id)
                    .Sum(x1 => x1.QuantityOrdered),
                TotalServed = _db.SingleMaterialRequests
                    .Where(x1 => x1.MaterialId == x.Id)
                    .Sum(x1 => x1.QuantityServed),
                TotalPayed = _db.SingleMaterialRequests
                    .Where(x1 => x1.MaterialId == x.Id && x1.MaterialsRequest.IsPayed)
                    .Sum(x1 => x1.QuantityOrdered),
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialInfoView>> Get(int id)
        {
            Material material = await _db.Materials
                .Include(x => x.SingleMaterialRequests)
                .ThenInclude(x => x.MaterialsRequest)
                .ThenInclude(x => x.Location)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (material == null)
                return NotFound();
            var materialInfoView = new MaterialInfoView
            {
                Id = material.Id,
                Name = material.Name,
                UnitOfMeasure = material.UnitOfMeasure,
                Price = material.Price,
                AmountPerPayedRequestInLocation = material.AmountPerPayedRequestInLocation,
                SingleMaterialRequestInfoViews = material.SingleMaterialRequests.Select(x =>
                    new SingleMaterialRequestInfoView
                    {
                        Id = x.Id,
                        QuantityOrdered = x.QuantityOrdered,
                        QuantityServed = x.QuantityServed,
                        Customer = x.MaterialsRequest.Customer,
                        IsPayed = x.MaterialsRequest.IsPayed,
                        LocationName = x.MaterialsRequest.Location.Name,
                        MaterialsRequestId = x.MaterialsRequestId,
                    }).ToList(),
            };
            return new ObjectResult(materialInfoView);
        }

        [HttpPost]
        public async Task<ActionResult<Material>> Post(Material material)
        {
            if (material == null)
            {
                return BadRequest();
            }

            if (_db.Materials.Any(x => x.Name == material.Name))
            {
                return Conflict();
            }

            _db.Materials.Add(material);
            await _db.SaveChangesAsync();
            if (material.AmountPerPayedRequestInLocation > 0)
            {
                AddSingleMaterialRequests(material.Name);
            }
            return Ok(material);
        }

        [HttpPut]
        public async Task<ActionResult<Material>> Put(Material material)
        {
            if (material == null)
            {
                return BadRequest();
            }
            if (!_db.Materials.Any(x => x.Id == material.Id))
            {
                return NotFound();
            }

            var oldMaterial = _db.Materials.AsNoTracking().First(x => x.Id == material.Id);
            if (oldMaterial.AmountPerPayedRequestInLocation == 0 && material.AmountPerPayedRequestInLocation > 0)
            {
                AddSingleMaterialRequests(material.Name);
            }

            if (oldMaterial.AmountPerPayedRequestInLocation > 0 && material.AmountPerPayedRequestInLocation == 0)
            {
                DeleteSingleMaterialRequests(material.Name);
            }

            _db.Update(material);
            await _db.SaveChangesAsync();
            
            return Ok(material);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Material>> Delete(int id)
        {
            Material material = _db.Materials.FirstOrDefault(x => x.Id == id);
            if (material == null)
            {
                return NotFound();
            }
            _db.Materials.Remove(material);
            await _db.SaveChangesAsync();
            return Ok(material);
        }

        private void AddSingleMaterialRequests(string materialName)
        {
            var material = _db.Materials.First(x => x.Name == materialName);

            var locationsWithPlayers = _db.Locations.Where(x => x.RequestsPayed > 0).ToList();
            foreach (var location in locationsWithPlayers)
            {
                var materialRequest = _db.MaterialsRequests
                    .FirstOrDefault(x => x.IsPayed &&
                                         x.LocationId == location.Id &&
                                         x.Customer == "За взносы");
                if (materialRequest != null)
                {
                    var singleMaterialRequest = new SingleMaterialRequest
                    {
                        QuantityOrdered = material.AmountPerPayedRequestInLocation * location.RequestsPayed,
                        QuantityServed = 0,
                        MaterialId = material.Id,
                        MaterialsRequestId = materialRequest.Id,
                    };
                    _db.SingleMaterialRequests.Add(singleMaterialRequest);
                    _db.SaveChanges();
                }
            }
        }

        private void DeleteSingleMaterialRequests(string materialName)
        {
            var material = _db.Materials.First(x => x.Name == materialName);
            var locationsWithPlayers = _db.Locations.Where(x => x.RequestsPayed > 0).ToList();
            foreach (var location in locationsWithPlayers)
            {
                var materialRequest = _db.MaterialsRequests
                    .FirstOrDefault(x => x.IsPayed &&
                                         x.LocationId == location.Id &&
                                         x.Customer == "За взносы");
                if (materialRequest != null)
                {
                    var singleMaterialRequest = materialRequest.SingleMaterialRequests
                        .First(x => x.MaterialId == material.Id);
                    _db.SingleMaterialRequests.Remove(singleMaterialRequest);
                    _db.SaveChanges();
                }
            }
        }
    }
}