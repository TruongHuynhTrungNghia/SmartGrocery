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
    [RoutePrefix("api/v{api-version:apiVersion}/transactions")]
    public class TransactionsController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public TransactionsController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route]
        [ResponseType(typeof(TransactionDto))]
        public IHttpActionResult GetTransactions(CancellationToken cancellation)
        {
            var request = new GetAllTransactionsQuery();
            var response = mediator.Send(request, cancellation);

            return Ok(response);
        }

        [HttpPost]
        [Route]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> CreateTransaction(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<CreateTransactionCommand>(request);
                var response = await mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(TransactionContract))]
        public IHttpActionResult GetTransactionyById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTransactionByIdQuery
            {
                Id = id
            };

            var response = mediator.Send(query, cancellationToken);
            var contract = mapper.Map<TransactionContract>(response);

            return Ok(contract);
        }

        [HttpPut]
        [Route]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Update(EditTransactionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<UpdateTransactionCommand>(request);

                var response = await mediator.Send(command, cancellationToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}