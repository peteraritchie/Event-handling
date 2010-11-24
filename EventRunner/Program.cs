using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using pvc.Core;
using pvc.Core.Messages;

namespace EventRunner
{
	class Program
	{
		static void Noop(FooMessage message) { }
		static void Main(string[] args)
		{
			var eventAggregator = new EventAggregator<Message>();
			var projectionHost = new ProjectionHost(eventAggregator);

			projectionHost.SetUpProjections();

			var inMemQueue = new InMemQueue<Message>();
			var reader = new QueueReader<Message>(inMemQueue);
			eventAggregator.AttachTo(reader);

			var foo = new QueueWriter<Message>(inMemQueue);
			reader.Start();
			while(true)
			{
				foo.Handle(new FooMessage());
				Thread.Sleep(1000);
			}

			//var dispatcher = new ByTypeDispatcher();
			//dispatcher.Subscribe(new FooHandler());
			//dispatcher.Handle(new FooMessage());
		}
	}

	internal class FooHandler : Consumes<FooMessage>
	{
		public void Handle(FooMessage message)
		{
			throw new YayException();
		}
	}

	internal class YayException : Exception
	{
	}

}
