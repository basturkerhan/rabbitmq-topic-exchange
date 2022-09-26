using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    public enum LogNames
    {
        Critical=1,
        Error=2,
        Warning=3,
        Info=4,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var uri = Environment.GetEnvironmentVariable("URI", EnvironmentVariableTarget.Process);
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("logs-topic", durable:true, type:ExchangeType.Topic);
            Random random = new Random();

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                LogNames log1 = (LogNames)random.Next(1, 5);
                LogNames log2 = (LogNames)random.Next(1, 5);
                LogNames log3 = (LogNames)random.Next(1, 5);
                var routeKey = $"{log1}.{log2}.{log3}";

                string message = $"Log: {x} and Type: {log1}-{log2}-{log3}";
                var messageBody = Encoding.UTF8.GetBytes(message); // kuyruğa bayt olarak gönderilir

                channel.BasicPublish("logs-topic", routeKey, null, messageBody);
                Console.WriteLine($"Log Gönderildi: {message}");
            });
  
            Console.ReadLine();
        }
    }
}
