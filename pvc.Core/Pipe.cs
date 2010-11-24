using System;
namespace pvc.Core
{
	public interface Pipe<TConsumes, TProduces> : Consumes<TConsumes>, Produces<TProduces> where TConsumes:Message where TProduces:Message
	{
	}
}

