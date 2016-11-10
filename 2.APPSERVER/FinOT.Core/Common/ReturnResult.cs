using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.Common
{
    public class ReturnResult<T>
    {
        public OperationStatus status;
        public T result;
    }
}
