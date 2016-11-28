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
        CommonDBHandler commondbHandler = new CommonDBHandler();
        public ApplicationProcessingDBHandler()
        {
            _dbContext = new ApplicationProcessingDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]);
        }
        #region "Get"
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
        public ReturnResult<CaseInfoM> GetCaseDetails(string caseID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            result.result = new CaseInfoM();
            CaseInfoM caseInfo = new CaseInfoM();

            int petitionFileID = 0;
            int ownerUserID = 0;
            int thirdPartyUSerID = 0;
            int appealID = 0;
            try
            {
               
                    var caseDetails = _dbContext.CaseDetails.Where(x => x.CaseID == caseID).FirstOrDefault();
                    if (caseDetails != null)
                    {
                        petitionFileID = caseDetails.PetitionFileID;
                        ownerUserID = caseDetails.OwnerUserID;
                        thirdPartyUSerID = caseDetails.ThirdPartyUserID;
                        appealID = (caseDetails.TenantAppealID == null) ? 0 : Convert.ToInt32(caseDetails.TenantAppealID);
                       // caseInfo.bThirdPartyRepresentation = (bool)caseDetails.bThirdPartyRepresentation;
                        caseInfo.bAgreeToCityMediation = (bool)caseDetails.bAgreeToCityMediation;

                        //TBD
                        caseInfo.CaseFileBy = 1;
                        caseInfo.bCaseFiledByThirdParty = (bool)caseDetails.bCaseFiledByThirdParty;
                        //TBD
                        caseInfo.CaseAssignedTo = "12345";
                        //TBD
                        caseInfo.CityUserFirstName = "City";
                        //TBD
                        caseInfo.CityUserLastName = "Admin";
                        //TBD
                        caseInfo.CityUserMailID = "testcity@gmail.com";

                        caseInfo.CaseID = caseDetails.CaseID;
                        caseInfo.TenantPetitionInfo = GetTenantPetition(petitionFileID).result;
                        caseInfo.TenantPetitionInfo.PetitionGrounds = GetPetitionGroundInfo(petitionFileID).result;
                        caseInfo.TenantPetitionInfo.LostServices = GetTenantLostServiceInfo(petitionFileID).result;
                        caseInfo.TenantPetitionInfo.RentIncreases = GetTenantRentalIncrementInfo(petitionFileID).result;
                        caseInfo.TenantAppealInfo.AppealGrounds = GetAppealGroundInfo(appealID).result;
                        //if (petitionFileID == 0)
                        //{
                        //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        //    return result;
                        //}
                        ReturnResult<UserInfoM> resultOwnerInfo = new ReturnResult<UserInfoM>();
                        resultOwnerInfo = commondbHandler.GetUserInfo(ownerUserID);
                       // caseInfo.OwnerInfo = resultOwnerInfo.result;
                        //if (ownerUserID == 0)
                        //{
                        //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        //    return result;
                        //}
                        //if (caseInfo.bThirdPartyRepresentation)
                        //{
                        //    ReturnResult<UserInfoM> resultThirdPartyInfo = new ReturnResult<UserInfoM>();
                        //    resultThirdPartyInfo = commondbHandler.GetUserInfo(thirdPartyUSerID);

                        //    caseInfo.ThirdPartyInfo = resultThirdPartyInfo.result;
                        //    //if (thirdPartyUSerID == 0)
                        //    //{
                        //    //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        //    //    return result;
                        //    //}
                        //}                    
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
                        tenantPetitionInfo.ThirdPartyInfo = commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ThirdPartyUserID).result;
                    }
                    if (TenantPetitionInfoDB.OwnerUserID >= 1)
                    {
                        tenantPetitionInfo.OwnerInfo = commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.OwnerUserID).result;
                    }
                    if (TenantPetitionInfoDB.PropertyManagerUserID >= 1)
                    {
                        tenantPetitionInfo.PropertyManager = commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.PropertyManagerUserID).result;
                    }

                    tenantPetitionInfo.NumberOfUnits = (int)TenantPetitionInfoDB.NumberOfUnits;
                    tenantPetitionInfo.UnitTypeId = TenantPetitionInfoDB.UnitTypeID;
                    tenantPetitionInfo.CurrentRentStatusID = TenantPetitionInfoDB.RentStatusID;
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
                        tenantPetitionInfo.ThirdPartyInfo = commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.ThirdPartyUserID).result;
                    }
                    tenantPetitionInfo.OwnerInfo = commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.OwnerUserID).result;
                    tenantPetitionInfo.PropertyManager = commondbHandler.GetUserInfo((int)TenantPetitionInfoDB.PropertyManagerUserID).result;
                }
                tenantPetitionInfo.NumberOfUnits = (int)TenantPetitionInfoDB.NumberOfUnits;
                tenantPetitionInfo.UnitTypeId = TenantPetitionInfoDB.UnitTypeID;
                tenantPetitionInfo.CurrentRentStatusID = TenantPetitionInfoDB.RentStatusID;
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
        private ReturnResult<List<TenantRentIncreaseInfoM>> GetTenantRentalIncrementInfo(int PetitionId)
        {
            ReturnResult<List<TenantRentIncreaseInfoM>> result = new ReturnResult<List<TenantRentIncreaseInfoM>>();
            List<TenantRentIncreaseInfoM> tenantRentIncreaseInfo = new List<TenantRentIncreaseInfoM>();
            try
            {
                
                    var TenantRentalIncrementInfoDB = _dbContext.TenantRentalIncrementInfos.Where(x => x.TenantPetitionID == PetitionId).ToList();
                    foreach (var item in TenantRentalIncrementInfoDB)
                    {
                        TenantRentIncreaseInfoM objTenantRentIncreaseInfoM = new TenantRentIncreaseInfoM();
                        //TenantRentalIncrementInfo rentIncrementDB = new TenantRentalIncrementInfo();
                        //rentIncrementDB.TenantPetitionID = petition.PetitionID;
                        objTenantRentIncreaseInfoM.bRentIncreaseNoticeGiven = Convert.ToBoolean(item.bRentIncreaseNoticeGiven);
                        objTenantRentIncreaseInfoM.RentIncreaseNoticeDate = Convert.ToDateTime(item.RentIncreaseNoticeDate);
                        objTenantRentIncreaseInfoM.RentIncreaseEffectiveDate = Convert.ToDateTime(item.RentIncreaseEffectiveDate);
                        objTenantRentIncreaseInfoM.RentIncreasedFrom = Convert.ToDecimal(item.RentIncreasedFrom);
                        objTenantRentIncreaseInfoM.RentIncreasedTo = Convert.ToDecimal(item.RentIncreasedTo);
                        objTenantRentIncreaseInfoM.bRentIncreaseContested = Convert.ToBoolean(item.bRentIncreaseContested);

                        tenantRentIncreaseInfo.Add(objTenantRentIncreaseInfoM);
                    }
                
                result.result = tenantRentIncreaseInfo;
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
        private ReturnResult<List<TenantLostServiceInfoM>> GetTenantLostServiceInfo(int PetitionID)
        {
            ReturnResult<List<TenantLostServiceInfoM>> result = new ReturnResult<List<TenantLostServiceInfoM>>();
            List<TenantLostServiceInfoM> tenantLostServiceInfo = new List<TenantLostServiceInfoM>();
            try
            {
                
                    var TenantLostServiceInfoDB = _dbContext.TenantLostServiceInfos.Where(x => x.TenantPetitionID == PetitionID).ToList();
                    foreach (var item in TenantLostServiceInfoDB)
                    {
                        TenantLostServiceInfoM objTenantLostServiceInfoM = new TenantLostServiceInfoM();

                        objTenantLostServiceInfoM.ReducedServiceDescription = item.ReducedServiceDescription;
                        objTenantLostServiceInfoM.EstimatedLoss = item.EstimatedLoss;
                        objTenantLostServiceInfoM.LossBeganDate = item.LossBeganDate;
                        objTenantLostServiceInfoM.PayingToServiceBeganDate = item.PayingToServiceBeganDate;

                        tenantLostServiceInfo.Add(objTenantLostServiceInfoM);
                    }
                
                result.result = tenantLostServiceInfo;
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
                        objTenantProblemInfoM.EstimatedLoss = item.EstimatedLoss;
                        objTenantProblemInfoM.ProblemBeganDate = item.ProblemBeganDate;

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
        private ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID)
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
                var TenantPetitionGroundInfoDB = _dbContext.TenantPetitionGroundInfos.Where(x => x.PetitionGroundID == petitionID).ToList();
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
                caseInfo.TenantPetitionInfo.LostServices = GetTenantLostServiceInfo(caseInfo.TenantPetitionInfo.PetitionID).result;
                caseInfo.TenantPetitionInfo.RentIncreases = GetTenantRentalIncrementInfo(caseInfo.TenantPetitionInfo.PetitionID).result;
               
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
        //private ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int PetitionID)
        //{
        //    ReturnResult<List<PetitionGroundM>> result = new ReturnResult<List<PetitionGroundM>>();
        //    List<PetitionGroundM> PetitionGroundInfo = new List<PetitionGroundM>();
        //    try
        //    {
               
        //            var petitionGrounds = _dbContext.PetitionGrounds;
        //            if (petitionGrounds == null)
        //            {
        //                result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
        //                return result;
        //            }
        //            else
        //            {
        //                foreach (var petitionGround in petitionGrounds)
        //                {
        //                    PetitionGroundM _petitionGround = new PetitionGroundM();
        //                    _petitionGround.PetitionGroundID = petitionGround.PetitionGroundID;
        //                    _petitionGround.PetitionGroundDescription = petitionGround.PetitionDescription;
        //                    PetitionGroundInfo.Add(_petitionGround);
        //                }
        //            }
        //            var TenantPetitionGroundInfoDB = _dbContext.TenantPetitionGroundInfos.Where(x => x.TenantPetitionID == PetitionID).ToList();
        //            foreach (var item in TenantPetitionGroundInfoDB)
        //            {
        //                foreach (var item1 in PetitionGroundInfo)
        //                {
        //                    if (item1.PetitionGroundID == item.PetitionGroundID)
        //                    {
        //                        item1.Selected = true;
        //                    }
        //                }
        //            }
                
        //        result.result = PetitionGroundInfo;
        //        result.status = new OperationStatus() { Status = StatusEnum.Success };

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        IExceptionHandler eHandler = new ExceptionHandler();
        //        result.status = eHandler.HandleException(ex);
        //        return result;
        //    }
        //}
        private ReturnResult<TenantAppealInfoM> GetAppealInfo(int appealID)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            result.result.AppealGrounds = GetAppealGroundInfo(appealID).result;

            return result;

        }
        ///<summary>
        ///Get appeal ground info based upon appealID
        ///</summary>
        ///<param name="appealID"></param>
        /// <returns></returns>
        private ReturnResult<List<AppealGroundM>> GetAppealGroundInfo(int appealID)
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
                    var TenantAppealGroundInfoDB = _dbContext.TenantAppealGroundInfos.Where(x => x.TenantAppealGroudID == appealID).ToList();
                    foreach (var item in TenantAppealGroundInfoDB)
                    {
                        foreach (var item1 in AppealGroundInfo)
                        {
                            if (item1.AppealGroundID == item.TenantAppealGroudID)
                            {
                                item1.Selected = true;
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
                    caseDetailsDB.PetitionFileID = petitionFileID;
                    //TBD
                    caseDetailsDB.PetitionCategoryID = 1;
                    //TBD
                    caseDetailsDB.TenantUserID = 1;
                    //caseDetailsDB.TenantUserID = caseInfo.TenantUserID;
             //       caseDetailsDB.bThirdPartyRepresentation = caseInfo.bThirdPartyRepresentation;
                    caseDetailsDB.ThirdPartyUserID = thirdPartyUSerID;
                    caseDetailsDB.OwnerUserID = ownerUserID;
                    caseDetailsDB.bAgreeToCityMediation = caseInfo.bAgreeToCityMediation;
                    //TBD
                    caseDetailsDB.CaseFiledBy = 1;
                    caseDetailsDB.bCaseFiledByThirdParty = caseInfo.bCaseFiledByThirdParty;
                    //TBD
                    caseDetailsDB.CaseAssignedTo = "12345";
                    //TBD
                    caseDetailsDB.CityUserFirstName = "City";
                    //TBD
                    caseDetailsDB.CityUserLastName = "Admin";
                    //TBD
                    caseDetailsDB.CityUserMailID = "testcity@gmail.com";
                    //TBD
                    caseDetailsDB.WorlFlowID = 1;
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
        private int GetPetitionFileID(int petitionID, int petitionCategory)
        {
            int petitionFileID = 0;

            PetitionDetail petitionDetailsDB = new PetitionDetail();

            if (petitionCategory == 1)
            {
                petitionDetailsDB.TenantPetitionID = petitionID;
                _dbContext.PetitionDetails.InsertOnSubmit(petitionDetailsDB);
                _dbContext.SubmitChanges();
                petitionFileID = petitionDetailsDB.PetitionFileID;
            }


            return petitionFileID;
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


                CaseDetail caseDetailsDB = new CaseDetail();
                caseDetailsDB.PetitionFileID = caseInfo.TenantPetitionInfo.PetitionID;
                ////TBD
                //caseDetailsDB.PetitionCategoryID = 1;
                ////TBD
                //caseDetailsDB.TenantUserID = 1;
                
                //TBD
                caseDetailsDB.CaseAssignedTo = "12345";
                //TBD
                caseDetailsDB.CityUserFirstName = "City";
                //TBD
                caseDetailsDB.CityUserLastName = "Admin";
                //TBD
                caseDetailsDB.CityUserMailID = "testcity@gmail.com";
                //TBD
                caseDetailsDB.WorlFlowID = 1;
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
        public ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();

            int ownerUserID = 0;
            int thirdPartyUserID = 0;
            int PropertyManagerUserID = 0;
            try
            {
                if (caseInfo.TenantPetitionInfo.bThirdPartyRepresentation)
                {
                    thirdPartyUserID = SaveUserInfo(caseInfo.TenantPetitionInfo.ThirdPartyInfo);
                    if (thirdPartyUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                }


                ownerUserID = SaveUserInfo(caseInfo.TenantPetitionInfo.OwnerInfo);
                if (ownerUserID == 0)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                    return result;
                }
                PropertyManagerUserID = ownerUserID;
                //PropertyManagerUserID = SaveUserInfo(caseInfo.TenantPetitionInfo.PropertyManager);
                //if (PropertyManagerUserID == 0)
                //{
                //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                //    return result;
                //}
                TenantPetitionInfo petitionDB = new TenantPetitionInfo();
                petitionDB.bThirdPartyRepresentation = caseInfo.TenantPetitionInfo.bThirdPartyRepresentation;
                petitionDB.ThirdPartyUserID = thirdPartyUserID;
                petitionDB.OwnerUserID = ownerUserID;
                petitionDB.PropertyManagerUserID = PropertyManagerUserID;
                petitionDB.NumberOfUnits = caseInfo.TenantPetitionInfo.NumberOfUnits;
                petitionDB.UnitTypeID =  caseInfo.TenantPetitionInfo.UnitTypeId;
                petitionDB.RentStatusID = caseInfo.TenantPetitionInfo.CurrentRentStatusID;
                petitionDB.ProvideExplanation = caseInfo.TenantPetitionInfo.ProvideExplanation;
                petitionDB.CreatedDate = DateTime.Now;
                petitionDB.PetitionFiledBy = UserID;
                _dbContext.TenantPetitionInfos.InsertOnSubmit(petitionDB);
                _dbContext.SubmitChanges();
                caseInfo.TenantPetitionInfo.PetitionID = petitionDB.TenantPetitionID;

                //petitionDB.NumberOfUnits = caseInfo.TenantPetitionInfo.NumberOfUnits;
                //petitionDB.UnitTypeID = caseInfo.TenantPetitionInfo.UnitTypeId;
                //petitionDB.RentStatusID = petition.CurrentRentStatusID;
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
                //petitionFileID = SaveTenantPetition(caseInfo.TenantPetitionInfo);
                //if (petitionFileID == 0)
                //{
                //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                //    return result;
                //}

                //CaseDetail caseDetailsDB = new CaseDetail();
                //caseDetailsDB.PetitionFileID = petitionFileID;
                ////TBD
                //caseDetailsDB.PetitionCategoryID = 1;
                ////TBD
                //caseDetailsDB.TenantUserID = 1;
                ////caseDetailsDB.TenantUserID = caseInfo.TenantUserID;
                //caseDetailsDB.bThirdPartyRepresentation = caseInfo.bThirdPartyRepresentation;
                //caseDetailsDB.ThirdPartyUserID = thirdPartyUSerID;
                //caseDetailsDB.OwnerUserID = ownerUserID;
                //caseDetailsDB.bAgreeToCityMediation = caseInfo.bAgreeToCityMediation;
                ////TBD
                //caseDetailsDB.CaseFiledBy = 1;
                //caseDetailsDB.bCaseFiledByThirdParty = caseInfo.bCaseFiledByThirdParty;
                ////TBD
                //caseDetailsDB.CaseAssignedTo = "12345";
                ////TBD
                //caseDetailsDB.CityUserFirstName = "City";
                ////TBD
                //caseDetailsDB.CityUserLastName = "Admin";
                ////TBD
                //caseDetailsDB.CityUserMailID = "testcity@gmail.com";
                ////TBD
                //caseDetailsDB.WorlFlowID = 1;
                //caseDetailsDB.CreatedDate = DateTime.Now;


                //_dbContext.CaseDetails.InsertOnSubmit(caseDetailsDB);
                //_dbContext.SubmitChanges();
                //caseInfo.CaseID = caseDetailsDB.CaseID;

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

        private int SaveUserInfo(UserInfoM userInfo)
        {
            int userID = 0;
            using (CommonDataContext db = new CommonDataContext())
            {
                var user = db.UserInfos.Where(x => (x.FirstName == userInfo.FirstName && x.LastName == userInfo.LastName && x.AddressLine1 == userInfo.AddressLine1 && x.AddressLine2 == userInfo.AddressLine2 && x.City == userInfo.City && x.State == userInfo.State && x.Zip == userInfo.Zip)).FirstOrDefault();

                if (user != null)
                {
                    userID = user.UserID;
                }
                else
                {
                    UserInfo userInfoDB = new UserInfo();
                    userInfoDB.FirstName = userInfo.FirstName;
                    userInfoDB.LastName = userInfo.LastName;
                    userInfoDB.AddressLine1 = userInfo.AddressLine1;
                    userInfoDB.AddressLine2 = userInfo.AddressLine2;
                    userInfoDB.City = userInfo.City;
                    userInfoDB.State = userInfo.State;
                    userInfoDB.Zip = userInfo.Zip;
                    userInfoDB.PhoneNumber = userInfo.PhoneNumber;
                    userInfoDB.ContactEmail = userInfo.Email;

                    db.UserInfos.InsertOnSubmit(userInfoDB);
                    db.SubmitChanges();
                    userID = userInfoDB.UserID;
                }
            }
            return userID;
        }

        private int SaveTenantPetition(TenantPetitionInfoM petition)
        {
            int petitionID = 0;
            int tenantPetitionID = SaveTenantPetitionInfo(petition);
            if (tenantPetitionID != 0)
            {
                petition.PetitionID = tenantPetitionID;
                SaveTenantRentalIncrementInfo(petition);
                SaveTenantLostServiceInfo(petition);
                SaveTenantProblemInfo(petition);
                SavePetitionGroundInfo(petition);
                petitionID = GetPetitionFileID(tenantPetitionID, 1);
            }
            return petitionID;
        }

        private int SaveTenantPetitionInfo(TenantPetitionInfoM petition)
        {
            int petitionID = 0;
            
            TenantPetitionInfo petitionDB = new TenantPetitionInfo();
                petitionDB.NumberOfUnits = petition.NumberOfUnits;
                petitionDB.UnitTypeID = petition.UnitTypeId;
                petitionDB.RentStatusID = petition.CurrentRentStatusID;
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

        public ReturnResult<bool> SaveTenantRentalIncrementInfo(TenantPetitionInfoM petition)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                foreach (var item in petition.RentIncreases)
                {
                    TenantRentalIncrementInfo rentIncrementDB = new TenantRentalIncrementInfo();
                    rentIncrementDB.TenantPetitionID = petition.PetitionID;
                    rentIncrementDB.bRentIncreaseNoticeGiven = item.bRentIncreaseNoticeGiven;
                    if (item.bRentIncreaseNoticeGiven)
                    {
                        //  rentIncrementDB.RentIncreaseNoticeDate = item.RentIncreaseNoticeDate;
                        rentIncrementDB.RentIncreaseNoticeDate = DateTime.Now;

                    }
                    //TBD
                    rentIncrementDB.RentIncreaseEffectiveDate = DateTime.Now;
                    // rentIncrementDB.RentIncreaseEffectiveDate = item.RentIncreaseEffectiveDate;
                    rentIncrementDB.RentIncreasedFrom = item.RentIncreasedFrom;
                    rentIncrementDB.RentIncreasedTo = item.RentIncreasedTo;
                    rentIncrementDB.bRentIncreaseContested = item.bRentIncreaseContested;

                    _dbContext.TenantRentalIncrementInfos.InsertOnSubmit(rentIncrementDB);
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

        public ReturnResult<bool> SaveTenantLostServiceInfo(TenantPetitionInfoM petition)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                if (petition.bLostService)
                {
                    foreach (var item in petition.LostServices)
                    {
                        TenantLostServiceInfo lostServiceDB = new TenantLostServiceInfo();
                        lostServiceDB.TenantPetitionID = petition.PetitionID;
                        lostServiceDB.ReducedServiceDescription = item.ReducedServiceDescription;
                        lostServiceDB.EstimatedLoss = item.EstimatedLoss;
                        //TBD
                        lostServiceDB.LossBeganDate = DateTime.Now;
                        //TBD
                        // lostServiceDB.LossBeganDate = item.LossBeganDate;
                        lostServiceDB.PayingToServiceBeganDate = DateTime.Now;
                        // lostServiceDB.PayingToServiceBeganDate = item.PayingToServiceBeganDate;

                        _dbContext.TenantLostServiceInfos.InsertOnSubmit(lostServiceDB);
                        _dbContext.SubmitChanges();
                    }

                }
                if (petition.bProblem)
                {
                    SaveTenantProblemInfo(petition);
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

        private void SaveTenantProblemInfo(TenantPetitionInfoM petition)
        {
            if (petition.bProblem)
            {
              
                    foreach (var item in petition.Problems)
                    {
                        TenantProblemInfo problemDB = new TenantProblemInfo();
                        problemDB.TenantPetitionID = petition.PetitionID;
                        problemDB.ProblemDescription = item.ProblemDescription;
                        problemDB.EstimatedLoss = item.EstimatedLoss;
                        //TBD
                        //  problemDB.ProblemBeganDate = item.ProblemBeganDate;
                        problemDB.ProblemBeganDate = DateTime.Now;
                        //TBD
                        problemDB.PayingToProblemBeganDate = DateTime.Now;
                        //  problemDB.PayingToProblemBeganDate = item.PayingToProblemBeganDate;

                        _dbContext.TenantProblemInfos.InsertOnSubmit(problemDB);
                        _dbContext.SubmitChanges();
                    }
                
            }
        }

        public ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
             try
            {
                petition.PetitionGrounds[0].Selected = true; //TBD
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

        public ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo)
        {
            int thirdPartyUserID = 0;
            int opposingPartyUserID = 0;
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                if (caseInfo.TenantAppealInfo.bThirdPartyRepresentation)
                {
                    thirdPartyUserID = SaveUserInfo(caseInfo.TenantAppealInfo.AppealThirdPartyInfo);
                    if (thirdPartyUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    caseInfo.TenantAppealInfo.thirdPartyUserID = thirdPartyUserID;

                }
                foreach (var item in caseInfo.TenantAppealInfo.AppealOpposingPartyInfo)
                {
                    opposingPartyUserID = SaveUserInfo(item);
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
                    caseInfo.TenantAppealInfo.AppealID = appealDB.TenantAppealID;


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
        public ReturnResult<Boolean> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo)
        {
            ReturnResult<Boolean> result = new ReturnResult<Boolean>();
            try
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
        public ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(CaseInfoM caseInfo)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {

                TenantAppealDetail appealDB = _dbContext.TenantAppealDetails.First(i => i.TenantAppealID == caseInfo.TenantAppealInfo.AppealID);
                caseInfo.TenantAppealInfo.OpposingPartyCommunicateDate = DateTime.Now;
                appealDB.OpposingPartyCommunicateDate = DateTime.Now;
                // appealDB.AppealFiledBy = DateTime.Now                   
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
        #endregion
        #region "Add"
        public ReturnResult<bool> AddAnotherOpposingParty(CaseInfoM caseInfo)
        {
            int opposingPartyUserID = 0;
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                foreach (var item in caseInfo.TenantAppealInfo.AppealOpposingPartyInfo)
                {
                    opposingPartyUserID = SaveUserInfo(item);
                    if (opposingPartyUserID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                    caseInfo.TenantAppealInfo.opposingPartyUserID.Add(opposingPartyUserID);
                }

                AppealOpposingParty appealOpposingDB = new AppealOpposingParty();
                appealOpposingDB.AppealID = caseInfo.TenantAppealInfo.AppealID;
                appealOpposingDB.OpposingPartyID = opposingPartyUserID;
                appealOpposingDB.CreatedDate = DateTime.Now;
                appealOpposingDB.IsDeleted = false;
                appealOpposingDB.ModifiedDate = DateTime.Now;

                _dbContext.AppealOpposingParties.InsertOnSubmit(appealOpposingDB);
                _dbContext.SubmitChanges();

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
        
        #region Owner petition

        public ReturnResult<bool> SaveOwnerPetitionInfo(OwnerPetitionInfoM model)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                if (model.OwnerPetitionID != 0)
                {
                    var petitionInfo = from r in _dbContext.OwnerPetitionInfos
                                       where r.OwnerPetitionID == model.OwnerPetitionID
                                       select r;
                    if (petitionInfo.Any())
                    {
                        petitionInfo.First().ApplicantUserID = model.ApplicantUserID;
                        petitionInfo.First().bThirdPartyRepresentation = model.bThirdPartyRepresentation;
                        petitionInfo.First().ThirdPartyUserID = model.ThirdPartyUserID;
                        petitionInfo.First().BuildingAcquiredDate = model.BuildingAcquiredDate;
                        petitionInfo.First().NumberOfUnits = model.NumberOfUnits;
                        petitionInfo.First().BusinessLicenseNumber = model.BusinessLicenseNumber;
                        petitionInfo.First().bDebitServiceCosts = model.bDebitServiceCosts;
                        petitionInfo.First().bIncreasedHousingCost = model.bIncreasedHousingCost;
                        petitionInfo.First().bPetitionFiledByThirdParty = model.bPetitionFiledByThirdParty;
                        petitionInfo.First().bAgreeToCityMediation = model.bAgreeToCityMediation;
                        petitionInfo.First().LanguageID = model.LanguageID;
                        petitionInfo.First().LastModifiedDate = DateTime.Now;

                    }
                }
                else
                {
                    OwnerPetitionInfo petitionInfo = new OwnerPetitionInfo();
                    petitionInfo.ApplicantUserID = model.ApplicantUserID;
                    petitionInfo.bThirdPartyRepresentation = model.bThirdPartyRepresentation;
                    petitionInfo.ThirdPartyUserID = model.ThirdPartyUserID;
                    petitionInfo.BuildingAcquiredDate = model.BuildingAcquiredDate;
                    petitionInfo.NumberOfUnits = model.NumberOfUnits;
                    petitionInfo.BusinessLicenseNumber = model.BusinessLicenseNumber;
                    petitionInfo.bDebitServiceCosts = model.bDebitServiceCosts;
                    petitionInfo.bIncreasedHousingCost = model.bIncreasedHousingCost;
                    petitionInfo.bPetitionFiledByThirdParty = model.bPetitionFiledByThirdParty;
                    petitionInfo.bAgreeToCityMediation = model.bAgreeToCityMediation;
                    petitionInfo.LanguageID = model.LanguageID;
                    petitionInfo.PetitionFiledBy = model.PetitionFiledBy;
                    petitionInfo.CreatedDate = DateTime.Now;
                    _dbContext.OwnerPetitionInfos.InsertOnSubmit(petitionInfo);
                    _dbContext.SubmitChanges();
                }
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

        public ReturnResult<bool> SaveOwnerPropertyInfo(List<OwnerPropertInfoM> models)
        {           
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                foreach (var model in models)
                {
                    if (model.OwnerPropertyID != 0)
                    {
                        var propertyInfo = from r in _dbContext.OwnerPropertyInfos
                                           where r.OwnerPropertyID == model.OwnerPropertyID
                                           select r;
                        if (propertyInfo.Any())
                        {
                            propertyInfo.First().UnitTypeID = model.UnitTypeID;
                            propertyInfo.First().TenantUserID = model.TenantUserID;
                            propertyInfo.First().MovedInDate = model.MovedInDate;
                            propertyInfo.First().InitialRent = model.InitialRent;
                            propertyInfo.First().bRAPNoticeGiven = model.bRAPNoticeGiven;
                            propertyInfo.First().RAPNoticeGivenDate = model.RAPNoticeGivenDate;
                            propertyInfo.First().RentStatusID = model.RentStatusID;
                            propertyInfo.First().bBanking = model.bBanking;
                            propertyInfo.First().bDebitServiceCosts = model.bDebitServiceCosts;
                            propertyInfo.First().bIncreasedHousingCost = model.bIncreasedHousingCost;
                            _dbContext.SubmitChanges();
                          }
                        
                    }
                    else
                    {

                        OwnerPropertyInfo propertyInfo = new OwnerPropertyInfo();
                        propertyInfo.OwnerPetitionID = model.OwnerPetitionID;
                        propertyInfo.UnitTypeID = model.UnitTypeID;
                        propertyInfo.TenantUserID = model.TenantUserID;
                        propertyInfo.MovedInDate = model.MovedInDate;
                        propertyInfo.InitialRent = model.InitialRent;
                        propertyInfo.bRAPNoticeGiven = model.bRAPNoticeGiven;
                        propertyInfo.RAPNoticeGivenDate = model.RAPNoticeGivenDate;
                        propertyInfo.RentStatusID = model.RentStatusID;
                        propertyInfo.bBanking = model.bBanking;
                        propertyInfo.bDebitServiceCosts = model.bDebitServiceCosts;
                        propertyInfo.bIncreasedHousingCost = model.bIncreasedHousingCost;
                        _dbContext.OwnerPropertyInfos.InsertOnSubmit(propertyInfo);
                        _dbContext.SubmitChanges();
                    }                   
                }
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                result.result = true;
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<bool> SaveOwnerRentIncreaseInfo(List<OwnerRentIncreaseInfoM> models)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                foreach (var model in models)
                {
                    if (model.RentalIncreaseInfoID != 0)
                    {
                        var rentIncreaseInfo = from r in _dbContext.OwnerRentalIncrementInfos
                                               where r.RentalIncreaseInfoID == model.RentalIncreaseInfoID
                                               select r;
                        if(rentIncreaseInfo.Any())
                        {
                            rentIncreaseInfo.First().bRentIncreaseNoticeGiven = model.bRentIncreaseNoticeGiven;
                            rentIncreaseInfo.First().RentIncreaseNoticeDate = model.RentIncreaseNoticeDate;
                            rentIncreaseInfo.First().RentIncreaseEffectiveDate = model.RentIncreaseEffectiveDate;
                            rentIncreaseInfo.First().RentIncreasedFrom = model.RentIncreasedFrom;
                            rentIncreaseInfo.First().RentIncreasedTo = model.RentIncreasedTo;
                            _dbContext.SubmitChanges();
                        }
                    }
                    else
                    {
                        OwnerRentalIncrementInfo rentIncreaseInfo = new OwnerRentalIncrementInfo();
                        rentIncreaseInfo.OwnerPropertyID = model.OwnerPropertyID;
                        rentIncreaseInfo.bRentIncreaseNoticeGiven = model.bRentIncreaseNoticeGiven;
                        rentIncreaseInfo.RentIncreaseNoticeDate = model.RentIncreaseNoticeDate;
                        rentIncreaseInfo.RentIncreaseEffectiveDate = model.RentIncreaseEffectiveDate;
                        rentIncreaseInfo.RentIncreasedFrom = model.RentIncreasedFrom;
                        rentIncreaseInfo.RentIncreasedTo = model.RentIncreasedTo;
                        _dbContext.OwnerRentalIncrementInfos.InsertOnSubmit(rentIncreaseInfo);
                        _dbContext.SubmitChanges();
                    }
                }            
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                result.result = true;
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
    }
}
