
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using notification_handler.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification_handler.UseCase.Notification.Query.GetAll
{
    public class Handler : IRequestHandler<Command, Dto>
    {
        private readonly Context konteks;

        public Handler(Context context)
        {
            konteks = context;
        }

        public async Task<Dto> Handle(Command request, CancellationToken cancellationToken)
        {

            receive();
            var notifData = await konteks.notif.ToListAsync();
            var result = new List<NotificationData>();

            foreach (var X in notifData)
            {
                result.Add(new NotificationData
                {
                    Id = X.id,
                    Title = X.title,
                    Message = X.message
                });
            }

            return new Dto
            {
                message = "notifications retrieved",
                success = true,
                Data = result
            };
        }
        public void receive()
        {
            var _client = new HttpClient();
            var _factory = new ConnectionFactory() { HostName = "localhost" };
            using (var _connection = _factory.CreateConnection())
            using (var _channel = _connection.CreateModel())
            {
                _channel.ExchangeDeclare("userDataExchange", "fanout");

                var queueName = _channel.QueueDeclare();
                _channel.QueueBind(queueName, "userDataExchange", string.Empty);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    Console.WriteLine($"Processing data from queue");
                    await _client.PostAsync("http://localhost:2000/notification", content);

                };
                _channel.BasicConsume(queue: "userData",
                                     autoAck: true,
                                     consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}