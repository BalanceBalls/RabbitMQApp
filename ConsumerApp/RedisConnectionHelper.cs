using System;
using StackExchange.Redis;

namespace ConsumerApp
{
	public class RedisConnectionHelper
	{
		private static readonly Lazy<ConnectionMultiplexer> _redisConnection;

		private static readonly IServer _redisServer;

		public static ConnectionMultiplexer RedisConnection =>  _redisConnection.Value;

		public static IServer RedisServer => _redisServer;

		static RedisConnectionHelper()
		{
			_redisConnection = new Lazy
				<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
			_redisServer = _redisConnection.Value.GetServer("localhost", 6379);
		}
	}
}