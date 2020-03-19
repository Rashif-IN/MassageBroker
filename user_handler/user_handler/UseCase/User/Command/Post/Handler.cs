using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using user_handler.Model;

namespace user_handler.UseCase.User.Command.Post
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
            var userdata = new user_model
            {
                name = request.data.Attributes.name,
                username = request.data.Attributes.username,
                email = request.data.Attributes.email,
                password = request.data.Attributes.password,
                address = request.data.Attributes.address
            };
            konteks.user.Add(userdata);
            await konteks.SaveChangesAsync(cancellationToken);

            var user = konteks.user.First(x => x.username == request.data.Attributes.username);
            var target = new TargetCommand() { Id = user.id, Email_destination = user.email };
            var client = new HttpClient();
            var command = new PostCommand()
            {
                Title = "hjfrhftcutcuc6ello rtyxdrtxdye4ty",
                Message = "you think this is hello world, but it was me dio",
                Type = "email",
                From = 56,
                Target = new List<TargetCommand>() { target }
            };

            var attributes = new Data<PostCommand>()
            { Attributes = command };

            var httpContent = new RequestData<PostCommand>()
            { data = attributes };

            var jsonObj = JsonConvert.SerializeObject(httpContent);

            //var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            //await client.PostAsync("http://localhost:2000/notification",content);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.ExchangeDeclare("userDataExchange", "fanout");
                channel.QueueDeclare(queue: "userData", durable: true, exclusive: false, autoDelete: false, arguments: null);
                var Body = Encoding.UTF8.GetBytes(jsonObj);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(exchange: "", routingKey: "userData",basicProperties: null,body: Body);
                Console.WriteLine("User data has been forwarded");
                Console.ReadLine();

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    Console.WriteLine($"Processing data from queue");
                    await client.PostAsync("http://localhost:2000/notification", content);

                };
                channel.BasicConsume(queue: "userData",
                                     autoAck: true,
                                     consumer: consumer);
            }
            Console.ReadLine(); 
            return new Dto
            {
                message = "user posted",
                success = true
            };
        }
    }
}
