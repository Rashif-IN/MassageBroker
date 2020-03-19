using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification_handler
{
    public class RabbitListener
    {
        public void Register()
        {
            var client = new HttpClient();
            var factory = new ConnectionFactory() { HostName = "some-rabbit" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("userDataExchange", "fanout");

                var queueName = channel.QueueDeclare();
                channel.QueueBind(queueName, "userDataExchange", string.Empty);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    Console.WriteLine($"Processing data from queue");
                    await client.PostAsync("http://container-notif:2000/notification", content);

                };
                channel.BasicConsume(queue: "userData",
                                     autoAck: true,
                                     consumer: consumer);

                Thread.Sleep(10000);
            }
        }
    }
}
