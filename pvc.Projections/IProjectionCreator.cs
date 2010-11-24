using System;

namespace pvc.Projections
{
	public interface IProjectionCreator
	{
		object Create(Type t);
	}
}