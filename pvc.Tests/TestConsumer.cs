using System;
using System.Collections.Generic;
using pvc.Core;

namespace pvc.Tests
{
	public class TooManyMessagesReceivedException : Exception {}
	public class NoMessagesReceivedException : Exception {}
	
	public class TestConsumer<T> : Consumes<T> where T:Message
	{
		private readonly List<T> _received = new List<T>();
		
		public TestConsumer() {
		}
		
		public T OnlyMessageReceived {
			get { 
				if(_received.Count > 1) throw new TooManyMessagesReceivedException();
				if(_received.Count == 0) throw new NoMessagesReceivedException();
				return _received[0];
			}
		}
		
		public bool WasCalled {
			get { return _received.Count > 0; }
		}
		
		public int TimesCalled {
			get { return _received.Count; }
		}
		
		public void Handle(T message) {
			_received.Add(message);
		}		
	}
	
}