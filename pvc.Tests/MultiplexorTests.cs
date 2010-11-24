using pvc.Core;
using NUnit.Framework;
using pvc.Tests.Messages;

namespace pvc.Tests
{
	[TestFixture]
	public class when_multiplexing_to_multiple_constructed_consumers : ConsumerFixture<TestMessage> {
		
		TestConsumer<TestMessage> firstConsumer;
		TestConsumer<TestMessage> secondConsumer;
		
		protected override Consumes<TestMessage> GivenConsumer ()
		{
			firstConsumer = new TestConsumer<TestMessage>();
			secondConsumer = new TestConsumer<TestMessage>();
			var m = new Multiplexor<TestMessage>(new [] {firstConsumer, secondConsumer});
			return m;
		}
		
		protected override TestMessage When()
		{
			return new TestMessage(); 
		}
		
		[Test]
		public void first_consumer_received_only_correct_message() {
			Assert.AreEqual(published, firstConsumer.OnlyMessageReceived);
		}
		
		[Test]
		public void second_consumer_received_only_correct_message() {
			Assert.AreEqual(published, secondConsumer.OnlyMessageReceived);
		}
		
	}	
	
	[TestFixture]
	public class when_multiplexing_to_removed_consumer : ConsumerFixture<TestMessage> {
		TestConsumer<TestMessage> attachedConsumer;
		TestConsumer<TestMessage> removedConsumer;
		
		protected override Consumes<TestMessage> GivenConsumer ()
		{
			Multiplexor<TestMessage> m = new Multiplexor<TestMessage>();
			attachedConsumer = new TestConsumer<TestMessage>();
			removedConsumer = new TestConsumer<TestMessage>();
			m.AttachConsumer(removedConsumer);
			m.AttachConsumer(attachedConsumer);
			m.RemoveConsumer(removedConsumer);
			return m;
		}
		
		protected override TestMessage When ()
		{
			return new TestMessage(); 
		}
		
		[Test]
		public void attached_consumer_received_correct_message() {
			Assert.AreEqual(published, attachedConsumer.OnlyMessageReceived);
		}
		
		[Test]
		public void removed_consumer_does_not_receive_message() {
			Assert.IsFalse(removedConsumer.WasCalled);
		}
}

	
	[TestFixture]
	public class when_multiplexing_to_multiple_attached_consumers : ConsumerFixture<TestMessage> {
		TestConsumer<TestMessage> firstConsumer;
		TestConsumer<TestMessage> secondConsumer;
		
		protected override Consumes<TestMessage> GivenConsumer ()
		{
			Multiplexor<TestMessage> m = new Multiplexor<TestMessage>();
			firstConsumer = new TestConsumer<TestMessage>();
			secondConsumer = new TestConsumer<TestMessage>();
			m.AttachConsumer(secondConsumer);
			m.AttachConsumer(firstConsumer);
			return m;
		}
		
		protected override TestMessage When ()
		{
			return new TestMessage(); 
		}
		
		[Test]
		public void first_consumer_received_only_correct_message() {
			Assert.AreEqual(published, firstConsumer.OnlyMessageReceived);
		}
		
		[Test]
		public void second_consumer_received_only_correct_message() {
			Assert.AreEqual(published, secondConsumer.OnlyMessageReceived);
		}
	}	
} 