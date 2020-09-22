namespace ConsumerApp
{
	public interface IMessageHandler<in T> where T : class
	{
		void HandleMessage(T message);
	}
}