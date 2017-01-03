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
        private readonly DashboardDataContext _dbDashboard;

        private ICommonDBHandler _commondbHandler;
        private IDashboardDBHandler _dashboarddbHandler;
        private IExceptionHandler _eHandler;
        //To be modified as dependency
        IAccountManagementDBHandler _accountdbHandler = new AccountManagementDBHandler();
        public ApplicationProcessingDBHandler(ICommonDBHandler commondbHandler, IDashboardDBHandler dashboarddbHandler, IExceptionHandler eHandler)
        {
            this._commondbHandler = commondbHandler;
            this._dashboarddbHandler = dashboarddbHandler;
            this._eHandler = eHandler;
            _dbContext = new ApplicationProcessingDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
            _dbAccount = new AccountManagementDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
            _dbDashboard = new DashboardDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
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

        public ReturnResult<TenantResponsePageSubnmissionStatusM> GetTRPageSubmissionStatus(int CustomerID)
        {
            ReturnResult<TenantResponsePageSubnmissionStatusM> result = new ReturnResult<TenantResponsePageSubnmissionStatusM>();
            TenantResponsePageSubnmissionStatusM model = new TenantResponsePageSubnmissionStatusM();
            try
            {
                var tResponse= _dbContext.TenantResponsePageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
                if (tResponse != null)
                {
                    model.ImportantInformation = Convert.ToBoolean(tResponse.ImportantInformation);
                    model.ApplicantInformation = Convert.ToBoolean(tResponse.ApplicantInformation);
                    model.ExemptionContested = Convert.ToBoolean(tResponse.ExemptionContested);
                    model.RentHistory = Convert.ToBoolean(tResponse.RentHistory);
                    model.AdditionalDocumentation = Convert.ToBoolean(tResponse.AdditionalDocumentation);
                    model.Review = Convert.ToBoolean(tResponse.Review);
                    model.Verification = Convert.ToBoolean(tResponse.Verification);
                    if(model.ImportantInformation == true)
                    {
                        model.PetitionType = true;
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

        

  

        public ReturnResult<AppealPageSubnmissionStatusM> GetAppealPageSubmissionStatus(int CustomerID)
        {
            ReturnResult<AppealPageSubnmissionStatusM> result = new ReturnResult<AppealPageSubnmissionStatusM>();
            AppealPageSubnmissionStatusM model = new AppealPageSubnmissionStatusM();
            try
            {
                var appealPageDB = _dbContext.AppealPageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
                if (appealPageDB != null)
                {
                    model.ImportantInformation = Convert.ToBoolean(appealPageDB.ImportantInformation);
                    model.ApplicantInformation = Convert.ToBoolean(appealPageDB.ApplicantInformation);
                    model.GroundsOfAppeal = Convert.ToBoolean(appealPageDB.GroundsOfAppeal);
                    model.AdditionalDocumentation = Convert.ToBoolean(appealPageDB.AdditionalDocumentation);
                    model.Review = Convert.ToBoolean(appealPageDB.Review);
                    model.ServingAppeal = Convert.ToBoolean(appealPageDB.ServingAppeal);
                    if (model.ImportantInformation == true)
                    {
                        model.AppealType = true;
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
        public ReturnResult<CaseInfoM> GetCaseInfo(string CaseID, int CustomerID)
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
                //caseInfo.AppealDate = Convert.ToDateTime(CaseDetailsDB.AppealDate);
                
                //if(CaseDetailsDB.TenantAppealID > 0)
                //{
                caseInfo.TenantAppealInfo = GetAppealApplicantInfo(caseInfo.CaseID, CustomerID).result;
                //}
                //else
                //{
                //    caseInfo.TenantAppealInfo.ApplicantUserInfo = caseInfo.TenantPetitionInfo.ApplicantUserInfo;
                //    caseInfo.TenantAppealInfo.AppealPropertyUserInfo = caseInfo.TenantAppealInfo.ApplicantUserInfo;
                //}
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
        public ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst(int UserID)
        {
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try 
            {
                AccountManagementDataContext db = new AccountManagementDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
                List<CaseInfoM> cases = new List<CaseInfoM>();

                var CityUserDB = db.CityUserAccounts.Where(x => x.CityUserID == UserID).FirstOrDefault();
                if(CityUserDB.IsAnalyst == true)
                {
                    var casesDB = _dbContext.CaseDetails.Where(x => x.CityAnalystUserID == UserID).ToList();
                    foreach (var item in casesDB)
                    {
                        CaseInfoM caseinfo = new CaseInfoM();
                        caseinfo.CaseID = item.CaseID;
                        caseinfo.C_ID = item.C_ID;

                        if (item.CityAnalystUserID != null)
                        {
                            var CityAnalystDB = db.CityUserAccounts.Where(x => x.CityUserID == item.CityAnalystUserID).FirstOrDefault();

                            caseinfo.CityAnalyst.UserID = (int)item.CityAnalystUserID;
                            caseinfo.CityAnalyst.FirstName = CityAnalystDB.FirstName;
                            caseinfo.CityAnalyst.LastName = CityAnalystDB.LastName;
                        }
                        if (item.HearingOfficerUserID != null)
                        {
                            var CityDB = db.CityUserAccounts.Where(x => x.CityUserID == item.HearingOfficerUserID).FirstOrDefault();

                            caseinfo.HearingOfficer.UserID = (int)item.HearingOfficerUserID;
                            caseinfo.HearingOfficer.FirstName = CityDB.FirstName;
                            caseinfo.HearingOfficer.LastName = CityDB.LastName;
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

                        var caseActivityStatusDb = _dbDashboard.CaseActivityStatus.Where(x => x.C_ID == caseinfo.C_ID).OrderByDescending(y => y.LastModifiedDate).FirstOrDefault();
                        if (caseActivityStatusDb != null)
                        {
                            var ActivityDb = _dbDashboard.Activities.Where(x => x.ActivityID == caseActivityStatusDb.ActivityID).FirstOrDefault();
                            if (ActivityDb != null)
                            {
                                caseinfo.LastActivity = ActivityDb.ActivityName;
                            }
                        }
                        cases.Add(caseinfo);
                    }
                }
                else if(CityUserDB.IsHearingOfficer == true)
                {
                    var casesDB = _dbContext.CaseDetails.Where(x => x.HearingOfficerUserID == UserID).ToList();
                    foreach (var item in casesDB)
                    {
                        CaseInfoM caseinfo = new CaseInfoM();
                        caseinfo.CaseID = item.CaseID;
                        caseinfo.C_ID = item.C_ID;

                        if (item.CityAnalystUserID != null)
                        {
                            var CityAnalystDB = db.CityUserAccounts.Where(x => x.CityUserID == item.CityAnalystUserID).FirstOrDefault();

                            caseinfo.CityAnalyst.UserID = (int)item.CityAnalystUserID;
                            caseinfo.CityAnalyst.FirstName = CityAnalystDB.FirstName;
                            caseinfo.CityAnalyst.LastName = CityAnalystDB.LastName;
                        }
                        if (item.HearingOfficerUserID != null)
                        {
                            var CityDB = db.CityUserAccounts.Where(x => x.CityUserID == item.HearingOfficerUserID).FirstOrDefault();

                            caseinfo.HearingOfficer.UserID = (int)item.HearingOfficerUserID;
                            caseinfo.HearingOfficer.FirstName = CityDB.FirstName;
                            caseinfo.HearingOfficer.LastName = CityDB.LastName;
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

                        var caseActivityStatusDb = _dbDashboard.CaseActivityStatus.Where(x => x.C_ID == caseinfo.C_ID).OrderByDescending(y => y.LastModifiedDate).FirstOrDefault();
                        if (caseActivityStatusDb != null)
                        {
                            var ActivityDb = _dbDashboard.Activities.Where(x => x.ActivityID == caseActivityStatusDb.ActivityID).FirstOrDefault();
                            if (ActivityDb != null)
                            {
                                caseinfo.LastActivity = ActivityDb.ActivityName;
                            }
                        }
                        cases.Add(caseinfo);
                    }
                }
                else if(CityUserDB.IsHearingOfficer == false && CityUserDB.IsAnalyst == false )
                { 
                    var casesDB = _dbContext.CaseDetails.Where(x => x.CityAnalystUserID == null || x.HearingOfficerUserID == null).ToList();
                    foreach (var item in casesDB)
                    {
                        CaseInfoM caseinfo = new CaseInfoM();
                        caseinfo.CaseID = item.CaseID;
                        caseinfo.C_ID = item.C_ID;

                        if (item.CityAnalystUserID != null)
                        {
                            var CityAnalystDB = db.CityUserAccounts.Where(x => x.CityUserID == item.CityAnalystUserID).FirstOrDefault();

                            caseinfo.CityAnalyst.UserID = (int)item.CityAnalystUserID;
                            caseinfo.CityAnalyst.FirstName = CityAnalystDB.FirstName;
                            caseinfo.CityAnalyst.LastName = CityAnalystDB.LastName;
                        }
                        if (item.HearingOfficerUserID != null)
                        {
                            var CityDB = db.CityUserAccounts.Where(x => x.CityUserID == item.HearingOfficerUserID).FirstOrDefault();

                            caseinfo.HearingOfficer.UserID = (int)item.HearingOfficerUserID;
                            caseinfo.HearingOfficer.FirstName = CityDB.FirstName;
                            caseinfo.HearingOfficer.LastName = CityDB.LastName;
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

                        var caseActivityStatusDb = _dbDashboard.CaseActivityStatus.Where(x => x.C_ID == caseinfo.C_ID).OrderByDescending(y => y.LastModifiedDate).FirstOrDefault();
                        if (caseActivityStatusDb != null)
                        {
                            var ActivityDb = _dbDashboard.Activities.Where(x => x.ActivityID == caseActivityStatusDb.ActivityID).FirstOrDefault();
                            if (ActivityDb != null)
                            {
                                caseinfo.LastActivity = ActivityDb.ActivityName;
                            }
                        }
                        cases.Add(caseinfo);
                    }
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
                    AccountManagementDataContext db = new AccountManagementDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
                    if (item.CityAnalystUserID != null)
                    {
                        var CityAnalystDB = db.CityUserAccounts.Where(x => x.CityUserID == item.CityAnalystUserID).FirstOrDefault();

                        caseinfo.CityAnalyst.UserID = (int)item.CityAnalystUserID;
                        caseinfo.CityAnalyst.FirstName = CityAnalystDB.FirstName;
                        caseinfo.CityAnalyst.LastName = CityAnalystDB.LastName;
                    }
                    if (item.HearingOfficerUserID != null)
                    {
                        var CityDB = db.CityUserAccounts.Where(x => x.CityUserID == item.HearingOfficerUserID).FirstOrDefault();

                        caseinfo.HearingOfficer.UserID = (int)item.HearingOfficerUserID;
                        caseinfo.HearingOfficer.FirstName = CityDB.FirstName;
                        caseinfo.HearingOfficer.LastName = CityDB.LastName;
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
        private ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfoForView(int PetitionID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {

                var TenantPetitionInfoDB = _dbContext.TenantPetitionInfos.Where(x => x.TenantPetitionID == PetitionID).FirstOrDefault();
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
                    tenantPetitionInfo.SelectedRangeOfUnits.RangeID = Convert.ToInt32(TenantPetitionInfoDB.RangeID);
                    tenantPetitionInfo.bCurrentRentStatus = TenantPetitionInfoDB.bRentStatus;
                    tenantPetitionInfo.ProvideExplanation = TenantPetitionInfoDB.ProvideExplanation;
                    tenantPetitionInfo.CustomerID =(int) TenantPetitionInfoDB.PetitionFiledBy;                    
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
        public ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                List<UnitTypeM> _units = new List<UnitTypeM>();
                List<NumberRangeForUnitsM> _rangeOfUnits = new List<NumberRangeForUnitsM>();

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
                    tenantPetitionInfo.SelectedRangeOfUnits.RangeID = Convert.ToInt32(TenantPetitionInfoDB.RangeID);
                    tenantPetitionInfo.bCurrentRentStatus = TenantPetitionInfoDB.bRentStatus;
                    tenantPetitionInfo.ProvideExplanation = TenantPetitionInfoDB.ProvideExplanation;
                    tenantPetitionInfo.CustomerID = (int)TenantPetitionInfoDB.PetitionFiledBy;
                }
                tenantPetitionInfo.UnitTypes = _units;
                tenantPetitionInfo.RangeOfUnits = _rangeOfUnits;
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

        public ReturnResult<CaseInfoM> GetPetitionViewInfo(int C_ID)
        {
            ReturnResult<CaseInfoM> caseInfoResult = new ReturnResult<CaseInfoM>();
            ReturnResult<TenantPetitionInfoM> tenantPetitionResult = new ReturnResult<TenantPetitionInfoM>();
            ReturnResult<List<PetitionGroundM>> GroundsResult = new ReturnResult<List<PetitionGroundM>>();
            ReturnResult<TenantRentalHistoryM> RentalHistoryResult = new ReturnResult<TenantRentalHistoryM>();
            ReturnResult<LostServicesPageM> LostServicesResult = new ReturnResult<LostServicesPageM>();
            CaseInfoM caseInfo = new CaseInfoM();
            try
            {                
                var caseDB = _dbContext.CaseDetails.Where(x => x.C_ID == C_ID).FirstOrDefault();
                var PetitionDB = _dbContext.PetitionDetails.Where(x => x.PetitionID == caseDB.PetitionID).FirstOrDefault();
                if (PetitionDB.TenantPetitionID != 0)
                {
                    tenantPetitionResult = GetTenantApplicationInfoForView((int)PetitionDB.TenantPetitionID);
                    if (tenantPetitionResult.status.Status != StatusEnum.Success)
                        return caseInfoResult;

                    GroundsResult = GetPetitionGroundInfo((int)tenantPetitionResult.result.PetitionID);
                    if (GroundsResult != null)
                    {
                        tenantPetitionResult.result.PetitionGrounds = GroundsResult.result;
                        tenantPetitionResult.status = GroundsResult.status;
                        if (GroundsResult.status.Status != StatusEnum.Success)
                            return caseInfoResult;
                    }

                    RentalHistoryResult = GetRentalHistoryInfo((int)tenantPetitionResult.result.PetitionID);
                    if (RentalHistoryResult != null)
                    {
                        tenantPetitionResult.result.TenantRentalHistory = RentalHistoryResult.result;
                        tenantPetitionResult.status = RentalHistoryResult.status;
                        if (RentalHistoryResult.status.Status != StatusEnum.Success)
                            return caseInfoResult;
                    }

                    LostServicesResult = GetTenantLostServiceInfo((int)tenantPetitionResult.result.PetitionID);
                    if (LostServicesResult != null)
                    {
                        tenantPetitionResult.result.LostServicesPage = LostServicesResult.result;
                        tenantPetitionResult.status = LostServicesResult.status;
                        if (LostServicesResult.status.Status != StatusEnum.Success)
                            return caseInfoResult;
                    }

                    caseInfo.TenantPetitionInfo = tenantPetitionResult.result;
                }
                caseInfoResult.result = caseInfo;
                caseInfoResult.status = new OperationStatus() { Status = StatusEnum.Success };
                return caseInfoResult;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                tenantPetitionResult.status = eHandler.HandleException(ex);
                return caseInfoResult;
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

        public ReturnResult<TenantAppealInfoM> GetAppealApplicantInfo(string caseID, int CustomerID)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            TenantAppealInfoM appealInfo = new TenantAppealInfoM();
            try
            {

                var appealInfoDB = _dbContext.TenantAppealDetails.Where(x => x.AppealFiledBy == CustomerID && x.IsSubmitted == false && x.CaseNumber == caseID).FirstOrDefault();
                if (appealInfoDB == null)
                {
                    result.result = appealInfo;
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                    return result;
                }
                else
                {
                    appealInfo.AppealID = appealInfoDB.AppealID;
                    appealInfo.ApplicantUserInfo = _commondbHandler.GetUserInfo(Convert.ToInt32(appealInfoDB.ApplicantUserID)).result;
                    appealInfo.bThirdPartyRepresentation = Convert.ToBoolean(appealInfoDB.bThirdPartyRepresentation);
                    if (appealInfoDB.bThirdPartyRepresentation == true)
                    {
                        appealInfo.AppealThirdPartyInfo = _commondbHandler.GetUserInfo((int)appealInfoDB.ThirdPartyUserID).result;
                    }
                    appealInfo.AppealPropertyUserInfo = _commondbHandler.GetUserInfo((int)appealInfoDB.PropertyUserID).result;
                    appealInfo.AppealDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(appealInfoDB.AppealDate));
                }

                result.result = appealInfo;
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

                _commondbHandler.PetitionFiledActivity(caseInfo.C_ID, caseInfo.CaseFileBy);

                var AppealDB = _dbContext.TenantAppealDetails.Where(x => x.AppealID == caseInfo.TenantAppealInfo.AppealID).FirstOrDefault();
                if (AppealDB != null)
                {
                    AppealDB.IsSubmitted = true;
                    _dbContext.SubmitChanges();
                }

                var PageStatus = _dbContext.AppealPageSubmissionStatus
                                            .Where(x => x.CustomerID == caseInfo.TenantAppealInfo.AppealFiledBy).FirstOrDefault();
                if (PageStatus != null)
                {
                    _dbContext.AppealPageSubmissionStatus.DeleteOnSubmit(PageStatus);
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
                var CustDetails = _dbAccount.CustomerDetails.Where(x => x.CustomerID == caseInfo.CaseFileBy).FirstOrDefault();
                if(CustDetails != null)
                {
                    if(CustDetails.CustomerIdentityKey != caseInfo.TenantPetitionInfo.Verification.pinVerify)
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.PinError };
                        return result;
                    }
                    if(CustDetails.CustomerIdentityKey != caseInfo.TenantPetitionInfo.Verification.pinMediation)
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.PinError };
                        return result;
                    }
                }
                TenantPetitionVerification verificationDB = new TenantPetitionVerification();
                verificationDB.bCaseMediation = caseInfo.TenantPetitionInfo.Verification.bCaseMediation;
                verificationDB.bDeclarePenalty = caseInfo.TenantPetitionInfo.Verification.bDeclarePenalty;
                verificationDB.bThirdParty = caseInfo.TenantPetitionInfo.Verification.bThirdParty;
                verificationDB.bThirdPartyMediation = caseInfo.TenantPetitionInfo.Verification.bThirdPartyMediation;
                verificationDB.PetitionID = caseInfo.TenantPetitionInfo.PetitionID;
                verificationDB.CreatedDate = DateTime.Now;
                _dbContext.TenantPetitionVerifications.InsertOnSubmit(verificationDB);
                _dbContext.SubmitChanges();

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

                _commondbHandler.PetitionFiledActivity(caseInfo.C_ID, caseInfo.CaseFileBy);

                //using (DashboardDataContext db = new DashboardDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]))
                //{
                //    string errorMessage = "";
                //    int? errorCode = 0;
                //    //TBD
                //    int returnCode = db.USP_NewActivityStatus_Save((int)ActivityDefaults.ActivityPetitionFiled, (int)StatusDefaults.StatusSubmitted,
                //                     caseInfo.C_ID, "", DateTime.Now, caseInfo.CaseFileBy, ref errorMessage, ref errorCode);

                //    if (errorCode != 0)
                //    {
                //        result.result = null;
                //        result.status = new OperationStatus() { Status = StatusEnum.DatabaseMessage, StatusMessage = errorMessage };
                //        return result;
                //    }
                //}

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
                    petitionDB.RangeID = caseInfo.TenantPetitionInfo.SelectedRangeOfUnits.RangeID;
                    petitionDB.IsSubmitted = false;
                    _dbContext.SubmitChanges();
                    caseInfo.TenantPetitionInfo.PetitionID = petitionDB.TenantPetitionID;

                    var PageStatus = _dbContext.TenantPetitionPageSubmissionStatus.Where(x => x.CustomerID == caseInfo.TenantPetitionInfo.CustomerID).FirstOrDefault();
                    if (PageStatus != null)
                    {
                        PageStatus.ApplicantInformation = true;
                        PageStatus.ImportantInformation = true;
                        _dbContext.SubmitChanges();
                    }
                    else
                    {
                        var PageStatusNew = new TenantPetitionPageSubmissionStatus();
                        PageStatusNew.CustomerID = caseInfo.TenantPetitionInfo.CustomerID;
                        PageStatusNew.ApplicantInformation = true;
                        PageStatusNew.ImportantInformation = true;
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
                    if (item.IsDeleted == false)
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
                    }
                }
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
                        if (item.IsDeleted == false)
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
                        if (item.IsDeleted == false)
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

        public ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo, int CustomerID)
        {
            
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            TenantAppealInfoM tenantAppeal = new TenantAppealInfoM();
            try
            {
                CommonDBHandler _dbCommon = new CommonDBHandler();

                var appealExistsDB = _dbContext.TenantAppealDetails.Where(x => x.CaseNumber == caseInfo.CaseID && x.AppealFiledBy == CustomerID).FirstOrDefault();
                if (appealExistsDB != null)
                {
                    tenantAppeal.AppealID = appealExistsDB.AppealID;
                    var applicantUserResult = _commondbHandler.SaveUserInfo(caseInfo.TenantAppealInfo.ApplicantUserInfo);
                    if (applicantUserResult.status.Status != StatusEnum.Success)
                    {
                        result.status = applicantUserResult.status;
                        return result;
                    }
                    appealExistsDB.ApplicantUserID = applicantUserResult.result.UserID;

                    var propertyUserResult = _commondbHandler.SaveUserInfo(caseInfo.TenantAppealInfo.AppealPropertyUserInfo);
                    if (applicantUserResult.status.Status != StatusEnum.Success)
                    {
                        result.status = propertyUserResult.status;
                        return result;
                    }
                    appealExistsDB.PropertyUserID = propertyUserResult.result.UserID;
                    appealExistsDB.AppealFiledBy = CustomerID;
                    appealExistsDB.CaseNumber = caseInfo.CaseID;
                    appealExistsDB.AppealCategoryID = caseInfo.TenantAppealInfo.AppealCategoryID;
                    appealExistsDB.IsSubmitted = false;
                    appealExistsDB.CreatedDate = DateTime.Now;
                    appealExistsDB.AppealDate = new DateTime(caseInfo.TenantAppealInfo.AppealDate.Year, caseInfo.TenantAppealInfo.AppealDate.Month, caseInfo.TenantAppealInfo.AppealDate.Day);
                    _dbContext.SubmitChanges();
                }
                else
                {
                    TenantAppealDetail appealDB = new TenantAppealDetail();
                    appealDB.AppealFiledBy = CustomerID;
                    appealDB.CaseNumber = caseInfo.CaseID;
                    appealDB.AppealCategoryID = caseInfo.TenantAppealInfo.AppealCategoryID;
                    appealDB.IsSubmitted = false;
                    appealDB.CreatedDate = DateTime.Now;
                    appealDB.AppealDate = new DateTime(caseInfo.TenantAppealInfo.AppealDate.Year, caseInfo.TenantAppealInfo.AppealDate.Month, caseInfo.TenantAppealInfo.AppealDate.Day);
                    var applicantUserResult = _commondbHandler.SaveUserInfo(caseInfo.TenantAppealInfo.ApplicantUserInfo);
                    if (applicantUserResult.status.Status != StatusEnum.Success)
                    {
                        result.status = applicantUserResult.status;
                        return result;
                    }
                    appealDB.ApplicantUserID = applicantUserResult.result.UserID;

                    var propertyUserResult = _commondbHandler.SaveUserInfo(caseInfo.TenantAppealInfo.AppealPropertyUserInfo);
                    if (applicantUserResult.status.Status != StatusEnum.Success)
                    {
                        result.status = propertyUserResult.status;
                        return result;
                    }
                    appealDB.PropertyUserID = propertyUserResult.result.UserID;

                    _dbContext.TenantAppealDetails.InsertOnSubmit(appealDB);
                    _dbContext.SubmitChanges();
                    caseInfo.TenantAppealInfo.AppealID = appealDB.AppealID;
                }
                //CaseDetail caseDB = _dbContext.CaseDetails.First(i => i.CaseID == caseInfo.CaseID);
                //caseDB.TenantAppealID = caseInfo.TenantAppealInfo.AppealID;
                //caseDB.LastModifiedDate = DateTime.Now;
                //_dbContext.SubmitChanges();

                var PageStatus = _dbContext.AppealPageSubmissionStatus
                                            .Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                if (PageStatus != null)
                {
                    PageStatus.ApplicantInformation = true;
                    PageStatus.ImportantInformation = true;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var PageStatusNew = new AppealPageSubmissionStatus();
                    PageStatusNew.CustomerID = CustomerID;
                    PageStatusNew.ApplicantInformation = true;
                    PageStatusNew.ImportantInformation = true;

                    _dbContext.AppealPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                    _dbContext.SubmitChanges();
                }

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
                var PageStatus = _dbContext.AppealPageSubmissionStatus
                                            .Where(x => x.CustomerID == tenantAppealInfo.AppealFiledBy).FirstOrDefault();
                if (PageStatus != null)
                {
                    PageStatus.GroundsOfAppeal = true;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var PageStatusNew = new AppealPageSubmissionStatus();
                    PageStatusNew.CustomerID = tenantAppealInfo.AppealFiledBy;
                    PageStatusNew.GroundsOfAppeal = true;

                    _dbContext.AppealPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                    _dbContext.SubmitChanges();
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

                var PageStatus = _dbContext.AppealPageSubmissionStatus
                                            .Where(x => x.CustomerID == tenantAppealInfo.AppealFiledBy).FirstOrDefault();
                if (PageStatus != null)
                {
                    PageStatus.ServingAppeal = true;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var PageStatusNew = new AppealPageSubmissionStatus();
                    PageStatusNew.CustomerID = tenantAppealInfo.AppealFiledBy;
                    PageStatusNew.ServingAppeal = true;

                    _dbContext.AppealPageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                    _dbContext.SubmitChanges();
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
                    if (item.IsDeleted == false)
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
      
        #region "TenantResponseGet"
        public ReturnResult<CaseInfoM> GetTenantResponseApplicationInfo(string CaseNumber, int CustomerID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var CaseDetailsDB = _dbContext.CaseDetails.Where(x => x.CaseID == CaseNumber).FirstOrDefault();
                if (CaseDetailsDB == null)
                {
                    result.result = null;
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                }
                CaseInfoM caseInfo = new CaseInfoM();
                caseInfo.C_ID = CaseDetailsDB.C_ID;
                caseInfo.CaseID = CaseDetailsDB.CaseID;

                List<UnitTypeM> _units = new List<UnitTypeM>();
                List<NumberRangeForUnitsM> _rangeOfUnits = new List<NumberRangeForUnitsM>();

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
                var TenantResponseInfoDB = _dbContext.TenantResponseApplicationInfos.Where(x => x.ResponseFiledBy == CustomerID
                                                && x.C_ID == caseInfo.C_ID
                                                && x.IsSubmitted == false).FirstOrDefault();
                TenantResponseInfoM tenantResponseInfo = new TenantResponseInfoM();
                if (TenantResponseInfoDB != null)
                {
                    tenantResponseInfo.TenantResponseID = TenantResponseInfoDB.TenantResponseID;
                    tenantResponseInfo.bThirdPartyRepresentation = (bool)TenantResponseInfoDB.bThirdPartyRepresentation;
                    if (tenantResponseInfo.bThirdPartyRepresentation)
                    {
                        tenantResponseInfo.ThirdPartyInfo = _commondbHandler.GetUserInfo((int)TenantResponseInfoDB.ThirdPartyUserID).result;
                    }
                    tenantResponseInfo.ApplicantUserInfo = _commondbHandler.GetUserInfo((int)TenantResponseInfoDB.ApplicantUserID).result;
                    tenantResponseInfo.OwnerInfo = _commondbHandler.GetUserInfo((int)TenantResponseInfoDB.OwnerUserID).result;
                    tenantResponseInfo.PropertyManager = _commondbHandler.GetUserInfo((int)TenantResponseInfoDB.PropertyManagerUserID).result;
                    if (tenantResponseInfo.OwnerInfo.UserID == tenantResponseInfo.PropertyManager.UserID)
                    {
                        tenantResponseInfo.bSameAsOwnerInfo = true;
                    }
                    tenantResponseInfo.NumberOfUnits = (int)TenantResponseInfoDB.NumberOfUnits;
                    tenantResponseInfo.UnitTypeId = TenantResponseInfoDB.UnitTypeID;
                    tenantResponseInfo.SelectedRangeOfUnits.RangeID = Convert.ToInt32(TenantResponseInfoDB.RangeID);
                    tenantResponseInfo.bCurrentRentStatus = TenantResponseInfoDB.bRentStatus;
                    tenantResponseInfo.ProvideExplanation = TenantResponseInfoDB.ProvideExplanation;
                    tenantResponseInfo.CustomerID = (int)TenantResponseInfoDB.ResponseFiledBy;
                }
                tenantResponseInfo.UnitTypes = _units;
                tenantResponseInfo.RangeOfUnits = _rangeOfUnits;

                
                caseInfo.TenantResponseInfo = tenantResponseInfo;
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

        public ReturnResult<CaseInfoM> GetTenantResponseExemptContestedInfo(int TenantResponseID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            CaseInfoM caseInfo = new CaseInfoM();
            TenantResponseExemptContestedInfoM exemptContested = new TenantResponseExemptContestedInfoM();
            try
            {
                var exemptContestedDB = _dbContext.TenantResponseExemptContestedInfos.Where(x => x.TenantResponseID == TenantResponseID).FirstOrDefault();
                if (exemptContestedDB != null)
                {
                    exemptContested.TenantResponseID = (int)exemptContestedDB.TenantResponseID;
                    exemptContested.Explaination = exemptContestedDB.Explaination;
                }
                caseInfo.TenantResponseInfo.ExemptContestedInfo = exemptContested;

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

        public ReturnResult<TenantResponseRentalHistoryM> GetTenantResponseRentalHistoryInfo(int TenantResponseID)
        {
            ReturnResult<TenantResponseRentalHistoryM> result = new ReturnResult<TenantResponseRentalHistoryM>();
            TenantResponseRentalHistoryM tenantResponseRentalHistory = new TenantResponseRentalHistoryM();
            try
            {
                var TenantResponseRentalHistoryDB = _dbContext.TenantResponseRentalHistories.Where(x => x.TenantResponseID == TenantResponseID).FirstOrDefault();
                if (TenantResponseRentalHistoryDB != null)
                {
                    tenantResponseRentalHistory.TenantResponseID = TenantResponseRentalHistoryDB.TenantResponseID;
                    tenantResponseRentalHistory.RentalAgreementDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(TenantResponseRentalHistoryDB.RentalAgreementDate));
                    tenantResponseRentalHistory.MoveInDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(TenantResponseRentalHistoryDB.MoveInDate));
                    tenantResponseRentalHistory.InitialRent = Convert.ToDecimal(TenantResponseRentalHistoryDB.InitialRent);
                    tenantResponseRentalHistory.bRAPNoticeGiven = Convert.ToBoolean(TenantResponseRentalHistoryDB.bRAPNoticeGiven);
                    tenantResponseRentalHistory.RAPNoticeGivenDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(TenantResponseRentalHistoryDB.RAPNoticeGivenDate));
                    
                    var TenantRentalIncrementInfoDB = _dbContext.TenantResponseRentalIncrementInfos.Where(x => x.TenantResponseID == TenantResponseID).ToList();
                    foreach (var item in TenantRentalIncrementInfoDB)
                    {
                        TenantResponseRentIncreaseInfoM objTenantRentIncreaseInfoM = new TenantResponseRentIncreaseInfoM();
                        objTenantRentIncreaseInfoM.bRentIncreaseNoticeGiven = Convert.ToBoolean(item.bRentIncreaseNoticeGiven);
                        objTenantRentIncreaseInfoM.RentIncreaseNoticeDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseNoticeDate));
                        objTenantRentIncreaseInfoM.RentIncreaseEffectiveDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseEffectiveDate));
                        objTenantRentIncreaseInfoM.RentIncreasedFrom = Convert.ToDecimal(item.RentIncreasedFrom);
                        objTenantRentIncreaseInfoM.RentIncreasedTo = Convert.ToDecimal(item.RentIncreasedTo);

                        tenantResponseRentalHistory.RentIncreases.Add(objTenantRentIncreaseInfoM);
                    }
                }

                result.result = tenantResponseRentalHistory;
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
        
        //Get Review Tenant Respomse
        public ReturnResult<TenantResponseInfoM> GetTenantResponseReviewInfo(string CaseNumber, int CustomerID)
        {
            ReturnResult<TenantResponseInfoM> tenantResponseResult = new ReturnResult<TenantResponseInfoM>();
            ReturnResult<CaseInfoM> ApplicationInfoResult = new ReturnResult<CaseInfoM>();
            ReturnResult<CaseInfoM> ExemptContestedInfoResult = new ReturnResult<CaseInfoM>();
            ReturnResult<TenantResponseRentalHistoryM> RentalHistoryResult = new ReturnResult<TenantResponseRentalHistoryM>();
            
            
            try
            {
                ApplicationInfoResult = GetTenantResponseApplicationInfo(CaseNumber, CustomerID);
                tenantResponseResult.result = ApplicationInfoResult.result.TenantResponseInfo;
                if (ApplicationInfoResult.status.Status != StatusEnum.Success)
                    return tenantResponseResult;

                ExemptContestedInfoResult = GetTenantResponseExemptContestedInfo((int)tenantResponseResult.result.TenantResponseID);
                if (ExemptContestedInfoResult != null)
                {
                    tenantResponseResult.result.ExemptContestedInfo = ExemptContestedInfoResult.result.TenantResponseInfo.ExemptContestedInfo;
                    tenantResponseResult.status = ExemptContestedInfoResult.status;
                    if (ExemptContestedInfoResult.status.Status != StatusEnum.Success)
                        return tenantResponseResult;
                }
                RentalHistoryResult = GetTenantResponseRentalHistoryInfo((int)tenantResponseResult.result.TenantResponseID);
                if (RentalHistoryResult != null)
                {
                    tenantResponseResult.result.TenantRentalHistory = RentalHistoryResult.result;
                    tenantResponseResult.status = RentalHistoryResult.status;
                    if (RentalHistoryResult.status.Status != StatusEnum.Success)
                        return tenantResponseResult;
                }

                return tenantResponseResult;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                tenantResponseResult.status = eHandler.HandleException(ex);
                return tenantResponseResult;
            }
        }
        
        #endregion "TenantResponseGet"

        #region "TenantResponseSave"
        public ReturnResult<CaseInfoM> SaveTenantResponseApplicationInfo(CaseInfoM caseInfo, int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();

            int ownerUserID = 0;
            int thirdPartyUserID = 0;
            int PropertyManagerUserID = 0;
            int applicantUserID = 0;
            try
            {
                CommonDBHandler _dbCommon = new CommonDBHandler();
                if (caseInfo.TenantResponseInfo.TenantResponseID > 0)
                {

                    if (caseInfo.TenantResponseInfo.bThirdPartyRepresentation)
                    {
                        thirdPartyUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.ThirdPartyInfo).result.UserID;
                        if (thirdPartyUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }

                    applicantUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.ApplicantUserInfo).result.UserID;
                    if (applicantUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    ownerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.OwnerInfo).result.UserID;
                    if (ownerUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    if (caseInfo.TenantResponseInfo.bSameAsOwnerInfo)
                    {
                        PropertyManagerUserID = ownerUserID;
                    }
                    else
                    {
                        PropertyManagerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.PropertyManager).result.UserID;
                        if (PropertyManagerUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }

                    TenantResponseApplicationInfo petitionDB = _dbContext.TenantResponseApplicationInfos
                                                        .Where(x => x.IsSubmitted == false
                                                            && x.TenantResponseID == caseInfo.TenantResponseInfo.TenantResponseID).FirstOrDefault();
                    petitionDB.bThirdPartyRepresentation = caseInfo.TenantResponseInfo.bThirdPartyRepresentation;

                    petitionDB.ThirdPartyUserID = thirdPartyUserID;
                    petitionDB.ApplicantUserID = applicantUserID;
                    petitionDB.OwnerUserID = ownerUserID;
                    petitionDB.PropertyManagerUserID = PropertyManagerUserID;
                    petitionDB.NumberOfUnits = caseInfo.TenantResponseInfo.NumberOfUnits;
                    petitionDB.UnitTypeID = caseInfo.TenantResponseInfo.UnitTypeId;
                    petitionDB.bRentStatus = caseInfo.TenantResponseInfo.bCurrentRentStatus;
                    if (caseInfo.TenantResponseInfo.bCurrentRentStatus == false)
                    {
                        petitionDB.ProvideExplanation = caseInfo.TenantResponseInfo.ProvideExplanation;
                    }
                    petitionDB.CreatedDate = DateTime.Now;
                    petitionDB.ResponseFiledBy = caseInfo.TenantResponseInfo.CustomerID;
                    petitionDB.RangeID = caseInfo.TenantResponseInfo.SelectedRangeOfUnits.RangeID;
                    petitionDB.IsSubmitted = false;
                    _dbContext.SubmitChanges();
                    caseInfo.TenantResponseInfo.TenantResponseID = petitionDB.TenantResponseID;

                    var PageStatus = _dbContext.TenantResponsePageSubmissionStatus
                                            .Where(x => x.CustomerID == caseInfo.TenantResponseInfo.CustomerID
                                            && x.TenantResponseID == caseInfo.TenantResponseInfo.TenantResponseID).FirstOrDefault();
                    if (PageStatus != null)
                    {
                        PageStatus.ApplicantInformation = true;
                        _dbContext.SubmitChanges();
                    }
                    else
                    {
                        var PageStatusNew = new TenantResponsePageSubmissionStatus();
                        PageStatusNew.CustomerID = caseInfo.TenantResponseInfo.CustomerID;
                        PageStatusNew.ApplicantInformation = true;
                        PageStatusNew.TenantResponseID = caseInfo.TenantResponseInfo.TenantResponseID;

                        _dbContext.TenantResponsePageSubmissionStatus.InsertOnSubmit(PageStatusNew);
                        _dbContext.SubmitChanges();
                    }
                }
                else
                {
                    if (caseInfo.TenantResponseInfo.bThirdPartyRepresentation)
                    {
                        thirdPartyUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.ThirdPartyInfo).result.UserID;
                        if (thirdPartyUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }

                    applicantUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.ApplicantUserInfo).result.UserID;
                    if (applicantUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    ownerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.OwnerInfo).result.UserID;
                    if (ownerUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    if (caseInfo.TenantResponseInfo.bSameAsOwnerInfo)
                    {
                        PropertyManagerUserID = ownerUserID;
                    }
                    else
                    {
                        PropertyManagerUserID = _dbCommon.SaveUserInfo(caseInfo.TenantResponseInfo.PropertyManager).result.UserID;
                        if (PropertyManagerUserID == 0)
                        {
                            result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            return result;
                        }
                    }

                    TenantResponseApplicationInfo responseDB = new TenantResponseApplicationInfo();
                    responseDB.bThirdPartyRepresentation = caseInfo.TenantResponseInfo.bThirdPartyRepresentation;

                    responseDB.ThirdPartyUserID = thirdPartyUserID;
                    responseDB.ApplicantUserID = applicantUserID;
                    responseDB.OwnerUserID = ownerUserID;
                    responseDB.PropertyManagerUserID = PropertyManagerUserID;
                    responseDB.NumberOfUnits = caseInfo.TenantResponseInfo.NumberOfUnits;
                    responseDB.UnitTypeID = caseInfo.TenantResponseInfo.UnitTypeId;
                    responseDB.bRentStatus = caseInfo.TenantResponseInfo.bCurrentRentStatus;
                    responseDB.C_ID = caseInfo.C_ID;
                    if (caseInfo.TenantResponseInfo.bCurrentRentStatus == false)
                    {
                        responseDB.ProvideExplanation = caseInfo.TenantResponseInfo.ProvideExplanation;
                    }
                    responseDB.CreatedDate = DateTime.Now;
                    responseDB.ResponseFiledBy = caseInfo.TenantResponseInfo.CustomerID;
                    responseDB.IsSubmitted = false;
                    _dbContext.TenantResponseApplicationInfos.InsertOnSubmit(responseDB);
                    _dbContext.SubmitChanges();
                    caseInfo.TenantResponseInfo.TenantResponseID = responseDB.TenantResponseID;

                    var PageStatus = _dbContext.TenantResponsePageSubmissionStatus
                                             .Where(x => x.CustomerID == caseInfo.TenantResponseInfo.CustomerID).FirstOrDefault();
                    if (PageStatus != null)
                    {
                        PageStatus.ApplicantInformation = true;
                        _dbContext.SubmitChanges();
                    }
                    else
                    {
                        var PageStatusNew = new TenantResponsePageSubmissionStatus();
                        PageStatusNew.CustomerID = caseInfo.TenantResponseInfo.CustomerID;
                        PageStatusNew.ApplicantInformation = true;
                        _dbContext.TenantResponsePageSubmissionStatus.InsertOnSubmit(PageStatusNew);
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

        public ReturnResult<bool> SaveTenantResponseExemptContestedInfo(TenantResponseExemptContestedInfoM message, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var exemptContestedDB = _dbContext.TenantResponseExemptContestedInfos.Where(x => x.TenantResponseID == message.TenantResponseID).FirstOrDefault();
                if (exemptContestedDB != null)
                {
                    exemptContestedDB.Explaination = message.Explaination;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var exemptContestedNewDB = new TenantResponseExemptContestedInfo();
                    exemptContestedNewDB.TenantResponseID = message.TenantResponseID;
                    exemptContestedNewDB.Explaination = message.Explaination;
                    _dbContext.TenantResponseExemptContestedInfos.InsertOnSubmit(exemptContestedNewDB);
                    _dbContext.SubmitChanges();
                }

                var PageStatus = _dbContext.TenantResponsePageSubmissionStatus.Where(x => x.CustomerID == CustomerID
                    && x.TenantResponseID == message.TenantResponseID).FirstOrDefault();
                if (PageStatus != null)
                {
                    PageStatus.ExemptionContested = true;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var PageStatusNew = new TenantResponsePageSubmissionStatus();
                    PageStatusNew.CustomerID = CustomerID;
                    PageStatusNew.ExemptionContested = true;
                    PageStatusNew.TenantResponseID = message.TenantResponseID;
                    _dbContext.TenantResponsePageSubmissionStatus.InsertOnSubmit(PageStatusNew);
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
        public ReturnResult<bool> SaveTenantResponseRentalHistoryInfo(TenantResponseRentalHistoryM rentalHistory, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var rentalHistoryRecord = _dbContext.TenantResponseRentalHistories.Where(x => x.TenantResponseID == rentalHistory.TenantResponseID).FirstOrDefault();
                if (rentalHistoryRecord != null)
                {
                    rentalHistoryRecord.TenantResponseID = rentalHistory.TenantResponseID;
                    rentalHistoryRecord.RentalAgreementDate = new DateTime(rentalHistory.RentalAgreementDate.Year, rentalHistory.RentalAgreementDate.Month, rentalHistory.RentalAgreementDate.Day);
                    rentalHistoryRecord.MoveInDate = new DateTime(rentalHistory.MoveInDate.Year, rentalHistory.MoveInDate.Month, rentalHistory.MoveInDate.Day);
                    rentalHistoryRecord.InitialRent = rentalHistory.InitialRent;
                    rentalHistoryRecord.RAPNoticeGivenDate = new DateTime(rentalHistory.RAPNoticeGivenDate.Year, rentalHistory.RAPNoticeGivenDate.Month, rentalHistory.RAPNoticeGivenDate.Day);
                    rentalHistoryRecord.bRAPNoticeGiven = rentalHistory.bRAPNoticeGiven;
                    rentalHistoryRecord.CreatedDate = DateTime.Now;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    TenantResponseRentalHistory rentalHistoryDB = new TenantResponseRentalHistory();
                    rentalHistoryDB.TenantResponseID = rentalHistory.TenantResponseID;
                    rentalHistoryDB.RentalAgreementDate = new DateTime(rentalHistory.RentalAgreementDate.Year, rentalHistory.RentalAgreementDate.Month, rentalHistory.RentalAgreementDate.Day);
                    rentalHistoryDB.MoveInDate = new DateTime(rentalHistory.MoveInDate.Year, rentalHistory.MoveInDate.Month, rentalHistory.MoveInDate.Day);
                    rentalHistoryDB.InitialRent = rentalHistory.InitialRent;
                    rentalHistoryDB.RAPNoticeGivenDate = new DateTime(rentalHistory.RAPNoticeGivenDate.Year, rentalHistory.RAPNoticeGivenDate.Month, rentalHistory.RAPNoticeGivenDate.Day);
                    rentalHistoryDB.bRAPNoticeGiven = rentalHistory.bRAPNoticeGiven;
                    rentalHistoryDB.CreatedDate = DateTime.Now;
                    _dbContext.TenantResponseRentalHistories.InsertOnSubmit(rentalHistoryDB);
                    _dbContext.SubmitChanges();
                }
                var rentIncrementRecord = _dbContext.TenantResponseRentalIncrementInfos.Where(x => x.TenantResponseID == rentalHistory.TenantResponseID).ToList();
                if (rentIncrementRecord != null)
                {
                    foreach (var item in rentIncrementRecord)
                    {
                        _dbContext.TenantResponseRentalIncrementInfos.DeleteOnSubmit(item);
                        _dbContext.SubmitChanges();
                    }
                }

                foreach (var item in rentalHistory.RentIncreases)
                {
                    if (item.IsDeleted == false)
                    {
                        TenantResponseRentalIncrementInfo rentIncrementDB = new TenantResponseRentalIncrementInfo();
                        rentIncrementDB.TenantResponseID = rentalHistory.TenantResponseID;
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

                        _dbContext.TenantResponseRentalIncrementInfos.InsertOnSubmit(rentIncrementDB);
                        _dbContext.SubmitChanges();
                    }
                }

                var PageStatus = _dbContext.TenantResponsePageSubmissionStatus.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                if (PageStatus != null)
                {
                    PageStatus.RentHistory = true;
                    _dbContext.SubmitChanges();
                }
                else
                {
                    var PageStatusNew = new TenantResponsePageSubmissionStatus();
                    PageStatusNew.CustomerID = CustomerID;
                    PageStatusNew.RentHistory = true;
                    _dbContext.TenantResponsePageSubmissionStatus.InsertOnSubmit(PageStatusNew);
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

        /// <summary>
        /// Submit tenant petition
        /// </summary>
        /// <param name="caseInfo"></param>
        /// <returns></returns>
        public ReturnResult<CaseInfoM> SubmitTenantResponse(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();

            try
            {
                var CustDetails = _dbAccount.CustomerDetails.Where(x => x.CustomerID == caseInfo.CaseFileBy).FirstOrDefault();
                if (CustDetails != null)
                {
                    if (CustDetails.CustomerIdentityKey != caseInfo.TenantResponseInfo.Verification.pinVerify)
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.PinError };
                        return result;
                    }
                    if (CustDetails.CustomerIdentityKey != caseInfo.TenantResponseInfo.Verification.pinMediation)
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.PinError };
                        return result;
                    }
                }
                
                TenantResponseVerification verificationDB = new TenantResponseVerification();
                verificationDB.bCaseMediation = caseInfo.TenantResponseInfo.Verification.bCaseMediation;
                verificationDB.bDeclarePenalty = caseInfo.TenantResponseInfo.Verification.bDeclarePenalty;
                verificationDB.bThirdParty = caseInfo.TenantResponseInfo.Verification.bThirdParty;
                verificationDB.bThirdPartyMediation = caseInfo.TenantResponseInfo.Verification.bThirdPartyMediation;
                verificationDB.CreatedDate = DateTime.Now;
                verificationDB.TenantResponseID = caseInfo.TenantResponseInfo.TenantResponseID;
                _dbContext.TenantResponseVerifications.InsertOnSubmit(verificationDB);
                _dbContext.SubmitChanges();

                var CaseInfoDB = _dbContext.CaseDetails.Where(x => x.CaseID == caseInfo.CaseID).FirstOrDefault();
                if (CaseInfoDB != null)
                {
                    CaseInfoDB.TenantResponseID = caseInfo.TenantResponseInfo.TenantResponseID;
                    _dbContext.SubmitChanges();
                }



                var ResponseDB = _dbContext.TenantResponseApplicationInfos.Where(x => x.TenantResponseID == caseInfo.TenantResponseInfo.TenantResponseID).FirstOrDefault();
                if (ResponseDB != null)
                {
                    ResponseDB.IsSubmitted = true;
                    _dbContext.SubmitChanges();
                }

                var PageStatus = _dbContext.TenantResponsePageSubmissionStatus
                                            .Where(x => x.CustomerID == caseInfo.CaseFileBy).FirstOrDefault();
                if (PageStatus != null)
                {
                    _dbContext.TenantResponsePageSubmissionStatus.DeleteOnSubmit(PageStatus);
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
        #endregion "TenantResponseSave"

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
                if (model.NumberOfUnitsRange == null || model.NumberOfUnitsRange.Count == 0)
                {
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
                            model.NumberOfUnitsRange.Add(obj);
                        }
                    }
                }
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
                    var accdbResult = _accountdbHandler.GetThirdPartyInfo(model.OwnerPetitionInfo.ApplicantInfo.CustomerID);
                    if (accdbResult.status.Status == StatusEnum.Success)
                    {
                        _applicantInfo.ThirdPartyUser = accdbResult.result.ThirdPartyUser;
                    }
                    //if (_applicantInfo.bThirdPartyRepresentation)
                    //{
                    //    var thirdPartyUserInforesult = _commondbHandler.GetUserInfo(applicantInfo.ThirdPartyUserID);
                    //    if (thirdPartyUserInforesult.status.Status != StatusEnum.Success)
                    //    {
                    //        result.status = thirdPartyUserInforesult.status;
                    //        return result;
                    //    }
                    //_applicantInfo.ThirdPartyUser = thirdPartyUserInforesult.result; 
                    //}
                    _applicantInfo.bBusinessLicensePaid = (applicantInfo.bBusinessLicensePaid != null) ? Convert.ToBoolean(applicantInfo.bBusinessLicensePaid) : false; 
                    _applicantInfo.BusinessLicenseNumber = applicantInfo.BusinessLicenseNumber;
                    _applicantInfo.bRentAdjustmentProgramFeePaid = (applicantInfo.bRentAdjustmentProgramFeePaid != null) ? Convert.ToBoolean(applicantInfo.bRentAdjustmentProgramFeePaid) : false; 
                    _applicantInfo.BuildingAcquiredDate =_commondbHandler.GetDateFromDatabase(Convert.ToDateTime(applicantInfo.BuildingAcquiredDate));
                    _applicantInfo.NumberOfUnits = (applicantInfo.NumberOfUnits != null) ? Convert.ToInt32(applicantInfo.NumberOfUnits) : 0;
                    _applicantInfo.bMoreThanOneStreetOnParcel = (applicantInfo.bMoreThanOneStreetOnParcel != null) ? Convert.ToBoolean(applicantInfo.bMoreThanOneStreetOnParcel) : false;
                    _applicantInfo.CustomerID = (applicantInfo.CustomerID != null) ? Convert.ToInt32(applicantInfo.CustomerID) : 0;
                    _applicantInfo.bPetitionFiled = applicantInfo.bPetitionFiled;
                    _applicantInfo.NumberOfUnitsRangeID = (applicantInfo.RangeID != null) ? Convert.ToInt32(applicantInfo.RangeID) : 0; 
                    model.OwnerPetitionInfo.ApplicantInfo = _applicantInfo;
                    result.result = model;
                }
                else
                {
                    var accdbResult = _accountdbHandler.GetThirdPartyInfo(model.OwnerResponseInfo.ApplicantInfo.CustomerID);
                    if (accdbResult.status.Status == StatusEnum.Success)
                    {
                        model.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser = accdbResult.result.ThirdPartyUser;
                    }
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
                int applicantInfoID = petition.ApplicantInfo.OwnerPetitionApplicantInfoID;
                if (applicantInfoID == 0)
                {
                    applicantInfoID = _dbContext.OwnerPetitionApplicantInfos.Where(r => r.CustomerID == petition.CustomerID && r.bPetitionFiled == false).Select(x => x.OwnerPetitionApplicantInfoID).FirstOrDefault();
                }
                var selectedReasons = _dbContext.OwnerRentIncreaseReasonInfos.Where(x => x.OwnerPetitionApplicantInfoID == applicantInfoID);

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
                int propertyID = model.OwnerPetitionInfo.PropertyInfo.OwnerPropertyID;
                if (propertyID == 0)
                {
                    propertyID = _dbContext.OwnerPetitionPropertyInfos.Where(r => r.CustomerID == model.CustomerID && r.bPetitionFiled == false).Select(x => x.OwnerPropertyID).First();
                }
                if (propertyID > 0)
                {
                    var propertyInfo = (from r in _dbContext.OwnerPetitionPropertyInfos
                                        where r.OwnerPropertyID == propertyID
                                        select r).First();
                    if (propertyInfo !=null)
                    {
                        model.OwnerPetitionInfo.PropertyInfo.MovedInDate = (propertyInfo.MovedInDate == null) ? null :  _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.MovedInDate));
                        model.OwnerPetitionInfo.PropertyInfo.InitialRent = propertyInfo.InitialRent;
                        model.OwnerPetitionInfo.PropertyInfo.RAPNoticeStatusID = propertyInfo.RAPNoticeStatusID;
                        model.OwnerPetitionInfo.PropertyInfo.RAPNoticeGivenDate = (propertyInfo.RAPNoticeGivenDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.RAPNoticeGivenDate));
                        model.OwnerPetitionInfo.PropertyInfo.CurrentOnRent = Convert.ToBoolean(propertyInfo.CurrentOnRent);
                    }

                    var rentIncreaseInfo = _dbContext.OwnerPetitionRentalIncrementInfos.Where(r => r.OwnerPropertyID == propertyID);
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

        public ReturnResult<CaseInfoM> GetOwnerReview(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var applicantInfoResult = GetOwnerApplicantInfo(model);
                if (applicantInfoResult.status.Status != StatusEnum.Success)
                {
                    result.status = applicantInfoResult.status;
                    return result;
                }
                model = applicantInfoResult.result;
                var justificationResult = GetRentIncreaseReasonInfo(model.OwnerPetitionInfo);
                if (justificationResult.status.Status != StatusEnum.Success)
                {
                    result.status = justificationResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.RentIncreaseReasons = justificationResult.result;
                var PropertyAndTenantInfoResult = GetOwnerPropertyAndTenantInfo(model.OwnerPetitionInfo.PropertyInfo);
                if (PropertyAndTenantInfoResult.status.Status != StatusEnum.Success)
                {
                    result.status = PropertyAndTenantInfoResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.PropertyInfo = PropertyAndTenantInfoResult.result;

                var RentIncreaseAndPropertyInfoResult = GetOwnerRentIncreaseAndPropertyInfo(model);
                if (RentIncreaseAndPropertyInfoResult.status.Status != StatusEnum.Success)
                {
                    result.status = RentIncreaseAndPropertyInfoResult.status;
                    return result;
                }
                model = RentIncreaseAndPropertyInfoResult.result;

               
                var documentResult = _commondbHandler.GetDocumentsByCategory(model.CustomerID, false, DocCategory.OwnerPetition.ToString());
                if (documentResult.status.Status == StatusEnum.Success)
                {
                    model.Documents = documentResult.result;
                }

                model.OwnerPetitionInfo.CustomerIdentityKey = Convert.ToInt32(_dbAccount.CustomerDetails.Where(x => x.CustomerID == model.CustomerID).Select(x => x.CustomerIdentityKey).FirstOrDefault());
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
                        if (model.OwnerPetitionInfo.ApplicantInfo.bThirdPartyRepresentation )
                        {
                            var thirdpartyUserResult = _commondbHandler.SaveUserInfo(model.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser);
                            if (thirdpartyUserResult.status.Status != StatusEnum.Success)
                            {
                                result.status = thirdpartyUserResult.status;
                                return result;
                            }
                            var saveThirdPartyResult = _accountdbHandler.SaveOrUpdateThirdPartyInfo(new ThirdPartyInfoM() { CustomerID = model.OwnerPetitionInfo.ApplicantInfo.CustomerID, ThirdPartyUser = thirdpartyUserResult.result });
                            if (saveThirdPartyResult.status.Status != StatusEnum.Success)
                            {
                                result.status = saveThirdPartyResult.status;
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
                            if (model.OwnerPetitionInfo.ApplicantInfo.NumberOfUnitsRangeID != 0)
                            {
                                applicantInfo.First().RangeID = model.OwnerPetitionInfo.ApplicantInfo.NumberOfUnitsRangeID;
                            }
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
                        if (model.OwnerPetitionInfo.ApplicantInfo.NumberOfUnitsRangeID != 0)
                        {
                            applicantInfo.RangeID = model.OwnerPetitionInfo.ApplicantInfo.NumberOfUnitsRangeID;
                        }
                        applicantInfo.bMoreThanOneStreetOnParcel = model.OwnerPetitionInfo.ApplicantInfo.bMoreThanOneStreetOnParcel;
                        applicantInfo.CustomerID = model.OwnerPetitionInfo.ApplicantInfo.CustomerID;
                        applicantInfo.bPetitionFiled = false;
                        _dbContext.OwnerPetitionApplicantInfos.InsertOnSubmit(applicantInfo);
                        _dbContext.SubmitChanges();
                        model.OwnerPetitionInfo.ApplicantInfo.OwnerPetitionApplicantInfoID = applicantInfo.OwnerPetitionApplicantInfoID;

                        OwnerPetitionPageSubmissionStatus oPetitionSubmission = new OwnerPetitionPageSubmissionStatus();
                        oPetitionSubmission.ImportantInformation = true;
                        oPetitionSubmission.ApplicantInformation = true;
                        oPetitionSubmission.CustomerID = model.CustomerID;
                        _dbContext.OwnerPetitionPageSubmissionStatus.InsertOnSubmit(oPetitionSubmission);
                        _dbContext.SubmitChanges();
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

                    var propertySubmission = _dbContext.OwnerPetitionPageSubmissionStatus.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();
                    if (propertySubmission !=null)
                    {
                        propertySubmission.RentalProperty = true;
                        _dbContext.SubmitChanges();
                    }

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
                    model.TenantInfo = tenantsInfoM;
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
                int applicantInfoID = petition.ApplicantInfo.OwnerPetitionApplicantInfoID;
                if (applicantInfoID == 0)
                {
                    applicantInfoID = _dbContext.OwnerPetitionApplicantInfos.Where(r => r.CustomerID == petition.CustomerID && r.bPetitionFiled == false).Select(x => x.OwnerPetitionApplicantInfoID).FirstOrDefault();
                }

                var rentIncreaseReasonDB = from r in _dbContext.OwnerRentIncreaseReasonInfos
                                           where r.OwnerPetitionApplicantInfoID == applicantInfoID
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
                                rentIncreaseReason.OwnerPetitionApplicantInfoID = applicantInfoID;
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
                            rentIncreaseReason.OwnerPetitionApplicantInfoID = applicantInfoID;
                            rentIncreaseReason.ReasonID = item.ReasonID;
                            _dbContext.OwnerRentIncreaseReasonInfos.InsertOnSubmit(rentIncreaseReason);
                            _dbContext.SubmitChanges();
                        }
                    }
                }

                var justtificationStatus = _dbContext.OwnerPetitionPageSubmissionStatus.Where(r => r.CustomerID == petition.CustomerID).Select(x => x.JustificationForRentIncrease).FirstOrDefault();
                if (!Convert.ToBoolean(justtificationStatus))
                {
                    justtificationStatus = true;
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
                int propertyID = model.OwnerPropertyID;
                if(propertyID == 0)
                {
                    propertyID = _dbContext.OwnerPetitionPropertyInfos.Where(r => r.CustomerID == model.CustomerID && r.bPetitionFiled == false).Select(x => x.OwnerPropertyID).First();                    
                }
                if (propertyID > 0)
                {
                    var propertyInfo = from r in _dbContext.OwnerPetitionPropertyInfos
                                       where r.OwnerPropertyID == propertyID
                                       select r;
                    if (propertyInfo.Any())
                    {
                        propertyInfo.First().MovedInDate = new DateTime(model.MovedInDate.Year, model.MovedInDate.Month, model.MovedInDate.Day);
                        propertyInfo.First().InitialRent = model.InitialRent;
                        propertyInfo.First().RAPNoticeStatusID = model.RAPNoticeStatusID;
                        propertyInfo.First().RAPNoticeGivenDate = new DateTime(model.RAPNoticeGivenDate.Year, model.RAPNoticeGivenDate.Month, model.RAPNoticeGivenDate.Day);
                        propertyInfo.First().CurrentOnRent = model.CurrentOnRent;
                        _dbContext.SubmitChanges();

                        var historySubmission = _dbContext.OwnerPetitionPageSubmissionStatus.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();
                        if (historySubmission != null)
                        {
                            historySubmission.RentHistory = true;
                            _dbContext.SubmitChanges();
                        }
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
                                rentIncreaseInfo.OwnerPropertyID = propertyID;
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

                var updateDocumentResult = _commondbHandler.UpdateDocumentCaseInfo(model.CustomerID, model.C_ID, DocCategory.OwnerPetition.ToString());
                if (updateDocumentResult.status.Status != StatusEnum.Success)
                {
                    result.status = updateDocumentResult.status;
                    return result;
                }

                var oPetitionSubmission = _dbContext.OwnerPetitionPageSubmissionStatus.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();
                if (oPetitionSubmission != null)
                {
                    _dbContext.OwnerPetitionPageSubmissionStatus.DeleteOnSubmit(oPetitionSubmission);
                    _dbContext.SubmitChanges();
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

        public ReturnResult<bool> OwnerUpdateAdditionalDocumentsPageSubmission(int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var oPetitionSubmission = _dbContext.OwnerPetitionPageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
                if (oPetitionSubmission != null)
                {
                    oPetitionSubmission.AdditionalDocumentation = true;
                    _dbContext.SubmitChanges();
                }

                result.result = true;
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

        public ReturnResult<bool> OwnerUpdateReviewPageSubmission(int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var ownerubmission = _dbContext.OwnerPetitionPageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
                if (ownerubmission != null)
                {
                    ownerubmission.Review = true;
                    _dbContext.SubmitChanges();
                }

                result.result = true;
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

       #region Owner Response Get Functions
       public ReturnResult<CaseInfoM> GetOResponseApplicantInfo(CaseInfoM model)
       {
           ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
           try
           {

               if(model.NumberOfUnitsRange == null ||model.NumberOfUnitsRange.Count == 0)               
               {
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
                           model.NumberOfUnitsRange.Add(obj);
                       }
                   }
               }

               var applicantInfo = _dbContext.OwnerResponseApplicantInfos.Where(r => r.CustomerID == model.OwnerResponseInfo.ApplicantInfo.CustomerID && r.bPetitionFiled == false).FirstOrDefault();
               if (applicantInfo == null)
               {
                   applicantInfo = _dbContext.OwnerResponseApplicantInfos.Where(r => r.CustomerID == model.OwnerResponseInfo.ApplicantInfo.CustomerID && r.bPetitionFiled == true).OrderByDescending(c => c.CreatedDate).FirstOrDefault();
               }

               if (applicantInfo != null)
               {
                   OwnerResponseApplicantInfoM _applicantInfo = new OwnerResponseApplicantInfoM();
                   _applicantInfo.OwnerResponseApplicantInfoID = applicantInfo.OwnerResponseApplicantInfoID;
                   var applicantUserInforesult = _commondbHandler.GetUserInfo(applicantInfo.ApplicantUserID);
                   if (applicantUserInforesult.status.Status != StatusEnum.Success)
                   {
                       result.status = applicantUserInforesult.status;
                       return result;
                   }
                   _applicantInfo.ApplicantUserInfo = applicantUserInforesult.result;
                   _applicantInfo.bThirdPartyRepresentation = (applicantInfo.bThirdPartyRepresentation != null) ? Convert.ToBoolean(applicantInfo.bThirdPartyRepresentation) : false;
                   var accdbResult = _accountdbHandler.GetThirdPartyInfo(model.OwnerResponseInfo.ApplicantInfo.CustomerID);
                   if (accdbResult.status.Status == StatusEnum.Success)
                   {
                       _applicantInfo.ThirdPartyUser = accdbResult.result.ThirdPartyUser;
                   }
                   
                   _applicantInfo.bBusinessLicensePaid = (applicantInfo.bBusinessLicensePaid != null) ? Convert.ToBoolean(applicantInfo.bBusinessLicensePaid) : false;
                   _applicantInfo.BusinessLicenseNumber = applicantInfo.BusinessLicenseNumber;
                   _applicantInfo.bRentAdjustmentProgramFeePaid = (applicantInfo.bRentAdjustmentProgramFeePaid != null) ? Convert.ToBoolean(applicantInfo.bRentAdjustmentProgramFeePaid) : false;
                   _applicantInfo.BuildingAcquiredDate = _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(applicantInfo.BuildingAcquiredDate));
                   _applicantInfo.NumberOfUnits = (applicantInfo.NumberOfUnits != null) ? Convert.ToInt32(applicantInfo.NumberOfUnits) : 0;
                   _applicantInfo.bMoreThanOneStreetOnParcel = (applicantInfo.bMoreThanOneStreetOnParcel != null) ? Convert.ToBoolean(applicantInfo.bMoreThanOneStreetOnParcel) : false;
                   _applicantInfo.CustomerID = (applicantInfo.CustomerID != null) ? Convert.ToInt32(applicantInfo.CustomerID) : 0; ;
                   _applicantInfo.bPetitionFiled =Convert.ToBoolean(applicantInfo.bPetitionFiled);
                   _applicantInfo.CaseRespondingTo = applicantInfo.CaseRespondingTo;
                   model.OwnerResponseInfo.ApplicantInfo.NumberOfUnitsRangeID = (applicantInfo.RangeID != null) ? Convert.ToInt32(applicantInfo.RangeID) : 0; 
                   model.OwnerResponseInfo.ApplicantInfo = _applicantInfo;
                   result.result = model;
               }
               else
               {
                   var accdbResult = _accountdbHandler.GetThirdPartyInfo(model.OwnerResponseInfo.ApplicantInfo.CustomerID);
                   if (accdbResult.status.Status == StatusEnum.Success)
                   {
                     model.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser = accdbResult.result.ThirdPartyUser;
                   }
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

       public ReturnResult<OwnerResponsePropertyInfoM> GetOResponsePropertyAndTenantInfo(OwnerResponsePropertyInfoM model)
       {
           ReturnResult<OwnerResponsePropertyInfoM> result = new ReturnResult<OwnerResponsePropertyInfoM>();
           try
           {
               if (!model.UnitTypes.Any())
               {
                   model.UnitTypes = getUnitTypes();
               }

                var propertyInfo = from r in _dbContext.OwnerResponsePropertyInfos
                                  where r.CustomerID == model.CustomerID && r.bPetitionFiled == false
                                  select r;
               if (propertyInfo.Any())
               {
                   model.OwnerPropertyID = propertyInfo.First().PropertyID;
                   model.UnitTypeID = propertyInfo.First().UnitTypeID;

                   var tentantInfo = from r in _dbContext.OwnerResponseTenantInfos                                     
                                     where r.PropertyID == model.OwnerPropertyID
                                     select r;
                   if (tentantInfo.Any())
                   {
                       List<OwnerPetitionTenantInfoM> tenants = new List<OwnerPetitionTenantInfoM>();
                       foreach (var item in tentantInfo)
                       {
                           OwnerPetitionTenantInfoM _tenant = new OwnerPetitionTenantInfoM();
                           var userResult = _commondbHandler.GetUserInfo(item.TenantUserID);
                           if (userResult.status.Status == StatusEnum.Success)
                           {
                               _tenant.TenantUserInfo = userResult.result;
                               _tenant.TenantInfoID = item.TenantInfoID;
                           }
                           tenants.Add(_tenant);                          
                       }
                       model.TenantInfo = tenants;
                   }
               }
               else
               {
                   
                   var applicantInfo = _dbContext.OwnerResponseApplicantInfos.Where(r => r.CustomerID == model.CustomerID && r.bPetitionFiled == false).FirstOrDefault();
                   if (applicantInfo != null)
                   {
                       string  caseid = applicantInfo.CaseRespondingTo;
                       var caseinfo = _dbContext.CaseDetails.Where(r => r.CaseID == caseid).FirstOrDefault();
                       if (caseinfo != null && caseinfo.PetitionCategoryID == 1)
                       {
                           var tenantPetitionID = _dbContext.PetitionDetails.Where(r => r.PetitionID == caseinfo.PetitionID).Select(x => x.TenantPetitionID).FirstOrDefault();
                           var userid = Convert.ToInt32(_dbContext.TenantPetitionInfos.Where(r => r.TenantPetitionID == tenantPetitionID).Select(x => x.ApplicantUserID).FirstOrDefault());
                           var tenantInfo = _commondbHandler.GetUserInfo(userid);
                           if (tenantInfo.status.Status == StatusEnum.Success)
                           {
                               OwnerPetitionTenantInfoM _tenant = new OwnerPetitionTenantInfoM();
                               _tenant.TenantUserInfo = tenantInfo.result;
                               model.TenantInfo.Add(_tenant);
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

       public ReturnResult<CaseInfoM> GetOResponseRentIncreaseAndPropertyInfo(CaseInfoM model)
       {
           ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
           try
           {
               model.RAPNoticeStatus = getRAPNoticeStatus();
               var resaons = _dbContext.OwnerRentIncreaseReasons;
               if (model.OwnerResponseInfo.PropertyInfo.Rent.RentIncreaseReasons == null || model.OwnerResponseInfo.PropertyInfo.Rent.RentIncreaseReasons.Count == 0)
               {
                   if (resaons.Any())
                   {

                       foreach (var reason in resaons)
                       {
                           OwnerRentIncreaseReasonsM _reason = new OwnerRentIncreaseReasonsM();
                           _reason.ReasonID = reason.ReasonID;
                           _reason.ReasonDescription = reason.Reason;
                           _reason.IsSelected = false;
                           model.OwnerResponseInfo.PropertyInfo.Rent.RentIncreaseReasons.Add(_reason);
                       }
                   }
               }
               if (model.OwnerResponseInfo.PropertyInfo.OwnerPropertyID > 0)               
               {
                   var propertyInfo = (from r in _dbContext.OwnerResponsePropertyInfos
                                       where r.PropertyID == model.OwnerResponseInfo.PropertyInfo.OwnerPropertyID
                                       select r).First();
                   if (propertyInfo != null)
                   {
                       model.OwnerResponseInfo.PropertyInfo.MovedInDate = (propertyInfo.MovedInDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.MovedInDate));
                       model.OwnerResponseInfo.PropertyInfo.InitialRent = propertyInfo.InitialRent;
                       model.OwnerResponseInfo.PropertyInfo.RAPNoticeStatusID = propertyInfo.RAPNoticeStatusID;
                       model.OwnerResponseInfo.PropertyInfo.RAPNoticeGivenDate = (propertyInfo.RAPNoticeGivenDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.RAPNoticeGivenDate));
                       model.OwnerResponseInfo.PropertyInfo.CurrentOnRent =Convert.ToBoolean(propertyInfo.CurrentOnRent);
                       model.OwnerResponseInfo.PropertyInfo.bCapitalImprovementIncrease = Convert.ToBoolean(propertyInfo.bCapitalImprovementIncrease);
                       model.OwnerResponseInfo.PropertyInfo.bCaptialImprovementContested = Convert.ToBoolean(propertyInfo.bCaptialImprovementContested);
                       model.OwnerResponseInfo.PropertyInfo.CaseNumbers = propertyInfo.CaseNumber;
                       model.OwnerResponseInfo.PropertyInfo.bRAPNoticeToRAPOffice = Convert.ToBoolean(propertyInfo.bRAPNoticeToRAPOffice);
                       model.OwnerResponseInfo.PropertyInfo.RAPNoticeToRAPOfficeDate = (propertyInfo.RAPNoticeToRAPOfficeDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.RAPNoticeToRAPOfficeDate));                   
                    
                   }

                   var rentIncreaseInfo = _dbContext.OwnerResponseRentalIncrementInfos.Where(r => r.PropertyID == model.OwnerResponseInfo.PropertyInfo.OwnerPropertyID);
                   
                   if (rentIncreaseInfo.Any())
                   {
                       List<OwnerResponseRentalIncrementInfoM> _rentalInfo = new List<OwnerResponseRentalIncrementInfoM>();
                       foreach (var item in rentIncreaseInfo)
                       {
                           OwnerResponseRentalIncrementInfoM _rentIncrease = new OwnerResponseRentalIncrementInfoM();
                           _rentIncrease.bRentIncreaseNoticeGiven = (bool)item.bRentIncreaseNoticeGiven;
                           _rentIncrease.RentIncreaseNoticeDate = (item.RentIncreaseNoticeDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseNoticeDate));
                           _rentIncrease.RentIncreaseEffectiveDate = (item.RentIncreaseEffectiveDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseEffectiveDate));
                           _rentIncrease.RentIncreasedFrom = item.RentIncreasedFrom;
                           _rentIncrease.RentIncreasedTo = item.RentIncreasedTo;
                           _rentIncrease.RentalIncreaseInfoID = item.RentalIncreaseInfoID;
                           if (resaons.Any())
                           {
                               foreach (var reason in resaons)
                               {
                                   OwnerRentIncreaseReasonsM _reason = new OwnerRentIncreaseReasonsM();
                                   _reason.ReasonID = reason.ReasonID;
                                   _reason.ReasonDescription = reason.Reason;
                                   _reason.IsSelected = false;
                                   _rentIncrease.RentIncreaseReasons.Add(_reason);
                               }
                           }
                           var selectedReasons = _dbContext.OwnerResponseRentIncreaseReasonInfos.Where(x => x.RentalIncreaseInfoID == _rentIncrease.RentalIncreaseInfoID);
                           if (selectedReasons.Any())
                           {
                               foreach (var reason in selectedReasons)
                               {
                                   _rentIncrease.RentIncreaseReasons.Where(r => r.ReasonID == reason.ReasonID).First().IsSelected = true;
                               }
                           }
                           _rentalInfo.Add(_rentIncrease);
                           //model.OwnerResponseInfo.PropertyInfo.RentalInfo.Add(_rentIncrease);
                       }
                       model.OwnerResponseInfo.PropertyInfo.RentalInfo = _rentalInfo;
                   }               
              }
               else
               {

                   var propertyInfo = (from r in _dbContext.OwnerResponsePropertyInfos
                                       where r.CustomerID == model.CustomerID && r.bPetitionFiled == false
                                       select r).First();
                 
                   if (propertyInfo != null)
                   {
                       model.OwnerResponseInfo.PropertyInfo.MovedInDate = (propertyInfo.MovedInDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.MovedInDate));
                       model.OwnerResponseInfo.PropertyInfo.InitialRent = propertyInfo.InitialRent;
                       model.OwnerResponseInfo.PropertyInfo.RAPNoticeStatusID = propertyInfo.RAPNoticeStatusID;
                       model.OwnerResponseInfo.PropertyInfo.RAPNoticeGivenDate = (propertyInfo.RAPNoticeGivenDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.RAPNoticeGivenDate));
                       model.OwnerResponseInfo.PropertyInfo.CurrentOnRent = Convert.ToBoolean(propertyInfo.CurrentOnRent);
                       model.OwnerResponseInfo.PropertyInfo.bCapitalImprovementIncrease = Convert.ToBoolean(propertyInfo.bCapitalImprovementIncrease);
                       model.OwnerResponseInfo.PropertyInfo.bCaptialImprovementContested = Convert.ToBoolean(propertyInfo.bCaptialImprovementContested);
                       model.OwnerResponseInfo.PropertyInfo.CaseNumbers = propertyInfo.CaseNumber;
                       model.OwnerResponseInfo.PropertyInfo.bRAPNoticeToRAPOffice = Convert.ToBoolean(propertyInfo.bRAPNoticeToRAPOffice);
                       model.OwnerResponseInfo.PropertyInfo.RAPNoticeToRAPOfficeDate = (propertyInfo.RAPNoticeToRAPOfficeDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(propertyInfo.RAPNoticeToRAPOfficeDate));

                   }
                   if (propertyInfo.PropertyID > 0)
                   {
                       var rentIncreaseInfo = _dbContext.OwnerResponseRentalIncrementInfos.Where(r => r.PropertyID == model.OwnerResponseInfo.PropertyInfo.OwnerPropertyID);
                      
                       if (rentIncreaseInfo.Any())
                       {
                           List<OwnerResponseRentalIncrementInfoM> _rentalInfo = new List<OwnerResponseRentalIncrementInfoM>();
                           foreach (var item in rentIncreaseInfo)
                           {
                               OwnerResponseRentalIncrementInfoM _rentIncrease = new OwnerResponseRentalIncrementInfoM();
                               _rentIncrease.bRentIncreaseNoticeGiven = (bool)item.bRentIncreaseNoticeGiven;
                               _rentIncrease.RentIncreaseNoticeDate = (item.RentIncreaseNoticeDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseNoticeDate));
                               _rentIncrease.RentIncreaseEffectiveDate = (item.RentIncreaseEffectiveDate == null) ? null : _commondbHandler.GetDateFromDatabase(Convert.ToDateTime(item.RentIncreaseEffectiveDate));
                               _rentIncrease.RentIncreasedFrom = item.RentIncreasedFrom;
                               _rentIncrease.RentIncreasedTo = item.RentIncreasedTo;
                               _rentIncrease.RentalIncreaseInfoID = item.RentalIncreaseInfoID;

                               if (resaons.Any())
                               {
                                   foreach (var reason in resaons)
                                   {
                                       OwnerRentIncreaseReasonsM _reason = new OwnerRentIncreaseReasonsM();
                                       _reason.ReasonID = reason.ReasonID;
                                       _reason.ReasonDescription = reason.Reason;
                                       _reason.IsSelected = false;
                                       _rentIncrease.RentIncreaseReasons.Add(_reason);
                                   }
                               }
                               var selectedReasons = _dbContext.OwnerResponseRentIncreaseReasonInfos.Where(x => x.RentalIncreaseInfoID == _rentIncrease.RentalIncreaseInfoID);
                               if (selectedReasons.Any())
                               {
                                   foreach (var reason in selectedReasons)
                                   {
                                       _rentIncrease.RentIncreaseReasons.Where(r => r.ReasonID == reason.ReasonID).First().IsSelected = true;
                                   }
                               }
                               _rentalInfo.Add(_rentIncrease);
                               //model.OwnerResponseInfo.PropertyInfo.RentalInfo.Add(_rentIncrease);
                           }
                           model.OwnerResponseInfo.PropertyInfo.RentalInfo = _rentalInfo;
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

       public ReturnResult<OwnerResponsePropertyInfoM> GetOResponseExemption(OwnerResponsePropertyInfoM model)
       {
           ReturnResult<OwnerResponsePropertyInfoM> result = new ReturnResult<OwnerResponsePropertyInfoM>();
           try
           {
               if (model.OwnerPropertyID > 0)
               {
                   var propertyInfo = (from r in _dbContext.OwnerResponsePropertyInfos
                                       where r.PropertyID == model.OwnerPropertyID
                                       select r).First();
                   if (propertyInfo != null)
                   {

                       model.bExemptFromRentAdjustment =Convert.ToBoolean(propertyInfo.bExemptFromRentAdjustment);
                       model.bPriorTenantLeftAfteQuitNotice =Convert.ToBoolean( propertyInfo.bPriorTenantLeftAfteQuitNotice);
                       model.PriorTenantLeftAfteQuitNoticeExplenation = propertyInfo.PriorTenantLeftAfteQuitNoticeExplenation;
                       model.bPriorTenantLeftAfteRentIncreaseNotice = Convert.ToBoolean(propertyInfo.bPriorTenantLeftAfteRentIncreaseNotice);
                       model.PriorTenantLeftAfteRentIncreaseNoticeExplenation = propertyInfo.PriorTenantLeftAfteRentIncreaseNoticeExplenation;
                       model.bPriorTenantEvicted = Convert.ToBoolean(propertyInfo.bPriorTenantEvicted);
                       model.PriorTenantEvictedExplenation = propertyInfo.PriorTenantEvictedExplenation;
                       model.bOutstandingViolations = Convert.ToBoolean(propertyInfo.bOutstandingViolations);
                       model.OutstandingViolationsExplenation = propertyInfo.OutstandingViolationsExplenation;
                       model.bSingleFamilyUnitOrCondominium = Convert.ToBoolean(propertyInfo.bSingleFamilyUnitOrCondominium);
                       model.SingleFamilyUnitOrCondominiumExplenation = propertyInfo.SingleFamilyUnitOrCondominiumExplenation;
                       model.bRoommatesWhenMoviedIN = Convert.ToBoolean(propertyInfo.bRoommatesWhenMoviedIN);
                       model.RoommatesWhenMoviedINExplenation = propertyInfo.RoommatesWhenMoviedINExplenation;
                       model.bUnitPruchased = Convert.ToBoolean(propertyInfo.bUnitPruchased);
                       model.UnitPruchasedExplenation = propertyInfo.UnitPruchasedExplenation;
                       model.PurchasedFrom = propertyInfo.PurchasedFrom;
                       model.bEntireBuildingPurchased = Convert.ToBoolean(propertyInfo.bEntireBuildingPurchased);
                       model.EntireBuildingPurchasedExplenation = propertyInfo.EntireBuildingPurchasedExplenation;
                       model.bRentControlledOtherThanRAP = Convert.ToBoolean(propertyInfo.bRentControlledOtherThanRAP);
                       model.bUnitNewlyConstructed = Convert.ToBoolean(propertyInfo.bUnitNewlyConstructed);
                       model.bTenantWasResidentOfHotelWhileFiling = Convert.ToBoolean(propertyInfo.bTenantWasResidentOfHotelWhileFiling);
                       model.bUnitWasRehabilitated = Convert.ToBoolean(propertyInfo.bUnitWasRehabilitated);
                       model.bUnitIsAccommodation = Convert.ToBoolean(propertyInfo.bUnitIsAccommodation);
                       model.bHasUnitOccupiedByOwner = Convert.ToBoolean(propertyInfo.bHasUnitOccupiedByOwner);

                   }

               }
               else
               {
                   var propertyInfo = (from r in _dbContext.OwnerResponsePropertyInfos
                                       where r.CustomerID == model.CustomerID && r.bPetitionFiled == false
                                       select r).First();
                   if (propertyInfo != null)
                   {
                       model.bExemptFromRentAdjustment = Convert.ToBoolean(propertyInfo.bExemptFromRentAdjustment);
                       model.bPriorTenantLeftAfteQuitNotice = Convert.ToBoolean(propertyInfo.bPriorTenantLeftAfteQuitNotice);
                       model.PriorTenantLeftAfteQuitNoticeExplenation = propertyInfo.PriorTenantLeftAfteQuitNoticeExplenation;
                       model.bPriorTenantLeftAfteRentIncreaseNotice = Convert.ToBoolean(propertyInfo.bPriorTenantLeftAfteRentIncreaseNotice);
                       model.PriorTenantLeftAfteRentIncreaseNoticeExplenation = propertyInfo.PriorTenantLeftAfteRentIncreaseNoticeExplenation;
                       model.bPriorTenantEvicted = Convert.ToBoolean(propertyInfo.bPriorTenantEvicted);
                       model.PriorTenantEvictedExplenation = propertyInfo.PriorTenantEvictedExplenation;
                       model.bOutstandingViolations = Convert.ToBoolean(propertyInfo.bOutstandingViolations);
                       model.OutstandingViolationsExplenation = propertyInfo.OutstandingViolationsExplenation;
                       model.bSingleFamilyUnitOrCondominium = Convert.ToBoolean(propertyInfo.bSingleFamilyUnitOrCondominium);
                       model.SingleFamilyUnitOrCondominiumExplenation = propertyInfo.SingleFamilyUnitOrCondominiumExplenation;
                       model.bRoommatesWhenMoviedIN = Convert.ToBoolean(propertyInfo.bRoommatesWhenMoviedIN);
                       model.RoommatesWhenMoviedINExplenation = propertyInfo.RoommatesWhenMoviedINExplenation;
                       model.bUnitPruchased = Convert.ToBoolean(propertyInfo.bUnitPruchased);
                       model.UnitPruchasedExplenation = propertyInfo.UnitPruchasedExplenation;
                       model.PurchasedFrom = propertyInfo.PurchasedFrom;
                       model.bEntireBuildingPurchased = Convert.ToBoolean(propertyInfo.bEntireBuildingPurchased);
                       model.EntireBuildingPurchasedExplenation = propertyInfo.EntireBuildingPurchasedExplenation;
                       model.bRentControlledOtherThanRAP = Convert.ToBoolean(propertyInfo.bRentControlledOtherThanRAP);
                       model.bUnitNewlyConstructed = Convert.ToBoolean(propertyInfo.bUnitNewlyConstructed);
                       model.bTenantWasResidentOfHotelWhileFiling = Convert.ToBoolean(propertyInfo.bTenantWasResidentOfHotelWhileFiling);
                       model.bUnitWasRehabilitated = Convert.ToBoolean(propertyInfo.bUnitWasRehabilitated);
                       model.bUnitIsAccommodation = Convert.ToBoolean(propertyInfo.bUnitIsAccommodation);
                       model.bHasUnitOccupiedByOwner = Convert.ToBoolean(propertyInfo.bHasUnitOccupiedByOwner);
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

       public ReturnResult<CaseInfoM> GetOResponseReview(CaseInfoM model)
       {
           ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
           try
           {
               var applicantInfoResult = GetOResponseApplicantInfo(model);
               if (applicantInfoResult.status.Status != StatusEnum.Success)
               {
                   result.status = applicantInfoResult.status;
                   return result;
               }
               model = applicantInfoResult.result;

               var PropertyAndTenantInfoResult = GetOResponsePropertyAndTenantInfo(model.OwnerResponseInfo.PropertyInfo);
               if (PropertyAndTenantInfoResult.status.Status != StatusEnum.Success)
               {
                   result.status = PropertyAndTenantInfoResult.status;
                   return result;
               }
               model.OwnerResponseInfo.PropertyInfo = PropertyAndTenantInfoResult.result;

               var RentIncreaseAndPropertyInfoResult = GetOResponseRentIncreaseAndPropertyInfo(model);
               if (RentIncreaseAndPropertyInfoResult.status.Status != StatusEnum.Success)
               {
                   result.status = RentIncreaseAndPropertyInfoResult.status;
                   return result;
               }
               model.OwnerResponseInfo.PropertyInfo = RentIncreaseAndPropertyInfoResult.result.OwnerResponseInfo.PropertyInfo;

               var exemptionResult = GetOResponseExemption(model.OwnerResponseInfo.PropertyInfo);
               if (exemptionResult.status.Status != StatusEnum.Success)
               {
                   result.status = exemptionResult.status;
                   return result;
               }
               model.OwnerResponseInfo.PropertyInfo = exemptionResult.result;

               var documentResult = _commondbHandler.GetDocumentsByCategory(model.CustomerID, false, DocCategory.OwnerResponse.ToString());
               if (documentResult.status.Status == StatusEnum.Success)
               {
                   model.Documents = documentResult.result;
               }

               model.OwnerResponseInfo.CustomerIdentityKey = Convert.ToInt32(_dbAccount.CustomerDetails.Where(x => x.CustomerID == model.CustomerID).Select(x => x.CustomerIdentityKey).FirstOrDefault());
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

       public ReturnResult<OwnerResponsePageSubnmissionStatusM> GetOResponseSubmissionStatus(int CustomerID)
       {
           ReturnResult<OwnerResponsePageSubnmissionStatusM> result = new ReturnResult<OwnerResponsePageSubnmissionStatusM>();
           OwnerResponsePageSubnmissionStatusM model = new OwnerResponsePageSubnmissionStatusM();
           try
           {
               var oResponse = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
               if (oResponse != null)
               {

                   model.ImportantInformation = Convert.ToBoolean(oResponse.ImportantInformation);
                   model.ApplicantInformation = Convert.ToBoolean(oResponse.ApplicantInformation);
                   model.RentalProperty = Convert.ToBoolean(oResponse.RentalProperty);
                   model.RentHistory = Convert.ToBoolean(oResponse.RentHistory);
                   model.DecreasedHousingServices = Convert.ToBoolean(oResponse.DecreasedHousingServices);
                   model.Exeption = Convert.ToBoolean(oResponse.Exeption);
                   model.AdditionalDocumentation = Convert.ToBoolean(oResponse.AdditionalDocumentation);
                   model.Review = Convert.ToBoolean(oResponse.Review);
                   model.Verification = Convert.ToBoolean(oResponse.Verification);
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

       #region Owner Response Save Functions
       public ReturnResult<CaseInfoM> SaveOResponseApplicantInfo(CaseInfoM model)
       {
           ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
           int applicantUserID = 0;
           int thirdPartyUserID = 0;
           {
               try
               {
                   if (model.bCaseFiledByThirdParty)
                   {
                       var applicantUserResult = _commondbHandler.SaveUserInfo(model.OwnerResponseInfo.ApplicantInfo.ApplicantUserInfo);
                       if (applicantUserResult.status.Status != StatusEnum.Success)
                       {
                           result.status = applicantUserResult.status;
                           return result;
                       }
                       applicantUserID = applicantUserResult.result.UserID;
                       thirdPartyUserID = model.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser.UserID;
                   }
                   else
                   {
                       if (model.OwnerResponseInfo.ApplicantInfo.bThirdPartyRepresentation)
                       {
                           var thirdpartyUserResult = _commondbHandler.SaveUserInfo(model.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser);
                           if (thirdpartyUserResult.status.Status != StatusEnum.Success)
                           {
                               result.status = thirdpartyUserResult.status;
                               return result;
                           }
                           var saveThirdPartyResult = _accountdbHandler.SaveOrUpdateThirdPartyInfo(new ThirdPartyInfoM() { CustomerID = model.OwnerResponseInfo.ApplicantInfo.CustomerID, ThirdPartyUser = thirdpartyUserResult.result });
                           if (saveThirdPartyResult.status.Status != StatusEnum.Success)
                           {
                               result.status = saveThirdPartyResult.status;
                               return result;
                           }
                           thirdPartyUserID = thirdpartyUserResult.result.UserID;
                       }
                       applicantUserID = model.OwnerResponseInfo.ApplicantInfo.ApplicantUserInfo.UserID;
                   }

                   if (model.OwnerResponseInfo.ApplicantInfo.OwnerResponseApplicantInfoID != 0 && !model.OwnerResponseInfo.ApplicantInfo.bPetitionFiled)
                   {
                       var applicantInfo = from r in _dbContext.OwnerResponseApplicantInfos
                                           where r.OwnerResponseApplicantInfoID == model.OwnerResponseInfo.ApplicantInfo.OwnerResponseApplicantInfoID
                                           select r;
                       if (applicantInfo.Any())
                       {
                           OwnerResponseApplicantInfoM _applicantInfo = new OwnerResponseApplicantInfoM();
                           applicantInfo.First().ApplicantUserID = applicantUserID;
                           applicantInfo.First().bThirdPartyRepresentation = model.OwnerResponseInfo.ApplicantInfo.bThirdPartyRepresentation;
                           if (thirdPartyUserID > 0)
                           {
                               applicantInfo.First().ThirdPartyUserID = thirdPartyUserID;
                           }
                           applicantInfo.First().bBusinessLicensePaid = model.OwnerResponseInfo.ApplicantInfo.bBusinessLicensePaid;
                           applicantInfo.First().BusinessLicenseNumber = model.OwnerResponseInfo.ApplicantInfo.BusinessLicenseNumber;
                           applicantInfo.First().bRentAdjustmentProgramFeePaid = model.OwnerResponseInfo.ApplicantInfo.bRentAdjustmentProgramFeePaid;
                           applicantInfo.First().CaseRespondingTo = model.OwnerResponseInfo.ApplicantInfo.CaseRespondingTo;
                           applicantInfo.First().BuildingAcquiredDate = new DateTime(model.OwnerResponseInfo.ApplicantInfo.BuildingAcquiredDate.Year, model.OwnerResponseInfo.ApplicantInfo.BuildingAcquiredDate.Month, model.OwnerResponseInfo.ApplicantInfo.BuildingAcquiredDate.Day);
                           applicantInfo.First().NumberOfUnits = model.OwnerResponseInfo.ApplicantInfo.NumberOfUnits;
                           if (model.OwnerResponseInfo.ApplicantInfo.NumberOfUnitsRangeID != 0)
                           {
                               applicantInfo.First().RangeID = model.OwnerResponseInfo.ApplicantInfo.NumberOfUnitsRangeID;
                           }
                           applicantInfo.First().bMoreThanOneStreetOnParcel = model.OwnerResponseInfo.ApplicantInfo.bMoreThanOneStreetOnParcel;
                           _dbContext.SubmitChanges();
                       }
                   }
                   else
                   {
                       OwnerResponseApplicantInfo applicantInfo = new OwnerResponseApplicantInfo();
                       applicantInfo.ApplicantUserID = applicantUserID;
                       applicantInfo.bThirdPartyRepresentation = model.OwnerResponseInfo.ApplicantInfo.bThirdPartyRepresentation;
                       if (thirdPartyUserID > 0)
                       {
                           applicantInfo.ThirdPartyUserID = thirdPartyUserID;
                       }
                       applicantInfo.bBusinessLicensePaid = model.OwnerResponseInfo.ApplicantInfo.bBusinessLicensePaid;
                       applicantInfo.BusinessLicenseNumber = model.OwnerResponseInfo.ApplicantInfo.BusinessLicenseNumber;
                       applicantInfo.bRentAdjustmentProgramFeePaid = model.OwnerResponseInfo.ApplicantInfo.bRentAdjustmentProgramFeePaid;
                       applicantInfo.BuildingAcquiredDate = new DateTime(model.OwnerResponseInfo.ApplicantInfo.BuildingAcquiredDate.Year, model.OwnerResponseInfo.ApplicantInfo.BuildingAcquiredDate.Month, model.OwnerResponseInfo.ApplicantInfo.BuildingAcquiredDate.Day);
                       applicantInfo.NumberOfUnits = model.OwnerResponseInfo.ApplicantInfo.NumberOfUnits;
                       if (model.OwnerResponseInfo.ApplicantInfo.NumberOfUnitsRangeID != 0)
                       {
                           applicantInfo.RangeID = model.OwnerResponseInfo.ApplicantInfo.NumberOfUnitsRangeID;
                       }
                       applicantInfo.bMoreThanOneStreetOnParcel = model.OwnerResponseInfo.ApplicantInfo.bMoreThanOneStreetOnParcel;
                       applicantInfo.CustomerID = model.OwnerResponseInfo.ApplicantInfo.CustomerID;
                       applicantInfo.CaseRespondingTo = model.OwnerResponseInfo.ApplicantInfo.CaseRespondingTo;
                       applicantInfo.bPetitionFiled = false;
                       _dbContext.OwnerResponseApplicantInfos.InsertOnSubmit(applicantInfo);
                       _dbContext.SubmitChanges();
                       model.OwnerResponseInfo.ApplicantInfo.OwnerResponseApplicantInfoID = applicantInfo.OwnerResponseApplicantInfoID;

                       OwnerResponsePageSubmissionStatus oResponseSubmission = new OwnerResponsePageSubmissionStatus();
                       oResponseSubmission.ImportantInformation = true;
                       oResponseSubmission.ApplicantInformation = true;
                       oResponseSubmission.CustomerID = model.CustomerID;
                       _dbContext.OwnerResponsePageSubmissionStatus.InsertOnSubmit(oResponseSubmission);
                       _dbContext.SubmitChanges();
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
       }

       public ReturnResult<OwnerResponsePropertyInfoM> SaveOResponsePropertyAndTenantInfo(OwnerResponsePropertyInfoM model)
       {
           ReturnResult<OwnerResponsePropertyInfoM> result = new ReturnResult<OwnerResponsePropertyInfoM>();
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

                   OwnerResponsePropertyInfo propertyInfo = new OwnerResponsePropertyInfo();
                   propertyInfo.UnitTypeID = model.UnitTypeID;
                   propertyInfo.CustomerID = model.CustomerID;
                   propertyInfo.bPetitionFiled = false;
                   _dbContext.OwnerResponsePropertyInfos.InsertOnSubmit(propertyInfo);
                   _dbContext.SubmitChanges();
                   model.OwnerPropertyID = propertyInfo.PropertyID;
               }
               if (model.TenantInfo.Any())
               {
                   
                   List<OwnerPetitionTenantInfoM> tenantsInfoM = new List<OwnerPetitionTenantInfoM>();
                   foreach (var tenant in model.TenantInfo)
                   {
                       if (tenant.IsDeleted == false)
                       {
                           var userResult = _commondbHandler.SaveUserInfo(tenant.TenantUserInfo);
                           if (userResult.status.Status == StatusEnum.Success)
                           {
                               var userinfo = from r in _dbContext.OwnerResponseTenantInfos
                                              where r.TenantUserID == userResult.result.UserID
                                              select r;
                               if (!userinfo.Any())
                               {
                                   OwnerResponseTenantInfo tenantInfo = new OwnerResponseTenantInfo();
                                   tenant.TenantUserInfo = userResult.result;
                                   tenantInfo.TenantUserID = tenant.TenantUserInfo.UserID;
                                   tenantInfo.PropertyID = model.OwnerPropertyID;
                                   _dbContext.OwnerResponseTenantInfos.InsertOnSubmit(tenantInfo);
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

                       }
                       else
                       {
                           if (tenant.TenantInfoID > 0)
                           {
                               var _tenant = _dbContext.OwnerResponseTenantInfos.Where(r => r.TenantInfoID == tenant.TenantInfoID).FirstOrDefault();
                               if (_tenant != null)
                               {
                                   _dbContext.OwnerResponseTenantInfos.DeleteOnSubmit(_tenant);
                                   _dbContext.SubmitChanges();
                               }
                           }
                       }
                   }
                   model.TenantInfo = tenantsInfoM;
               }
               var oResponseSubmission = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();
               if (oResponseSubmission != null)
               {
                   oResponseSubmission.RentalProperty = true;                   
                   _dbContext.SubmitChanges();
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

       public ReturnResult<OwnerResponsePropertyInfoM> SaveOResponseRentIncreaseAndUpdatePropertyInfo(OwnerResponsePropertyInfoM model)
       {
           ReturnResult<OwnerResponsePropertyInfoM> result = new ReturnResult<OwnerResponsePropertyInfoM>();
           try
           {
               if (model.OwnerPropertyID > 0)
               {
                   var propertyInfo = from r in _dbContext.OwnerResponsePropertyInfos
                                      where r.PropertyID == model.OwnerPropertyID
                                      select r;
                   if (propertyInfo.Any())
                   {
                       propertyInfo.First().MovedInDate = new DateTime(model.MovedInDate.Year, model.MovedInDate.Month, model.MovedInDate.Day);
                       propertyInfo.First().InitialRent = model.InitialRent;
                       propertyInfo.First().RAPNoticeStatusID = model.RAPNoticeStatusID;
                       propertyInfo.First().RAPNoticeGivenDate = new DateTime(model.RAPNoticeGivenDate.Year, model.RAPNoticeGivenDate.Month, model.RAPNoticeGivenDate.Day);
                       propertyInfo.First().CurrentOnRent = model.CurrentOnRent;
                       propertyInfo.First().bCapitalImprovementIncrease = model.bCapitalImprovementIncrease;
                       propertyInfo.First().bCaptialImprovementContested = model.bCaptialImprovementContested;
                       propertyInfo.First().CaseNumber = model.CaseNumbers;
                       propertyInfo.First().bRAPNoticeToRAPOffice = model.bRAPNoticeToRAPOffice;
                      // propertyInfo.First().RAPNoticeToRAPOfficeDate = new DateTime(model.RAPNoticeToRAPOfficeDate.Year, model.RAPNoticeToRAPOfficeDate.Month, model.RAPNoticeToRAPOfficeDate.Day);
                       _dbContext.SubmitChanges();
                   }
               }
               else
               {
                   var propertyInfo = from r in _dbContext.OwnerResponsePropertyInfos
                                      where r.CustomerID == model.CustomerID && r.bPetitionFiled == false
                                      select r;
                   if (propertyInfo.Any())
                   {
                       propertyInfo.First().MovedInDate = new DateTime(model.MovedInDate.Year, model.MovedInDate.Month, model.MovedInDate.Day);
                       propertyInfo.First().InitialRent = model.InitialRent;
                       propertyInfo.First().RAPNoticeStatusID = model.RAPNoticeStatusID;
                       propertyInfo.First().RAPNoticeGivenDate = new DateTime(model.RAPNoticeGivenDate.Year, model.RAPNoticeGivenDate.Month, model.RAPNoticeGivenDate.Day);
                       propertyInfo.First().CurrentOnRent = model.CurrentOnRent;
                       propertyInfo.First().bCapitalImprovementIncrease = model.bCapitalImprovementIncrease;
                       propertyInfo.First().bCaptialImprovementContested = model.bCaptialImprovementContested;
                       propertyInfo.First().CaseNumber = model.CaseNumbers;
                       propertyInfo.First().bRAPNoticeToRAPOffice = model.bRAPNoticeToRAPOffice;
                      // propertyInfo.First().RAPNoticeToRAPOfficeDate = new DateTime(model.RAPNoticeToRAPOfficeDate.Year, model.RAPNoticeToRAPOfficeDate.Month, model.RAPNoticeToRAPOfficeDate.Day);
                       _dbContext.SubmitChanges();
                   }
               }

               if (model.RentalInfo.Any())
               {
                   List<OwnerResponseRentalIncrementInfoM> _rentalInfo = new List<OwnerResponseRentalIncrementInfoM>();
                   foreach (var rent in model.RentalInfo)
                   {
                       if (rent.isDeleted == false)
                       {
                           //if (rent.RentalIncreaseInfoID != 0)
                           //{
                           //    var rentIncreaseInfo = from r in _dbContext.OwnerResponseRentalIncrementInfos
                           //                           where r.RentalIncreaseInfoID == rent.RentalIncreaseInfoID
                           //                           select r;
                           //    if (rentIncreaseInfo.Any())
                           //    {
                           //        rentIncreaseInfo.First().bRentIncreaseNoticeGiven = rent.bRentIncreaseNoticeGiven;
                           //        rentIncreaseInfo.First().RentIncreaseNoticeDate = new DateTime(rent.RentIncreaseNoticeDate.Year, rent.RentIncreaseNoticeDate.Month, rent.RentIncreaseNoticeDate.Day);
                           //        rentIncreaseInfo.First().RentIncreaseEffectiveDate = new DateTime(rent.RentIncreaseEffectiveDate.Year, rent.RentIncreaseEffectiveDate.Month, rent.RentIncreaseEffectiveDate.Day);
                           //        rentIncreaseInfo.First().RentIncreasedFrom = rent.RentIncreasedFrom;
                           //        rentIncreaseInfo.First().RentIncreasedTo = rent.RentIncreasedTo;
                           //        _dbContext.SubmitChanges();
                           //        _rentalInfo.Add(rent);
                           //    }
                           //}
                           
                          if (rent.RentalIncreaseInfoID == 0)
                           {
                               OwnerResponseRentalIncrementInfo rentIncreaseInfo = new OwnerResponseRentalIncrementInfo();
                               rentIncreaseInfo.PropertyID = model.OwnerPropertyID;
                               rentIncreaseInfo.bRentIncreaseNoticeGiven = rent.bRentIncreaseNoticeGiven;
                               rentIncreaseInfo.RentIncreaseNoticeDate = new DateTime(rent.RentIncreaseNoticeDate.Year, rent.RentIncreaseNoticeDate.Month, rent.RentIncreaseNoticeDate.Day);
                               rentIncreaseInfo.RentIncreaseEffectiveDate = new DateTime(rent.RentIncreaseEffectiveDate.Year, rent.RentIncreaseEffectiveDate.Month, rent.RentIncreaseEffectiveDate.Day);
                               rentIncreaseInfo.RentIncreasedFrom = rent.RentIncreasedFrom;
                               rentIncreaseInfo.RentIncreasedTo = rent.RentIncreasedTo;
                               _dbContext.OwnerResponseRentalIncrementInfos.InsertOnSubmit(rentIncreaseInfo);
                               _dbContext.SubmitChanges();
                               rent.RentalIncreaseInfoID = rentIncreaseInfo.RentalIncreaseInfoID;
                               _rentalInfo.Add(rent);
                           }
                           else
                          {
                              _rentalInfo.Add(rent);
                          }
                       }
                       else
                       {
                           if (rent.RentalIncreaseInfoID != 0)
                           {
                               var rentIncreaseReasonDB = from r in _dbContext.OwnerResponseRentIncreaseReasonInfos
                                                          where r.RentalIncreaseInfoID == rent.RentalIncreaseInfoID
                                                          select r;
                               if (rentIncreaseReasonDB.Any())
                               {
                                   foreach (var item in rentIncreaseReasonDB)
                                   {
                                       _dbContext.OwnerResponseRentIncreaseReasonInfos.DeleteOnSubmit(item);
                                       _dbContext.SubmitChanges();
                                   }
                               }
                               var rentIncreaseInfo = from r in _dbContext.OwnerResponseRentalIncrementInfos
                                                      where r.RentalIncreaseInfoID == rent.RentalIncreaseInfoID
                                                      select r;
                               if (rentIncreaseInfo.Any())
                               {
                                   _dbContext.OwnerResponseRentalIncrementInfos.DeleteOnSubmit(rentIncreaseInfo.Where(x => x.RentalIncreaseInfoID == rent.RentalIncreaseInfoID).First());
                                   _dbContext.SubmitChanges();
                               }
                               
                           }
                       }
                   }
                   model.RentalInfo = _rentalInfo;

                   foreach (var rentIncrease in model.RentalInfo)
                   {
                       if (rentIncrease.RentIncreaseReasons.Select(x => x.IsSelected == true).Any())
                       {
                           var rentIncreaseReasonDB = from r in _dbContext.OwnerResponseRentIncreaseReasonInfos
                                                      where r.RentalIncreaseInfoID == rentIncrease.RentalIncreaseInfoID
                                                      select r;
                           if (rentIncreaseReasonDB.Any())
                           {
                               // do nothing 
                               //foreach (var item in rentIncrease.RentIncreaseReasons)
                               //{
                               //    if (item.IsSelected)
                               //    {
                               //        if (!rentIncreaseReasonDB.Select(x => x.ReasonID == item.ReasonID).Any())
                               //        {
                               //            OwnerResponseRentIncreaseReasonInfo rentIncreaseReason = new OwnerResponseRentIncreaseReasonInfo();
                               //            rentIncreaseReason.RentalIncreaseInfoID = rentIncrease.RentalIncreaseInfoID;
                               //            rentIncreaseReason.ReasonID = item.ReasonID;
                               //            _dbContext.OwnerResponseRentIncreaseReasonInfos.InsertOnSubmit(rentIncreaseReason);
                               //            _dbContext.SubmitChanges();
                               //        }
                               //    }
                               //    else
                               //    {
                               //        if (rentIncreaseReasonDB.Select(x => x.ReasonID == item.ReasonID).Any())
                               //        {
                               //            OwnerResponseRentIncreaseReasonInfo rentIncreaseReason = new OwnerResponseRentIncreaseReasonInfo();
                               //            rentIncreaseReason.RentalIncreaseInfoID = rentIncrease.RentalIncreaseInfoID;
                               //            rentIncreaseReason.ReasonID = item.ReasonID;
                               //            _dbContext.OwnerResponseRentIncreaseReasonInfos.DeleteOnSubmit(rentIncreaseReason);
                               //            _dbContext.SubmitChanges();
                               //        }
                               //    }

                               //}
                           }
                           else
                           {
                               foreach (var item in rentIncrease.RentIncreaseReasons)
                               {
                                   if (item.IsSelected)
                                   {
                                       OwnerResponseRentIncreaseReasonInfo rentIncreaseReason = new OwnerResponseRentIncreaseReasonInfo();
                                       rentIncreaseReason.RentalIncreaseInfoID = rentIncrease.RentalIncreaseInfoID;
                                       rentIncreaseReason.ReasonID = item.ReasonID;
                                       _dbContext.OwnerResponseRentIncreaseReasonInfos.InsertOnSubmit(rentIncreaseReason);
                                       _dbContext.SubmitChanges();
                                   }
                               }
                           }
                       }
                   }
               }
               var oResponseSubmission = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();
               if (oResponseSubmission != null)
               {
                   oResponseSubmission.RentHistory = true;
                   _dbContext.SubmitChanges();
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

       public ReturnResult<OwnerResponsePropertyInfoM> SaveOResponseExemption(OwnerResponsePropertyInfoM model)
       {
           ReturnResult<OwnerResponsePropertyInfoM> result = new ReturnResult<OwnerResponsePropertyInfoM>();
           try
           {
               if (model.OwnerPropertyID > 0)
               {
                   var propertyInfo = (from r in _dbContext.OwnerResponsePropertyInfos
                                       where r.PropertyID == model.OwnerPropertyID
                                       select r).First();
                   if (propertyInfo != null)
                   {
                       propertyInfo.bExemptFromRentAdjustment = model.bExemptFromRentAdjustment;
                       propertyInfo.bPriorTenantLeftAfteQuitNotice = model.bPriorTenantLeftAfteQuitNotice;
                       propertyInfo.PriorTenantLeftAfteQuitNoticeExplenation = model.PriorTenantLeftAfteQuitNoticeExplenation;
                       propertyInfo.bPriorTenantLeftAfteRentIncreaseNotice = model.bPriorTenantLeftAfteRentIncreaseNotice;
                       propertyInfo.PriorTenantLeftAfteRentIncreaseNoticeExplenation = model.PriorTenantLeftAfteRentIncreaseNoticeExplenation;
                       propertyInfo.bPriorTenantEvicted = model.bPriorTenantEvicted;
                       propertyInfo.PriorTenantEvictedExplenation = model.PriorTenantEvictedExplenation;
                       propertyInfo.bOutstandingViolations = model.bOutstandingViolations;
                       propertyInfo.OutstandingViolationsExplenation = model.OutstandingViolationsExplenation;
                       propertyInfo.bSingleFamilyUnitOrCondominium = model.bSingleFamilyUnitOrCondominium;
                       propertyInfo.SingleFamilyUnitOrCondominiumExplenation = model.SingleFamilyUnitOrCondominiumExplenation;
                       propertyInfo.bRoommatesWhenMoviedIN = model.bRoommatesWhenMoviedIN;
                       propertyInfo.RoommatesWhenMoviedINExplenation = model.RoommatesWhenMoviedINExplenation;
                       propertyInfo.bUnitPruchased = model.bUnitPruchased;
                       propertyInfo.UnitPruchasedExplenation = model.UnitPruchasedExplenation;
                       propertyInfo.PurchasedFrom = model.PurchasedFrom;
                       propertyInfo.bEntireBuildingPurchased = model.bEntireBuildingPurchased;
                       propertyInfo.EntireBuildingPurchasedExplenation = model.EntireBuildingPurchasedExplenation;
                       propertyInfo.bRentControlledOtherThanRAP = model.bRentControlledOtherThanRAP;
                       propertyInfo.bUnitNewlyConstructed = model.bUnitNewlyConstructed;
                       propertyInfo.bTenantWasResidentOfHotelWhileFiling = model.bTenantWasResidentOfHotelWhileFiling;
                       propertyInfo.bUnitWasRehabilitated = model.bUnitWasRehabilitated;
                       propertyInfo.bUnitIsAccommodation = model.bUnitIsAccommodation;
                       propertyInfo.bHasUnitOccupiedByOwner = model.bHasUnitOccupiedByOwner;
                       _dbContext.SubmitChanges();
                   }

               }
               else
               {
                   var propertyInfo = (from r in _dbContext.OwnerResponsePropertyInfos
                                       where r.CustomerID == model.CustomerID && r.bPetitionFiled == false
                                       select r).First();
                   if (propertyInfo != null)
                   {
                       propertyInfo.bExemptFromRentAdjustment = model.bExemptFromRentAdjustment;
                       propertyInfo.bPriorTenantLeftAfteQuitNotice = model.bPriorTenantLeftAfteQuitNotice;
                       propertyInfo.PriorTenantLeftAfteQuitNoticeExplenation = model.PriorTenantLeftAfteQuitNoticeExplenation;
                       propertyInfo.bPriorTenantLeftAfteRentIncreaseNotice = model.bPriorTenantLeftAfteRentIncreaseNotice;
                       propertyInfo.PriorTenantLeftAfteRentIncreaseNoticeExplenation = model.PriorTenantLeftAfteRentIncreaseNoticeExplenation;
                       propertyInfo.bPriorTenantEvicted = model.bPriorTenantEvicted;
                       propertyInfo.PriorTenantEvictedExplenation = model.PriorTenantEvictedExplenation;
                       propertyInfo.bOutstandingViolations = model.bOutstandingViolations;
                       propertyInfo.OutstandingViolationsExplenation = model.OutstandingViolationsExplenation;
                       propertyInfo.bSingleFamilyUnitOrCondominium = model.bSingleFamilyUnitOrCondominium;
                       propertyInfo.SingleFamilyUnitOrCondominiumExplenation = model.SingleFamilyUnitOrCondominiumExplenation;
                       propertyInfo.bRoommatesWhenMoviedIN = model.bRoommatesWhenMoviedIN;
                       propertyInfo.RoommatesWhenMoviedINExplenation = model.RoommatesWhenMoviedINExplenation;
                       propertyInfo.bUnitPruchased = model.bUnitPruchased;
                       propertyInfo.UnitPruchasedExplenation = model.UnitPruchasedExplenation;
                       propertyInfo.PurchasedFrom = model.PurchasedFrom;
                       propertyInfo.bEntireBuildingPurchased = model.bEntireBuildingPurchased;
                       propertyInfo.EntireBuildingPurchasedExplenation = model.EntireBuildingPurchasedExplenation;
                       propertyInfo.bRentControlledOtherThanRAP = model.bRentControlledOtherThanRAP;
                       propertyInfo.bUnitNewlyConstructed = model.bUnitNewlyConstructed;
                       propertyInfo.bTenantWasResidentOfHotelWhileFiling = model.bTenantWasResidentOfHotelWhileFiling;
                       propertyInfo.bUnitWasRehabilitated = model.bUnitWasRehabilitated;
                       propertyInfo.bUnitIsAccommodation = model.bUnitIsAccommodation;
                       propertyInfo.bHasUnitOccupiedByOwner = model.bHasUnitOccupiedByOwner;
                       _dbContext.SubmitChanges();
                   }
               }

               var oResponseSubmission = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();
               if (oResponseSubmission != null)
               {
                   oResponseSubmission.Exeption = true;
                   _dbContext.SubmitChanges();
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


       public ReturnResult<CaseInfoM> SubmitOwnerResponse(CaseInfoM model)
       {
           ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();     
          try
           {
              int c_id = 0;
             
               OwnerResponseInfo oResponse = new OwnerResponseInfo();
               oResponse.OwnerResponseApplicantInfoID = model.OwnerResponseInfo.ApplicantInfo.OwnerResponseApplicantInfoID;
               oResponse.OwnerResponsePropertyID = model.OwnerResponseInfo.PropertyInfo.OwnerPropertyID;
               oResponse.bAgreeToCityMediation = model.OwnerResponseInfo.bAgreeToCityMediation;
               oResponse.CreatedDate = DateTime.Now;
               _dbContext.OwnerResponseInfos.InsertOnSubmit(oResponse);
               _dbContext.SubmitChanges();
               model.OwnerResponseInfo.OwnerResponseID = oResponse.OwnerResponseID;

               if (model.OwnerResponseInfo.OwnerResponseID > 0)
               {
                   var caseinfo = _dbContext.CaseDetails.Where(r => r.CaseID == model.OwnerResponseInfo.ApplicantInfo.CaseRespondingTo).First();

                   if(caseinfo != null)
                   {   
                       c_id = caseinfo.C_ID;
                       caseinfo.OwnerResponseID = model.OwnerResponseInfo.OwnerResponseID;
                       _dbContext.SubmitChanges();
                   }
                   else
                   {
                       result.status = new OperationStatus() { Status = StatusEnum.OwnerResponseSubmissionFailed };
                       _commondbHandler.SaveErrorLog(result.status);
                       return result;
                   }
               }
               else
               {

                   result.status = new OperationStatus() { Status = StatusEnum.OwnerResponseSubmissionFailed };
                   _commondbHandler.SaveErrorLog(result.status);
                   return result;
               }

               var applicantInfo = _dbContext.OwnerResponseApplicantInfos.Where(r => r.OwnerResponseApplicantInfoID == model.OwnerResponseInfo.ApplicantInfo.OwnerResponseApplicantInfoID).FirstOrDefault();
               applicantInfo.bPetitionFiled = true;
               _dbContext.SubmitChanges();
               var propertyInfo = _dbContext.OwnerResponsePropertyInfos.Where(r => r.PropertyID == model.OwnerResponseInfo.PropertyInfo.OwnerPropertyID).FirstOrDefault();
               propertyInfo.bPetitionFiled = true;
               _dbContext.SubmitChanges();

               var updateDocumentResult = _commondbHandler.UpdateDocumentCaseInfo(model.CustomerID, c_id, DocCategory.OwnerResponse.ToString());
               if (updateDocumentResult.status.Status != StatusEnum.Success)
               {
                   result.status = updateDocumentResult.status;
                   return result;
               }

               var oResponseSubmission = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();
               if (oResponseSubmission != null)
               {
                   _dbContext.OwnerResponsePageSubmissionStatus.DeleteOnSubmit(oResponseSubmission);
                   _dbContext.SubmitChanges();
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

       public ReturnResult<bool> OResponseUpdateDecreasedHousingPageSubmission(int CustomerID)
       {
           ReturnResult<bool> result = new ReturnResult<bool>();
           try
           {
               var oResponseSubmission = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
               if (oResponseSubmission != null)
               {
                   oResponseSubmission.DecreasedHousingServices = true;
                   _dbContext.SubmitChanges();
               }

               result.result = true;
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

       public ReturnResult<bool> OResponseUpdateAdditionalDocumentsPageSubmission(int CustomerID)
       {
           ReturnResult<bool> result = new ReturnResult<bool>();
           try
           {
               var oResponseSubmission = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
               if (oResponseSubmission != null)
               {
                   oResponseSubmission.AdditionalDocumentation = true;
                   _dbContext.SubmitChanges();
               }

               result.result = true;
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
        
       public ReturnResult<bool> OResponseUpdateReviewPageSubmission(int CustomerID)
       {
           ReturnResult<bool> result = new ReturnResult<bool>();
           try
           {
               var oResponseSubmission = _dbContext.OwnerResponsePageSubmissionStatus.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
               if (oResponseSubmission != null)
               {
                   oResponseSubmission.Review = true;
                   _dbContext.SubmitChanges();
               }

               result.result = true;
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
