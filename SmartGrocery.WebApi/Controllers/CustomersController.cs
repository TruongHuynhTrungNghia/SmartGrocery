using AutoMapper;
using MediatR;
using Microsoft.Web.Http;
using SmartGrocery.UseCase.Customer;
using System;
using System.Threading;
using System.Web.Http;

namespace SmartGrocery.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVersion}/customers")]
    public class CustomersController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CustomersController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("id")]
        public IHttpActionResult GetAllCustomer(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetCustomerByIdQuery
            {
                CustomerId = id
            };

            var response = mediator.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}