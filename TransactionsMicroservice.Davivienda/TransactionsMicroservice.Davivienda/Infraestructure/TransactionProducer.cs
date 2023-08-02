using RabbitMQ.Client;
using System.Text;

namespace TransactionsMicroservice.Davivienda.Infraestructure
{
    public class TransactionProducer : ConnectionBuilderBase, IMessageProducer
    {
        public TransactionProducer(IConfiguration configuration) : base(configuration) { }
        
        public void Produce(string message)
        {
            var factory = GetConnection;
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", 
                routingKey: _queueName, 
                basicProperties: null, 
                body: body);

            Console.WriteLine($" [x] Sent Message {message}");
        }
    }
}
