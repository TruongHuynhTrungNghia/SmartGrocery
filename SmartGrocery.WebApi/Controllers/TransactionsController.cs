using AutoMapper;
using MediatR;
using Microsoft.Web.Http;
using SmartGrocery.UseCase.Transactions;
using SmartGrocery.WebApi.Contracts.Transaction;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetTransactions(CancellationToken cancellation)
        {
            var request = new GetAllTransactionsQuery();
            var response = mediator.Send(request, cancellation);
            var transactionContract = mapper.Map<Transaction[]>(response.Result);

            return Ok(transactionContract);
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(TransactionDetails))]
        public IHttpActionResult GetTransactionyById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTransactionByIdQuery
            {
                Id = id
            };

            var response = mediator.Send(query, cancellationToken);
            var contract = mapper.Map<TransactionDetails>(response.Result);

            return Ok(contract);
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

        [HttpDelete]
        [Route]
        [ResponseType(typeof(string))]
        public IHttpActionResult Delete (string TransasctionId, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteTransactionCommand
                {
                    TransactionId = TransasctionId
                };

                var respone = mediator.Send(command, cancellationToken);

                return Ok(respone);
            }
            catch (TransactionIsNotExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}