
using RabbitMQ.Client;

namespace TransactionsMicroservice.Davivienda.Infrastructure
{

    public abstract class ConnectionBuilderBase
    {
        protected readonly string _rabbitMQHost;
        protected readonly string _userName;
        protected readonly string _password;
        protected readonly string _port;
        protected readonly string _exchangeName;
        protected readonly string _queueName;

        public ConnectionBuilderBase(IConfiguration configuration)
        {
            Console.WriteLine("se llamo el base");
            _rabbitMQHost = configuration["RabbitMQ:Host"];
            _userName = configuration["RabbitMQ:UserName"];
            _password = configuration["RabbitMQ:Password"];
            _exchangeName = configuration["RabbitMQ:ExchangeName"]; // Add a new key for ExchangeName
            _port = configuration["RabbitMQ:Port"];  
            _queueName = configuration["RabbitMQ:QueueName"]; // Add a new key for QueueName
        }

        protected ConnectionFactory GetConnection =>
            new ConnectionFactory()
            {
                HostName = _rabbitMQHost,
                Port = int.Parse(_port),
                UserName = _userName,
                Password = _password
            };
    }

}