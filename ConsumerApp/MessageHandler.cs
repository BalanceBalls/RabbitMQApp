using System.Linq;
using System.Text.Json;
using StackExchange.Redis;

namespace ConsumerApp
{
	public class MessageHandler : IMessageHandler<Message>
	{
		private readonly IDatabase _redisCache;

		public MessageHandler()
		{
			_redisCache = RedisConnectionHelper.RedisConnection.GetDatabase();
		}

		public void HandleMessage(Message message)
		{
			_redisCache.StringSet(message.GetHashCode().ToString(), JsonSerializer.Serialize(message));
		}
	}
}