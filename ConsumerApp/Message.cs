using System;

namespace ConsumerApp
{
	public class Message
	{
		public string Author { get; set; } = "DefaultAuthor";

		public string Content { get; set; } = "DefaultContent";

		public DateTime IssueDate { get; } = DateTime.Now;

		/// <summary>Serves as the default hash function.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return (int)(IssueDate.Ticks/int.MaxValue) + Content.Length * Author.Length;
		}
	}
}