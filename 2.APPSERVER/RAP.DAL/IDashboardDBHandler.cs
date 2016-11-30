using System;
using RAP.Core.Common;
using RAP.Core.DataModels;
using System.Collections.Generic;

namespace RAP.DAL
{
    public interface IDashboardDBHandler
    {
        ReturnResult<List<ActivityStatus_M>> GetActivityStatusForCase(int C_ID);
    }
}
