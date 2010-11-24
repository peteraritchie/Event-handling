namespace pvc.Core
{
	public interface IMessagePublisher<T> where T : Message
	{
		void Publish(T message);
	}
}