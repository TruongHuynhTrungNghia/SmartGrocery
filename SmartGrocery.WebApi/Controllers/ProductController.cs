
using AutoMapper;
using MediatR;
using Microsoft.Web.Http;
using SmartGrocery.UseCase.Product;
using SmartGrocery.WebApi.Contracts.BaseProduct;
using System;
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
        [Route("{id}")]
        [ResponseType(typeof(ProductContract))]
        public async Task<IHttpActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetBaseproductByIdQuery
            {
                ProductId = id
            };

            var productDto = await mediator.Send(request, cancellationToken);

            if (productDto != null)
            {
                var contract = mapper.Map<ProductContract>(productDto);
                return Ok(contract);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route]
        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<CreateBaseProductCommand>(request);
                var response = await mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (DuplicateProductException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
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
            catch(NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route]
        public IHttpActionResult DeleteProduct(string productNumber, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteProductCommand
                {
                    ProductNumber = productNumber
                };

                var response = mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
        }
    }
}