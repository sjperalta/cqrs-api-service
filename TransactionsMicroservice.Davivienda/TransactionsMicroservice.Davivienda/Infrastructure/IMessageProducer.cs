namespace TransactionsMicroservice.Davivienda.Infrastructure
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
