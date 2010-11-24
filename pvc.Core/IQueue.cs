namespace pvc.Core
{
	public interface IQueue<T>
	{
		bool TryDequeue(out T item);
		void Enqueue(T item);
		void MarkComplete(T item);
	}
}