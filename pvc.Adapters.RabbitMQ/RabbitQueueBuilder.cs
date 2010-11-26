using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace pvc.Adapters.RabbitMQ
{
	public class RabbitQueueBuilder<T>
	{
		public string HostName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public int Port { get; set; }
		public string Exchange { get; set; }
		public string Queue { get; set; }
		public IFormatter Formatter { get; set; }
		public bool RequiresAck { get; set; }

		public RabbitQueueBuilder()
		{
			Formatter = new BinaryFormatter();
			HostName = "localhost";
			Port = 5672;
		}

		public RabbitQueueBuilder(string hostName, string userName, string password, int port, string exchange, string queue, IFormatter formatter, bool requiresAck)
		{
			HostName = hostName;
			UserName = userName;
			Password = password;
			Port = port;
			Exchange = exchange;
			Queue = queue;
			Formatter = formatter;
			RequiresAck = requiresAck;
		}
		
		public RabbitQueueBuilder<T> WithExchange(string exchange)
		{
			Exchange = exchange;
			return this;
		}

		public RabbitQueueBuilder<T> ToHost(string hostName)
		{
			HostName = hostName;
			return this;
		}

		public RabbitQueueBuilder<T> AsAnonymous()
		{
			UserName = "guest";
			return this;
		}

		public RabbitQueueBuilder<T> WithCredentials(string userName, string password)
		{
			UserName = userName;
			Password = password;
			return this;
		}

		public RabbitQueueBuilder<T> OnPort(int port)
		{
			Port = port;
			return this;
		}

		public RabbitQueueBuilder<T> ToQueue(string queue)
		{
			Queue = queue;
			return this;
		}

		public RabbitQueueBuilder<T> WithFormatter(IFormatter formatter)
		{
			Formatter = formatter;
			return this;
		}

		public RabbitQueueBuilder<T> RequiringAck
		{
			get
			{
				RequiresAck = true;
				return this;
			}
		}

		public static implicit operator RabbitQueue<T>(RabbitQueueBuilder<T> builder)
		{
			return
				new RabbitQueue<T>(new RabbitCreationParams(builder.HostName, builder.UserName, builder.Password, builder.Port,
				                                            builder.Exchange, builder.Queue,
				                                            builder.Formatter, builder.RequiresAck));

		}

	}
}