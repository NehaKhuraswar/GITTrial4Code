using System;
namespace RAP.Core.Common
{
   public interface IExceptionHandler
    {
        OperationStatus HandleException(Exception ex);
    }
}
