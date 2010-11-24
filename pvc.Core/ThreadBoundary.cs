using System;
//using System.Collections.Concurrent; //replace later with concurrent queue, need to work in mono now
using System.Collections.Generic;
using System.Threading;
namespace pvc.Core
{
	public class ThreadBoundary<T> : Pipe<T,T> where T:Message
	{
		private readonly Queue<T> _queue = new Queue<T>();
		private Consumes<T> _consumer;
		private readonly int _sleepMilliseconds;

		private void Run()
		{
			while(true)
			{
				try
				{
					T item;
					var current = new List<T>();
					lock(_queue) {
						while (_queue.Count > 0)
						{
							item = _queue.Dequeue();
							current.Add(item);
						}
					}
					current.ForEach(x => _consumer.Handle(x));
					Thread.Sleep(_sleepMilliseconds);
				}
				catch(Exception Ex)
				{
					//what to do with errors at this point?
				}
			}
		}

		public ThreadBoundary(Consumes<T> consumer, int sleepMilliseconds)
		{
			_consumer = consumer;
			_sleepMilliseconds = sleepMilliseconds;
			var t = new Thread(Run) {IsBackground = true};
			t.Start();
		}

		public void Handle(T message)
		{
			lock(_queue) {
				_queue.Enqueue(message);
			}
		}

		public void AttachConsumer(Consumes<T> consumer)
		{
			_consumer = consumer;
		}
	}
}