using System;
using RAP.Core.Common;
using RAP.Core.DataModels;

namespace RAP.DAL
{
   public interface ICommonDBHandler
    {
       ReturnResult<UserInfoM> SaveUserInfo(UserInfoM userInfo);
       ReturnResult<UserInfoM> GetUserInfo(int UserId);
       void SaveErrorLog(OperationStatus status);
       CustomDate GetDateFromDatabase(DateTime DatabaseDate);
       ReturnResult<DocumentM> SaveDocument(DocumentM doc);
       
    }
}
