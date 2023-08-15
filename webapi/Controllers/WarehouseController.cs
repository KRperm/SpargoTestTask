using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Responses;
using WebApi.Dto.Warehouse;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class WarehouseController : GenericItemControllerBase<Warehouse, WarehouseDto, CreateWarehouseRequest>
    {
        public WarehouseController(ICompanyService companyService, IMapper mapper) : base(companyService, mapper, companyService.WarehouseHelper)
        {
        }

        protected override bool AreReferencedItemsMissing(CreateWarehouseRequest request, out ItemsNotExistsResponse response)
        {
            if (companyService.PharmacyHelper.ItemExists(request.PharmacyId))
            {
                response = new ItemsNotExistsResponse { FieldsReferringToMissingItems = Array.Empty<string>() };
                return false;
            }

            response = new ItemsNotExistsResponse { FieldsReferringToMissingItems = new[] { nameof(request.PharmacyId) } };
            return true;
        }

        protected override bool CreateRequestIsValid(CreateWarehouseRequest request)
        {
            return true;
        }
    }
}
