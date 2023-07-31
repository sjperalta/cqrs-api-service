namespace TransactionsMicroservice.Davivienda.Infrastructure
{
    public interface IMessageProducer
    {
        void Produce(string message);
    }
}
