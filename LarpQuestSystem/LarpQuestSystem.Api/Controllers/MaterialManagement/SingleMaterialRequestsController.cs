using LarpQuestSystem.Data;
using LarpQuestSystem.Data.Model.MaterialManagement;
using LarpQuestSystem.Data.Model.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LarpQuestSystem.Api.Controllers.MaterialManagement
{
    [Authorize(Policy = Policies.IsMaterialsManager)]
    [Route("api/[controller]")]
    [ApiController]
    public class SingleMaterialRequestsController : ControllerBase
    {
        readonly LarpSystemContext _db;

        public SingleMaterialRequestsController(LarpSystemContext context)
        {
            _db = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SingleMaterialRequestInfoView>> Get(int id)
        {
            SingleMaterialRequest singleRequest = await _db.SingleMaterialRequests
                .Include(x => x.MaterialsRequest)
                .ThenInclude(x => x.Location)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (singleRequest == null)
                return NotFound();

            var singleMaterialRequestInfoView = new SingleMaterialRequestInfoView
            {
                Id = singleRequest.Id,
                Customer = singleRequest.MaterialsRequest.Customer,
                IsPayed = singleRequest.MaterialsRequest.IsPayed,
                LocationName = singleRequest.MaterialsRequest.Location.Name,
                MaterialsRequestId = singleRequest.MaterialsRequestId,
                QuantityOrdered = singleRequest.QuantityOrdered,
                QuantityServed = singleRequest.QuantityServed,
            };
            return new ObjectResult(singleMaterialRequestInfoView);
        }

        [HttpPut]
        public async Task<ActionResult<SingleMaterialRequestInfoView>> Put(SingleMaterialRequestInfoView singleMaterialRequestInfoView)
        {
            if (singleMaterialRequestInfoView == null)
            {
                return BadRequest();
            }
            if (!_db.SingleMaterialRequests.Any(x => x.Id == singleMaterialRequestInfoView.Id))
            {
                return NotFound();
            }
            var singleMaterialRequest = _db.SingleMaterialRequests.First(x => x.Id == singleMaterialRequestInfoView.Id);
            singleMaterialRequest.QuantityServed = singleMaterialRequestInfoView.QuantityServed;
            _db.Update(singleMaterialRequest);
            await _db.SaveChangesAsync();

            return Ok(singleMaterialRequest);
        }
    }
}