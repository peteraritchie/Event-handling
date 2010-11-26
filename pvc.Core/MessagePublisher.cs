namespace pvc.Core
{
	public class MessagePublisher<T> : Produces<T>, IMessagePublisher<T> where T : Message
	{
		private readonly Multiplexor<T> multiplexor = new Multiplexor<T>();

		public void AttachConsumer(Consumes<T> consumer)
		{
			multiplexor.AttachConsumer(consumer);
		}

		public void Publish(T message)
		{
			multiplexor.Handle(message);
		}
	} 
}
