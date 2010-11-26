using System.IO;
using System.Runtime.Serialization;
using pvc.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace pvc.Adapters.RabbitMQ
{
	public class RabbitQueue<T> : IQueue<T>
	{
		private readonly string queue;
		private readonly IFormatter formatter;
		private readonly bool requiresAck;
		private readonly ConnectionFactory connectionFactory;
		private readonly IConnection connection;
		private readonly IModel model;
		private readonly string hostName;
		private readonly string exchange;
		private readonly Subscription subscription;
		private BasicDeliverEventArgs currentDeliveryArgs;

		public RabbitQueue(RabbitCreationParams rabbitCreationParams)
		{
			this.hostName = rabbitCreationParams.HostName;
			this.exchange = rabbitCreationParams.Exchange;
			this.queue = rabbitCreationParams.Queue;
			this.formatter = rabbitCreationParams.Formatter;
			this.requiresAck = rabbitCreationParams.RequiresAck;
			connectionFactory = new ConnectionFactory
			{
				HostName = rabbitCreationParams.HostName,
				UserName = rabbitCreationParams.UserName,
				Password = rabbitCreationParams.Password,
				Port = rabbitCreationParams.Port
			};
			connection = connectionFactory.CreateConnection();
			model = connection.CreateModel();
			subscription = new Subscription(model, rabbitCreationParams.Queue, false);
		}

		public bool TryDequeue(out T item)
		{
			int messageCount = 0;
			currentDeliveryArgs = subscription.Next();
			item = (T)formatter.Deserialize(new MemoryStream(currentDeliveryArgs.Body));
			return true;
		}

		public void Enqueue(T item)
		{
			var ibp = model.CreateBasicProperties();
			var shitbird = new MemoryStream();
			formatter.Serialize(shitbird, item);
			model.BasicPublish(exchange, string.Empty, false, false, ibp, shitbird.ToArray());
		}

		public void MarkComplete(T item)
		{
			if (currentDeliveryArgs != null)
				subscription.Ack(currentDeliveryArgs);
		}

		public override string ToString()
		{
			return hostName + ":" + exchange + ":" + queue;
		}
	}
}