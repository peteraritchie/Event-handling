using System;
using NUnit.Framework;
using pvc.Core;
using pvc.Tests.Messages;

namespace pvc.Tests
{
	[TestFixture]
	class when_build_a_wrapped_action
	{
		[Test]
		public void a_null_action_throws_argumentnullexception() {
			Assert.Throws<ArgumentNullException>(() => new ActionWrappingConsumer<TestMessage>(null)); 
		}
	}

	[TestFixture]
	class when_calling_a_wrapped_action : ConsumerFixture<TestMessage>
	{
		int called = 0;
		private TestMessage message = null;
		protected override Consumes<TestMessage> GivenConsumer()
		{
			return new ActionWrappingConsumer<TestMessage>(x =>
			                                               	{
			                                               		called++;
			                                               		message = x;
			                                               	});
		}

		protected override TestMessage When()
		{
			return new TestMessage();
		}

		public override void SetUp()
		{
			message = null;
			called = 0;
			base.SetUp();
		}

		[Test]
		public void the_action_is_called()
		{
		     Assert.AreEqual(published, message);
		}

		[Test]
		public void the_action_is_called_once()
		{
			Assert.AreEqual(1, called);
		}

	}
}
