using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
    //public class OwnerPetitionInfoM
    //{
    //    private List<OwnerPropertInfoM> _ownerPropertInfo = new List<OwnerPropertInfoM>();
    //    public int OwnerPetitionID { get; set; }
    //    public int ApplicantUserID { get; set; }
    //    public bool bThirdPartyRepresentation { get; set; }
    //    public int ThirdPartyUserID { get; set; }
    //    public DateTime BuildingAcquiredDate { get; set; }
    //    public int NumberOfUnits { get; set; }
    //    public string BusinessLicenseNumber { get; set; }
    //    public bool bDebitServiceCosts { get; set; }
    //    public bool bIncreasedHousingCost { get; set; }
    //    public bool bPetitionFiledByThirdParty { get; set; }
    //    public bool bAgreeToCityMediation { get; set; }
    //    public int LanguageID { get; set; }
    //    public int PetitionFiledBy { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public string LastModifiedBy { get; set; }
    //    public DateTime LastModifiedDate { get; set; }
    //    public bool bIsPetitionSubmitted { get; set; }
    //    public List<OwnerPropertInfoM> OwnerPropertInfo
    //    {
    //        get
    //        {
    //            return _ownerPropertInfo;
    //        }
    //        set
    //        {
    //            _ownerPropertInfo = value;
    //        }
    //    }
    //}
    //public class OwnerPropertInfoM
    //{
    //    private List<OwnerRentIncreaseInfoM> _rentIncrease = new List<OwnerRentIncreaseInfoM>();
    //    public int OwnerPropertyID { get; set; }
    //    public int OwnerPetitionID { get; set; }
    //    public int UnitTypeID { get; set; }
    //    public int TenantUserID { get; set; }
    //    public DateTime MovedInDate { get; set; }
    //    public decimal InitialRent { get; set; }
    //    public bool bRAPNoticeGiven { get; set; }
    //    public DateTime RAPNoticeGivenDate { get; set; }
    //    public int RentStatusID { get; set; }
    //    public bool bBanking { get; set; }
    //    public bool bDebitServiceCosts { get; set; }
    //    public bool bIncreasedHousingCost { get; set; }
    //    public List<OwnerRentIncreaseInfoM> RentIncrease
    //    {
    //        get
    //        {
    //            return _rentIncrease;
    //        }
    //        set
    //        {
    //            _rentIncrease = value;
    //        }
    //    }

    //}
    //public class OwnerRentIncreaseInfoM
    //{
    //    public int RentalIncreaseInfoID { get; set; }
    //    public int OwnerPropertyID { get; set; }
    //    public bool bRentIncreaseNoticeGiven { get; set; }
    //    public DateTime RentIncreaseNoticeDate { get; set; }
    //    public decimal RentIncreasedFrom { get; set; }
    //    public decimal RentIncreasedTo { get; set; }
    //    public DateTime RentIncreaseEffectiveDate { get; set; }
    //}

    #region New changes
  public class OwnerPetitionInfoM
  {
      public OwnerPetitionInfoM()
      {
          ApplicantInfo = new OwnerPetitionApplicantInfoM();
          PropertyInfo = new OwnerPetitionPropertyInfoM();
      }
      private List<OwnerRentIncreaseReasonsM> _rentIncreaseReasons = new List<OwnerRentIncreaseReasonsM>();
      public int OwnerPetitionID { get; set; }
      public OwnerPetitionApplicantInfoM ApplicantInfo { get; set; }
      public OwnerPetitionPropertyInfoM PropertyInfo { get; set; }
      public bool bPetitionFiledByThirdParty { get; set; }
      public bool bAgreeToCityMediation { get; set; }
      public int PetitionFiledBy { get; set; }
      public DateTime CreatedDate { get; set; }
      public string LastModifiedBy { get; set; }
      public DateTime LastModifiedDate { get; set; }
      public List<OwnerRentIncreaseReasonsM> RentIncreaseReasons
      {
          get
          {
              return _rentIncreaseReasons;
          }
          set
          {
              _rentIncreaseReasons = value;
          }
      }

  }
    
    public class OwnerPetitionApplicantInfoM
    {
        public OwnerPetitionApplicantInfoM()
        {
            ThirdPartyUser = new UserInfoM();
            ApplicantUserInfo = new UserInfoM();
        }
        public int OwnerPetitionApplicantInfoID { get; set; }
        public UserInfoM ApplicantUserInfo { get; set; }
        public bool bThirdPartyRepresentation { get; set; }
        public UserInfoM ThirdPartyUser { get; set; }
        public bool bBusinessLicensePaid { get; set; }
        public string BusinessLicenseNumber { get; set; }
        public bool bRentAdjustmentProgramFeePaid { get; set; }
        public DateTime? BuildingAcquiredDate { get; set; }
        public int NumberOfUnits { get; set; }
        public bool bMoreThanOneStreetOnParcel { get; set; }
        public int CustomerID { get; set; }
        public int bPetitionFiled { get; set; }
        public string RAPFee { get; set; }
    }

    public class OwnerPetitionPropertyInfoM
    {
        private List<OwnerPetitionTenantInfoM> _tenantInfo = new List<OwnerPetitionTenantInfoM>();
        private List<OwnerPetitionRentalIncrementInfoM> _rentalInfo = new List<OwnerPetitionRentalIncrementInfoM>();
        private List<UnitTypeM> _unitTypes = new List<UnitTypeM>();
        private List<CurrentOnRentM> _currentOnRent = new List<CurrentOnRentM>();
        public int OwnerPropertyID { get; set; }
        public int UnitTypeID { get; set; }
        public DateTime MovedInDate { get; set; }
        public decimal InitialRent { get; set; }
        public bool bRAPNoticeGiven { get; set; }
        public DateTime RAPNoticeGivenDate { get; set; }
        public int RentStatusID { get; set; }
        public int CustomerID { get; set; }
        public bool bPetitionFiled { get; set; }
        public List<OwnerPetitionTenantInfoM> TenantInfo
        {
            get
            {
                return _tenantInfo;
            }
            set
            {
                _tenantInfo = value;
            }
        }
        public List<OwnerPetitionRentalIncrementInfoM> RentalInfo
        {
            get
            {
                return _rentalInfo;
            }
            set
            {
                _rentalInfo = value;
            }
        }
        public List<UnitTypeM> UnitTypes
        {
            get
            {
                return _unitTypes;
            }
            set
            {
                _unitTypes = value;
            }
        }

        public List<CurrentOnRentM> CurrentOnRent
        {
            get
            {
                return _currentOnRent;
            }
            set
            {
                _currentOnRent = value;
            }
        }
     
    }

    public class OwnerPetitionTenantInfoM
    {
        public OwnerPetitionTenantInfoM()
        {
            TenantUserInfo = new UserInfoM();
        }
        public int TenantInfoID { get; set; }
        public int OwnerPropertyID { get; set; }
        public UserInfoM TenantUserInfo { get; set; }

    }

    public class OwnerRentIncreaseReasonsM
    {
        private bool _isSelected = false;
        public int ReasonID { get; set; }
        public string ReasonDescription { get; set; }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }
    }

    public class OwnerPetitionRentalIncrementInfoM
    {
        
        public int RentalIncreaseInfoID { get; set; }
        public int OwnerPropertyID { get; set; }
        public bool bRentIncreaseNoticeGiven { get; set; }
        public DateTime RentIncreaseNoticeDate { get; set; }
        public DateTime RentIncreaseEffectiveDate { get; set; }
        public decimal RentIncreasedFrom { get; set; }
        public decimal RentIncreasedTo { get; set; }
        
       
    }
    #endregion


}

