using MediatR;
using TransactionsMicroservice.Davivienda.Models;

namespace TransactionsMicroservice.Davivienda.Features.Transactions
{
   public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
   {
        private readonly BankDbContext _context;
        public CreateTransactionCommandHandler(BankDbContext context){
            _context = context;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = new Transaction
                {
                    Description = request.Description,
                    Amount = request.Amount,
                    TransactionType = request.TransactionType
                };
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync(cancellationToken);

                return transaction.TransactionId;
            }
            catch(Exception)
            {
                throw;
            }
        }
   }
}