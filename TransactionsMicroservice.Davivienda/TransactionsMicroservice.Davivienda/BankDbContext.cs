using Microsoft.EntityFrameworkCore;
using TransactionsMicroservice.Davivienda.Models;

namespace TransactionsMicroservice.Davivienda
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
