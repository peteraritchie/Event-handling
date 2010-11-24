using NUnit.Framework;
using pvc.Core;
using pvc.Tests.Messages;

namespace pvc.Tests
{
	[TestFixture]
	public class widening_consumer_can_widen_pipeline_to_consumer_in_constructor : ConsumerFixture<TestMessage> {
		readonly TestConsumer<Message> testConsumer = new TestConsumer<Message>();
		
		protected override Consumes<TestMessage> GivenConsumer ()
		{
			return new WideningConsumer<TestMessage, Message>(testConsumer);
		}
		
		protected override TestMessage When ()
		{
			return new TestMessage();
		}
		
		[Test]
		public void consumer_receives_correct_message_with_type_widened() {
			Assert.AreEqual(published, testConsumer.OnlyMessageReceived);
		}
	}
}

