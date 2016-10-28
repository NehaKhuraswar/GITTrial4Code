using RAP.Core.DataModels;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface INotificationService
    {
        string CorrelationId { get; set; }
        
    }
}
