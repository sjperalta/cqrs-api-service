using MediatR;

namespace TransactionsMicroservice.Davivienda.Features.Transactions
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public int TransactionType { get; set; }
    }
}