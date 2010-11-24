using System;
using System.Threading;

namespace pvc.Core
{
	public class QueueReader<T> : Produces<T> where T : Message
	{
		private readonly IQueue<T> _queue;
		private Consumes<T> _consumer;

		public QueueReader(IQueue<T> queue)
		{
			_queue = queue;

		}

		private void Run()
		{
			while (true)
			{
				try
				{
					T item;
					if(_queue.TryDequeue(out item))
					{
						_consumer.Handle(item);
						_queue.MarkComplete(item);
					}
				}
				catch (Exception Ex)
				{
					//Todo: Add log4net
					//Dead letter
					//Stop?
					//??
				}
			}
		}

		public void Start()
		{
			var t = new Thread(Run) { IsBackground = true };
			t.Start();
		}

		public void AttachConsumer(Consumes<T> consumer)
		{
			_consumer = consumer;
		}
	}


}