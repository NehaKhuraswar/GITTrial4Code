using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RAP.Core.Common;
using RAP.Core.DataModels;

namespace RAP.DAL
{
    public class ApplicationProcessingDBHandler : IApplicationProcessingDBHandler
    {
        private readonly string _connString;
        private readonly ApplicationProcessingDataContext _dbContext;
        private readonly AccountManagementDataContext _dbAccount;
       
        private ICommonDBHandler _commondbHandler;
        private IDashboardDBHandler _dashboarddbHandler;
        private IExceptionHandler _eHandler;
        public ApplicationProcessingDBHandler(ICommonDBHandler commondbHandler, IDashboardDBHandler dashboarddbHandler, IExceptionHandler eHandler)
        {
            this._commondbHandler = commondbHandler;
            this._dashboarddbHandler = dashboarddbHandler;
            this._eHandler = eHandler;
            _dbContext = new ApplicationProcessingDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
            _dbAccount = new AccountManagementDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
        }
      
        #region "Get"
        /// <summary>
        /// Model to set Left Navigation Class
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ReturnResult<PetitionPageSubnmissionStatusM> GetPageSubmissionStatus(int CustomerID)
        {
            ReturnResult<PetitionPageSubnmissionStatusM> result = new ReturnResult<PetitionPageSubnmissionStatusM>();
            PetitionPageSubnmissionStatusM model = new PetitionPageSubnmissionStatusM();
            try
            {
                var tPetition = _dbContext.TenantPetitionPageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
                if (tPetition != null)
                {
                    model.TenantPetition.ImportantInformation =  Convert.ToBoolean(tPetition.ImportantInformation);
                    model.TenantPetition.ApplicantInformation = Convert.ToBoolean(tPetition.ApplicantInformation);
                    model.TenantPetition.GroundsForPetition = Convert.ToBoolean(tPetition.GroundsForPetition);
                    model.TenantPetition.RentHistory = Convert.ToBoolean(tPetition.RentHistory);
                    model.TenantPetition.LostService = Convert.ToBoolean(tPetition.LostService);
                    model.TenantPetition.AdditionalDocumentation = Convert.ToBoolean(tPetition.AdditionalDocumentation);
                    model.TenantPetition.Review = Convert.ToBoolean(tPetition.Review);
                    model.TenantPetition.Verification = Convert.ToBoolean(tPetition.Verification);
                }
                var oPetition = _dbContext.OwnerPetitionPageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
                if (oPetition != null)
                {
                    model.OwnerPetition.ImportantInformation = Convert.ToBoolean(oPetition.ImportantInformation);
                    model.OwnerPetition.ApplicantInformation = Convert.ToBoolean(oPetition.ApplicantInformation);
                    model.OwnerPetition.JustificationForRentIncrease = Convert.ToBoolean(oPetition.JustificationForRentIncrease);
                    model.OwnerPetition.RentalProperty = Convert.ToBoolean(oPetition.RentalProperty);
                    model.OwnerPetition.RentHistory = Convert.ToBoolean(oPetition.RentHistory);
                    model.OwnerPetition.AdditionalDocumentation = Convert.ToBoolean(oPetition.AdditionalDocumentation);
                    model.OwnerPetition.Review = Convert.ToBoolean(oPetition.Review);
                    model.OwnerPetition.Verification = Convert.ToBoolean(oPetition.Verification);
                }
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }
        

        /// <summary>
        /// Gets the data needed to to display on the tenant petition form
        /// </summary>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> GetCaseDetails()
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            result.result = new CaseInfoM();
            TenantPetitionInfoM _petition = new TenantPetitionInfoM();
            TenantAppealInfoM _appeal = new TenantAppealInfoM();
            List<UnitTypeM> _units = new List<UnitTypeM>();
            List<CurrentOnRentM> _rentStatusItems = new List<CurrentOnRentM>();
            List<PetitionGroundM> _petitionGrounds = new List<PetitionGroundM>();
            List<AppealGroundM> _appealGrounds = new List<AppealGroundM>();
            List<NumberRangeForUnitsM> _rangeOfUnits = new List<NumberRangeForUnitsM>();
            try
            {

                var units = _dbContext.UnitTypes;
                if (units == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var unit in units)
                    {
                        UnitTypeM _unit = new UnitTypeM();
                        _unit.UnitTypeID = unit.UnitTypeID;
                        _unit.UnitDescription = unit.Description;
                        _units.Add(_unit);
                    }

                }

                if (_units.Count == 0)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }

                var rentStausItems = _dbContext.CurrentOnRentStatus;
                if (rentStausItems == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var rentStatusItem in rentStausItems)
                    {
                        CurrentOnRentM _rentStatusItem = new CurrentOnRentM();
                        _rentStatusItem.StatusID = rentStatusItem.RentStatusID;
                        _rentStatusItem.Status = rentStatusItem.RentStatus;
                        _rentStatusItems.Add(_rentStatusItem);
                    }
                }

                if (_rentStatusItems.Count == 0)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }

                var petitionGrounds = _dbContext.PetitionGrounds;
                if (petitionGrounds == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var petitionGround in petitionGrounds)
                    {
                        PetitionGroundM _petitionGround = new PetitionGroundM();
                        _petitionGround.PetitionGroundID = petitionGround.PetitionGroundID;
                        _petitionGround.PetitionGroundDescription = petitionGround.PetitionDescription;
                        _petitionGrounds.Add(_petitionGround);
                    }
                }
               
                var rangeDB = _dbContext.NumberRangeForUnits.ToList();
                if (rangeDB == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var item in rangeDB)
                    {
                        NumberRangeForUnitsM obj = new NumberRangeForUnitsM();
                        obj.RangeID = item.RangeID;
                        obj.RangeDesc = item.RangeDesc;
                        _rangeOfUnits.Add(obj);
                    }
                }

                
                var appealGrounds = _dbContext.AppealGrounds;
                if (appealGrounds == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var appealGround in appealGrounds)
                    {
                        AppealGroundM _appealGround = new AppealGroundM();
                        _appealGround.AppealGroundID = appealGround.AppealGroundID;
                        _appealGround.AppealDescription = appealGround.AppealDescription;
                        _appealGrounds.Add(_appealGround);
                    }
                }

                if (_petitionGrounds.Count == 0)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }

                _petition.UnitTypes = _units;
                _petition.CurrentOnRent = _rentStatusItems;
                _petition.PetitionGrounds = _petitionGrounds;
                _petition.RangeOfUnits = _rangeOfUnits;
                _appeal.AppealGrounds = _appealGrounds;
                result.result.TenantPetitionInfo = _petition;
                result.result.TenantAppealInfo = _appeal;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        ///<summary>
        ///Get the Case Details based upon Case ID
        ///</summary>
        ///<param name="caseID"></param>
        /// <returns></returns>
        //Get Review Tenant Petition
        public ReturnResult<CaseInfoM> GetCaseInfo(string CaseID)
        {
            ReturnResult<TenantPetitionInfoM> tenantPetitionResult = new ReturnResult<TenantPetitionInfoM>();
            ReturnResult<List<PetitionGroundM>> GroundsResult = new ReturnResult<List<PetitionGroundM>>();
            ReturnResult<TenantRentalHistoryM> RentalHistoryResult = new ReturnResult<TenantRentalHistoryM>();
            ReturnResult<LostServicesPageM> LostServicesResult = new ReturnResult<LostServicesPageM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                if(_dbContext == null )
                {
                     result.result = null;
                     result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                     return result;
                }
                var CaseDetailsDB = _dbContext.CaseDetails.Where(x => x.CaseID == CaseID).FirstOrDefault();
                if (CaseDetailsDB == null)
                {
                    result.result = null;
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                CaseInfoM caseInfo = new CaseInfoM();
                caseInfo.C_ID = CaseDetailsDB.C_ID;
                caseInfo.CaseID = CaseDetailsDB.CaseID;
                caseInfo.AppealDate = Convert.ToDateTime(CaseDetailsDB.AppealDate);

                AccountManagementDBHandler accDBHandler = new AccountManagementDBHandler();
                if (accDBHandler != null && CaseDetailsDB.CityAnalystUserID != null)
                {
                    caseInfo.CityAnalyst = accDBHandler.GetCityUser((int)CaseDetailsDB.CityAnalystUserID).result;
                }

                int PetitionCategoryID = (int)CaseDetailsDB.PetitionCategoryID;
                if(PetitionCategoryID == 1)
                {
                    var PetitionDetailsDB = _dbContext.PetitionDetails.Where(x => x.PetitionID == CaseDetailsDB.PetitionID).FirstOrDefault();

                    tenantPetitionResult = GetCaseTenantApplicationInfo((int)PetitionDetailsDB.TenantPetitionID);
                    if (tenantPetitionResult.status.Status != StatusEnum.Success)
                        return result;

                    GroundsResult = GetPetitionGroundInfo((int)tenantPetitionResult.result.PetitionID);
                    if (GroundsResult != null)
                    {
                        tenantPetitionResult.result.PetitionGrounds = GroundsResult.result;
                        tenantPetitionResult.status = GroundsResult.status;
                        if (GroundsResult.status.Status != StatusEnum.Success)
                            return result;
                    }

                    RentalHistoryResult = GetRentalHistoryInfo((int)tenantPetitionResult.result.PetitionID);
                    if (RentalHistoryResult != null)
                    {
                        tenantPetitionResult.result.TenantRentalHistory = RentalHistoryResult.result;
                        tenantPetitionResult.status = RentalHistoryResult.status;
                        if (RentalHistoryResult.status.Status != StatusEnum.Success)
                            return result;
                    }

                    LostServicesResult = GetTenantLostServiceInfo((int)tenantPetitionResult.result.PetitionID);
                    if (LostServicesResult != null)
                    {
                        tenantPetitionResult.result.LostServicesPage = LostServicesResult.result;
                        tenantPetitionResult.status = LostServicesResult.status;
                        if (LostServicesResult.status.Status != StatusEnum.Success)
                            return result;
                    }
                    caseInfo.TenantPetitionInfo = tenantPetitionResult.result;
                    result.result = caseInfo;
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                }

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                tenantPetitionResult.status = eHandler.HandleException(ex);
                return result;
            }
        }
        private ReturnResult<TenantPetitionInfoM> GetTenantPetition(int UserID, int a)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                var TenantPetitionInfoDB = _dbContext.TenantPetitionInfos.Where(x => x.PetitionFiledBy == UserID).FirstOrDefault();
                TenantPetitionInfoM tenantPetitionInfo = new TenantPetitionInfoM();
                if (TenantPetitionInfoDB != null)
                {
                    tenantPetitionInfo.PetitionID = TenantPetitionInfoDB.TenantPetitionID;
                    tenantPetitionInfo.bThirdPartyRepresentation = (bool)TenantPetitionInfoDB.bThirdPartyRepresentation;
                    if (tenantPetitionInfo.bThirdPartyRepresentation)
                    {
                        tenantPetitionInfo.ThirdPartyInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ThirdPartyUserID).result;
                    }
                    if (TenantPetitionInfoDB.OwnerUserID >= 1)
                    {
                        tenantPetitionInfo.OwnerInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.OwnerUserID).result;
                    }
                    if (TenantPetitionInfoDB.PropertyManagerUserID >= 1)
                    {
                        tenantPetitionInfo.PropertyManager = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.PropertyManagerUserID).result;
                    }

                    tenantPetitionInfo.NumberOfUnits = (int)TenantPetitionInfoDB.NumberOfUnits;
                    tenantPetitionInfo.UnitTypeId = TenantPetitionInfoDB.UnitTypeID;
                    tenantPetitionInfo.bCurrentRentStatus= TenantPetitionInfoDB.bRentStatus;
                    tenantPetitionInfo.ProvideExplanation = TenantPetitionInfoDB.ProvideExplanation;
                }

                result.result = tenantPetitionInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

        ///<summary>
        ///Get the Tenant Petition based upon Petition ID
        ///</summary>
        ///<param name="PetitionId"></param>
        /// <returns></returns>
        private ReturnResult<TenantPetitionInfoM> GetTenantPetition(int PetitionId)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                var TenantPetitionInfoDB = _dbContext.TenantPetitionInfos.Where(x => x.TenantPetitionID == PetitionId).FirstOrDefault();
                TenantPetitionInfoM tenantPetitionInfo = new TenantPetitionInfoM();
                if (TenantPetitionInfoDB != null)
                {

                    tenantPetitionInfo.bThirdPartyRepresentation =(bool) TenantPetitionInfoDB.bThirdPartyRepresentation;
                    if(tenantPetitionInfo.bThirdPartyRepresentation)
                    {
                        tenantPetitionInfo.ThirdPartyInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ThirdPartyUserID).result;
                    }
                    tenantPetitionInfo.OwnerInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.OwnerUserID).result;
                    tenantPetitionInfo.PropertyManager = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.PropertyManagerUserID).result;
                }
                tenantPetitionInfo.NumberOfUnits = (int)TenantPetitionInfoDB.NumberOfUnits;
                tenantPetitionInfo.UnitTypeId = TenantPetitionInfoDB.UnitTypeID;
                tenantPetitionInfo.bCurrentRentStatus = TenantPetitionInfoDB.bRentStatus;
                tenantPetitionInfo.ProvideExplanation = TenantPetitionInfoDB.ProvideExplanation;
                
                result.result = tenantPetitionInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        // Get cases for the staff dashboard which doesnot have analyst or the hearing officer assigned
        public ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst()
        {
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try 
            {
                List<CaseInfoM> cases = new List<CaseInfoM>();
                var casesDB = _dbContext.CaseDetails.Where(x => x.CityAnalystUserID == null || x.HearingOfficerUserID == null).ToList();
                foreach(var item in casesDB)
                {
                    CaseInfoM caseinfo = new CaseInfoM();
                    caseinfo.CaseID = item.CaseID;
                    caseinfo.C_ID = item.C_ID;
                    if (item.CityAnalystUserID != null)
                    {
                        caseinfo.CityAnalyst.UserID = (int)item.CityAnalystUserID;
                    }
                    if (item.HearingOfficerUserID != null)
                    {
                        caseinfo.HearingOfficer.UserID = (int)item.HearingOfficerUserID;
                    }
                    caseinfo.CreatedDate = Convert.ToDateTime(item.CreatedDate);
                    caseinfo.LastModifiedDate = Convert.ToDateTime(item.LastModifiedDate);
                    

                    // Get the petition applicant info
                    var petitionDetailsDb =  _dbContext.PetitionDetails.Where(x => x.PetitionID == item.PetitionID).FirstOrDefault();

                    if(petitionDetailsDb.TenantPetitionID != null)
                    {
                        var TenantPetitionDB = _dbContext.TenantPetitionInfos.Where(x => x.TenantPetitionID == petitionDetailsDb.TenantPetitionID).FirstOrDefault();
                        ReturnResult<UserInfoM> applicantUser = _commondbHandler.GetUserInfo((int)TenantPetitionDB.ApplicantUserID);
                        if(applicantUser != null)
                        {
                            caseinfo.TenantPetitionInfo.ApplicantUserInfo = applicantUser.result;
                        }
                    }
                    else if(petitionDetailsDb.OwnerPetitionID != null)
                    {
                        var OwnerPetitionDB = _dbContext.OwnerPetitionInfos.Where(x => x.OwnerPetitionID == petitionDetailsDb.OwnerPetitionID).FirstOrDefault();
                        ReturnResult<UserInfoM> applicantUser = _commondbHandler.GetUserInfo((int)OwnerPetitionDB.OwnerPetitionApplicantInfoID);
                        if (applicantUser != null)
                        {
                            caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo = applicantUser.result;
                        }
                    }
                    cases.Add(caseinfo);
                }
                result.result = cases;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

        }

        // Get cases for the individual customer ID
        public ReturnResult<List<CaseInfoM>> GetCasesForCustomer(int CustomerID)
        {
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try
            {
                List<CaseInfoM> cases = new List<CaseInfoM>();
                var casesDB = _dbContext.CaseDetails.Where(x => x.CaseFiledBy == CustomerID).ToList();
                foreach (var item in casesDB)
                {
                    CaseInfoM caseinfo = new CaseInfoM();
                    caseinfo.CaseID = item.CaseID;
                    caseinfo.C_ID = item.C_ID;
                    caseinfo.PetitionCategoryID = (int) item.PetitionCategoryID;
                    if (item.CityAnalystUserID != null)
                    {
                        caseinfo.CityAnalyst.UserID = (int)item.CityAnalystUserID;
                    }
                    if (item.HearingOfficerUserID != null)
                    {
                        caseinfo.HearingOfficer.UserID = (int)item.HearingOfficerUserID;
                    }
                    caseinfo.CreatedDate = Convert.ToDateTime(item.CreatedDate);
                    caseinfo.LastModifiedDate = Convert.ToDateTime(item.LastModifiedDate);


                    // Get the petition applicant info
                    var petitionDetailsDb = _dbContext.PetitionDetails.Where(x => x.PetitionID == item.PetitionID).FirstOrDefault();

                    if (petitionDetailsDb.TenantPetitionID != null)
                    {
                        var TenantPetitionDB = _dbContext.TenantPetitionInfos.Where(x => x.TenantPetitionID == petitionDetailsDb.TenantPetitionID).FirstOrDefault();
                        ReturnResult<UserInfoM> applicantUser = _commondbHandler.GetUserInfo((int)TenantPetitionDB.ApplicantUserID);
                        if (applicantUser != null)
                        {
                            caseinfo.TenantPetitionInfo.ApplicantUserInfo = applicantUser.result;
                        }
                    }
                    else if (petitionDetailsDb.OwnerPetitionID != null)
                    {
                        var OwnerPetitionDB = _dbContext.OwnerPetitionInfos.Where(x => x.OwnerPetitionID == petitionDetailsDb.OwnerPetitionID).FirstOrDefault();
                        ReturnResult<UserInfoM> applicantUser = _commondbHandler.GetUserInfo((int)OwnerPetitionDB.OwnerPetitionApplicantInfoID);
                        if (applicantUser != null)
                        {
                            caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo = applicantUser.result;
                        }
                    }
                    caseinfo.ActivityStatus =  _dashboarddbHandler.GetActivityStatusForCase(item.C_ID).result;

                    cases.Add(caseinfo);
                }
                result.result = cases;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

        }
        private ReturnResult<TenantPetitionInfoM> GetCaseTenantApplicationInfo(int PetitionID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                List<UnitTypeM> _units = new List<UnitTypeM>();
                var units = _dbContext.UnitTypes;
                if (units == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var unit in units)
                    {
                        UnitTypeM _unit = new UnitTypeM();
                        _unit.UnitTypeID = unit.UnitTypeID;
                        _unit.UnitDescription = unit.Description;
                        _units.Add(_unit);
                    }

                }
                var TenantPetitionInfoDB = _dbContext.TenantPetitionInfos.Where(x => x.TenantPetitionID == PetitionID
                                                && x.IsSubmitted == true).FirstOrDefault();
                TenantPetitionInfoM tenantPetitionInfo = new TenantPetitionInfoM();
                if (TenantPetitionInfoDB != null)
                {
                    tenantPetitionInfo.PetitionID = TenantPetitionInfoDB.TenantPetitionID;
                    tenantPetitionInfo.bThirdPartyRepresentation = (bool)TenantPetitionInfoDB.bThirdPartyRepresentation;
                    if (tenantPetitionInfo.bThirdPartyRepresentation)
                    {
                        tenantPetitionInfo.ThirdPartyInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ThirdPartyUserID).result;
                    }
                    tenantPetitionInfo.ApplicantUserInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ApplicantUserID).result;
                    tenantPetitionInfo.OwnerInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.OwnerUserID).result;
                    tenantPetitionInfo.PropertyManager = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.PropertyManagerUserID).result;
                    if (tenantPetitionInfo.OwnerInfo.UserID == tenantPetitionInfo.PropertyManager.UserID)
                    {
                        tenantPetitionInfo.bSameAsOwnerInfo = true;
                    }
                    tenantPetitionInfo.NumberOfUnits = (int)TenantPetitionInfoDB.NumberOfUnits;
                    tenantPetitionInfo.UnitTypeId = TenantPetitionInfoDB.UnitTypeID;
                    tenantPetitionInfo.bCurrentRentStatus = TenantPetitionInfoDB.bRentStatus;
                    tenantPetitionInfo.ProvideExplanation = TenantPetitionInfoDB.ProvideExplanation;
                    tenantPetitionInfo.CustomerID =(int) TenantPetitionInfoDB.PetitionFiledBy;                    
                }
                tenantPetitionInfo.UnitTypes = _units;

                result.result = tenantPetitionInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                List<UnitTypeM> _units = new List<UnitTypeM>();
                var units = _dbContext.UnitTypes;
                if (units == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var unit in units)
                    {
                        UnitTypeM _unit = new UnitTypeM();
                        _unit.UnitTypeID = unit.UnitTypeID;
                        _unit.UnitDescription = unit.Description;
                        _units.Add(_unit);
                    }

                }
                var TenantPetitionInfoDB = _dbContext.TenantPetitionInfos.Where(x => x.PetitionFiledBy == CustomerID
                                                && x.IsSubmitted == false).FirstOrDefault();
                TenantPetitionInfoM tenantPetitionInfo = new TenantPetitionInfoM();
                if (TenantPetitionInfoDB != null)
                {
                    tenantPetitionInfo.PetitionID = TenantPetitionInfoDB.TenantPetitionID;
                    tenantPetitionInfo.bThirdPartyRepresentation = (bool)TenantPetitionInfoDB.bThirdPartyRepresentation;
                    if (tenantPetitionInfo.bThirdPartyRepresentation)
                    {
                        tenantPetitionInfo.ThirdPartyInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ThirdPartyUserID).result;
                    }
                    tenantPetitionInfo.ApplicantUserInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ApplicantUserID).result;
                    tenantPetitionInfo.OwnerInfo = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.OwnerUserID).result;
                    tenantPetitionInfo.PropertyManager = _commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.PropertyManagerUserID).result;
                    if (tenantPetitionInfo.OwnerInfo.UserID == tenantPetitionInfo.PropertyManager.UserID)
                    {
                        tenantPetitionInfo.bSameAsOwnerInfo = true;
                    }
                    tenantPetitionInfo.NumberOfUnits = (int)TenantPetitionInfoDB.NumberOfUnits;
                    tenantPetitionInfo.UnitTypeId = TenantPetitionInfoDB.UnitTypeID;
                    tenantPetitionInfo.bCurrentRentStatus = TenantPetitionInfoDB.bRentStatus;
                    tenantPetitionInfo.ProvideExplanation = TenantPetitionInfoDB.ProvideExplanation;
                    tenantPetitionInfo.CustomerID =(int) TenantPetitionInfoDB.PetitionFiledBy;                    
                }
                tenantPetitionInfo.UnitTypes = _units;

                result.result = tenantPetitionInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId)
        {
            ReturnResult<TenantRentalHistoryM> result = new ReturnResult<TenantRentalHistoryM>();
            TenantRentalHistoryM tenantRentalHistory = new TenantRentalHistoryM();
            try
            {
                var TenantRentalHistoryDB = _dbContext.TenantRentalHistories.Where(x => x.PetitionID == PetitionId).FirstOrDefault();
                if (TenantRentalHistoryDB != null)
                {
                    tenantRentalHistory.PetitionID = TenantRentalHistoryDB.PetitionID;
                    tenantRentalHistory.InitialRent = TenantRentalHistoryDB.InitialRent;
                    tenantRentalHistory.bRAPNoticeGiven = TenantRentalHistoryDB.bRAPNoticeGiven;
                    tenantRentalHistory.bRentControlledByAgency = TenantRentalHistoryDB.bRentControlledByAgency;
                    tenantRentalHistory.MoveInDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(TenantRentalHistoryDB.MoveInDate));
                    tenantRentalHistory.RAPNoticeGivenDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(TenantRentalHistoryDB.RAPNoticeGivenDate));
                    //tenantRentalHistory.PreviousCaseIDs = TenantRentalHistoryDB.P;

                    var TenantRentalIncrementInfoDB = _dbContext.TenantRentalIncrementInfos.Where(x => x.TenantPetitionID == PetitionId).ToList();
                    foreach (var item in TenantRentalIncrementInfoDB)
                    {
                        TenantRentIncreaseInfoM objTenantRentIncreaseInfoM = new TenantRentIncreaseInfoM();
                        objTenantRentIncreaseInfoM.bRentIncreaseNoticeGiven = Convert.ToBoolean(item.bRentIncreaseNoticeGiven);
                        objTenantRentIncreaseInfoM.RentIncreaseNoticeDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseNoticeDate));
                        objTenantRentIncreaseInfoM.RentIncreaseEffectiveDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseEffectiveDate));
                        objTenantRentIncreaseInfoM.RentIncreasedFrom = Convert.ToDecimal(item.RentIncreasedFrom);
                        objTenantRentIncreaseInfoM.RentIncreasedTo = Convert.ToDecimal(item.RentIncreasedTo);
                        objTenantRentIncreaseInfoM.bRentIncreaseContested = Convert.ToBoolean(item.bRentIncreaseContested);

                        tenantRentalHistory.RentIncreases.Add(objTenantRentIncreaseInfoM);
                    }
                }

                result.result = tenantRentalHistory;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

        }
        

        // getting the lost services and problems for the tenant petition form
        public ReturnResult<LostServicesPageM> GetTenantLostServiceInfo(int PetitionID)
        {
            ReturnResult<LostServicesPageM> result = new ReturnResult<LostServicesPageM>();
            LostServicesPageM obj = new LostServicesPageM();
            List<TenantLostServiceInfoM> tenantLostServiceInfo = new List<TenantLostServiceInfoM>();
            try
            {
                
                    var TenantLostServiceInfoDB = _dbContext.TenantLostServiceInfos.Where(x => x.TenantPetitionID == PetitionID).ToList();
                    foreach (var item in TenantLostServiceInfoDB)
                    {
                        TenantLostServiceInfoM objTenantLostServiceInfoM = new TenantLostServiceInfoM();

                        objTenantLostServiceInfoM.ReducedServiceDescription = item.ReducedServiceDescription;
                        objTenantLostServiceInfoM.EstimatedLoss = Convert.ToDecimal(item.EstimatedLoss);

                        objTenantLostServiceInfoM.LossBeganDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.LossBeganDate));
                        
                        tenantLostServiceInfo.Add(objTenantLostServiceInfoM);
                    }
                    obj.LostServices = tenantLostServiceInfo;

                    if(tenantLostServiceInfo.Count() > 0)
                    {
                        obj.bLostService = true;
                    }


                    List<TenantProblemInfoM> Problems = GetTenantProblemInfo(PetitionID).result;
                    obj.Problems = Problems;
                    if (Problems.Count() > 0)
                    {
                        obj.bProblem = true;
                    }

                    result.result = obj;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

        //Get Review Tenant Petition
        public ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int CustomerID)
        {
            ReturnResult<TenantPetitionInfoM> tenantPetitionResult = new ReturnResult<TenantPetitionInfoM>();
            ReturnResult<List<PetitionGroundM>> GroundsResult = new ReturnResult<List<PetitionGroundM>>();
            ReturnResult<TenantRentalHistoryM> RentalHistoryResult = new ReturnResult<TenantRentalHistoryM>();
            ReturnResult<LostServicesPageM> LostServicesResult = new ReturnResult<LostServicesPageM>();

            try
            {
                tenantPetitionResult = GetTenantApplicationInfo(CustomerID);
                if (tenantPetitionResult.status.Status != StatusEnum.Success)
                    return tenantPetitionResult;

                GroundsResult = GetPetitionGroundInfo((int)tenantPetitionResult.result.PetitionID);
                if (GroundsResult != null)
                {
                    tenantPetitionResult.result.PetitionGrounds = GroundsResult.result;
                    tenantPetitionResult.status = GroundsResult.status;
                    if (GroundsResult.status.Status != StatusEnum.Success)
                        return tenantPetitionResult;
                }

                RentalHistoryResult = GetRentalHistoryInfo((int)tenantPetitionResult.result.PetitionID);
                if (RentalHistoryResult != null)
                {
                    tenantPetitionResult.result.TenantRentalHistory = RentalHistoryResult.result;
                    tenantPetitionResult.status = RentalHistoryResult.status;
                    if (RentalHistoryResult.status.Status != StatusEnum.Success)
                        return tenantPetitionResult;
                }

                LostServicesResult = GetTenantLostServiceInfo((int)tenantPetitionResult.result.PetitionID);
                if (LostServicesResult != null)
                {
                    tenantPetitionResult.result.LostServicesPage = LostServicesResult.result;
                    tenantPetitionResult.status = LostServicesResult.status;
                    if (LostServicesResult.status.Status != StatusEnum.Success)
                        return tenantPetitionResult;
                }

                return tenantPetitionResult;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                tenantPetitionResult.status = eHandler.HandleException(ex);
                return tenantPetitionResult;
            }        
        }
        private ReturnResult<List<TenantProblemInfoM>> GetTenantProblemInfo(int PetitionID)
        {
            ReturnResult<List<TenantProblemInfoM>> result = new ReturnResult<List<TenantProblemInfoM>>();
            List<TenantProblemInfoM> tenantProblemInfo = new List<TenantProblemInfoM>();
            try
            {
                
                    var TenantProblemInfoDB = _dbContext.TenantProblemInfos.Where(x => x.TenantPetitionID == PetitionID).ToList();
                    foreach (var item in TenantProblemInfoDB)
                    {
                        TenantProblemInfoM objTenantProblemInfoM = new TenantProblemInfoM();
                        objTenantProblemInfoM.ProblemDescription = item.ProblemDescription;
                        objTenantProblemInfoM.EstimatedLoss = Convert.ToDecimal(item.EstimatedLoss);
                        objTenantProblemInfoM.ProblemBeganDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.ProblemBeganDate));

                        tenantProblemInfo.Add(objTenantProblemInfoM);
                    }
                
                result.result = tenantProblemInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Get Petition ground info
        /// </summary>
        /// <param name="petitionID"></param>
        /// <returns>Petition Ground Object</returns>
        public ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID)
        {
            ReturnResult<List<PetitionGroundM>> result = new ReturnResult<List<PetitionGroundM>>();
            List<PetitionGroundM> PetitionGroundInfo = new List<PetitionGroundM>();
            try
            {

                var petitionGrounds = _dbContext.PetitionGrounds;
                if (petitionGrounds == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var petitionGround in petitionGrounds)
                    {
                        PetitionGroundM _petitionGround = new PetitionGroundM();
                        _petitionGround.PetitionGroundID = petitionGround.PetitionGroundID;
                        _petitionGround.PetitionGroundDescription = petitionGround.PetitionDescription;
                        PetitionGroundInfo.Add(_petitionGround);
                    }
                }
                var TenantPetitionGroundInfoDB = _dbContext.TenantPetitionGroundInfos.Where(x => x.TenantPetitionID == petitionID).ToList();
                foreach (var item in TenantPetitionGroundInfoDB)
                {
                    foreach (var item1 in PetitionGroundInfo)
                    {
                        if (item1.PetitionGroundID == item.PetitionGroundID)
                        {
                            item1.Selected = true;
                        }
                    }
                }

                result.result = PetitionGroundInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
      


        public ReturnResult<CaseInfoM> GetCaseDetails(int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            result.result = new CaseInfoM();
            CaseInfoM caseInfo = new CaseInfoM();           
            try
            {  
                
                  
                caseInfo.TenantPetitionInfo = GetTenantPetition(UserID, 0).result;

                var units = _dbContext.UnitTypes;
                if (units == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var unit in units)
                    {
                        UnitTypeM _unit = new UnitTypeM();
                        _unit.UnitTypeID = unit.UnitTypeID;
                        _unit.UnitDescription = unit.Description;
                        caseInfo.TenantPetitionInfo.UnitTypes.Add(_unit);
                    }

                }


                var rentStausItems = _dbContext.CurrentOnRentStatus;
                if (rentStausItems == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                else
                {
                    foreach (var rentStatusItem in rentStausItems)
                    {
                        CurrentOnRentM _rentStatusItem = new CurrentOnRentM();
                        _rentStatusItem.StatusID = rentStatusItem.RentStatusID;
                        _rentStatusItem.Status = rentStatusItem.RentStatus;
                        caseInfo.TenantPetitionInfo.CurrentOnRent.Add(_rentStatusItem);
                    }
                }

               
                caseInfo.TenantPetitionInfo.PetitionGrounds = GetPetitionGroundInfo(caseInfo.TenantPetitionInfo.PetitionID).result;
                //caseInfo.TenantPetitionInfo.LostServices = GetTenantLostServiceInfo(caseInfo.TenantPetitionInfo.PetitionID).result;
               
                result.result = caseInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        private ReturnResult<TenantAppealInfoM> GetAppealInfo(int appealID)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
       //     result.result.AppealGrounds = GetAppealGroundInfo(appealID).result;

            return result;

        }
         public ReturnResult<CaseInfoM> GetAppealServe(int AppealID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            CaseInfoM caseInfo = new CaseInfoM();
            ServeAppealM appealServe = new ServeAppealM();
            try
            {
                var appealServeDB = _dbContext.ServeAppeals.Where(x => x.AppealID == AppealID).FirstOrDefault();
                if (appealServeDB != null)
                {
                    appealServe.AppealID = (int)appealServeDB.AppealID;
                    appealServe.bAcknowledgeNamePin = (bool)appealServeDB.bAcknowledgeNamePin;
                    appealServe.bDeclaration = (bool)appealServeDB.bDeclaration;
                    appealServe.bThirdParty = (bool)appealServeDB.bThirdParty;
                    appealServe.PenaltyDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(appealServeDB.PenaltyDate));

                    var opposingPartiesDB = _dbContext.AppealOpposingParties.Where(x => x.AppealID == AppealID).ToList();
                    if(opposingPartiesDB != null)
                    {
                        foreach(var item in opposingPartiesDB)
                        {
                            appealServe.OpposingParty.Add(_commondbHandler.GetUserInfo((int)item.OpposingPartyID).result);
                        }
                    }

                }
                caseInfo.TenantAppealInfo.serveAppeal = appealServe;

                result.result = caseInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        ///<summary>
        ///Get appeal ground info based upon appealID
        ///</summary>
        ///<param name="appealID"></param>
        /// <returns></returns>
        public ReturnResult<List<AppealGroundM>> GetAppealGroundInfo(string CaseNumber, int AppealFiledBy)
        {
            ReturnResult<List<AppealGroundM>> result = new ReturnResult<List<AppealGroundM>>();
            List<AppealGroundM> AppealGroundInfo = new List<AppealGroundM>();
            try
            {
                
                    var appealGrounds = _dbContext.AppealGrounds;
                    if (appealGrounds == null)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                        return result;
                    }
                    else
                    {
                        foreach (var appealGround in appealGrounds)
                        {
                            AppealGroundM _appealGround = new AppealGroundM();
                            _appealGround.AppealGroundID = appealGround.AppealGroundID;
                            _appealGround.AppealDescription = appealGround.AppealDescription;
                            AppealGroundInfo.Add(_appealGround);
                        }
                    }
                    var appealInfoDb = _dbContext.TenantAppealDetails.Where(x => x.CaseNumber == CaseNumber && x.AppealFiledBy == AppealFiledBy
                                                                            && x.IsSubmitted == false).FirstOrDefault();
                    if (appealInfoDb != null)
                    {
                        var TenantAppealGroundInfoDB = _dbContext.TenantAppealGroundInfos
                                                                .Where(x => x.AppealID == appealInfoDb.AppealID).ToList();
                        foreach (var item in TenantAppealGroundInfoDB)
                        {
                            foreach (var item1 in AppealGroundInfo)
                            {
                                if (item1.AppealGroundID == item.AppealGroundID)
                                {
                                    item1.Selected = true;
                                }
                            }
                        }
                    }
                
                result.result = AppealGroundInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

        //Get Review Tenant Petition
        public ReturnResult<TenantAppealInfoM> GetTenantAppealReviewInfo(string CaseNumber, int AppealFiledBy)
        {
            ReturnResult<TenantAppealInfoM> tenantAppealResult = new ReturnResult<TenantAppealInfoM>();
            ReturnResult<ServeAppealM> ServeAppealResult = new ReturnResult<ServeAppealM>();
            ReturnResult<TenantRentalHistoryM> RentalHistoryResult = new ReturnResult<TenantRentalHistoryM>();
            ReturnResult<LostServicesPageM> LostServicesResult = new ReturnResult<LostServicesPageM>();
            
            try
            {
                tenantAppealResult.result.AppealGrounds = GetAppealGroundInfo(CaseNumber, AppealFiledBy).result;
                if (tenantAppealResult.status.Status != StatusEnum.Success)
                    return tenantAppealResult;

                ServeAppealResult.result = GetAppealServe((int)tenantAppealResult.result.AppealID).result.TenantAppealInfo.serveAppeal ;
                if (ServeAppealResult != null)
                {
                    tenantAppealResult.result.serveAppeal = ServeAppealResult.result;
                    tenantAppealResult.status = ServeAppealResult.status;
                    if (ServeAppealResult.status.Status != StatusEnum.Success)
                        return tenantAppealResult;
                }

                return tenantAppealResult;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                tenantAppealResult.status = eHandler.HandleException(ex);
                return tenantAppealResult;
            }
        }

        /// <summary>
        /// Files the petition details
        /// </summary>
        /// <param name="caseInfo"></param>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> SubmitAppeal(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
           
            try
            {
                var CaseInfoDB = _dbContext.CaseDetails.Where(x => x.CaseID == caseInfo.CaseID).FirstOrDefault();
                if (CaseInfoDB != null)
                {
                    CaseInfoDB.TenantAppealID = caseInfo.TenantAppealInfo.AppealID;
                    _dbContext.SubmitChanges();
                }

                var AppealDB = _dbContext.TenantAppealDetails.Where(x => x.AppealID == caseInfo.TenantAppealInfo.AppealID).FirstOrDefault();
                if (AppealDB != null)
                {
                    AppealDB.IsSubmitted = true;
                    _dbContext.SubmitChanges();
                }

                result.result = caseInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        // tbd to be removed
        /// <summary>
        /// Files the petition details
        /// </summary>
        /// <param name="caseInfo"></param>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            int petitionFileID = 0;
            int ownerUserID = 0;
            int thirdPartyUSerID = 0;
            try
            {
                petitionFileID = SaveTenantPetition(caseInfo.TenantPetitionInfo);
                if (petitionFileID == 0)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                    return result;
                }

                //ownerUserID = SaveUserInfo(caseInfo.OwnerInfo);
                //if (ownerUserID == 0)
                //{
                //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                //    return result;
                //}
                //if (caseInfo.bThirdPartyRepresentation)
                //{
                //    thirdPartyUSerID = SaveUserInfo(caseInfo.ThirdPartyInfo);
                //    if (thirdPartyUSerID == 0)
                //    {
                //        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                //        return result;
                //    }
                //}

               
                    CaseDetail caseDetailsDB = new CaseDetail();
                    caseDetailsDB.PetitionID = petitionFileID;
                    //TBD
                    caseDetailsDB.PetitionCategoryID = 1;
                    //TBD
                 
                    //caseDetailsDB.TenantUserID = caseInfo.TenantUserID;
             //       caseDetailsDB.bThirdPartyRepresentation = caseInfo.bThirdPartyRepresentation;
                   
                
                    //TBD
                    caseDetailsDB.CaseFiledBy = 1;
                    caseDetailsDB.bCaseFiledByThirdParty = caseInfo.bCaseFiledByThirdParty;
       
                    caseDetailsDB.CreatedDate = DateTime.Now;


                    _dbContext.CaseDetails.InsertOnSubmit(caseDetailsDB);
                    _dbContext.SubmitChanges();
                    caseInfo.CaseID = caseDetailsDB.CaseID;
                
                result.result = caseInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        private int GetPetitionID(int petitionID, int petitionCategory)
        {
            int _petitionID = 0;

            PetitionDetail petitionDetailsDB = new PetitionDetail();

            if (petitionCategory == 1)
            {
                petitionDetailsDB.TenantPetitionID = petitionID;
                _dbContext.PetitionDetails.InsertOnSubmit(petitionDetailsDB);
                _dbContext.SubmitChanges();
                _petitionID = petitionDetailsDB.PetitionID;
            }
            if (petitionCategory == 2)
            {
                petitionDetailsDB.OwnerPetitionID = petitionID;
                _dbContext.PetitionDetails.InsertOnSubmit(petitionDetailsDB);
                _dbContext.SubmitChanges();
                _petitionID = petitionDetailsDB.PetitionID;
            }

            return _petitionID;
        }
        #endregion "Get"
        #region "Save"
        /// <summary>
        /// Submit tenant petition
        /// </summary>
        /// <param name="caseInfo"></param>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
           
            try
            {
                PetitionDetail petitionDetail = new PetitionDetail();
                petitionDetail.TenantPetitionID = caseInfo.TenantPetitionInfo.PetitionID;
                _dbContext.PetitionDetails.InsertOnSubmit(petitionDetail);
                _dbContext.SubmitChanges();

                CaseDetail caseDetailsDB = new CaseDetail();
                caseDetailsDB.PetitionID = (int)petitionDetail.PetitionID;
                caseDetailsDB.PetitionCategoryID = 1;
                caseDetailsDB.CaseFiledBy = caseInfo.CaseFileBy;
                caseDetailsDB.CreatedDate = DateTime.Now;
                caseDetailsDB.LastModifiedByType = 3;
                caseDetailsDB.LastModifiedBy = caseInfo.CaseFileBy;
                caseDetailsDB.LastModifiedDate = DateTime.Now;  
                _dbContext.CaseDetails.InsertOnSubmit(caseDetailsDB);
                _dbContext.SubmitChanges();
                caseInfo.CaseID = caseDetailsDB.CaseID;
                caseInfo.C_ID = caseDetailsDB.C_ID;

                if (caseInfo.C_ID > 0)
                {
                    TenantPetitionInfo PetitionDB = _dbContext.TenantPetitionInfos.Where(x => x.TenantPetitionID == caseInfo.TenantPetitionInfo.PetitionID).FirstOrDefault();
                    if(PetitionDB != null)
                    {
                        PetitionDB.IsSubmitted = true;
                        _dbContext.SubmitChanges();
                    }

                    TenantPetitionPageSubmissionStatus PageStatus = _dbContext.TenantPetitionPageSubmissionStatus.Where(x => x.CustomerID == caseInfo.CaseFileBy).FirstOrDefault();
                    if(PageStatus != null)
                    {
                        _dbContext.TenantPetitionPageSubmissionStatus.DeleteOnSubmit(PageStatus);
                        _dbContext.SubmitChanges();
                    }
                    
                }

                result.result = caseInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        
        public ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();

            int ownerUserID = 0;
            int thirdPartyUserID = 0;
            int PropertyManagerUserID = 0;
            int applicantUserID = 0;
            try
            {
                CommonDBHandler _dbCommon = new CommonDBHandler();
                if (caseInfo.TenantPetitionInfo.PetitionID > 0)
                {

                    if (caseInfo.TenantPetitionInfo.bThirdPartyRepresentation)
                    {
                        thirdPartyUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.ThirdPartyInfo).result.UserID;
                        if (thirdPartyUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }

                    applicantUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.ApplicantUserInfo).result.UserID;
                    if (applicantUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    ownerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.OwnerInfo).result.UserID;
                    if (ownerUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    if (caseInfo.TenantPetitionInfo.bSameAsOwnerInfo)
                    {
                        PropertyManagerUserID = ownerUserID;
                    }
                    else
                    {
                        PropertyManagerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.PropertyManager).result.UserID;
                        if (PropertyManagerUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }

                    TenantPetitionInfo petitionDB = _dbContext.TenantPetitionInfos
                                                        .Where(x => x.IsSubmitted == false && x.TenantPetitionID == caseInfo.TenantPetitionInfo.PetitionID).FirstOrDefault();
                    petitionDB.bThirdPartyRepresentation = caseInfo.TenantPetitionInfo.bThirdPartyRepresentation;

                    petitionDB.ThirdPartyUserID = thirdPartyUserID;
                    petitionDB.ApplicantUserID = applicantUserID;
                    petitionDB.OwnerUserID = ownerUserID;
                    petitionDB.PropertyManagerUserID = PropertyManagerUserID;
                    petitionDB.NumberOfUnits = caseInfo.TenantPetitionInfo.NumberOfUnits;
                    petitionDB.UnitTypeID =  caseInfo.TenantPetitionInfo.UnitTypeId;
                    petitionDB.bRentStatus = caseInfo.TenantPetitionInfo.bCurrentRentStatus;
                    if (caseInfo.TenantPetitionInfo.bCurrentRentStatus == false)
                    {
                        petitionDB.ProvideExplanation = caseInfo.TenantPetitionInfo.ProvideExplanation;
                    }
                    petitionDB.CreatedDate = DateTime.Now;
                    petitionDB.PetitionFiledBy = caseInfo.TenantPetitionInfo.CustomerID;
                    petitionDB.IsSubmitted = false;
                    _dbContext.SubmitChanges();
                    caseInfo.TenantPetitionInfo.PetitionID = petitionDB.TenantPetitionID;

                    var PageStatus = _dbContext.TenantPetitionPageSubmissionStatus.Where(x => x.CustomerID == caseInfo.TenantPetitionInfo.CustomerID).FirstOrDefault();
                    if (PageStatus != null)
                    {
                        PageStatus.ApplicantInformation = true;
                        _dbContext.SubmitChanges();
                    }
                    else
                    {
                        var PageStatusNew = new TenantPetitionPageSubmissionStatus();
                        PageStatusNew.CustomerID = caseInfo.TenantPetitionInfo.CustomerID;
                        PageStatusNew.ApplicantInformation = true;
                        _dbContext.TenantPetitionPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                        _dbContext.SubmitChanges();
                    }
                }
                else
                {
                    if (caseInfo.TenantPetitionInfo.bThirdPartyRepresentation)
                    {
                        thirdPartyUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.ThirdPartyInfo).result.UserID;
                        if (thirdPartyUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }

                    applicantUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.ApplicantUserInfo).result.UserID;
                    if (applicantUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    ownerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.OwnerInfo).result.UserID;
                    if (ownerUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    if (caseInfo.TenantPetitionInfo.bSameAsOwnerInfo)
                    {
                        PropertyManagerUserID = ownerUserID;
                    }
                    else
                    {
                        PropertyManagerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantPetitionInfo.PropertyManager).result.UserID;
                        if (PropertyManagerUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }
                
                    TenantPetitionInfo petitionDB = new TenantPetitionInfo();
                    petitionDB.bThirdPartyRepresentation = caseInfo.TenantPetitionInfo.bThirdPartyRepresentation;

                    petitionDB.ThirdPartyUserID = thirdPartyUserID;
                    petitionDB.ApplicantUserID = applicantUserID;
                    petitionDB.OwnerUserID = ownerUserID;
                    petitionDB.PropertyManagerUserID = PropertyManagerUserID;
                    petitionDB.NumberOfUnits = caseInfo.TenantPetitionInfo.NumberOfUnits;
                    petitionDB.UnitTypeID =  caseInfo.TenantPetitionInfo.UnitTypeId;
                    petitionDB.bRentStatus = caseInfo.TenantPetitionInfo.bCurrentRentStatus;
                    if (caseInfo.TenantPetitionInfo.bCurrentRentStatus == false)
                    {
                        petitionDB.ProvideExplanation = caseInfo.TenantPetitionInfo.ProvideExplanation;
                    }
                    petitionDB.CreatedDate = DateTime.Now;
                    petitionDB.PetitionFiledBy = caseInfo.TenantPetitionInfo.CustomerID;
                    petitionDB.IsSubmitted = false;
                    _dbContext.TenantPetitionInfos.InsertOnSubmit(petitionDB);
                    _dbContext.SubmitChanges();
                    caseInfo.TenantPetitionInfo.PetitionID = petitionDB.TenantPetitionID;

                    var PageStatus = _dbContext.TenantPetitionPageSubmissionStatus.Where(x=>x.CustomerID == caseInfo.TenantPetitionInfo.CustomerID).FirstOrDefault();
                    if (PageStatus != null)
                    {
                        PageStatus.ApplicantInformation = true;
                        _dbContext.SubmitChanges();
                    }
                    else
                    {
                        var PageStatusNew = new TenantPetitionPageSubmissionStatus();
                        PageStatusNew.CustomerID = caseInfo.TenantPetitionInfo.CustomerID;
                        PageStatusNew.ApplicantInformation = true;
                        _dbContext.TenantPetitionPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                        _dbContext.SubmitChanges();
                    }
                }
                
                result.result = caseInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        

        private int SaveTenantPetition(TenantPetitionInfoM petition)
        {
            int petitionID = 0;
            int tenantPetitionID = SaveTenantPetitionInfo(petition);
            if (tenantPetitionID != 0)
            {
                petition.PetitionID = tenantPetitionID;
                //SaveTenantLostServiceInfo(petition);
                //SaveTenantProblemInfo(petition);
                //SavePetitionGroundInfo(petition);
                petitionID = GetPetitionID(tenantPetitionID, 1);
            }
            return petitionID;
        }

        private int SaveTenantPetitionInfo(TenantPetitionInfoM petition)
        {
            int petitionID = 0;
            
            TenantPetitionInfo petitionDB = new TenantPetitionInfo();
                petitionDB.NumberOfUnits = petition.NumberOfUnits;
                petitionDB.UnitTypeID = petition.UnitTypeId;
                petitionDB.bRentStatus = petition.bCurrentRentStatus;
                //petitionDB.LegalWithHoldingExplanation = petition.LegalWithHoldingExplanation;
                //petitionDB.bCitationDocUnavailable = petition.bCitationDocUnavailable;
                ////To be removed
                //petitionDB.MoveInDate = DateTime.Now;
                //// petitionDB.MoveInDate = petition.MoveInDate;
                //petitionDB.InitialRent = petition.InitialRent;
                //petitionDB.bRAPNoticeGiven = petition.bRAPNoticeGiven;
                //// To be removed
                //petitionDB.RAPNoticeGivnDate = DateTime.Now;
                ////  petitionDB.RAPNoticeGivnDate = petition.RAPNoticeGivenDate;
                //petitionDB.bRentControlledByAgency = petition.bRentControlledByAgency;
                //petitionDB.bPetitionFiledPrviously = petition.bPetitionFiledPrviously;
                //petitionDB.PreviousCaseIDs = petition.PreviousCaseIDs;
                //petitionDB.bLostService = petition.bLostService;
                //petitionDB.bSeriousProblem = petition.bProblem;

                _dbContext.TenantPetitionInfos.InsertOnSubmit(petitionDB);
                _dbContext.SubmitChanges();
                petitionID = petitionDB.TenantPetitionID;
            
            return petitionID;
        }

        public ReturnResult<bool> SaveTenantRentalHistoryInfo(TenantRentalHistoryM rentalHistory, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var rentalHistoryRecord = _dbContext.TenantRentalHistories.Where(x => x.PetitionID == rentalHistory.PetitionID).FirstOrDefault();
                if (rentalHistoryRecord != null)
                {                   
                    rentalHistoryRecord.PetitionID = rentalHistory.PetitionID;
                    rentalHistoryRecord.MoveInDate = new DateTime(rentalHistory.MoveInDate.Year, rentalHistory.MoveInDate.Month, rentalHistory.MoveInDate.Day);
                    rentalHistoryRecord.InitialRent = rentalHistory.InitialRent;
                    rentalHistoryRecord.RAPNoticeGivenDate = new DateTime(rentalHistory.RAPNoticeGivenDate.Year, rentalHistory.RAPNoticeGivenDate.Month, rentalHistory.RAPNoticeGivenDate.Day);
                    rentalHistoryRecord.bRAPNoticeGiven = rentalHistory.bRAPNoticeGiven;
                    rentalHistoryRecord.bRentControlledByAgency = rentalHistory.bRentControlledByAgency;
                    rentalHistoryRecord.CreatedDate = DateTime.Now;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    TenantRentalHistory rentalHistoryDB = new TenantRentalHistory();
                    rentalHistoryDB.PetitionID = rentalHistory.PetitionID;
                    rentalHistoryDB.MoveInDate = new DateTime(rentalHistory.MoveInDate.Year, rentalHistory.MoveInDate.Month, rentalHistory.MoveInDate.Day);
                    rentalHistoryDB.InitialRent = rentalHistory.InitialRent;
                    rentalHistoryDB.RAPNoticeGivenDate = new DateTime(rentalHistory.RAPNoticeGivenDate.Year, rentalHistory.RAPNoticeGivenDate.Month, rentalHistory.RAPNoticeGivenDate.Day);
                    rentalHistoryDB.bRAPNoticeGiven = rentalHistory.bRAPNoticeGiven;
                    rentalHistoryDB.bRentControlledByAgency = rentalHistory.bRentControlledByAgency;
                    rentalHistoryDB.CreatedDate = DateTime.Now;
                    _dbContext.TenantRentalHistories.InsertOnSubmit(rentalHistoryDB);
                    _dbContext.SubmitChanges();
                }
                var rentIncrementRecord = _dbContext.TenantRentalIncrementInfos.Where(x =>x.TenantPetitionID == rentalHistory.PetitionID).ToList();
                if(rentIncrementRecord != null)
                {
                    foreach(var item in rentIncrementRecord)
                    {
                        _dbContext.TenantRentalIncrementInfos.DeleteOnSubmit(item);
                        _dbContext.SubmitChanges();
                    }
                }

                foreach (var item in rentalHistory.RentIncreases)
                {
                    TenantRentalIncrementInfo rentIncrementDB = new TenantRentalIncrementInfo();
                    rentIncrementDB.TenantPetitionID = rentalHistory.PetitionID;
                    rentIncrementDB.bRentIncreaseNoticeGiven = item.bRentIncreaseNoticeGiven;
                    if (item.bRentIncreaseNoticeGiven)
                    {
                        rentIncrementDB.RentIncreaseNoticeDate = new DateTime(item.RentIncreaseNoticeDate.Year,
                            item.RentIncreaseNoticeDate.Month, item.RentIncreaseNoticeDate.Day);

                    }                    
                    rentIncrementDB.RentIncreaseEffectiveDate = new DateTime(item.RentIncreaseEffectiveDate.Year,
                            item.RentIncreaseEffectiveDate.Month, item.RentIncreaseEffectiveDate.Day);
                    rentIncrementDB.RentIncreasedFrom = item.RentIncreasedFrom;
                    rentIncrementDB.RentIncreasedTo = item.RentIncreasedTo;
                    rentIncrementDB.bRentIncreaseContested = item.bRentIncreaseContested;

                    _dbContext.TenantRentalIncrementInfos.InsertOnSubmit(rentIncrementDB);
                    _dbContext.SubmitChanges();


                    var PageStatus = _dbContext.TenantPetitionPageSubmissionStatus.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                    if (PageStatus != null)
                    {
                        PageStatus.RentHistory = true;
                        _dbContext.SubmitChanges();
                    }
                    else
                    {
                        var PageStatusNew = new TenantPetitionPageSubmissionStatus();
                        PageStatusNew.CustomerID = CustomerID;
                        PageStatusNew.RentHistory = true;
                        _dbContext.TenantPetitionPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                        _dbContext.SubmitChanges();
                    } 
                }
                
                result.result = true;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

        }

        public ReturnResult<bool> SaveTenantLostServiceInfo(LostServicesPageM message, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var lostServicesRecord = _dbContext.TenantLostServiceInfos.Where(x => x.TenantPetitionID == message.PetitionID).ToList();
                if (lostServicesRecord != null)
                {
                    foreach (var item in lostServicesRecord)
                    {
                        _dbContext.TenantLostServiceInfos.DeleteOnSubmit(item);
                        _dbContext.SubmitChanges();
                    }
                }
                if (message.bLostService)
                {
                    foreach (var item in message.LostServices)
                    {
                        TenantLostServiceInfo lostServiceDB = new TenantLostServiceInfo();
                        lostServiceDB.TenantPetitionID = message.PetitionID;
                        lostServiceDB.ReducedServiceDescription = item.ReducedServiceDescription;
                        lostServiceDB.EstimatedLoss = item.EstimatedLoss;
                        lostServiceDB.LossBeganDate = new DateTime(item.LossBeganDate.Year,
                            item.LossBeganDate.Month, item.LossBeganDate.Day);
                        //lostServiceDB.PayingToServiceBeganDate = new DateTime(item.PayingToServiceBeganDate.Year,
                        //    item.PayingToServiceBeganDate.Month, item.PayingToServiceBeganDate.Day);

                        _dbContext.TenantLostServiceInfos.InsertOnSubmit(lostServiceDB);
                        _dbContext.SubmitChanges();
                    }

                }
                var ProblemsRecord = _dbContext.TenantProblemInfos.Where(x => x.TenantPetitionID == message.PetitionID).ToList();
                if (ProblemsRecord != null)
                {
                    foreach (var item in ProblemsRecord)
                    {
                        _dbContext.TenantProblemInfos.DeleteOnSubmit(item);
                        _dbContext.SubmitChanges();
                    }
                }
                if (message.bProblem)
                {
                    SaveTenantProblemInfo(message);
                }

                var PageStatus = _dbContext.TenantPetitionPageSubmissionStatus.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                if (PageStatus != null)
                {
                    PageStatus.LostService = true;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var PageStatusNew = new TenantPetitionPageSubmissionStatus();
                    PageStatusNew.CustomerID = CustomerID;
                    PageStatusNew.LostService = true;
                    _dbContext.TenantPetitionPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                    _dbContext.SubmitChanges();
                }

                result.result = true;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
           
        }

        private void SaveTenantProblemInfo(LostServicesPageM message)
        {
            if (message.bProblem)
            {

                foreach (var item in message.Problems)
                    {
                        TenantProblemInfo problemDB = new TenantProblemInfo();
                        problemDB.TenantPetitionID = message.PetitionID;
                        problemDB.ProblemDescription = item.ProblemDescription;
                        problemDB.EstimatedLoss = item.EstimatedLoss;
                        //TBD
                        //  problemDB.ProblemBeganDate = item.ProblemBeganDate;
                        problemDB.ProblemBeganDate = new DateTime(item.ProblemBeganDate.Year,
                            item.ProblemBeganDate.Month, item.ProblemBeganDate.Day);
                    
                        

                        _dbContext.TenantProblemInfos.InsertOnSubmit(problemDB);
                        _dbContext.SubmitChanges();
                    }
                
            }
        }

        public ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var groundsDb = from r in _dbContext.TenantPetitionGroundInfos
                                           where r.TenantPetitionID == petition.PetitionID
                                           select r;
                if (groundsDb.Any())
                {
                    foreach (var item in petition.PetitionGrounds)
                    {
                        if (item.Selected)
                        {
                            if (!groundsDb.Where(x => x.PetitionGroundID == item.PetitionGroundID).Any())
                            {
                                TenantPetitionGroundInfo petitionGroundsDB = new TenantPetitionGroundInfo();
                                petitionGroundsDB.TenantPetitionID = petition.PetitionID;
                                petitionGroundsDB.PetitionGroundID = item.PetitionGroundID;

                                _dbContext.TenantPetitionGroundInfos.InsertOnSubmit(petitionGroundsDB);
                                _dbContext.SubmitChanges();
                            }
                        }
                        else
                        {
                            if (groundsDb.Where(x => x.PetitionGroundID == item.PetitionGroundID).Any())
                            {
                                _dbContext.TenantPetitionGroundInfos.DeleteOnSubmit(groundsDb.Where(x => x.PetitionGroundID == item.PetitionGroundID).First());
                                _dbContext.SubmitChanges();
                            }
                        }

                    }
                }
                else
                {
                    foreach (var item in petition.PetitionGrounds)
                    {
                        if (item.Selected)
                        {
                            TenantPetitionGroundInfo petitionGroundsDB = new TenantPetitionGroundInfo();
                            petitionGroundsDB.TenantPetitionID = petition.PetitionID;
                            petitionGroundsDB.PetitionGroundID = item.PetitionGroundID;

                            _dbContext.TenantPetitionGroundInfos.InsertOnSubmit(petitionGroundsDB);
                            _dbContext.SubmitChanges();
                        }
                    }
                }
                var PageStatus = _dbContext.TenantPetitionPageSubmissionStatus.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                if (PageStatus != null)
                {
                    PageStatus.GroundsForPetition = true;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var PageStatusNew = new TenantPetitionPageSubmissionStatus();
                    PageStatusNew.CustomerID = CustomerID;
                    PageStatusNew.GroundsForPetition = true;
                    _dbContext.TenantPetitionPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                    _dbContext.SubmitChanges();
                }


                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
            //ReturnResult<bool> result = new ReturnResult<bool>();
            // try
            //{
            //    petition.PetitionGrounds[0].Selected = true; //TBD
            //    foreach (var item in petition.PetitionGrounds)
            //    {
            //        if (item.Selected)
            //        {
            //            TenantPetitionGroundInfo petitionGroundsDB = new TenantPetitionGroundInfo();
            //            petitionGroundsDB.TenantPetitionID = petition.PetitionID;
            //            petitionGroundsDB.PetitionGroundID = item.PetitionGroundID;

            //            _dbContext.TenantPetitionGroundInfos.InsertOnSubmit(petitionGroundsDB);
            //            _dbContext.SubmitChanges();
            //        }
            //    }
            //    result.result = true;
            //    result.status = new OperationStatus() { Status = StatusEnum.Success };
            //    return result;
            //}
            // catch (Exception ex)
            // {
            //     IExceptionHandler eHandler = new ExceptionHandler();
            //     result.status = eHandler.HandleException(ex);
            //     return result;
            // }
            
        }

        //TBD - to be removed as we dont need to save the APpeal info
        public ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo)
        {
            int thirdPartyUserID = 0;
            int opposingPartyUserID = 0;
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                CommonDBHandler _dbCommon = new CommonDBHandler();
                if (caseInfo.TenantAppealInfo.bThirdPartyRepresentation)
                {
                    thirdPartyUserID = _dbCommon.SaveUserInfo(caseInfo.TenantAppealInfo.AppealThirdPartyInfo).result.UserID;
                    if (thirdPartyUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    caseInfo.TenantAppealInfo.thirdPartyUserID = thirdPartyUserID;

                }
                foreach (var item in caseInfo.TenantAppealInfo.AppealOpposingPartyInfo)
                {
                    opposingPartyUserID = _dbCommon.SaveUserInfo(item).result.UserID;
                    if (opposingPartyUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    caseInfo.TenantAppealInfo.opposingPartyUserID.Add(opposingPartyUserID);
                }
                
                    TenantAppealDetail appealDB = new TenantAppealDetail();
                    appealDB.bThirdPartyRepresentation = caseInfo.TenantAppealInfo.bThirdPartyRepresentation;
                    appealDB.ThirdPartyUserID = caseInfo.TenantAppealInfo.thirdPartyUserID;
                    appealDB.CreatedDate = DateTime.Now;

                    _dbContext.TenantAppealDetails.InsertOnSubmit(appealDB);
                    _dbContext.SubmitChanges();
                    caseInfo.TenantAppealInfo.AppealID = appealDB.AppealID;


                    CaseDetail caseDB = _dbContext.CaseDetails.First(i => i.CaseID == caseInfo.CaseID);
                    caseDB.TenantAppealID = caseInfo.TenantAppealInfo.AppealID;
                    caseDB.LastModifiedDate = DateTime.Now;
                    _dbContext.SubmitChanges();
                



                result.result = caseInfo.TenantAppealInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

        }
        public ReturnResult<TenantAppealInfoM> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                var appealExistsDB = _dbContext.TenantAppealDetails.Where(x => x.CaseNumber == tenantAppealInfo.CaseNumber && x.AppealFiledBy == tenantAppealInfo.AppealFiledBy).FirstOrDefault();
                if (appealExistsDB != null)
                {
                    tenantAppealInfo.AppealID = appealExistsDB.AppealID;
                }
                else
                {
                    TenantAppealDetail appealDB = new TenantAppealDetail();
                    appealDB.AppealFiledBy = tenantAppealInfo.AppealFiledBy;
                    appealDB.CaseNumber = tenantAppealInfo.CaseNumber;
                    appealDB.AppealCategoryID = tenantAppealInfo.AppealCategoryID;
                    appealDB.IsSubmitted = false;
                    appealDB.CreatedDate = DateTime.Now;

                    _dbContext.TenantAppealDetails.InsertOnSubmit(appealDB);
                    _dbContext.SubmitChanges();
                    tenantAppealInfo.AppealID = appealDB.AppealID;
                }

                var groundsDb = from r in _dbContext.TenantAppealGroundInfos
                                where r.AppealID == tenantAppealInfo.AppealID
                                select r;
                if (groundsDb.Any())
                {
                    
                    foreach (var item in tenantAppealInfo.AppealGrounds)
                    {
                        if (item.Selected)
                        {
                            if (!groundsDb.Where(x => x.AppealGroundID == item.AppealGroundID).Any())
                            {
                                TenantAppealGroundInfo TenantAppealGroundInfoDB = new TenantAppealGroundInfo();
                                TenantAppealGroundInfoDB.AppealID = tenantAppealInfo.AppealID;
                                TenantAppealGroundInfoDB.AppealGroundID = item.AppealGroundID;
                                TenantAppealGroundInfoDB.CreatedDate = DateTime.Now;
                                TenantAppealGroundInfoDB.IsDeleted = false;

                                _dbContext.TenantAppealGroundInfos.InsertOnSubmit(TenantAppealGroundInfoDB);
                                _dbContext.SubmitChanges();
                            }
                        }
                        else
                        {
                            if (groundsDb.Where(x => x.AppealGroundID == item.AppealGroundID).Any())
                            {
                                _dbContext.TenantAppealGroundInfos.DeleteOnSubmit(groundsDb.Where(x => x.AppealGroundID == item.AppealGroundID).First());
                                _dbContext.SubmitChanges();
                            }
                        }

                    }
                }
                else
                {
                    foreach (var item in tenantAppealInfo.AppealGrounds)
                    {
                        if (item.Selected)
                        {
                            TenantAppealGroundInfo TenantAppealGroundInfoDB = new TenantAppealGroundInfo();
                            TenantAppealGroundInfoDB.AppealID = tenantAppealInfo.AppealID;
                            TenantAppealGroundInfoDB.AppealGroundID = item.AppealGroundID;
                            TenantAppealGroundInfoDB.CreatedDate = DateTime.Now;
                            TenantAppealGroundInfoDB.IsDeleted = false;

                            _dbContext.TenantAppealGroundInfos.InsertOnSubmit(TenantAppealGroundInfoDB);
                            _dbContext.SubmitChanges();
                        }
                    }
                   
                }


                result.result = tenantAppealInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

        }
        public ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(TenantAppealInfoM tenantAppealInfo, int CustomerID)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                var CustDetails = _dbAccount.CustomerDetails.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                if(CustDetails != null)
                {
                    if(CustDetails.CustomerIdentityKey != tenantAppealInfo.serveAppeal.pin)
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.PinError };
                        return result;
                    }
                }
                

                ServeAppeal appealDB = _dbContext.ServeAppeals.Where(i => i.AppealID == tenantAppealInfo.AppealID).FirstOrDefault();
                if(appealDB != null)
                {
                    appealDB.bAcknowledgeNamePin = tenantAppealInfo.serveAppeal.bAcknowledgeNamePin;
                    appealDB.bDeclaration = tenantAppealInfo.serveAppeal.bDeclaration;
                    appealDB.bThirdParty = tenantAppealInfo.serveAppeal.bThirdParty;
                    appealDB.PenaltyDate = new DateTime(tenantAppealInfo.serveAppeal.PenaltyDate.Year, tenantAppealInfo.serveAppeal.PenaltyDate.Month, tenantAppealInfo.serveAppeal.PenaltyDate.Day);
                    appealDB.CreatedDate = DateTime.Now;                   
                }
                else
                {
                    ServeAppeal appealNewDB = new ServeAppeal();
                    appealNewDB.AppealID = tenantAppealInfo.AppealID;
                    appealNewDB.bAcknowledgeNamePin = tenantAppealInfo.serveAppeal.bAcknowledgeNamePin;
                    appealNewDB.bDeclaration = tenantAppealInfo.serveAppeal.bDeclaration;
                    appealNewDB.bThirdParty = tenantAppealInfo.serveAppeal.bThirdParty;
                    appealNewDB.PenaltyDate = new DateTime(tenantAppealInfo.serveAppeal.PenaltyDate.Year,tenantAppealInfo.serveAppeal.PenaltyDate.Month, tenantAppealInfo.serveAppeal.PenaltyDate.Day);
                    appealNewDB.CreatedDate = DateTime.Now;
                    _dbContext.ServeAppeals.InsertOnSubmit(appealNewDB);
                }              
                _dbContext.SubmitChanges();

                AddAnotherOpposingParty(tenantAppealInfo);

                result.result = tenantAppealInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        #endregion
        #region "Add"
        public ReturnResult<bool> AddAnotherOpposingParty(TenantAppealInfoM tenantAppealInfo)
        {
            int opposingPartyUserID = 0;
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {



                var appealOpposingExists = _dbContext.AppealOpposingParties.Where(x => x.AppealID == tenantAppealInfo.AppealID).ToList();
                if (appealOpposingExists != null)
                {
                    foreach (var item in appealOpposingExists)
                    {
                        _dbContext.AppealOpposingParties.DeleteOnSubmit(item);
                        _dbContext.SubmitChanges();
                    }
                }

                foreach (var item in tenantAppealInfo.serveAppeal.OpposingParty)
                {
                    opposingPartyUserID = _commondbHandler.SaveUserInfo(item).result.UserID;
                    if (opposingPartyUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    tenantAppealInfo.opposingPartyUserID.Add(opposingPartyUserID);

                    AppealOpposingParty appealOpposingDB = new AppealOpposingParty();
                    appealOpposingDB.AppealID = tenantAppealInfo.AppealID;
                    appealOpposingDB.OpposingPartyID = opposingPartyUserID;
                    appealOpposingDB.CreatedDate = DateTime.Now;
                    appealOpposingDB.IsDeleted = false;
                    appealOpposingDB.ModifiedDate = DateTime.Now;

                    _dbContext.AppealOpposingParties.InsertOnSubmit(appealOpposingDB);
                    _dbContext.SubmitChanges();
                }


                result.result = true;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        #endregion   
      
  
        #region Petition category  
        public ReturnResult<CaseInfoM> GetPetitioncategory()
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<PetitionCategoryM> _categories = new List<PetitionCategoryM>();
            CaseInfoM model = new CaseInfoM();
            try
            {
                PetitionCategory petitionCategory = new PetitionCategory();
                var categories = _dbContext.PetitionCategories;
                if(categories == null)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                }
                else
                {
                    foreach(var category in categories)
                    {
                        PetitionCategoryM _category = new PetitionCategoryM();
                        _category.PetitionCategoryID = category.PetitionCategoryID;
                        _category.PetitionCategory = category.PetitionCategory1;
                        _categories.Add(_category);
                    }
                }
                model.PetitionCategory = _categories;
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch(Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }
        
        #endregion
        #region Owner Petition Get Functions
        /// <summary>
        /// Gets Applicant information based on CustomerID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var applicantInfo = _dbContext.OwnerPetitionApplicantInfos.Where(r => r.CustomerID == model.OwnerPetitionInfo.ApplicantInfo.CustomerID && r.bPetitionFiled == false).FirstOrDefault();
                if(applicantInfo == null) 
                {
                    applicantInfo = _dbContext.OwnerPetitionApplicantInfos.Where(r => r.CustomerID == model.OwnerPetitionInfo.ApplicantInfo.CustomerID && r.bPetitionFiled == true).OrderByDescending(c => c.CreatedDate).FirstOrDefault();
                }                 
                
                if(applicantInfo !=null)
                {
                    OwnerPetitionApplicantInfoM _applicantInfo = new OwnerPetitionApplicantInfoM();
                    _applicantInfo.OwnerPetitionApplicantInfoID = applicantInfo.OwnerPetitionApplicantInfoID;                 
                    var applicantUserInforesult = _commondbHandler.GetUserInfo(applicantInfo.ApplicantUserID);
                    if (applicantUserInforesult.status.Status != StatusEnum.Success)
                    {
                        result.status = applicantUserInforesult.status;
                        return result;
                    }
                    _applicantInfo.ApplicantUserInfo = applicantUserInforesult.result;
                    _applicantInfo.bThirdPartyRepresentation =(applicantInfo.bThirdPartyRepresentation != null) ? Convert.ToBoolean(applicantInfo.bThirdPartyRepresentation) : false;
                    if (_applicantInfo.bThirdPartyRepresentation)
                    {
                        var thirdPartyUserInforesult = _commondbHandler.GetUserInfo(applicantInfo.ThirdPartyUserID);
                        if (thirdPartyUserInforesult.status.Status != StatusEnum.Success)
                        {
                            result.status = thirdPartyUserInforesult.status;
                            return result;
                        }
                    _applicantInfo.ThirdPartyUser = thirdPartyUserInforesult.result;                  }
                    _applicantInfo.bBusinessLicensePaid = (applicantInfo.bBusinessLicensePaid != null) ? Convert.ToBoolean(applicantInfo.bBusinessLicensePaid) : false; 
                    _applicantInfo.BusinessLicenseNumber = applicantInfo.BusinessLicenseNumber;
                    _applicantInfo.bRentAdjustmentProgramFeePaid = (applicantInfo.bRentAdjustmentProgramFeePaid != null) ? Convert.ToBoolean(applicantInfo.bRentAdjustmentProgramFeePaid) : false; 
                    _applicantInfo.BuildingAcquiredDate =_commondbHandler.GetDateFromDatabase(Convert.ToDateTime(applicantInfo.BuildingAcquiredDate));
                    _applicantInfo.NumberOfUnits = (applicantInfo.NumberOfUnits != null) ? Convert.ToInt32(applicantInfo.NumberOfUnits) : 0;
                    _applicantInfo.bMoreThanOneStreetOnParcel = (applicantInfo.bMoreThanOneStreetOnParcel != null) ? Convert.ToBoolean(applicantInfo.bMoreThanOneStreetOnParcel) : false;
                    _applicantInfo.CustomerID = (applicantInfo.CustomerID != null) ? Convert.ToInt32(applicantInfo.CustomerID) : 0; ;
                    _applicantInfo.bPetitionFiled = applicantInfo.bPetitionFiled;
                    model.OwnerPetitionInfo.ApplicantInfo = _applicantInfo;
                    result.result = model;
                }
                else
                {
                    result.result = model;
                }

                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }

        /// <summary>
        /// Gets Rent increase reason with previous selection information
        /// </summary>
        /// <param name="petition"></param>
        /// <returns></returns>
        public ReturnResult<List<OwnerRentIncreaseReasonsM>> GetRentIncreaseReasonInfo(OwnerPetitionInfoM petition)
        {
            ReturnResult<List<OwnerRentIncreaseReasonsM>> result = new ReturnResult<List<OwnerRentIncreaseReasonsM>>();
            List<OwnerRentIncreaseReasonsM> _reasons = new List<OwnerRentIncreaseReasonsM>();
            try
            {
                var resaons = _dbContext.OwnerRentIncreaseReasons;

                var selectedReasons = _dbContext.OwnerRentIncreaseReasonInfos.Where(x => x.OwnerPetitionApplicantInfoID == petition.ApplicantInfo.OwnerPetitionApplicantInfoID);

                if (resaons.Any())
                {
                    foreach (var item in resaons)
                    {
                        OwnerRentIncreaseReasonsM _reason = new OwnerRentIncreaseReasonsM();
                        _reason.ReasonID = item.ReasonID;
                        _reason.ReasonDescription = item.Reason;
                        _reason.IsSelected = false;
                        _reasons.Add(_reason);
                    }
                }
                if (selectedReasons.Any())
                {
                    foreach (var item in selectedReasons)
                    {
                        _reasons.Where(r => r.ReasonID == item.ReasonID).First().IsSelected = true;
                    }
                }
                result.result = _reasons;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }

        public ReturnResult<OwnerPetitionPropertyInfoM> GetOwnerPropertyAndTenantInfo(OwnerPetitionPropertyInfoM model)
        {
            ReturnResult<OwnerPetitionPropertyInfoM> result = new ReturnResult<OwnerPetitionPropertyInfoM>();
            try
            {
                if (!model.UnitTypes.Any())
                {
                    model.UnitTypes = getUnitTypes();
                }
                var propertyInfo = from r in _dbContext.OwnerPetitionPropertyInfos
                                   where r.CustomerID == model.CustomerID && r.bPetitionFiled == false
                                   select r;
                if (propertyInfo.Any())
                {
                    model.OwnerPropertyID = propertyInfo.First().OwnerPropertyID;
                    model.UnitTypeID = propertyInfo.First().UnitTypeID;

                    var tentantInfo = from r in _dbContext.OwnerPetitionTenantInfos
                                      where r.OwnerPropertyID == model.OwnerPropertyID
                                      select r;
                    if (tentantInfo.Any())
                    {
                        foreach (var item in tentantInfo)
                        {
                            OwnerPetitionTenantInfoM _tenant = new OwnerPetitionTenantInfoM();
                            var userResult = _commondbHandler.GetUserInfo(item.TenantUserID);
                            if (userResult.status.Status == StatusEnum.Success)
                            {
                                _tenant.TenantUserInfo = userResult.result;
                                _tenant.TenantInfoID = item.TenantInfoID;
                            }
                            model.TenantInfo.Add(_tenant);
                        }
                    }
                }
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> GetOwnerRentIncreaseAndPropertyInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                model.RAPNoticeStatus = getRAPNoticeStatus();
                model.CurrentOnRent = getCurrentRentStatus();
                if (model.OwnerPetitionInfo.PropertyInfo.OwnerPropertyID > 0)
                {
                    var propertyInfo = (from r in _dbContext.OwnerPetitionPropertyInfos
                                        where r.OwnerPropertyID == model.OwnerPetitionInfo.PropertyInfo.OwnerPropertyID
                                        select r).First();
                    if (propertyInfo !=null)
                    {
                        model.OwnerPetitionInfo.PropertyInfo.MovedInDate = (propertyInfo.MovedInDate == null) ? null :  _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.MovedInDate));
                        model.OwnerPetitionInfo.PropertyInfo.InitialRent = propertyInfo.InitialRent;
                        model.OwnerPetitionInfo.PropertyInfo.RAPNoticeStatusID = propertyInfo.RAPNoticeStatusID;
                        model.OwnerPetitionInfo.PropertyInfo.RAPNoticeGivenDate = (propertyInfo.RAPNoticeGivenDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.RAPNoticeGivenDate));
                        model.OwnerPetitionInfo.PropertyInfo.RentStatusID = propertyInfo.RentStatusID;
                    }

                    var rentIncreaseInfo = _dbContext.OwnerPetitionRentalIncrementInfos.Where(r => r.OwnerPropertyID == model.OwnerPetitionInfo.PropertyInfo.OwnerPropertyID);
                    if (rentIncreaseInfo.Any())
                    {
                        foreach (var item in rentIncreaseInfo)
                        {
                            OwnerPetitionRentalIncrementInfoM _rentIncrease = new OwnerPetitionRentalIncrementInfoM();
                            _rentIncrease.bRentIncreaseNoticeGiven = (bool) item.bRentIncreaseNoticeGiven;
                            _rentIncrease.RentIncreaseNoticeDate = (item.RentIncreaseNoticeDate == null) ? null: _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseNoticeDate));
                            _rentIncrease.RentIncreaseEffectiveDate = (item.RentIncreaseEffectiveDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseEffectiveDate));
                            _rentIncrease.RentIncreasedFrom = item.RentIncreasedFrom;
                            _rentIncrease.RentIncreasedTo = item.RentIncreasedTo;
                            model.OwnerPetitionInfo.PropertyInfo.RentalInfo.Add(_rentIncrease);
                        }
                    }
                }
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }       


        #endregion
        
        #region Owner petition Save Functions
        /// <summary>
        /// Save or Update Applicant Information page of Owners petition.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            int applicantUserID = 0;
            int thirdPartyUserID = 0;           
            {
                try
                {
                    if(model.bCaseFiledByThirdParty)
                    {
                        var applicantUserResult = _commondbHandler.SaveUserInfo(model.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo);
                        if (applicantUserResult.status.Status != StatusEnum.Success)
                        {
                            result.status = applicantUserResult.status;
                            return result;
                        }
                        applicantUserID = applicantUserResult.result.UserID;
                        thirdPartyUserID = model.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.UserID;
                    }
                    else
                    {
                        if (model.OwnerPetitionInfo.ApplicantInfo.bThirdPartyRepresentation)
                        {
                            var thirdpartyUserResult = _commondbHandler.SaveUserInfo(model.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser);
                            if (thirdpartyUserResult.status.Status != StatusEnum.Success)
                            {
                                result.status = thirdpartyUserResult.status;
                                return result;
                            }
                            thirdPartyUserID = thirdpartyUserResult.result.UserID;
                        }
                        applicantUserID = model.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.UserID;
                    }

                    if (model.OwnerPetitionInfo.ApplicantInfo.OwnerPetitionApplicantInfoID != 0 && !model.OwnerPetitionInfo.ApplicantInfo.bPetitionFiled)
                    {
                        var applicantInfo = from r in _dbContext.OwnerPetitionApplicantInfos
                                            where r.OwnerPetitionApplicantInfoID == model.OwnerPetitionInfo.ApplicantInfo.OwnerPetitionApplicantInfoID
                                            select r;
                        if (applicantInfo.Any())
                        {
                            OwnerPetitionApplicantInfo _applicantInfo = new OwnerPetitionApplicantInfo();
                            applicantInfo.First().ApplicantUserID = applicantUserID;
                            applicantInfo.First().bThirdPartyRepresentation = model.OwnerPetitionInfo.ApplicantInfo.bThirdPartyRepresentation;
                            if (thirdPartyUserID > 0)
                            {
                                applicantInfo.First().ThirdPartyUserID = thirdPartyUserID;
                            }
                            applicantInfo.First().bBusinessLicensePaid = model.OwnerPetitionInfo.ApplicantInfo.bBusinessLicensePaid;
                            applicantInfo.First().BusinessLicenseNumber = model.OwnerPetitionInfo.ApplicantInfo.BusinessLicenseNumber;
                            applicantInfo.First().bRentAdjustmentProgramFeePaid = model.OwnerPetitionInfo.ApplicantInfo.bRentAdjustmentProgramFeePaid;
                            applicantInfo.First().BuildingAcquiredDate = new DateTime(model.OwnerPetitionInfo.ApplicantInfo.BuildingAcquiredDate.Year, model.OwnerPetitionInfo.ApplicantInfo.BuildingAcquiredDate.Month, model.OwnerPetitionInfo.ApplicantInfo.BuildingAcquiredDate.Day);
                            applicantInfo.First().NumberOfUnits = model.OwnerPetitionInfo.ApplicantInfo.NumberOfUnits;
                            applicantInfo.First().bMoreThanOneStreetOnParcel = model.OwnerPetitionInfo.ApplicantInfo.bMoreThanOneStreetOnParcel;
                            _dbContext.SubmitChanges();
                        }
                    }
                    else
                    {
                        OwnerPetitionApplicantInfo applicantInfo = new OwnerPetitionApplicantInfo();
                        applicantInfo.ApplicantUserID = applicantUserID;
                        applicantInfo.bThirdPartyRepresentation = model.OwnerPetitionInfo.ApplicantInfo.bThirdPartyRepresentation;
                        if (thirdPartyUserID > 0)
                        {
                            applicantInfo.ThirdPartyUserID = thirdPartyUserID;
                        }
                        applicantInfo.bBusinessLicensePaid = model.OwnerPetitionInfo.ApplicantInfo.bBusinessLicensePaid;
                        applicantInfo.BusinessLicenseNumber = model.OwnerPetitionInfo.ApplicantInfo.BusinessLicenseNumber;
                        applicantInfo.bRentAdjustmentProgramFeePaid = model.OwnerPetitionInfo.ApplicantInfo.bRentAdjustmentProgramFeePaid;
                        applicantInfo.BuildingAcquiredDate = new DateTime(model.OwnerPetitionInfo.ApplicantInfo.BuildingAcquiredDate.Year, model.OwnerPetitionInfo.ApplicantInfo.BuildingAcquiredDate.Month, model.OwnerPetitionInfo.ApplicantInfo.BuildingAcquiredDate.Day);
                        applicantInfo.NumberOfUnits = model.OwnerPetitionInfo.ApplicantInfo.NumberOfUnits;
                        applicantInfo.bMoreThanOneStreetOnParcel = model.OwnerPetitionInfo.ApplicantInfo.bMoreThanOneStreetOnParcel;
                        applicantInfo.CustomerID = model.OwnerPetitionInfo.ApplicantInfo.CustomerID;
                        applicantInfo.bPetitionFiled = false;
                        _dbContext.OwnerPetitionApplicantInfos.InsertOnSubmit(applicantInfo);
                        _dbContext.SubmitChanges();
                        model.OwnerPetitionInfo.ApplicantInfo.OwnerPetitionApplicantInfoID = applicantInfo.OwnerPetitionApplicantInfoID;                      
                    }
                    result.result = model;
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                    return result;                   
                }
                catch(Exception ex)
                {
                    result.status = _eHandler.HandleException(ex);
                    _commondbHandler.SaveErrorLog(result.status);
                    return result;
                }
            }
        }
       
        /// <summary>
        /// Save Owner petition information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public ReturnResult<OwnerPetitionInfoM> SaveOwnerPetitionInfo(OwnerPetitionInfoM model)
        //{
        //    ReturnResult<OwnerPetitionInfoM> result = new ReturnResult<OwnerPetitionInfoM>();
        //    try
        //    {
        //        OwnerPetitionInfo petitionInfo = new OwnerPetitionInfo();
        //        petitionInfo.OwnerPetitionApplicantInfoID = model.ApplicantInfo.ApplicantUserID;
        //        petitionInfo.OwnerPropertyID = model.PropertyInfo.OwnerPropertyID;
        //        petitionInfo.bPetitionFiledByThirdParty = model.bPetitionFiledByThirdParty;
        //        petitionInfo.bAgreeToCityMediation = model.bAgreeToCityMediation;
        //        petitionInfo.PetitionFiledBy = model.PetitionFiledBy;
        //        petitionInfo.CreatedDate = DateTime.Now;
        //        _dbContext.OwnerPetitionInfos.InsertOnSubmit(petitionInfo);
        //        _dbContext.SubmitChanges();
        //        model.PropertyInfo.OwnerPropertyID = petitionInfo.OwnerPetitionID;
        //        result.result = model;
        //        result.status = new OperationStatus() { Status = StatusEnum.Success };
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.status = _eHandler.HandleException(ex);
        //        _commondbHandler.SaveErrorLog(result.status);
        //        return result;
        //    }
        //}
        /// <summary>
        /// Save Owner property information (Rental Property page on Owner ptition) and addes Tenant information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<OwnerPetitionPropertyInfoM> SaveOwnerPropertyAndTenantInfo(OwnerPetitionPropertyInfoM model)
        {
            ReturnResult<OwnerPetitionPropertyInfoM> result = new ReturnResult<OwnerPetitionPropertyInfoM>();
            try
            {
                if (model.OwnerPropertyID != 0)
                {
                    var propertyInfo = from r in _dbContext.OwnerPetitionPropertyInfos
                                       where r.OwnerPropertyID == model.OwnerPropertyID
                                       select r;
                    if (propertyInfo.Any())
                    {
                        propertyInfo.First().UnitTypeID = model.UnitTypeID;
                        _dbContext.SubmitChanges();
                    }
                }
                else
                {

                    OwnerPetitionPropertyInfo propertyInfo = new OwnerPetitionPropertyInfo();
                    propertyInfo.UnitTypeID = model.UnitTypeID;
                    propertyInfo.CustomerID = model.CustomerID;
                    propertyInfo.bPetitionFiled = false;
                    _dbContext.OwnerPetitionPropertyInfos.InsertOnSubmit(propertyInfo);
                    _dbContext.SubmitChanges();
                    model.OwnerPropertyID = propertyInfo.OwnerPropertyID;
                }
                if (model.TenantInfo.Any())
                {
                    OwnerPetitionTenantInfo tenantInfo = new OwnerPetitionTenantInfo();
                    List<OwnerPetitionTenantInfoM> tenantsInfoM = new List<OwnerPetitionTenantInfoM>();
                    foreach (var tenant in model.TenantInfo)
                    {
                        if (tenant.IsDeleted == false)
                        {
                            var userResult = _commondbHandler.SaveUserInfo(tenant.TenantUserInfo);
                            if (userResult.status.Status == StatusEnum.Success)
                            {
                                var userinfo = from r in _dbContext.OwnerPetitionTenantInfos
                                               where r.TenantUserID == userResult.result.UserID
                                               select r;
                                if (!userinfo.Any())
                                {

                                    tenant.TenantUserInfo = userResult.result;
                                    tenantInfo.TenantUserID = tenant.TenantUserInfo.UserID;
                                    tenantInfo.OwnerPropertyID = model.OwnerPropertyID;
                                    _dbContext.OwnerPetitionTenantInfos.InsertOnSubmit(tenantInfo);
                                    _dbContext.SubmitChanges();
                                    tenant.TenantInfoID = tenantInfo.TenantInfoID;
                                    tenantsInfoM.Add(tenant);
                                }
                                else
                                {
                                    tenantsInfoM.Add(tenant);
                                }
                            }
                            else
                            {
                                result.status = userResult.status;
                                return result;
                            }
                            model.TenantInfo = tenantsInfoM;
                        }
                        else
                        {
                            if (tenant.TenantInfoID > 0)
                            {
                                var _tenant = _dbContext.OwnerPetitionTenantInfos.Where(r => r.TenantInfoID == tenant.TenantInfoID).FirstOrDefault();
                                if (_tenant != null)
                                {
                                    _dbContext.OwnerPetitionTenantInfos.DeleteOnSubmit(_tenant);
                                    _dbContext.SubmitChanges();
                                }
                            }
                        }
                    }
                }
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }
     
       /// <summary>
        /// Saves Rent increase reason based on OwnerPetitionApplicantInfoID
       /// </summary>
       /// <param name="petition"></param>
       /// <returns></returns>          
        public ReturnResult<bool> SaveRentIncreaseReasonInfo(OwnerPetitionInfoM petition)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var rentIncreaseReasonDB = from r in _dbContext.OwnerRentIncreaseReasonInfos
                                           where r.OwnerPetitionApplicantInfoID == petition.ApplicantInfo.OwnerPetitionApplicantInfoID
                                           select r;
                if (rentIncreaseReasonDB.Any())
                {
                    foreach (var item in petition.RentIncreaseReasons)
                    {
                        if (item.IsSelected)
                        {
                            if (!rentIncreaseReasonDB.Where(x => x.ReasonID == item.ReasonID).Any())
                            {
                                OwnerRentIncreaseReasonInfo rentIncreaseReason = new OwnerRentIncreaseReasonInfo();
                                rentIncreaseReason.OwnerPetitionApplicantInfoID = petition.ApplicantInfo.OwnerPetitionApplicantInfoID;
                                rentIncreaseReason.ReasonID = item.ReasonID;
                                _dbContext.OwnerRentIncreaseReasonInfos.InsertOnSubmit(rentIncreaseReason);
                                _dbContext.SubmitChanges();
                            }
                        }
                        else
                        {
                            if (rentIncreaseReasonDB.Where(x => x.ReasonID == item.ReasonID).Any())
                            {
                                //OwnerRentIncreaseReasonInfo rentIncreaseReason = new OwnerRentIncreaseReasonInfo();
                                //rentIncreaseReason.OwnerPetitionApplicantInfoID = petition.ApplicantInfo.OwnerPetitionApplicantInfoID;
                                //rentIncreaseReason.ReasonID = item.ReasonID;
                                _dbContext.OwnerRentIncreaseReasonInfos.DeleteOnSubmit(rentIncreaseReasonDB.Where(x => x.ReasonID == item.ReasonID).First());
                                _dbContext.SubmitChanges();
                            }
                        }

                    }
                }
                else
                {
                    foreach (var item in petition.RentIncreaseReasons)
                    {
                        if (item.IsSelected)
                        {
                            OwnerRentIncreaseReasonInfo rentIncreaseReason = new OwnerRentIncreaseReasonInfo();
                            rentIncreaseReason.OwnerPetitionApplicantInfoID = petition.ApplicantInfo.OwnerPetitionApplicantInfoID;
                            rentIncreaseReason.ReasonID = item.ReasonID;
                            _dbContext.OwnerRentIncreaseReasonInfos.InsertOnSubmit(rentIncreaseReason);
                            _dbContext.SubmitChanges();
                        }
                    }
                }
                
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }

        /// <summary>
        /// Saves Owner rental increment information and updates property information (Rent History page on Owner ptition)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<OwnerPetitionPropertyInfoM> SaveOwnerRentIncreaseAndUpdatePropertyInfo(OwnerPetitionPropertyInfoM model)
        {
            ReturnResult<OwnerPetitionPropertyInfoM> result = new ReturnResult<OwnerPetitionPropertyInfoM>();
            try
            {
                if (model.OwnerPropertyID > 0)
                {
                    var propertyInfo = from r in _dbContext.OwnerPetitionPropertyInfos
                                       where r.OwnerPropertyID == model.OwnerPropertyID
                                       select r;
                    if (propertyInfo.Any())
                    {
                        propertyInfo.First().MovedInDate = new DateTime(model.MovedInDate.Year, model.MovedInDate.Month, model.MovedInDate.Day);
                        propertyInfo.First().InitialRent = model.InitialRent;
                        propertyInfo.First().RAPNoticeStatusID = model.RAPNoticeStatusID;
                        propertyInfo.First().RAPNoticeGivenDate = new DateTime(model.RAPNoticeGivenDate.Year, model.RAPNoticeGivenDate.Month, model.RAPNoticeGivenDate.Day);
                        propertyInfo.First().RentStatusID = model.RentStatusID;
                        _dbContext.SubmitChanges();
                    }
                }

                if (model.RentalInfo.Any())
                {
                    List<OwnerPetitionRentalIncrementInfoM> _rentalInfo = new List<OwnerPetitionRentalIncrementInfoM>();
                    foreach (var rent in model.RentalInfo)
                    {
                        if (rent.isDeleted == false)
                        {
                            if (rent.RentalIncreaseInfoID != 0)
                            {
                                var rentIncreaseInfo = from r in _dbContext.OwnerPetitionRentalIncrementInfos
                                                       where r.RentalIncreaseInfoID == rent.RentalIncreaseInfoID
                                                       select r;
                                if (rentIncreaseInfo.Any())
                                {
                                    rentIncreaseInfo.First().bRentIncreaseNoticeGiven = rent.bRentIncreaseNoticeGiven;
                                    rentIncreaseInfo.First().RentIncreaseNoticeDate = new DateTime(rent.RentIncreaseNoticeDate.Year, rent.RentIncreaseNoticeDate.Month, rent.RentIncreaseNoticeDate.Day);
                                    rentIncreaseInfo.First().RentIncreaseEffectiveDate = new DateTime(rent.RentIncreaseEffectiveDate.Year, rent.RentIncreaseEffectiveDate.Month, rent.RentIncreaseEffectiveDate.Day);
                                    rentIncreaseInfo.First().RentIncreasedFrom = rent.RentIncreasedFrom;
                                    rentIncreaseInfo.First().RentIncreasedTo = rent.RentIncreasedTo;
                                    _dbContext.SubmitChanges();
                                    _rentalInfo.Add(rent);
                                }
                            }
                            else
                            {
                                OwnerPetitionRentalIncrementInfo rentIncreaseInfo = new OwnerPetitionRentalIncrementInfo();
                                rentIncreaseInfo.OwnerPropertyID = model.OwnerPropertyID;
                                rentIncreaseInfo.bRentIncreaseNoticeGiven = rent.bRentIncreaseNoticeGiven;
                                rentIncreaseInfo.RentIncreaseNoticeDate = new DateTime(rent.RentIncreaseNoticeDate.Year, rent.RentIncreaseNoticeDate.Month, rent.RentIncreaseNoticeDate.Day);
                                rentIncreaseInfo.RentIncreaseEffectiveDate = new DateTime(rent.RentIncreaseEffectiveDate.Year, rent.RentIncreaseEffectiveDate.Month, rent.RentIncreaseEffectiveDate.Day);
                                rentIncreaseInfo.RentIncreasedFrom = rent.RentIncreasedFrom;
                                rentIncreaseInfo.RentIncreasedTo = rent.RentIncreasedTo;
                                _dbContext.OwnerPetitionRentalIncrementInfos.InsertOnSubmit(rentIncreaseInfo);
                                _dbContext.SubmitChanges();
                                rent.RentalIncreaseInfoID = rentIncreaseInfo.RentalIncreaseInfoID;
                                _rentalInfo.Add(rent);
                            }
                        }
                        else
                        {
                            if (rent.RentalIncreaseInfoID != 0)
                            {
                                var rentIncreaseInfo = from r in _dbContext.OwnerPetitionRentalIncrementInfos
                                                       where r.RentalIncreaseInfoID == rent.RentalIncreaseInfoID
                                                       select r;
                                if (rentIncreaseInfo.Any())
                                {
                                    _dbContext.OwnerPetitionRentalIncrementInfos.DeleteOnSubmit(rentIncreaseInfo.Where(x => x.RentalIncreaseInfoID == rent.RentalIncreaseInfoID).First());
                                }
                            }
                        }
                    }
                    model.RentalInfo = _rentalInfo;

                    //foreach (var rentIncrease in model.RentalInfo)
                    //{
                    //    if (rentIncrease.RentIncreaseReasons.Select(x => x.IsSelected == true).Any())
                    //    {
                    //        var rentIncreaseReasonDB = from r in _dbContext.OwnerRentIncreaseReasonInfos
                    //                                   where r.RentalIncreaseInfoID == rentIncrease.RentalIncreaseInfoID
                    //                                   select r;
                    //        if (rentIncreaseReasonDB.Any())
                    //        {
                    //            foreach (var item in rentIncrease.RentIncreaseReasons)
                    //            {
                    //                if (item.IsSelected)
                    //                {
                    //                    if (!rentIncreaseReasonDB.Select(x => x.ReasonID == item.ReasonID).Any())
                    //                    {
                    //                        OwnerRentIncreaseReasonInfo rentIncreaseReason = new OwnerRentIncreaseReasonInfo();
                    //                        rentIncreaseReason.RentalIncreaseInfoID = rentIncrease.RentalIncreaseInfoID;
                    //                        rentIncreaseReason.ReasonID = item.ReasonID;
                    //                        _dbContext.OwnerRentIncreaseReasonInfos.InsertOnSubmit(rentIncreaseReason);
                    //                        _dbContext.SubmitChanges();
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (rentIncreaseReasonDB.Select(x => x.ReasonID == item.ReasonID).Any())
                    //                    {
                    //                        OwnerRentIncreaseReasonInfo rentIncreaseReason = new OwnerRentIncreaseReasonInfo();
                    //                        rentIncreaseReason.RentalIncreaseInfoID = rentIncrease.RentalIncreaseInfoID;
                    //                        rentIncreaseReason.ReasonID = item.ReasonID;
                    //                        _dbContext.OwnerRentIncreaseReasonInfos.DeleteOnSubmit(rentIncreaseReason);
                    //                        _dbContext.SubmitChanges();
                    //                    }
                    //                }

                    //            }
                    //        }
                    //        else
                    //        {
                    //            foreach (var item in rentIncrease.RentIncreaseReasons)
                    //            {
                    //                if (item.IsSelected)
                    //                {
                    //                    OwnerRentIncreaseReasonInfo rentIncreaseReason = new OwnerRentIncreaseReasonInfo();
                    //                    rentIncreaseReason.RentalIncreaseInfoID = rentIncrease.RentalIncreaseInfoID;
                    //                    rentIncreaseReason.ReasonID = item.ReasonID;
                    //                    _dbContext.OwnerRentIncreaseReasonInfos.InsertOnSubmit(rentIncreaseReason);
                    //                    _dbContext.SubmitChanges();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }

                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }        

        /// <summary>
        /// Files Owner petition and generates CASE ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> SubmitOwnerPetition(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            CaseDetail caseDetails = new CaseDetail();
            int ownerPetitionID = 0;
            int petitionID = 0;
            try
            
            {                
                ownerPetitionID = SaveOwnerPetitionInfo(model.OwnerPetitionInfo);
                if(ownerPetitionID == 0)
                {
                    result.status.Status = StatusEnum.DatabaseException;
                    return result;
                }
                else
                {
                    petitionID = GetPetitionID(ownerPetitionID, model.PetitionCategoryID);
                    if (petitionID == 0)
                    {
                        result.status.Status = StatusEnum.DatabaseException;
                        return result;
                    }
                }
                caseDetails.PetitionID = petitionID;
                caseDetails.PetitionCategoryID = model.PetitionCategoryID;
                caseDetails.CaseFiledBy = model.CaseFileBy;
                caseDetails.bCaseFiledByThirdParty = model.bCaseFiledByThirdParty;
                caseDetails.CreatedDate = DateTime.Now;
                _dbContext.CaseDetails.InsertOnSubmit(caseDetails);
                _dbContext.SubmitChanges();
                model.C_ID = caseDetails.C_ID;
                model.CaseID = caseDetails.CaseID;

                var applicantInfo = _dbContext.OwnerPetitionApplicantInfos.Where(r => r.OwnerPetitionApplicantInfoID == model.OwnerPetitionInfo.ApplicantInfo.OwnerPetitionApplicantInfoID).FirstOrDefault();
                applicantInfo.bPetitionFiled = true;             
                var propertyInfo = _dbContext.OwnerPetitionPropertyInfos.Where(r => r.OwnerPropertyID == model.OwnerPetitionInfo.PropertyInfo.OwnerPropertyID).FirstOrDefault();
                propertyInfo.bPetitionFiled = true;
                _dbContext.SubmitChanges();

                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch(Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commondbHandler.SaveErrorLog(result.status);
                return result;
            }
        }
       private int SaveOwnerPetitionInfo(OwnerPetitionInfoM model)
        {
            int ownerPetitionID = 0;
            OwnerPetitionInfo petitionInfo = new OwnerPetitionInfo();
            petitionInfo.OwnerPetitionApplicantInfoID = model.ApplicantInfo.OwnerPetitionApplicantInfoID;
            petitionInfo.OwnerPropertyID = model.PropertyInfo.OwnerPropertyID;
            petitionInfo.bAgreeToCityMediation = model.bAgreeToCityMediation;           
            petitionInfo.CreatedDate = DateTime.Now;
            _dbContext.OwnerPetitionInfos.InsertOnSubmit(petitionInfo);
            _dbContext.SubmitChanges();
            ownerPetitionID = petitionInfo.OwnerPetitionID;
            return ownerPetitionID;
        }
        #endregion

       private List<UnitTypeM> getUnitTypes()
       {
           List<UnitTypeM> _units = new List<UnitTypeM>();
           var units = _dbContext.UnitTypes;
           if (units != null)
           {
               foreach (var unit in units)
               {
                   UnitTypeM _unit = new UnitTypeM();
                   _unit.UnitTypeID = unit.UnitTypeID;
                   _unit.UnitDescription = unit.Description;
                   _units.Add(_unit);
               }
           }
           return _units;
       }

        private List<RAPNoticeStausM> getRAPNoticeStatus()
        {
            List<RAPNoticeStausM> _rapStatus = new List<RAPNoticeStausM>();
            var rapStatus = _dbContext.RAPNoticeStatus;
            if(rapStatus !=null)
            {
                foreach(var item in rapStatus)
                {
                    RAPNoticeStausM _status = new RAPNoticeStausM();
                    _status.RAPNoticeStatusID = item.RAPNoticeStatusID;
                    _status.RAPNoticeStatus = item.RAPNoticeStatus1;
                    _rapStatus.Add(_status);
                }
            }
            return _rapStatus;
        }

        private List<CurrentOnRentM> getCurrentRentStatus()
        {
            List<CurrentOnRentM> _rentStatusItems = new List<CurrentOnRentM>();
            var rentStausItems = _dbContext.CurrentOnRentStatus;
           if(rentStausItems !=null)
            {
                foreach (var rentStatusItem in rentStausItems)
                {
                    CurrentOnRentM _rentStatusItem = new CurrentOnRentM();
                    _rentStatusItem.StatusID = rentStatusItem.RentStatusID;
                    _rentStatusItem.Status = rentStatusItem.RentStatus;
                    _rentStatusItems.Add(_rentStatusItem);
                }
            }
           return _rentStatusItems;
        }
    
    
    }
}
