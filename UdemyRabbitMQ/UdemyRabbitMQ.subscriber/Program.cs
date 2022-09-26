using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace UdemyRabbitMQ.subscriber // consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var uri = Environment.GetEnvironmentVariable("URI", EnvironmentVariableTarget.Process);
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);

            // Kuyruk oluşturma
            var queueName = channel.QueueDeclare().QueueName;
            var routeKey = "*.Error.*";
            channel.QueueBind(queueName, "logs-topic", routeKey);
            channel.BasicConsume(queueName, false, consumer);
            Console.WriteLine("Loglar Dinleniyor...");

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(500);
                Console.WriteLine("Kuyruktan Gelen Mesaj: " + message);

                channel.BasicAck(e.DeliveryTag, false); // mesajı artık kuyruktan silebilirsin diye belirttik (yukarıyı false yaptığımız için)
            };

            Console.ReadLine();
        }

    }
}
