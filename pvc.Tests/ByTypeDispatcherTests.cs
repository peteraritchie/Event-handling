
using NUnit.Framework;
using pvc.Core;
using pvc.Tests.Messages;

namespace pvc.Tests
{
	public class when_dispatching_a_message : ConsumerFixture<Message> {
		TestConsumer<TestMessage> correctSubscriber;
		TestConsumer<OtherMessage> incorrectSubscriber;
		TestConsumer<BaseMessage> baseClassSubscriber;
		
		
		protected override Consumes<Message> GivenConsumer ()
		{
			var dispatcher = new ByTypeDispatcher<Message>();
			correctSubscriber = new TestConsumer<TestMessage>();
			incorrectSubscriber = new TestConsumer<OtherMessage>();
			baseClassSubscriber = new TestConsumer<BaseMessage>();
			dispatcher.Subscribe(correctSubscriber);
			dispatcher.Subscribe(incorrectSubscriber);
			dispatcher.Subscribe(baseClassSubscriber);
			return dispatcher;
		}
		
		protected override Message When ()
		{
			return new TestMessage();
		}
		
		[Test]
		public void exactly_subscribed_consumer_receives_correct_message() {
			Assert.AreEqual(published, correctSubscriber.OnlyMessageReceived);
		}
		
		[Test]
		public void other_consumer_did_not_receive_message() {
			Assert.IsFalse(incorrectSubscriber.WasCalled);
		}
		
		[Test]
		public void base_subscribed_consumer_receives_correct_message() {
			Assert.AreEqual(published, baseClassSubscriber.OnlyMessageReceived);
		}
	}
}

