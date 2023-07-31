using RabbitMQ.Client;
using System.Text;
using System.Transactions;

namespace TransactionsMicroservice.Davivienda.Infrastructure
{
    public class TransactionPublisher : ConnectionBuilderBase, IMessageProducer
    {
        public TransactionPublisher(IConfiguration configuration) : base(configuration) { }

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

            Console.WriteLine($" [x] Rabbit Sent Message Type: {typeof(Transaction)} - body {message}");
        }
    }
}
