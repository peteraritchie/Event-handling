using System.Reflection;
using pvc.Core;

namespace pvc.Projections
{
	public class ProjectionHost
	{
		private readonly EventAggregator<Message> _aggregator;
		private readonly IProjectionCreator _creator;

		public ProjectionHost(EventAggregator<Message> aggregator) : this(aggregator, new ActivatorProjectionCreator())
		{
		}

		public ProjectionHost(EventAggregator<Message> aggregator, IProjectionCreator creator)
		{
			_aggregator = aggregator;
			_creator = creator;
		}

		public void SetUpProjections()
		{
			foreach(var t in TypeFinder.GetTypesMatching<ProjectionAttribute>())
			{
				var projector = _creator.Create(t);

				foreach(var genericInterface in t.GetInterfaces())
				{
					if(genericInterface.IsGenericType)
					{
						var typeDef = genericInterface.GetGenericTypeDefinition();
						if (typeDef.Equals(typeof(Consumes<>)))
						{
							var messageType = genericInterface.GetGenericArguments(); 
							MethodInfo method = typeof(EventAggregator<Message>).GetMethod("SubscribeTo");
							MethodInfo generic = method.MakeGenericMethod(messageType);
							generic.Invoke(_aggregator, new [] {projector});
						}
					}
				}
			}		
		}
	}
}