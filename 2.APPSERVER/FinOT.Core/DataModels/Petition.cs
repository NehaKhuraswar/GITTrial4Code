using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
    public class CaseInfoM
    {
        public CaseInfoM()
        {
            CityAnalyst = new CityUserAccount_M();
            HearingOfficer = new CityUserAccount_M();
            OwnerPetitionTenantInfo = new OwnerPetitionTenantInfoM();
            OwnerPetitionRentalIncrementInfo = new OwnerPetitionRentalIncrementInfoM();
        }
        private TenantPetitionInfoM _tenantPetitionInfo = new TenantPetitionInfoM();
        private TenantAppealInfoM _tenantappealInfo = new TenantAppealInfoM();  
        private List<AppealGroundM> _appealGrounds = new List<AppealGroundM>();
        private List<ActivityStatus_M> _activityStatus = new List<ActivityStatus_M>();
        private OwnerPetitionInfoM _ownerPetitionInfo = new OwnerPetitionInfoM();
        private List<PetitionCategoryM> _petitionCategory = new List<PetitionCategoryM>();
        private List<RAPNoticeStausM> _rapStatus = new List<RAPNoticeStausM>();
        private List<CurrentOnRentM> _currentOnRent = new List<CurrentOnRentM>();
        public string CaseID { get; set; }
        public int C_ID { get; set; }
        public bool Selected { get; set; }
        public int PetitionCategoryID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public TenantPetitionInfoM TenantPetitionInfo
        {
            get
            {
                return _tenantPetitionInfo;
            }
            set
            {
                _tenantPetitionInfo = value;
            }
        }
        public OwnerPetitionInfoM OwnerPetitionInfo
        {
            get
            {
                return _ownerPetitionInfo;
            }
            set
            {
                _ownerPetitionInfo = value;
            }
        }

        public OwnerPetitionTenantInfoM OwnerPetitionTenantInfo { get; set; }
        public OwnerPetitionRentalIncrementInfoM OwnerPetitionRentalIncrementInfo { get; set; }
        public TenantAppealInfoM TenantAppealInfo
        {
            get
            {
                return _tenantappealInfo;
            }
            set
            {
                _tenantappealInfo = value;
            }
        }
        public List<PetitionCategoryM> PetitionCategory
        {
            get
            {
                return _petitionCategory;
            }
            set
            {
                _petitionCategory = value;
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
        public List<RAPNoticeStausM> RAPNoticeStatus
        {
            get
            {
                return _rapStatus;
            }
            set
            {
                _rapStatus = value;
            }
        }
        public bool bCaseFiledByThirdParty { get; set; }
        public int CaseFileBy { get; set; }
        public CityUserAccount_M CityAnalyst { get; set; }
        public CityUserAccount_M HearingOfficer { get; set; }        
        public DateTime HearingDate { get; set; }
        public DateTime AppealDate { get; set; }
        public bool bThirdPartyRepresentationAppeal { get; set; }
        public List<ActivityStatus_M> ActivityStatus
        {
            get
            {
                return _activityStatus;
            }
            set
            {
                _activityStatus = value;
            }
        }   

    }

    public class CityUser
    {
        public int CityUserID { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int AccountTypeID { get; set; }
        public int EmployeeID { get; set; }
        public string Password { get; set; }  

    }

    public class PetitionCategoryM
    {
        public int PetitionCategoryID { get; set; }
        public string PetitionCategory { get; set; }
    }
    public class CustomDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
    public class RAPNoticeStausM
    {
        public int RAPNoticeStatusID { get; set; }
        public string RAPNoticeStatus { get; set; }
    }
    public class PetitionPageSubnmissionStatusM
    {
        public PetitionPageSubnmissionStatusM()
        {
            OwnerPetition = new OwnerPetitionPageSubnmissionStatusM();
            TenantPetition = new TenantPetitionPageSubnmissionStatusM();
        }
        public OwnerPetitionPageSubnmissionStatusM OwnerPetition { get; set; }
        public TenantPetitionPageSubnmissionStatusM TenantPetition { get; set; } 
    }
    public class OwnerPetitionPageSubnmissionStatusM
    {
        public int CustomerID { get; set; }
        public bool ImportantInformation { get; set; }
        public bool ApplicantInformation { get; set; }
        public bool JustificationForRentIncrease { get; set; }
        public bool RentalProperty { get; set; }
        public bool RentHistory { get; set; }
        public bool AdditionalDocumentation { get; set; }
        public bool Review { get; set; }
        public bool Verification { get; set; }
    }

    public class TenantPetitionPageSubnmissionStatusM
    {
        public int CustomerID { get; set; }
        public bool ImportantInformation { get; set; }
        public bool ApplicantInformation { get; set; }
        public bool GroundsForPetition { get; set; }
        public bool RentHistory { get; set; }
        public bool LostService { get; set; }
        public bool AdditionalDocumentation { get; set; }
        public bool Review { get; set; }
        public bool Verification { get; set; }
    }
}
