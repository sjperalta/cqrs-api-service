using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TransactionsMicroservice.Davivienda.Models;

namespace TransactionsMicroservice.Davivienda.Infrastructure
{
    public class TransactionConsumer : IMessageConsumer
    {
        private readonly string _rabbitMQHost;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _port;
        private readonly string _exchangeName;
        //private readonly string _queueName;

        public TransactionConsumer(IConfiguration configuration)
        {
            _rabbitMQHost = configuration["RabbitMQ:Host"];
            _userName = configuration["RabbitMQ:UserName"];
            _password = configuration["RabbitMQ:Password"];
            _exchangeName = configuration["RabbitMQ:ExchangeName"]; // Add a new key for ExchangeName
            _port = configuration["RabbitMQ:Port"];                                                 //_queueName = configuration["RabbitMQ:QueueName"]; // Add a new key for QueueName
        }

        public void ReceiveMessage()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQHost,
                Port = int.Parse(_port),
                UserName = _userName,
                Password = _password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);

                // Declare a new queue and bind it to the fanout exchange
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: _exchangeName,
                                  routingKey: "");

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
