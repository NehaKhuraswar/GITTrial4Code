using System;
using RAP.Core.Common;
namespace RAP.DAL
{
    public interface IAccountManagementDBHandler
    {
       ReturnResult<bool> AuthorizeCollaborator(RAP.Core.DataModels.CollaboratorAccessM access);
       ReturnResult<RAP.Core.DataModels.CustomerInfo> ChangePassword(RAP.Core.DataModels.CustomerInfo message);     
        bool CheckCustAccount(RAP.Core.DataModels.CustomerInfo message);
        ReturnResult<RAP.Core.DataModels.CityUserAccount_M> CreateCityUserAccount(RAP.Core.DataModels.CityUserAccount_M message);
        ReturnResult<RAP.Core.DataModels.CustomerInfo> EditCustomer(RAP.Core.DataModels.CustomerInfo message);
       ReturnResult<string> ForgetPwd(string email);
        ReturnResult<RAP.Core.DataModels.SearchResult> GetAccountSearch(RAP.Core.DataModels.AccountSearch accountSearch);
        ReturnResult<System.Collections.Generic.List<RAP.Core.DataModels.AccountType>> GetAccountTypes(int AccountTypeID);
       ReturnResult<RAP.Core.DataModels.Collaborator> GetAuthorizedUsers(int custID);
        ReturnResult<RAP.Core.DataModels.CityUserAccount_M> GetCityUser(RAP.Core.DataModels.CityUserAccount_M message);
        ReturnResult<RAP.Core.DataModels.CityUserAccount_M> GetCityUser(int UserID);
        ReturnResult<RAP.Core.DataModels.CityUserAccount_M> GetCityUserFromID(int CityUserID);
       ReturnResult<RAP.Core.DataModels.CustomerInfo> GetCustomer(RAP.Core.DataModels.CustomerInfo message);
        ReturnResult<RAP.Core.DataModels.CustomerInfo> GetCustomer(int CustomerID);
        ReturnResult<System.Collections.Generic.List<RAP.Core.DataModels.StateM>> GetStateList();
        ReturnResult<RAP.Core.DataModels.ThirdPartyInfoM> GetThirdPartyInfo(int CustomerID);
        ReturnResult<RAP.Core.DataModels.TranslationServiceInfoM> GetTranslationServiceInfo(int CustomerID);
        ReturnResult<bool> RemoveThirdParty(int CustID, int thirdPartyRepresentationID);
        ReturnResult<string> ResendPin(RAP.Core.DataModels.CustomerInfo message);
        ReturnResult<RAP.Core.DataModels.CustomerInfo> SaveCustomer(RAP.Core.DataModels.CustomerInfo message);
        ReturnResult<bool> DeleteCustomer(RAP.Core.DataModels.CustomerInfo message);
       ReturnResult<bool> SaveOrUpdateThirdPartyInfo(RAP.Core.DataModels.ThirdPartyInfoM model);
       ReturnResult<bool> SaveTranslationServiceInfo(RAP.Core.DataModels.TranslationServiceInfoM model);
       ReturnResult<RAP.Core.DataModels.ThirdPartyInfoM> RemoveThirdPartyInfo(RAP.Core.DataModels.ThirdPartyInfoM model);
        ReturnResult<RAP.Core.DataModels.CustomerInfo> SearchInviteCollaborator(string message);
    }
}
