using System;
using pvc.Core;

namespace pvc.Projections
{
	public class ActivatorProjectionCreator : IProjectionCreator
	{
		public object Create(Type t)
		{
			return Activator.CreateInstance(t);
		}
	}
}