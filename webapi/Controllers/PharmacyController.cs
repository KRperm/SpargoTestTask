using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Pharmacy;
using WebApi.Dto.Responses;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PharmacyController : GenericItemControllerBase<Pharmacy, PharmacyDto, CreatePharmacyRequest>
    {
        public PharmacyController(ICompanyService companyService, IMapper mapper) : base(companyService, mapper, companyService.PharmacyHelper)
        {
        }

        protected override bool AreReferencedItemsMissing(CreatePharmacyRequest request, out ItemsNotExistsResponse response)
        {
            response = new ItemsNotExistsResponse { FieldsReferringToMissingItems = Array.Empty<string>() };
            return false;
        }

        protected override bool CreateRequestIsValid(CreatePharmacyRequest request)
        {
            return true;
        }
    }
}
