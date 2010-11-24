using System;
using System.Collections.Generic;
using System.Linq;

namespace pvc.Core
{
	public class Multiplexor<T> : Pipe<T, T> where T:Message
	{
		private readonly List<Consumes<T>> _consumers;

		public Multiplexor() : this(null)
		{
		}

		public void AttachConsumer(Consumes<T> consumer) {
			_consumers.Add(consumer);
		}
		
		public Multiplexor(IEnumerable<Consumes<T>> consumers)
		{
			_consumers = consumers == null ? new List<Consumes<T>>() : consumers.ToList();
		}

		public void RemoveConsumer(Consumes<T> consumer)
		{
			_consumers.Remove(consumer);
		}

		public void Handle(T message)
		{ 
			_consumers.ForEach(x => x.Handle(message));
		}
	}
}
