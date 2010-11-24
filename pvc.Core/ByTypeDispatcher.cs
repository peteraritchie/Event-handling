using System;
using System.Collections.Generic;

namespace pvc.Core
{
	public class ByTypeDispatcher<TBaseMessage> : Consumes<TBaseMessage> where TBaseMessage : Message
	{
		private readonly Dictionary<Type, Multiplexor<TBaseMessage>> _subscriptions = new Dictionary<Type, Multiplexor<TBaseMessage>>();

		public void Subscribe<TDerivedMessage>(Consumes<TDerivedMessage> handler) where TDerivedMessage : TBaseMessage
		{
			Multiplexor<TBaseMessage> handlers;
			if (!_subscriptions.TryGetValue(typeof(TDerivedMessage), out handlers))
			{
				handlers = new Multiplexor<TBaseMessage>();
				_subscriptions.Add(typeof(TDerivedMessage), handlers);
			}
			handlers.AttachConsumer(new NarrowingConsumer<TBaseMessage, TDerivedMessage>(handler));
		}

		public void Handle(TBaseMessage message)
		{
			var t = message.GetType();
			Publish(message, t);
			do
			{
				t = t.BaseType;
				Publish(message, t);
			} while (t != typeof(TBaseMessage));
		}

		private void Publish(TBaseMessage message, Type publishAs)
		{
			Multiplexor<TBaseMessage> handler;
			if (_subscriptions.TryGetValue(publishAs, out handler))
			{
				handler.Handle(message);
			}
		}
	}
}