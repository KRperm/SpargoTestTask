using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Product;
using WebApi.Dto.Responses;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProductController : GenericItemControllerBase<Product, ProductDto, CreateProductRequest>
    {
        public ProductController(ICompanyService companyService, IMapper mapper) : base(companyService, mapper, companyService.ProductHelper)
        {
        }

        [HttpGet("InPharmacy/{pharmacyId}")]
        public IActionResult GetProductsByPharmacy(Guid pharmacyId)
        {
            var stocks = companyService.GetProductsByPharmacy(pharmacyId);
            var dtos = stocks.Select(x => new StockDto
            {
                Id = x.product.Id,
                Name = x.product.Name,
                TotalAmount = x.amount
            });
            return Ok(dtos);
        }

        protected override bool AreReferencedItemsMissing(CreateProductRequest request, out ItemsNotExistsResponse response)
        {
            response = new ItemsNotExistsResponse { FieldsReferringToMissingItems = Array.Empty<string>() };
            return false;
        }

        protected override bool CreateRequestIsValid(CreateProductRequest request)
        {
            return true;
        }
    }
}
