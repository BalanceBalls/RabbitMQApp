using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var messageHandler = new MessageHandler();
			
			var redisCache = RedisConnectionHelper.RedisConnection.GetDatabase();
			var keys = RedisConnectionHelper.RedisServer.Keys().ToList();
			Console.WriteLine("History of messages:");
			foreach (var message in keys.Select(key => redisCache.StringGet(key)))
			{
				Console.WriteLine(message.ToString());
			}
			
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "hello",
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += (model, ea) =>
				{
					var body = ea.Body.ToArray();
					var encodedMessage = Encoding.UTF8.GetString(body);
					var paredModel = JsonSerializer.Deserialize<Message>(encodedMessage);
					messageHandler.HandleMessage(paredModel);
					Console.WriteLine(" [x] Received message from {0} , message says : {1} , sent date - {2}", paredModel.Author, paredModel.Content, paredModel.IssueDate);
				};
				channel.BasicConsume(queue: "hello",
					autoAck: true,
					consumer: consumer);

				Console.WriteLine(" Press [enter] to exit.");
				Console.ReadLine();
			}
		}
	}
}