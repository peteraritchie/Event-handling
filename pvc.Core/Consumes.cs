namespace pvc.Core
{
	public interface Consumes<TMessage> where TMessage : Message
	{
		void Handle(TMessage message);
	}
}