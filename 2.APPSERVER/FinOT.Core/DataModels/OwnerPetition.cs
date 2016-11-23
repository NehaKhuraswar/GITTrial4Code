using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{   
        public class OwnerPetitionInfoM
        {
            private List<OwnerPropertInfoM> _ownerPropertInfo = new List<OwnerPropertInfoM>();
            public int OwnerPetitionID { get; set; }
            public int ApplicantUserID { get; set; }
            public bool bThirdPartyRepresentation { get; set; }
            public int ThirdPartyUserID { get; set; }
            public DateTime BuildingAcquiredDate { get; set; }
            public int NumberOfUnits { get; set; }
            public string BusinessLicenseNumber { get; set; }
            public bool bDebitServiceCosts { get; set; }
            public bool bIncreasedHousingCost { get; set; }
            public bool bPetitionFiledByThirdParty { get; set; }
            public bool bAgreeToCityMediation { get; set; }
            public int LanguageID { get; set; }
            public int PetitionFiledBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public string LastModifiedBy { get; set; }
            public DateTime LastModifiedDate { get; set; }
            public bool bIsPetitionSubmitted { get; set; }
            public List<OwnerPropertInfoM> OwnerPropertInfo
            {
                get
                {
                    return _ownerPropertInfo;
                }
                set
                {
                    _ownerPropertInfo = value;
                }
            }
        }
       public class OwnerPropertInfoM
        {
            private List<OwnerRentIncreaseInfoM> _rentIncrease = new List<OwnerRentIncreaseInfoM>();
            public int OwnerPropertyID { get; set; }
            public int OwnerPetitionID { get; set; }
            public int UnitTypeID { get; set; }
            public int TenantUserID { get; set; }
            public DateTime MovedInDate { get; set; }
            public decimal InitialRent { get; set; }
            public bool bRAPNoticeGiven { get; set; }
            public DateTime RAPNoticeGivenDate { get; set; }
            public int RentStatusID { get; set; }
            public bool bBanking { get; set; }
            public bool bDebitServiceCosts { get; set; }
            public bool bIncreasedHousingCost { get; set; }
            public List<OwnerRentIncreaseInfoM> RentIncrease
            {
                get
                {
                    return _rentIncrease;
                }
                set
                {
                    _rentIncrease = value;
                }
            }

          }
       public class OwnerRentIncreaseInfoM
       {
           public int RentalIncreaseInfoID { get; set; }
           public int OwnerPropertyID { get; set; }
           public bool bRentIncreaseNoticeGiven { get; set; }
           public DateTime RentIncreaseNoticeDate { get; set; }
           public decimal RentIncreasedFrom { get; set; }
           public decimal RentIncreasedTo { get; set; }
           public DateTime RentIncreaseEffectiveDate { get; set; }
       }

    }

