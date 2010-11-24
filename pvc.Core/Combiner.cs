namespace pvc.Core
{
	public class Combiner<TBase> : Produces<TBase> where TBase:Message
	{
		private Consumes<TBase> _handler;
	
		public void AttachConsumer(Consumes<TBase> consumer) {
			_handler = consumer;
		}
		
		public Consumes<TDerived> GetConsumer<TDerived>() where TDerived:TBase {
			return new WideningConsumer<TDerived, TBase>(_handler);
		}
	}
}


