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
        CommonDBHandler commondbHandler = new CommonDBHandler();
        public ApplicationProcessingDBHandler()
        {
            _connString = ConfigurationManager.AppSettings["RAPDBConnectionString"];
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
            try
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    var units = db.UnitTypes;
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

                    var rentStausItems = db.CurrentOnRentStatus;
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

                    var petitionGrounds = db.PetitionGrounds;
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
                    var appealGrounds = db.AppealGrounds;
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
                }
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
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
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    var caseDetails = db.CaseDetails.Where(x => x.CaseID == caseID).FirstOrDefault();
                    if (caseDetails != null)
                    {
                        petitionFileID = caseDetails.PetitionFileID;
                        ownerUserID = caseDetails.OwnerUserID;
                        thirdPartyUSerID = caseDetails.ThirdPartyUserID;
                        appealID = (caseDetails.TenantAppealID == null) ? 0 : Convert.ToInt32(caseDetails.TenantAppealID);
                        caseInfo.bThirdPartyRepresentation = (bool)caseDetails.bThirdPartyRepresentation;
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
                        caseInfo.TenantAppealInfo.AppealGrounds = GetAppealGroundInfo(appealID).result;
                        //if (petitionFileID == 0)
                        //{
                        //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        //    return result;
                        //}
                        ReturnResult<UserInfoM> resultOwnerInfo = new ReturnResult<UserInfoM>();
                        resultOwnerInfo = commondbHandler.GetUserInfo(ownerUserID);
                        caseInfo.OwnerInfo = resultOwnerInfo.result;
                        //if (ownerUserID == 0)
                        //{
                        //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        //    return result;
                        //}
                        if (caseInfo.bThirdPartyRepresentation)
                        {
                            ReturnResult<UserInfoM> resultThirdPartyInfo = new ReturnResult<UserInfoM>();
                            resultThirdPartyInfo = commondbHandler.GetUserInfo(thirdPartyUSerID);

                            caseInfo.ThirdPartyInfo = resultThirdPartyInfo.result;
                            //if (thirdPartyUSerID == 0)
                            //{
                            //    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                            //    return result;
                            //}
                        }

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
        private ReturnResult<TenantPetitionInfoM> GetTenantPetition(int PetitionID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                result = GetTenantPetitionInfo(PetitionID);
                if (result != null)
                {

                    result.result.RentIncreases = GetTenantRentalIncrementInfo(PetitionID).result;
                    result.result.LostServices = GetTenantLostServiceInfo(PetitionID).result;
                    result.result.Problems = GetTenantProblemInfo(PetitionID).result;
                    result.result.PetitionGrounds = GetPetitionGroundInfo(PetitionID).result;
                    // TBD
                    //   petitionID = GetPetitionFileID(tenantPetitionID, 1);
                }
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        private ReturnResult<TenantPetitionInfoM> GetTenantPetitionInfo(int PetitionID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            TenantPetitionInfoM tenantPetitionInfo = new TenantPetitionInfoM();
            try
            {
                if (PetitionID != 0)
                {
                    using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                    {
                        var tenantPetitionInfoDB = db.TenantPetitionInfos.Where(x => x.TenantPetitionID == PetitionID).FirstOrDefault();
                        //TenantPetitionInfo petitionDB = new TenantPetitionInfo();
                        tenantPetitionInfo.NumberOfUnits = (int)tenantPetitionInfoDB.NumberOfUnits;
                        tenantPetitionInfo.UnitTypeId = tenantPetitionInfoDB.UnitTypeID;
                        tenantPetitionInfo.CurrentRentStatusID = tenantPetitionInfoDB.RentStatusID;
                        tenantPetitionInfo.LegalWithHoldingExplanation = tenantPetitionInfoDB.LegalWithHoldingExplanation;
                        tenantPetitionInfo.bCitationDocUnavailable = Convert.ToBoolean(tenantPetitionInfoDB.bCitationDocUnavailable);
                        //To be removed
                        tenantPetitionInfo.MoveInDate = tenantPetitionInfoDB.MoveInDate;
                        tenantPetitionInfo.InitialRent = tenantPetitionInfoDB.InitialRent;
                        tenantPetitionInfo.bRAPNoticeGiven = Convert.ToBoolean(tenantPetitionInfoDB.bRAPNoticeGiven);
                        // To be removed
                        tenantPetitionInfo.RAPNoticeGivenDate = Convert.ToDateTime(tenantPetitionInfoDB.RAPNoticeGivnDate);
                        tenantPetitionInfo.bRentControlledByAgency = Convert.ToBoolean(tenantPetitionInfoDB.bRentControlledByAgency);
                        tenantPetitionInfo.bPetitionFiledPrviously = Convert.ToBoolean(tenantPetitionInfoDB.bPetitionFiledPrviously);
                        tenantPetitionInfo.PreviousCaseIDs = tenantPetitionInfoDB.PreviousCaseIDs;
                        tenantPetitionInfo.bLostService = Convert.ToBoolean(tenantPetitionInfoDB.bLostService);
                        tenantPetitionInfo.bProblem = Convert.ToBoolean(tenantPetitionInfoDB.bSeriousProblem);

                        result.result = tenantPetitionInfo;
                        result.status = new OperationStatus() { Status = StatusEnum.Success };

                    }
                }
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
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    var TenantRentalIncrementInfoDB = db.TenantRentalIncrementInfos.Where(x => x.TenantPetitionID == PetitionId).ToList();
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
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    var TenantLostServiceInfoDB = db.TenantLostServiceInfos.Where(x => x.TenantPetitionID == PetitionID).ToList();
                    foreach (var item in TenantLostServiceInfoDB)
                    {
                        TenantLostServiceInfoM objTenantLostServiceInfoM = new TenantLostServiceInfoM();

                        objTenantLostServiceInfoM.ReducedServiceDescription = item.ReducedServiceDescription;
                        objTenantLostServiceInfoM.EstimatedLoss = item.EstimatedLoss;
                        objTenantLostServiceInfoM.LossBeganDate = item.LossBeganDate;
                        objTenantLostServiceInfoM.PayingToServiceBeganDate = item.PayingToServiceBeganDate;

                        tenantLostServiceInfo.Add(objTenantLostServiceInfoM);
                    }
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
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    var TenantProblemInfoDB = db.TenantProblemInfos.Where(x => x.TenantPetitionID == PetitionID).ToList();
                    foreach (var item in TenantProblemInfoDB)
                    {
                        TenantProblemInfoM objTenantProblemInfoM = new TenantProblemInfoM();
                        objTenantProblemInfoM.ProblemDescription = item.ProblemDescription;
                        objTenantProblemInfoM.EstimatedLoss = item.EstimatedLoss;
                        objTenantProblemInfoM.ProblemBeganDate = item.ProblemBeganDate;

                        tenantProblemInfo.Add(objTenantProblemInfoM);
                    }
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

        private ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int PetitionID)
        {
            ReturnResult<List<PetitionGroundM>> result = new ReturnResult<List<PetitionGroundM>>();
            List<PetitionGroundM> PetitionGroundInfo = new List<PetitionGroundM>();
            try
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    var petitionGrounds = db.PetitionGrounds;
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
                    var TenantPetitionGroundInfoDB = db.TenantPetitionGroundInfos.Where(x => x.TenantPetitionID == PetitionID).ToList();
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
        private ReturnResult<TenantAppealInfoM> GetAppealInfo(int appealID)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            result.result.AppealGrounds = GetAppealGroundInfo(appealID).result;

            return result;

        }
        private ReturnResult<List<AppealGroundM>> GetAppealGroundInfo(int appealID)
        {
            ReturnResult<List<AppealGroundM>> result = new ReturnResult<List<AppealGroundM>>();
            List<AppealGroundM> AppealGroundInfo = new List<AppealGroundM>();
            try
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    var appealGrounds = db.AppealGrounds;
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
                    var TenantAppealGroundInfoDB = db.TenantAppealGroundInfos.Where(x => x.TenantAppealGroudID == appealID).ToList();
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
        /// 

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

                ownerUserID = SaveUserInfo(caseInfo.OwnerInfo);
                if (ownerUserID == 0)
                {
                    result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                    return result;
                }
                if (caseInfo.bThirdPartyRepresentation)
                {
                    thirdPartyUSerID = SaveUserInfo(caseInfo.ThirdPartyInfo);
                    if (thirdPartyUSerID == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
                }

                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    CaseDetail caseDetailsDB = new CaseDetail();
                    caseDetailsDB.PetitionFileID = petitionFileID;
                    //TBD
                    caseDetailsDB.PetitionCategoryID = 1;
                    //TBD
                    caseDetailsDB.TenantUserID = 1;
                    //caseDetailsDB.TenantUserID = caseInfo.TenantUserID;
                    caseDetailsDB.bThirdPartyRepresentation = caseInfo.bThirdPartyRepresentation;
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


                    db.CaseDetails.InsertOnSubmit(caseDetailsDB);
                    db.SubmitChanges();
                    caseInfo.CaseID = caseDetailsDB.CaseID;
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
            using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
            {
                TenantPetitionInfo petitionDB = new TenantPetitionInfo();
                petitionDB.NumberOfUnits = petition.NumberOfUnits;
                petitionDB.UnitTypeID = petition.UnitTypeId;
                petitionDB.RentStatusID = petition.CurrentRentStatusID;
                petitionDB.LegalWithHoldingExplanation = petition.LegalWithHoldingExplanation;
                petitionDB.bCitationDocUnavailable = petition.bCitationDocUnavailable;
                //To be removed
                petitionDB.MoveInDate = DateTime.Now;
                // petitionDB.MoveInDate = petition.MoveInDate;
                petitionDB.InitialRent = petition.InitialRent;
                petitionDB.bRAPNoticeGiven = petition.bRAPNoticeGiven;
                // To be removed
                petitionDB.RAPNoticeGivnDate = DateTime.Now;
                //  petitionDB.RAPNoticeGivnDate = petition.RAPNoticeGivenDate;
                petitionDB.bRentControlledByAgency = petition.bRentControlledByAgency;
                petitionDB.bPetitionFiledPrviously = petition.bPetitionFiledPrviously;
                petitionDB.PreviousCaseIDs = petition.PreviousCaseIDs;
                petitionDB.bLostService = petition.bLostService;
                petitionDB.bSeriousProblem = petition.bProblem;

                db.TenantPetitionInfos.InsertOnSubmit(petitionDB);
                db.SubmitChanges();
                petitionID = petitionDB.TenantPetitionID;
            }
            return petitionID;
        }


        private void SaveTenantRentalIncrementInfo(TenantPetitionInfoM petition)
        {
            using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
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

                    db.TenantRentalIncrementInfos.InsertOnSubmit(rentIncrementDB);
                    db.SubmitChanges();
                }
            }

        }

        private void SaveTenantLostServiceInfo(TenantPetitionInfoM petition)
        {
            if (petition.bLostService)
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
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

                        db.TenantLostServiceInfos.InsertOnSubmit(lostServiceDB);
                        db.SubmitChanges();
                    }
                }
            }
        }

        private void SaveTenantProblemInfo(TenantPetitionInfoM petition)
        {
            if (petition.bProblem)
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
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

                        db.TenantProblemInfos.InsertOnSubmit(problemDB);
                        db.SubmitChanges();
                    }
                }
            }
        }

        private void SavePetitionGroundInfo(TenantPetitionInfoM petition)
        {
            using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
            {
                foreach (var item in petition.PetitionGrounds)
                {
                    if (item.Selected)
                    {
                        TenantPetitionGroundInfo petitionGroundsDB = new TenantPetitionGroundInfo();
                        petitionGroundsDB.TenantPetitionID = petition.PetitionID;
                        petitionGroundsDB.PetitionGroundID = item.PetitionGroundID;

                        db.TenantPetitionGroundInfos.InsertOnSubmit(petitionGroundsDB);
                        db.SubmitChanges();
                    }
                }
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
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    TenantAppealDetail appealDB = new TenantAppealDetail();
                    appealDB.bThirdPartyRepresentation = caseInfo.TenantAppealInfo.bThirdPartyRepresentation;
                    appealDB.ThirdPartyUserID = caseInfo.TenantAppealInfo.thirdPartyUserID;
                    appealDB.CreatedDate = DateTime.Now;

                    db.TenantAppealDetails.InsertOnSubmit(appealDB);
                    db.SubmitChanges();
                    caseInfo.TenantAppealInfo.AppealID = appealDB.TenantAppealID;


                    CaseDetail caseDB = db.CaseDetails.First(i => i.CaseID == caseInfo.CaseID);
                    caseDB.TenantAppealID = caseInfo.TenantAppealInfo.AppealID;
                    caseDB.LastModifiedDate = DateTime.Now;
                    db.SubmitChanges();
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
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    AppealOpposingParty appealOpposingDB = new AppealOpposingParty();
                    appealOpposingDB.AppealID = caseInfo.TenantAppealInfo.AppealID;
                    appealOpposingDB.OpposingPartyID = opposingPartyUserID;
                    appealOpposingDB.CreatedDate = DateTime.Now;
                    appealOpposingDB.IsDeleted = false;
                    appealOpposingDB.ModifiedDate = DateTime.Now;

                    db.AppealOpposingParties.InsertOnSubmit(appealOpposingDB);
                    db.SubmitChanges();
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

                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                    TenantAppealDetail appealDB = db.TenantAppealDetails.First(i => i.TenantAppealID == caseInfo.TenantAppealInfo.AppealID);
                    caseInfo.TenantAppealInfo.OpposingPartyCommunicateDate = DateTime.Now;
                    appealDB.OpposingPartyCommunicateDate = DateTime.Now;
                   // appealDB.AppealFiledBy = DateTime.Now                   
                    db.SubmitChanges();
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
        public ReturnResult<Boolean> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo)
        {
            ReturnResult<Boolean> result = new ReturnResult<Boolean>();
            try
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
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

                            db.TenantAppealGroundInfos.InsertOnSubmit(TenantAppealGroundInfoDB);
                            db.SubmitChanges();
                        }
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

        private int GetPetitionFileID(int petitionID, int petitionCategory)
        {
            int petitionFileID = 0;
            using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
            {
                PetitionDetail petitionDetailsDB = new PetitionDetail();

                if (petitionCategory == 1)
                {
                    petitionDetailsDB.TenantPetitionID = petitionID;
                    db.PetitionDetails.InsertOnSubmit(petitionDetailsDB);
                    db.SubmitChanges();
                    petitionFileID = petitionDetailsDB.PetitionFileID;
                }

            }
            return petitionFileID;
        }

    }
}
