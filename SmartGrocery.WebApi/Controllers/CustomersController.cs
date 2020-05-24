using AutoMapper;
using MediatR;
using Microsoft.Web.Http;
using SmartGrocery.UseCase.Customer;
using SmartGrocery.WebApi.Contracts.Customer;
using System;
using System.Threading;
using System.Threading.Tasks;
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
        [Route("{customerNumber}")]
        public IHttpActionResult GetCustomerById(string customerNumber, CancellationToken cancellationToken)
        {
            var request = new GetCustomerByIdQuery
            {
                CustomerId = customerNumber
            };

            var response = mediator.Send(request, cancellationToken);

            var contract = mapper.Map<CustomerDetailsContract>(response.Result);

            return Ok(contract);
        }

        [HttpPost]
        [Route]
        public IHttpActionResult Create(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<CreateCustomerCommand>(request);

                var response = mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (DuplicateCustomerException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route]
        public IHttpActionResult Edit(EditCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<EditCustomerCommand>(request);
                var response = mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route]
        public IHttpActionResult GetAllCustomers(CancellationToken cancellationToken)
        {
            var query = new GetAllCustomerQuery();
            var response = mediator.Send(query, cancellationToken);
            var contract = mapper.Map<CustomerContract[]>(response.Result);

            return Ok(contract);
        }

        [HttpGet]
        [Route("lists/{searchTerm}")]
        public async Task<IHttpActionResult> SearchCustomer(string searchTerm, CancellationToken cancellationToken)
        {
            var query = new SearchCustomerQuery
            {
                SearchTerm = searchTerm
            };

            var response = await mediator.Send(query, cancellationToken);
            var contract = mapper.Map<CustomerContract[]>(response);

            return Ok(contract);
        }
    }
}