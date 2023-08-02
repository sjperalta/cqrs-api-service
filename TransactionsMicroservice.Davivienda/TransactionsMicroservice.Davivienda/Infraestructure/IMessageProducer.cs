namespace TransactionsMicroservice.Davivienda.Infraestructure
{
    public interface IMessageProducer
    {
        void Produce(string message);
    }
}
