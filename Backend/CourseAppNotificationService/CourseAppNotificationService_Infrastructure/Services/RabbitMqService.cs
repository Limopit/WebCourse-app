using System.Text;
using CourseAppNotificationService_Domain;
using CourseAppNotificationService_Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CourseAppNotificationService_Infrastructure.Services
{
    public class RabbitMqService : IRabbitMqService, IAsyncDisposable
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly string _queueName;
        private readonly INotificationService _notificationService;

        public RabbitMqService(IConfiguration configuration, INotificationService notificationService)
        {
            var hostName = configuration["RabbitMQ:HostName"];
            var port = int.Parse(configuration["RabbitMQ:Port"]);
            var userName = configuration["RabbitMQ:UserName"];
            var password = configuration["RabbitMQ:Password"];
            _queueName = configuration["RabbitMQ:QueueName"];
            _notificationService = notificationService;

            var factory = new ConnectionFactory
            {
                HostName = hostName,
                Port = port,
                UserName = userName,
                Password = password
            };

            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.QueueDeclareAsync(_queueName, durable: false, exclusive: false, autoDelete: false);
        }

        public async Task PublishAsync(Notification notification)
        {
            var body = Encoding.UTF8.GetBytes(notification.Message);

            await _channel.BasicPublishAsync(exchange: string.Empty,
                routingKey: _queueName,
                body: body);
            
            await _notificationService.SendNotificationToUserAsync(notification);
        }


        public async Task SubscribeAsync(Func<string, Task> handler)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                
                //var notification = JsonConvert.DeserializeObject<Notification>(message);
                
                await handler(message);
            };

            await _channel.BasicConsumeAsync(queue: _queueName,
                autoAck: true,
                consumer: consumer);
        }

        public async ValueTask DisposeAsync()
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }
    }
}