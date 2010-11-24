using System;

namespace pvc.Core
{
	public class WideningConsumer<TConsumes, TProduces> : Pipe<TConsumes, TProduces>
		where TConsumes : TProduces
		where TProduces : Message
	{
		private Consumes<TProduces> _inner;

		public WideningConsumer(Consumes<TProduces> inner)
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