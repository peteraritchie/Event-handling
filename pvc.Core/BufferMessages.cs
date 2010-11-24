using System.Collections.Generic;

namespace pvc.Core
{
	public class BufferMessages<T> : Pipe<T,T> where T:Message
	{
		private readonly List<T> _buffer = new List<T>();
		private Consumes<T> _consumer;
		private bool _isPassThrough = false;

		public void BecomePassThrough()
		{
			FlushBuffer();
			_isPassThrough = true;
		}

		private void FlushBuffer()
		{
			lock(_buffer)
			{
				//dont really do this way use a thread and queue internally as you don't want to block this thread.
				_buffer.ForEach(Publish);
			}
		}

		private void Publish(T message)
		{
			lock (_buffer)
			{
				//see comment in FlushBuffer
				_consumer.Handle(message);
			}
		}

		public void Handle(T message)
		{
			if (_isPassThrough) Publish(message);
			else _buffer.Add(message);
		}

		public void AttachConsumer(Consumes<T> consumer)
		{
			_consumer = consumer;
		}
	}
}
