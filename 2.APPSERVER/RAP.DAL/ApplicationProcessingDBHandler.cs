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
    public class ApplicationProcessingDBHandler
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
       public ReturnResult<TenantPetitionInfoM> GetTenantPetitionInfo()
       {
           ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
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

                   result.result.UnitTypes = _units;
                   result.result.CurrentOnRent = _rentStatusItems;
                   result.result.PetitionGrounds = _petitionGrounds;

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

       private int SaveUserInfo(UserInfoM userInfo)
       {
           int userID;
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
           int petitionID;
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

    }
}
