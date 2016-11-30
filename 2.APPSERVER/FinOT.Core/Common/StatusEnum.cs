﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.Common
{
    public enum StatusEnum
    {
        Success,

        #region Data
        NoDataFound,
        DatabaseException,
        AccountAlreadyExist,
        DatabaseMessage,
        #endregion
        
        #region Exceptions
        SystemException,
        InvalidArgumentException,
        NullArgumentException,
        TimeoutException,
        FaultException,
        CommunicationException,
        AuthenticationFailed,
        #endregion
        #region System
        UploadFailed,
        #endregion
    }
}
