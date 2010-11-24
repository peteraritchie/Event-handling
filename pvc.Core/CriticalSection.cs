using System;
using System.Threading;

namespace pvc.Core
{
	class CriticalSection<T> : Pipe<T,T> where T:Message
	{
		private readonly object _lockObject;
		private readonly TimeSpan _timeout;
		private Consumes<T> _consumer;

		public CriticalSection(object lockObject) : this(lockObject, TimeSpan.MaxValue, null)
		{
		}

		public CriticalSection(Consumes<T> consumer) : this(new object(), consumer)
		{
		}

		public CriticalSection(object lockObject, Consumes<T> consumer) : this(lockObject, TimeSpan.MaxValue, consumer)
		{
		}

		public CriticalSection(object lockObject, TimeSpan timeout, Consumes<T> consumer)
		{
			_lockObject = lockObject;
			_timeout = timeout;
			_consumer = consumer;
		}

		public CriticalSection() : this(new object())
		{		
		}

		public void Handle(T message)
		{
			if(!Monitor.TryEnter(_lockObject, _timeout))
			{
				throw new UnableToAcquireLockException();
			}
			try
			{
				_consumer.Handle(message);
			}
			finally
			{
				Monitor.Exit(_lockObject);
			}
		}

		public void AttachConsumer(Consumes<T> consumer)
		{
			_consumer = consumer;
		}
	}
}
