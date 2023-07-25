
namespace TransactionsMicroservice.Davivienda.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public int TransactionType { get; set; }
    }
}