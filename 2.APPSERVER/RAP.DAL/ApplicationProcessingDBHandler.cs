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
       public ApplicationProcessingDBHandler()
        {
            _connString =  ConfigurationManager.AppSettings["RAPDBConnectionString"];
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
           List<UnitTypeM> _units = new List<UnitTypeM>();
           List<CurrentOnRentM> _rentStatusItems = new List<CurrentOnRentM>();
           List<PetitionGroundM> _petitionGrounds = new List<PetitionGroundM>();
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

                   if (_petitionGrounds.Count == 0)
                   {
                       result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                       return result;
                   }

                   _petition.UnitTypes = _units;
                   _petition.CurrentOnRent = _rentStatusItems;
                   _petition.PetitionGrounds = _petitionGrounds;
                   result.result.TenantPetitionInfo = _petition;
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
                   caseDetailsDB.TenantUserID = caseInfo.TenantUserID;
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
           using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
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
                   userInfoDB.email = userInfo.Email;

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
               petitionDB.MoveInDate = petition.MoveInDate;
               petitionDB.InitialRent = petition.InitalRent;
               petitionDB.bRAPNoticeGiven = petition.bRAPNoticeGiven;
               petitionDB.RAPNoticeGivnDate = petition.RAPNoticeGivenDate;
               petitionDB.bRentControlledByAgency = petition.bRentControlledByAgency;
               petitionDB.bPetitionFiledPrviously = petition.bPetitionFiledPrviously;
               petitionDB.PreviousCaseIDs = petition.PreviousCaseIDs;
               petitionDB.bLostService = petition.bLostService;
               petitionDB.bSeriousProblem = petitionDB.bSeriousProblem;

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
                       rentIncrementDB.RentIncreaseNoticeDate = item.RentIncreaseNoticeDate;
                   }
                   rentIncrementDB.RentIncreaseEffectiveDate = item.RentIncreaseEffectiveDate;
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
                       lostServiceDB.LossBeganDate = item.LossBeganDate;
                       lostServiceDB.PayingToServiceBeganDate = item.PayingToServiceBeganDate;

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
                       problemDB.ProblemBeganDate = item.ProblemBeganDate;
                       problemDB.PayingToProblemBeganDate = item.PayingToProblemBeganDate;

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
               foreach(var item in petition.PetitionGrounds)
               {
                   if(item.Selected)
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
