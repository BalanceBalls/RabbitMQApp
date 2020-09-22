using System;

namespace ProducerApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var messageProducer = new MessageProducer();
			while (true)
			{
				var message = new Message();
				Console.WriteLine("Enter author's name: ");
				message.Author = Console.ReadLine();
				Console.WriteLine("Enter message: ");
				message.Content = Console.ReadLine();
				
				messageProducer.ProduceMessage(message);
			}
		}
	}
}