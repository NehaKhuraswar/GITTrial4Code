using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RAP.Core.DataModels
{
    public class TenantResponseInfoM
    {
        public TenantResponseInfoM()
        {
            ThirdPartyInfo = new UserInfoM();
            OwnerInfo = new UserInfoM();
            PropertyManager = new UserInfoM();
            Document = new DocumentM();
            SelectedRangeOfUnits = new NumberRangeForUnitsM();
            ApplicantUserInfo = new UserInfoM();
            Verification = new VerificationM();
            RangeOfUnits = new List<NumberRangeForUnitsM>();
            UnitTypes = new List<UnitTypeM>();
        }
        
        public bool bThirdPartyRepresentation { get; set; }
        public UserInfoM ApplicantUserInfo { get; set; } 
        public UserInfoM ThirdPartyInfo { get; set; }
        public UserInfoM OwnerInfo { get; set; }
        public bool bSameAsOwnerInfo { get; set; }
        public UserInfoM PropertyManager { get; set; }
        public int TenantResponseID { get; set; }
        public int NumberOfUnits { get; set; }
        public NumberRangeForUnitsM SelectedRangeOfUnits { get; set; }
        public int UnitTypeId { get; set; }
        public bool bCurrentRentStatus { get; set; }
        public string ProvideExplanation { get; set; }
        public TenantRentalHistoryM TenantRentalHistory { get; set; }
        public DocumentM Document { get; set; }
        public bool bPetitionFiledPrviously { get; set; }
        public string PreviousCaseIDs { get; set; }
        public LostServicesPageM LostServicesPage { get; set; }
        public VerificationM Verification { get; set; }
        public int CustomerID { get; set; }
        public List<NumberRangeForUnitsM> RangeOfUnits{get;set;}
        public List<UnitTypeM> UnitTypes{get;set;}
    }

    public class TenantResponseRentalHistoryM
    {
        public TenantResponseRentalHistoryM()
        {
            RentIncreases = new List<TenantRentIncreaseInfoM>();
        }
        public int TenantResponseID { get; set; }
        public CustomDate MoveInDate { get; set; }
        public decimal InitialRent { get; set; }
        public bool bRAPNoticeGiven { get; set; }
        public CustomDate RAPNoticeGivenDate { get; set; }
        public List<TenantRentIncreaseInfoM> RentIncreases { get; set; }
    }

    public class TenantResponseRentIncreaseInfoM
    {
        public bool bRentIncreaseNoticeGiven { get; set; }
        public CustomDate RentIncreaseNoticeDate { get; set; }
        public decimal RentIncreasedFrom { get; set; }
        public decimal RentIncreasedTo { get; set; }
        public CustomDate RentIncreaseEffectiveDate { get; set; }
    }   

    public class TenantResponseExemptContestedInfoM
    {
        public int TenantResponseID { get; set; }
        public string Explaination { get; set; }
    }

   
}
