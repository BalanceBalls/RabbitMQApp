using System;

namespace ProducerApp
{
	public class Message
	{
		public string Author { get; set; } = "DefaultAuthor";

		public string Content { get; set; } = "DefaultContent";

		public DateTime IssueDate { get; } = DateTime.Now;
	}
}