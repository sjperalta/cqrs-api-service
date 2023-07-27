using MediatR;
using Newtonsoft.Json;
using TransactionsMicroservice.Davivienda.Infraestructure;
using TransactionsMicroservice.Davivienda.Models;

namespace TransactionsMicroservice.Davivienda.Features.Transactions
{
   public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
   {
        private readonly BankDbContext _context;
        private readonly IMessageProducer _messageProducer;
        public CreateTransactionCommandHandler(BankDbContext context, IMessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
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

                var message = JsonConvert.SerializeObject(transaction);
                _messageProducer.Produce(message);

                return transaction.TransactionId;
            }
            catch(Exception)
            {
                throw;
            }
        }
   }
}