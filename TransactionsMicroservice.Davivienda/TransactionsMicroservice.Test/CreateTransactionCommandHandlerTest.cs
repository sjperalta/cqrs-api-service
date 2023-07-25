using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TransactionsMicroservice.Davivienda;
using TransactionsMicroservice.Davivienda.Features.Transactions;

namespace TransactionsMicroservice.Test
{
    public class CreateTransactionCommandHandlerTest
    {
        private BankDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BankDbContext(options);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsTransactionId()
        {
            // Arrange
            var context = CreateDbContext();
            var handler = new CreateTransactionCommandHandler(context);

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