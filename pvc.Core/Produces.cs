namespace pvc.Core
{
	public interface Produces<T> where T:Message
	{
		void AttachConsumer(Consumes<T> consumer);
	}
}