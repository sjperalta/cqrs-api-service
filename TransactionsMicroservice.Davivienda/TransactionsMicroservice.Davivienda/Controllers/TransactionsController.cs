
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionsMicroservice.Davivienda.Models;
using TransactionsMicroservice.Davivienda.Features.Transactions;
using TransactionsMicroservice.Davivienda.Features.Transactions.Dto;

namespace TransactionsMicroservice.Davivienda.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Transaction>>> GetTransactions()
        {
            var query = new GetTransactionQuery();
            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTransaction(CreateTransactionCommand command)
        {
            var transactionId = await _mediator.Send(command);
            return Ok(transactionId);
        }
    }
}