using System;
using RAP.Core.Common;
using RAP.Core.DataModels;

namespace RAP.DAL
{
   public interface ICommonDBHandler
    {
       ReturnResult<UserInfoM> SaveUserInfo(UserInfoM userInfo);
       ReturnResult<UserInfoM> GetUserInfo(int UserId);
       void SaveErrorDetails(string ErrorNumber, string ErrorMessage, string ErrorMessageDetails, int CustID, string OperationName);
    }
}
