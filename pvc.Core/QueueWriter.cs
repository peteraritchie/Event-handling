namespace pvc.Core
{
	public class QueueWriter<T> : Consumes<T> where T: Message
	{
		private readonly IQueue<T> _queue;

		public QueueWriter(IQueue<T> queue)
		{
			_queue = queue;
		}

		public void Handle(T message)
		{
			_queue.Enqueue(message);
		}
	}
}