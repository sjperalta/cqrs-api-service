using MediatR;
using TransactionsMicroservice.Davivienda.Models;
using TransactionsMicroservice.Davivienda.Features.Transactions.Dto;
using Microsoft.EntityFrameworkCore;

namespace TransactionsMicroservice.Davivienda.Features.Transactions
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, List<Transaction>>
    {
        private readonly BankDbContext _context;
        public GetTransactionQueryHandler(BankDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _context.Transactions.ToListAsync(cancellationToken);
        }
    }
}
