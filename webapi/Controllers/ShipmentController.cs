using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Responses;
using WebApi.Dto.Shipment;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ShipmentController : GenericItemControllerBase<Shipment, ShipmentDto, CreateShipmentRequest>
    {
        public ShipmentController(ICompanyService companyService, IMapper mapper) : base(companyService, mapper, companyService.ShipmentHelper)
        {
        }

        [HttpGet("InWarehouse/{warehouseId}")]
        public IActionResult GetShipmentsByWarehouse(Guid warehouseId)
        {
            var shipments = companyService.ShipmentHelper.GetAllByWarehouse(warehouseId);
            var dtos = mapper.Map<IEnumerable<ShipmentDto>>(shipments);
            return Ok(dtos);
        }

        protected override bool AreReferencedItemsMissing(CreateShipmentRequest request, out ItemsNotExistsResponse response)
        {
            var fieldsWithMissing = new List<string>();
            var itemsMissing = false;

            if (!companyService.WarehouseHelper.ItemExists(request.WarehouseId))
            {
                itemsMissing = true;
                fieldsWithMissing.Add(nameof(request.WarehouseId));
            }

            if (!companyService.ProductHelper.ItemExists(request.ProductId))
            {
                itemsMissing = true;
                fieldsWithMissing.Add(nameof(request.ProductId));
            }

            response = new ItemsNotExistsResponse { FieldsReferringToMissingItems = fieldsWithMissing };
            return itemsMissing;
        }

        protected override bool CreateRequestIsValid(CreateShipmentRequest request)
        {
            return request.Amount > 0;
        }
    }
}
