using System;

namespace pvc.Core
{
	public class NarrowingConsumer<TConsumes, TProduces> : Pipe<TConsumes, TProduces>
		where TProduces : TConsumes
		where TConsumes : Message
	{
		private Consumes<TProduces> _inner;

		public NarrowingConsumer(Consumes<TProduces> inner)
		{
			if (inner == null) throw new ArgumentNullException("inner");
			_inner = inner;
		}

		public void AttachConsumer(Consumes<TProduces> consumer) {
			_inner = consumer;
		}
		
		public void Handle(TConsumes message)
		{
			_inner.Handle((TProduces) message);
		}
	}

}