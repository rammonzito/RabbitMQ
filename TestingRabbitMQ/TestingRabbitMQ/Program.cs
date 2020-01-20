using System;
using System.Text;
using RabbitMQ.Client;

namespace TestingRabbitMQ
{
    class Enviar
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

                    string message = "First Local RabbitMQ!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "TestRabbitMQ",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine("Mensagem Enviada!");
                }
                Console.ReadLine();
            }
        }
    }
}
