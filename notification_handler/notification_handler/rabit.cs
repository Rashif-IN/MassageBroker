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
            //receiver
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

