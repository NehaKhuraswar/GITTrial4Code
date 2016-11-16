using System;
using RAP.Core.Common;
using RAP.Core.DataModels;

namespace RAP.DAL
{
   public interface ICommonDBHandler
    {
       ReturnResult<int> SaveUserInfo(UserInfoM userInfo);
    }
}
