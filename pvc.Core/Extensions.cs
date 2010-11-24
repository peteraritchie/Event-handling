using System;
namespace pvc.Core
{
	public static class Extensions
	{
		public static Consumes<T> AsConsumer<T>(this Action<T> action) where T:Message {
			return new ActionWrappingConsumer<T>(action); 
		}

		public static Action<T> AsAction<T>(this Consumes<T> consumer) where T:Message
		{
			return consumer.Handle;
		}

		public static Produces<T> AcceptingMany<T>(this Produces<T> produces) where T: Message
		{
			var multi = new Multiplexor<T>();
			produces.AttachConsumer(multi);
			return multi;
		}
	}
}

