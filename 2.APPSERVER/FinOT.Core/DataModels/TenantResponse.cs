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
            ExemptContestedInfo = new TenantResponseExemptContestedInfoM();
        }
        
        public bool bThirdPartyRepresentation { get; set; }
        public UserInfoM ApplicantUserInfo { get; set; } 
        public UserInfoM ThirdPartyInfo { get; set; }
        public bool ThirdPartyEmailNotification { get; set; }
        public bool ThirdPartyMailNotification { get; set; }
        public UserInfoM OwnerInfo { get; set; }
        public bool bSameAsOwnerInfo { get; set; }
        public UserInfoM PropertyManager { get; set; }
        public int TenantResponseID { get; set; }
        public int? NumberOfUnits { get; set; }
        public NumberRangeForUnitsM SelectedRangeOfUnits { get; set; }
        public int UnitTypeId { get; set; }
        public bool bCurrentRentStatus { get; set; }
        public string ProvideExplanation { get; set; }
        public TenantResponseRentalHistoryM TenantRentalHistory { get; set; }
        public DocumentM Document { get; set; }
        public bool bPetitionFiledPrviously { get; set; }
        public string PreviousCaseIDs { get; set; }
        public VerificationM Verification { get; set; }
        public int CustomerID { get; set; }
        public List<NumberRangeForUnitsM> RangeOfUnits{get;set;}
        public List<UnitTypeM> UnitTypes{get;set;}
        public TenantResponseExemptContestedInfoM ExemptContestedInfo { get; set; }
    }

    public class TenantResponseRentalHistoryM
    {
        public TenantResponseRentalHistoryM()
        {
            RentIncreases = new List<TenantResponseRentIncreaseInfoM>();
            Documents = new List<DocumentM>();
            Document = new DocumentM();
        }
        public int TenantResponseID { get; set; }
        public CustomDate RentalAgreementDate { get; set; }
        public CustomDate MoveInDate { get; set; }
        public decimal? InitialRent { get; set; }
        public bool bRAPNoticeGiven { get; set; }
        public CustomDate RAPNoticeGivenDate { get; set; }
        public List<TenantResponseRentIncreaseInfoM> RentIncreases { get; set; }
        public List<DocumentM> Documents { get; set; }
        public DocumentM Document { get; set; }
    }

    public class TenantResponseRentIncreaseInfoM
    {
        public bool bRentIncreaseNoticeGiven { get; set; }
        public CustomDate RentIncreaseNoticeDate { get; set; }
        public decimal? RentIncreasedFrom { get; set; }
        public decimal? RentIncreasedTo { get; set; }
        public CustomDate RentIncreaseEffectiveDate { get; set; }
        public bool IsDeleted { get; set; }
    }   

    public class TenantResponseExemptContestedInfoM
    {
        public TenantResponseExemptContestedInfoM()
        {
            Explaination = string.Empty;
        }

        public int TenantResponseID { get; set; }
        public string Explaination { get; set; }
    }

   
}
