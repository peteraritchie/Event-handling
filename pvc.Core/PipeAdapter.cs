using System;
namespace pvc.Core
{
	public class PipeAdapter<TIn, TOut> : Pipe<TIn, TOut> where TIn:Message where TOut:Message
	{
		private readonly Consumes<TIn> _consumer;
		private readonly Produces<TOut> _producer;
		
		public void Handle(TIn message) {
			_consumer.Handle(message);
		}
		
		public void AttachConsumer(Consumes<TOut> consumer) {
			_producer.AttachConsumer(consumer);
		} 
		
		private PipeAdapter(Consumes<TIn> consumer, Produces<TOut> producer) {
			_consumer = consumer;
			_producer = producer;
		}
	}
}

