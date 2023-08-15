using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Responses;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    public abstract class GenericItemControllerBase<TEntity, TDto, TCreateRequest> : ControllerBase where TEntity : class, IIdentifiable
    {
        protected readonly ICompanyService companyService;
        protected readonly IMapper mapper;
        protected readonly GenericItemHelperBase<TEntity> helper;

        protected GenericItemControllerBase(ICompanyService companyService, IMapper mapper, GenericItemHelperBase<TEntity> helper)
        {
            this.companyService = companyService;
            this.mapper = mapper;
            this.helper = helper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = helper.GetAll();
            var dtos = mapper.Map<IEnumerable<TDto>>(items);
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create(TCreateRequest request)
        {
            if (!CreateRequestIsValid(request))
                return UnprocessableEntity();
            if (AreReferencedItemsMissing(request, out var notExistsResponse))
                return UnprocessableEntity(notExistsResponse);
            var newItem = mapper.Map<TEntity>(request);
            newItem.Id = Guid.NewGuid();
            helper.Create(newItem);
            var response = new CreateResponse { IdOfCreatedObject = newItem.Id };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = helper.Delete(id);
            var response = new DeleteResponse { IsDeleted = isDeleted };
            return Ok(response);
        }

        protected abstract bool AreReferencedItemsMissing(TCreateRequest request, out ItemsNotExistsResponse response);
        protected abstract bool CreateRequestIsValid(TCreateRequest request);
    }
}
