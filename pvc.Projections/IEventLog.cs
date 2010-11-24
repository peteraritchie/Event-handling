using System.Collections.Generic;
using pvc.Core;

namespace pvc.Projections
{
    public interface IEventLog
    {
		 void Save(IEnumerable<Message> messages);
    }
}