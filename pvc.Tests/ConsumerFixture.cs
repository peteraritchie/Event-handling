using System;
using NUnit.Framework;
using pvc.Core;

namespace pvc.Tests
{
	public abstract class ConsumerFixture<TMessage> where TMessage : Message
	{
		protected abstract Consumes<TMessage> GivenConsumer();
		protected Exception caught;
		protected abstract TMessage When();
		protected Consumes<TMessage> consumer;
		protected TMessage published;
		
		[SetUp]
		public void SetUp() {
			consumer = GivenConsumer();
			try {
				published = When();
				if(published != null)
					consumer.Handle(published);
			}
			catch(Exception Ex) {
				caught = Ex;
			}
		}
	}
}

