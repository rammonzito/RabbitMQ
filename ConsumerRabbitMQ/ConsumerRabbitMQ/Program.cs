using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerRabbitMQ
{
    class Program
    {
        static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "TestRabbitMQ",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(message);
                    };
                    channel.BasicConsume(queue: "TestRabbitMQ",
                        autoAck: true,
                        consumer: consumer);

                    Console.WriteLine("Consumer Subiu!");
                    Console.ReadLine();
                }
            }
        }
    }
}
