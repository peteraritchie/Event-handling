using NUnit.Framework;
using pvc.Core;
using pvc.Tests.Messages;

namespace pvc.Tests
{
	[TestFixture]
	public class when_test_consumer_receives_a_single_message : ConsumerFixture<TestMessage> {
		
		TestConsumer<TestMessage> sut;
		
		protected override Consumes<TestMessage> GivenConsumer ()
		{
			sut = new TestConsumer<TestMessage>();
			return sut;
		}
		
		protected override TestMessage When ()
		{
			return new TestMessage();
		}
		
		[Test]
		public void only_message_received_returns_proper_message() {
			Assert.AreEqual(published, sut.OnlyMessageReceived);
		}
		
		[Test]
		public void was_called_is_set_to_true() {
			Assert.IsTrue(sut.WasCalled);
		}
		
		[Test]
		public void times_called_is_one() {
			Assert.AreEqual(1, sut.TimesCalled);
		}
	}
	
	[TestFixture]
	public class when_test_consumer_receives_two_messages {
		TestConsumer<TestMessage> sut;
		
		[SetUp]
		public void SetUp() {
			sut = new TestConsumer<TestMessage>();
			sut.Handle(new TestMessage());
			sut.Handle(new TestMessage());
		}
		
		[Test, ExpectedException(typeof(TooManyMessagesReceivedException))]
		public void only_message_received_throws_too_many_messages_received_exception() {
			TestMessage x = sut.OnlyMessageReceived; 
			//TODO switch to assert.throws
		}
		
		[Test]
		public void was_called_is_set_to_true() {
			Assert.IsTrue(sut.WasCalled);
		}
		
		[Test]
		public void times_called_is_two() {
			Assert.AreEqual(2, sut.TimesCalled);
		}
	}
	
	[TestFixture]
	public class when_a_test_consumer_is_constructed {
		
		TestConsumer<TestMessage> sut;
		
		[SetUp]
		public void SetUp() {
			sut = new TestConsumer<TestMessage>();
		}
		
		[Test, ExpectedException(typeof(NoMessagesReceivedException))]
		public void only_message_received_throws_no_messages_received_exception() {
			TestMessage x = sut.OnlyMessageReceived; 
			//TODO switch to assert.throws
		}
		
		[Test]
		public void was_called_is_set_to_false() {
			Assert.IsFalse(sut.WasCalled);
		}
		
		[Test]
		public void times_called_is_zero() {
			Assert.AreEqual(0, sut.TimesCalled);
		}
	}
}

