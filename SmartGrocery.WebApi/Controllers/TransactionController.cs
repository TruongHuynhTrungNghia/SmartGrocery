using AutoMapper;
using MediatR;
using Microsoft.Web.Http;
using SmartGrocery.UseCase.Transactions;
using SmartGrocery.WebApi.Contracts.Transaction;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SmartGrocery.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVerison}/transactions")]
    public class TransactionController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public TransactionController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpPost]
        [Route]
        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> CreateTransaction(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<CreateTransactionCommand>(request);
                var response = await mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}