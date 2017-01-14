using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RAP.Core.DataModels
{
    
    //public class CaseInfoM
    //{
    //    private TenantPetitionInfoM _tenantPetitionInfo = new TenantPetitionInfoM();
    //    private TenantAppealInfoM _tenantappealInfo = new TenantAppealInfoM();
    //    private UserInfoM _thirdPartyInfo = new UserInfoM();
    //    private UserInfoM _ownerInfo = new UserInfoM();
        
    //    private List<AppealGroundM> _appealGrounds = new List<AppealGroundM>();
    //    private List<DocumentM> _documnts = new List<DocumentM>();
        
    //    public string CaseID { get; set; }
    //    public int C_ID { get; set; }
    //    public int PetitionCategoryID { get; set; }
    //    public TenantPetitionInfoM TenantPetitionInfo
    //    {
    //        get
    //        {
    //            return _tenantPetitionInfo;
    //        }
    //        set
    //        {
    //            _tenantPetitionInfo = value;
    //        }
    //    }    
    //    public TenantAppealInfoM TenantAppealInfo
    //    {
    //        get
    //        {
    //            return _tenantappealInfo;
    //        }
    //        set
    //        {
    //            _tenantappealInfo = value;
    //        }
    //    }
                
    //    public int TenantUserID { get; set; }        
    //    public bool bAgreeToCityMediation { get; set; }
    //    public bool bCaseFiledByThirdParty { get; set; }
    //    public int CaseFileBy { get; set; }
    //    public string CaseAssignedTo { get; set; }
    //    public string CityUserFirstName { get; set; }
    //    public string CityUserLastName { get; set; }
    //    public string CityUserMailID { get; set; }
    //    public int WorlFlowID { get; set; }
    //    public DateTime HearingDate { get; set; }
    //    public DateTime AppealDate { get; set; }
    //    public bool bThirdPartyRepresentationAppeal { get; set; }
    //    private List<ActivityStatus_M> _activityStatus = new List<ActivityStatus_M>();

    //    public List<ActivityStatus_M> ActivityStatus
    //    {
    //        get
    //        {
    //            return _activityStatus;
    //        }
    //        set
    //        {
    //            _activityStatus = value;
    //        }
    //    }
        

    //    public List<DocumentM> Documents
    //    {
    //        get
    //        {
    //            return _documnts;
    //        }
    //        set
    //        {
    //            _documnts = value;
    //        }
    //    }
        
    //}

    public class UserInfoM
    {
        public UserInfoM()
        {
            State = new StateM();
            City = "Oakland";
            apnAddress = new APNAddress();
        }
        public int UserID { get; set; }
        public string BusinessName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public StateM State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAPNAddress { get; set; }
        public APNAddress apnAddress { get; set; }
    }

    public class APNAddress
    {
        public APNAddress()
        {
            State = new StateM();
        }
        public int UserID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public StateM State { get; set; }
        public string Zip { get; set; }
        public string APNNumber { get; set; }

    }
    public class TenantAppealInfoM
    {
        private List<DocumentM> _documents = new List<DocumentM>();
        public TenantAppealInfoM()
        {
            serveAppeal = new ServeAppealM();
            AppealOpposingPartyInfo = new List<UserInfoM>();
            AppealDate = new CustomDate();
        }
        public int AppealID;
        public int AppealCategoryID;
        private List<AppealGroundM> _appealGrounds = new List<AppealGroundM>();
        // public string CaseID { get; set; }
        private bool _appealFiled = false;
        public bool bThirdPartyRepresentation = false;
        public string CaseNumber;
        public ServeAppealM serveAppeal { get; set; }
        public CustomDate AppealDate { get; set; }
        public UserInfoM ApplicantUserInfo { get; set; }
        // private UserInfoM _appealThirdPartyInfo = new UserInfoM();
        public UserInfoM ThirdPartyInfo = new UserInfoM();
        public UserInfoM AppealPropertyUserInfo = new UserInfoM();
        public int thirdPartyUserID;

        public List<UserInfoM> AppealOpposingPartyInfo;
        public List<int> opposingPartyUserID = new List<int>();
        public DateTime CreatedDate;
        public DateTime OpposingPartyCommunicateDate;

        public bool bAppealfiled
        {
            get
            {
                return _appealFiled;
            }
            set
            {
                _appealFiled = value;
            }
        }
        public int AppealFiledBy { get; set; }
        public List<AppealGroundM> AppealGrounds
        {
            get
            {
                return _appealGrounds;
            }
            set
            {
                _appealGrounds = value;
            }
        }

        public List<DocumentM> Documents
        {
            get
            {
                return _documents;
            }
            set
            {
                _documents = value;
            }
        }

    }

    public class VerificationM
    {
        public VerificationM()
        {
            date = DateTime.Now;
        }
        public DateTime date { get; set; }
        public string pinVerify { get; set; }
        public bool bAcknowledgePinName { get; set; }
        public bool bDeclarePenalty { get; set; }
        public bool bThirdParty { get; set; }
        public string pinMediation { get; set; }
        public bool bAcknowledgePinNameMediation { get; set; }
        public bool bCaseMediation { get; set; }
        public bool bThirdPartyMediation { get; set; }
    }

    public class TenantPetitionInfoM
    {
        public TenantPetitionInfoM()
        {
            ThirdPartyInfo = new UserInfoM();
            OwnerInfo = new UserInfoM();
            PropertyManager = new UserInfoM();
           // RentIncreases = new List<TenantRentIncreaseInfoM>();
            Document = new DocumentM();
            SelectedRangeOfUnits = new NumberRangeForUnitsM();
            ApplicantUserInfo = new UserInfoM();
            LostServicesPage = new LostServicesPageM();
            Verification = new VerificationM();

        }
        private List<UnitTypeM> _unitTypes = new List<UnitTypeM>();
        private List<CurrentOnRentM> _currentOnRent = new List<CurrentOnRentM>();
        private List<PetitionGroundM> _petitionGrounds = new List<PetitionGroundM>();
        private List<NumberRangeForUnitsM> _rangeOfUnits = new List<NumberRangeForUnitsM>();
        
        public bool bThirdPartyRepresentation { get; set; }
        public UserInfoM ApplicantUserInfo { get; set; } 
        public UserInfoM ThirdPartyInfo { get; set; }
        public UserInfoM OwnerInfo { get; set; }
        public bool bSameAsOwnerInfo { get; set; }
        public UserInfoM PropertyManager { get; set; }
        public int PetitionID { get; set; }
        public int NumberOfUnits { get; set; }
        public NumberRangeForUnitsM SelectedRangeOfUnits { get; set; }
        public int UnitTypeId { get; set; }
        public bool bCurrentRentStatus { get; set; }
        public string ProvideExplanation { get; set; }
        public bool bCitationDocUnavailable { get; set; }
        public List<PetitionGroundM> PetitionGrounds { get; set; }
        public TenantRentalHistoryM TenantRentalHistory { get; set; }
        public DocumentM Document { get; set; }
        public bool bPetitionFiledPrviously { get; set; }
        public string PreviousCaseIDs { get; set; }
        public LostServicesPageM LostServicesPage { get; set; }
        public VerificationM Verification { get; set; }
        
        public int CustomerID { get; set; }

        public List<NumberRangeForUnitsM> RangeOfUnits
        {
            get
            {
                return _rangeOfUnits;
            }
            set
            {
                _rangeOfUnits = value;
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
    public class LostServicesPageM
    {
        public LostServicesPageM()
        {
            LostServices = new List<TenantLostServiceInfoM>();
            Problems = new List<TenantProblemInfoM>();
            Document = new DocumentM();
        }
        private List<DocumentM> _documents = new List<DocumentM>();
        public int PetitionID { get; set; }
        public bool bHouseServiceDecreased { get; set; }
        public bool bLostService { get; set; }
        public List<TenantLostServiceInfoM> LostServices{get; set;} 
        public bool bProblem { get; set; }
        public List<TenantProblemInfoM> Problems { get; set; }
        public DocumentM Document { get; set; }
        public List<DocumentM> Documents
        {
            get
            {
                return _documents;
            }
            set
            {
                _documents = value;
            }
        }
   
    }
    public class TenantRentalHistoryM
    {
        public TenantRentalHistoryM ()
        {
            RentIncreases = new List<TenantRentIncreaseInfoM>();
        }
        public int PetitionID { get; set; }
        public CustomDate MoveInDate { get; set; }
        public decimal InitialRent { get; set; }
        public bool bRAPNoticeGiven { get; set; }
        public CustomDate RAPNoticeGivenDate { get; set; }
        public bool bRentControlledByAgency { get; set; }
        public string PreviousCaseIDs { get; set; }
        public bool bPetitionFiledPrviously { get; set; }
        public List<TenantRentIncreaseInfoM> RentIncreases { get; set; }
    }
    public class TenantRentIncreaseInfoM
    {
        public bool bRentIncreaseNoticeGiven { get; set; }
        public CustomDate RentIncreaseNoticeDate { get; set; }
        public decimal RentIncreasedFrom { get; set; }
        public decimal RentIncreasedTo { get; set; }
        public CustomDate RentIncreaseEffectiveDate { get; set; }
        public bool bRentIncreaseContested { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TenantLostServiceInfoM
    {
        public string ReducedServiceDescription { get; set; }
        public decimal EstimatedLoss { get; set; }
        public CustomDate LossBeganDate { get; set; }
        public CustomDate PayingToServiceBeganDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TenantProblemInfoM
    {
        public string ProblemDescription { get; set; }
        public decimal EstimatedLoss { get; set; }
        public CustomDate ProblemBeganDate { get; set; }
        public CustomDate PayingToProblemBeganDate { get; set; }
        public bool IsDeleted { get; set; }
    }
   
    public class UnitTypeM
    {
        public int UnitTypeID { get; set; }
        public string UnitDescription { get; set; }
    }
    
    public class CurrentOnRentM
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
   
    public class PetitionGroundM
    {
        private bool _selected = false;
        public int PetitionGroundID { get; set; }
        public string PetitionGroundDescription { get; set; }
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }
    }

    public class AppealGroundM
    {
        //public AppealGroundM()
        //{
        //    appeal = new List<AppealGroundMSub>();
        //}
        private bool _selected = false;
        public int AppealGroundID { get; set; }
        public string AppealDescription { get; set; }
       // List<AppealGroundMSub> appeal { get; set; }
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }
    }
   
}
