namespace pvc.Core
{
	public class EventAggregator<TBase> where TBase:Message
	{
		private readonly ByTypeDispatcher<TBase> _dispatcher;
		private readonly Combiner<TBase> _combiner;

		public EventAggregator()
		{
			_dispatcher = new ByTypeDispatcher<TBase>();
			_combiner = new Combiner<TBase>();
			_combiner.AttachConsumer(_dispatcher);
		}

		 public void AttachTo<TDerived>(Produces<TDerived> producer) where TDerived:TBase
		 {
		 	producer.AttachConsumer(_combiner.GetConsumer<TDerived>());
		 }

		public void SubscribeTo<TDerived>(Consumes<TDerived> consumer) where TDerived:TBase
		{
			_dispatcher.Subscribe(consumer);
		}
	}
}
