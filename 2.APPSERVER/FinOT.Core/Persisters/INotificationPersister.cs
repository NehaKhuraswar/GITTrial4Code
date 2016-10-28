using System.Data;

namespace RAP.Core.Persisters
{
    public interface INotificationPersister
    {
        string CorrelationId { get; set; }
        
    }
}
