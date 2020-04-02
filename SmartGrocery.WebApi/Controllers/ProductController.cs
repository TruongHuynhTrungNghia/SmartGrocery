using AutoMapper;
using MediatR;
using Microsoft.Web.Http;
using SmartGrocery.UseCase.Product;
using SmartGrocery.WebApi.Contracts.BaseProduct;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SmartGrocery.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVersion}/products")]
    public class ProductController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ProductController(
            IMapper mapper,
            IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        [ResponseType(typeof(ProductContract))]
        public async Task<IHttpActionResult> Details(CancellationToken cancellationToken)
        {
            var request = new GetAllProductsQuery();
            var response = await mediator.Send(request, cancellationToken);

            var contract = mapper.Map<IEnumerable<BaseProductDto>>(response);

            return Ok(contract);
        }

        [HttpGet]
        [ResponseType(typeof(ProductContract))]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetBaseproductByIdQuery
            {
                ProductId = id
            };

            var productDto = await mediator.Send(request, cancellationToken);

            var contract = mapper.Map<ProductContract>(productDto);
            return Ok(contract);
        }

        [HttpPost]
        [Route]
        [ResponseType(typeof(Guid))]
        public IHttpActionResult CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<CreateBaseProductCommand>(request);
                var response = mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (DuplicateProductException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> EditProduct(EditProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<EditProductCommnand>(request);
                var response = await mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteProductCommand
                {
                    Id = id
                };

                mediator.Send(command, cancellationToken);

                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}