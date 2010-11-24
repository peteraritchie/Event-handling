namespace pvc.Core
{
	public class NullConsumer<T> : Consumes<T> where T:Message
	{
		public void Handle(T message)
		{
		}
	}
}
