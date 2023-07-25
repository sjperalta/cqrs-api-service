using MediatR;
using TransactionsMicroservice.Davivienda.Models;

namespace TransactionsMicroservice.Davivienda.Features.Transactions.Dto
{
    public class GetTransactionQuery : IRequest<List<Transaction>> 
    {
    }
}
