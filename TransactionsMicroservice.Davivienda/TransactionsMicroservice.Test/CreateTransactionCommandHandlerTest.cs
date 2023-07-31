using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TransactionsMicroservice.Davivienda;
using TransactionsMicroservice.Davivienda.Features.Transactions;
using TransactionsMicroservice.Davivienda.Infrastructure;

namespace TransactionsMicroservice.Test
{
    public class CreateTransactionCommandHandlerTest
    {
        private IMessageProducer _messageProducer;
        private BankDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BankDbContext(options);
        }

        public CreateTransactionCommandHandlerTest(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsTransactionId()
        {
            // Arrange
            var context = CreateDbContext();
            var handler = new CreateTransactionCommandHandler(context, _messageProducer);

            var request = new CreateTransactionCommand
            {
                Description = "Test Transaction",
                Amount = 100,
                TransactionType = 1
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeGreaterThan(0);
        }
    }
}