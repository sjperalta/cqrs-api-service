using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace TransactionsMicroservice.Davivienda.Infraestructure
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
            _rabbitMQHost = configuration["RabbitMQ:Host"];
            _userName = configuration["RabbitMQ:UserName"];
            _password = configuration["RabbitMQ:Password"];
            _port = configuration["RabbitMQ:Port"];
            _exchangeName = configuration["RabbitMQ:ExchangeName"];
            _queueName = configuration["RabbitMQ:QueueName"];
        }

        protected ConnectionFactory GetConnection =>
            new ConnectionFactory()
            {
                HostName = _rabbitMQHost,
                UserName = _userName,
                Password = _password,
                Port = int.Parse(_port)
            };   
    }
}
