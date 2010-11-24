using System;
namespace pvc.Core
{
	public static class Extensions
	{
		public static Consumes<T> AsConsumer<T>(this Action<T> action) where T:Message
		{
			if (action == null) throw new ArgumentNullException("action");
			return new ActionWrappingConsumer<T>(action);
		}

		public static Action<T> AsAction<T>(this Consumes<T> consumer) where T:Message
		{
			if (consumer == null) throw new ArgumentNullException("consumer");
			return consumer.Handle;
		}

		public static void TryConsume<T>(this Consumes<T> consumer, T message) where T:Message
		{
			if (consumer == null) return;
			consumer.Handle(message);
		}

		public static Produces<T> AcceptingMany<T>(this Produces<T> produces) where T: Message
		{
			if (produces == null) throw new ArgumentNullException("produces");
			var multi = new Multiplexor<T>();
			produces.AttachConsumer(multi);
			return multi;
		}
	}
}

