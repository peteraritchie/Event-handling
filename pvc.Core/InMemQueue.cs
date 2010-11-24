//using System.Collections.Concurrent;
using System.Collections.Generic;
namespace pvc.Core
{
	public class InMemQueue<T> : IQueue<T> 
	{
		private Queue<T> _queue = new Queue<T>(); //make this a concurrent queue, need to build on 3.5 for mono now

		public bool TryDequeue(out T item)
		{
			lock(_queue) {
				if(_queue.Count > 0)  {
					item =_queue.Dequeue();
					return true;
				}
				else {
					item = default(T);
					return false;
				}
			}
		}

		public void Enqueue(T item)
		{
			_queue.Enqueue(item);
		}

		public void MarkComplete(T item)
		{ 
		}
	}
}