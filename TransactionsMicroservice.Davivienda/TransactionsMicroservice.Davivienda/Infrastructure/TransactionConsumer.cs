using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TransactionsMicroservice.Davivienda.Models;

namespace TransactionsMicroservice.Davivienda.Infrastructure
{
    public class TransactionConsumer : ConnectionBuilderBase, IMessageConsumer
    {
        public TransactionConsumer(IConfiguration configuration) : base(configuration) { }
        
        public void Consume()
        {
            var factory = GetConnection;
            Console.WriteLine("Trying to get a connection");

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            { 
                channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);

                // Declare a new queue and bind it to the fanout exchange
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: _exchangeName,
                                  routingKey: "");

                Console.WriteLine("RabbitMQ consuming messages.");
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var transaction = JsonConvert.DeserializeObject<Transaction>(message);
                    Console.WriteLine($" [x] Received: {message}");
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
