using System;

namespace pvc.Tests.Messages
{
	public class TestMessage : BaseMessage 
	{
		public readonly Guid Id;
		public TestMessage () : this(Guid.NewGuid()) { }
		
		public override bool Equals (object obj)
		{
			var other = obj as TestMessage;
			return other == null ? false : other.Id == this.Id;
		}
		
		public override int GetHashCode ()
		{
			return Id.GetHashCode();
		}
		
		public TestMessage (Guid id) { 
			Id = id;
		}
	}
}

