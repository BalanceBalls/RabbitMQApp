using System.Threading.Tasks;

namespace ProducerApp
{
	public interface IMessageProducer<in T> where T : class
	{
		void ProduceMessage(T message);
	}
}