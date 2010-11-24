using System;
namespace pvc.Core
{
	public class ActionWrappingConsumer<T> : Consumes<T> where T:Message 
	{
		private readonly Action<T> _action;
		public void Handle(T message) {
			_action(message);
		}
		public ActionWrappingConsumer (Action<T> action)
		{
			_action = action; 
		}
	}
}