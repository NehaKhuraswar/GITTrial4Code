using System.Data;

namespace RAP.Core.Persisters
{
    public interface IApplicationProcessingPersister
    {
        string CorrelationId { get; set; }

    }
}
