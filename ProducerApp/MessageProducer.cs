using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace ProducerApp
{
	public class MessageProducer : IMessageProducer<Message> 
	{
		public void ProduceMessage(Message message)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: "hello",
						durable: false,
						exclusive: false,
						autoDelete: false,
						arguments: null);

					var encodedMessage = JsonSerializer.Serialize(message);
					var body = Encoding.UTF8.GetBytes(encodedMessage);

					channel.BasicPublish(exchange: "",
						routingKey: "hello",
						basicProperties: null,
						body: body);
					Console.WriteLine(" [x] Sent {0}", encodedMessage);
				}
			}
		}
	}
}