using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using SmartGrocery.UseCase.Customer;
using Microsoft.Web.Http;

namespace SmartGrocery.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVersion}/customers")]
    public class CustomersController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomersController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetCustomerId (Guid id, CancellationToken cancellationToken)
        {
            var customerDetails = new GetCustomerByIdQuery()
            {
                CustomerId = id
            };

            var response = new CustomerDetailsDto();

            try
            {
                response = await mediator.Send(customerDetails, cancellationToken);
            }
            catch (Exception ex)
            {

            }

            return Ok(response);
        }
    }
}