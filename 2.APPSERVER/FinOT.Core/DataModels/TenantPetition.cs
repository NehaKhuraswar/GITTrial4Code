﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
    
    public class CaseInfoM
    {
        public int PetitionCategoryID { get; set; }
        public TenantPetitionInfoM TenantPetitionInfo { get; set; }
        public int TenantUserID { get; set; }
        public bool bThirdPartyRepresentation { get; set; }
        public UserInfoM ThirdPartyInfo { get; set; }
        public UserInfoM OwnerInfo { get; set; }
        public bool bAgreeToCityMediation { get; set; }
        public bool bCaseFiledByThirdParty { get; set; }
        public int CaseFileBy { get; set; }
        public string CaseAssignedTo { get; set; }
        public string CityUserFirstName { get; set; }
        public string CityUserLastName { get; set; }
        public string CityUserMailID { get; set; }
        public int WorlFlowID { get; set; }
        public DateTime HearingDate { get; set; }
        public DateTime AppealDate { get; set; }
    }

    public class UserInfoM
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }

    public class TenantPetitionInfoM
    {
        private List<UnitTypeM> _unitTypes = new List<UnitTypeM>();
        private List<CurrentOnRentM> _currentOnRent = new List<CurrentOnRentM>();
        private List<PetitionGroundM> _petitionGrounds = new List<PetitionGroundM>();
        private List<TenantRentIncreaseInfoM> _rentIncreases = new List<TenantRentIncreaseInfoM>();
        private List<TenantLostServiceInfoM> _lostServices = new List<TenantLostServiceInfoM>();
        private List<TenantProblemInfoM> _problems = new List<TenantProblemInfoM>();
        public int PetitionID { get; set; }
        public int NumberOfUnits { get; set; }
        public int UnitTypeId { get; set; }
        public int CurrentRentStatusID { get; set; }
        public string LegalWithHoldingExplanation { get; set; }
        public bool bCitationDocUnavailable { get; set; }
        public List<PetitionGroundM> PetitionGrounds
        {
            get
            {
                return _petitionGrounds;
            }
            set
            {
                _petitionGrounds = value;
            }
        }
        public DateTime MoveInDate { get; set; }
        public double InitalRent { get; set; }
        public bool bRAPNoticeGiven { get; set; }
        public DateTime RAPNoticeGivenDate { get; set; }
        public bool bRentControlledByAgency { get; set; }
        public List<TenantRentIncreaseInfoM> RentIncreases
        {
            get
            {
                return _rentIncreases;
            }
            set
            {
                _rentIncreases = value;
            }
        }
        public bool bPetitionFiledPrviously { get; set; }
        public string PreviousCaseIDs { get; set; }
        public bool bLostService { get; set; }
        public  List<TenantLostServiceInfoM> LostServices
        {
            get
            {
                return _lostServices;
            }
            set
            {
                _lostServices = value;
            }
        }
        public bool bProblem { get; set; }
        public List<TenantProblemInfoM> Problems
        {
            get
            {
                return _problems;
            }
            set
            {
                _problems = value;
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

    public class TenantRentIncreaseInfoM
    {
        public bool bRentIncreaseNoticeGiven { get; set; }
        public DateTime RentIncreaseNoticeDate { get; set; }
        public double RentIncreasedFrom { get; set; }
        public double RentIncreasedTo { get; set; }
        public DateTime RentIncreaseEffectiveDate { get; set; }
        public bool bRentIncreaseContested { get; set; }
    }

    public class TenantLostServiceInfoM
    {
        public string ReducedServiceDescription { get; set; }
        public double EstimatedLoss { get; set; }
        public DateTime LossBeganDate { get; set; }
        public DateTime PayingToServiceBeganDate { get; set; }
    }

    public class TenantProblemInfoM
    {
        public string ProblemDescription { get; set; }
        public double EstimatedLoss { get; set; }
        public DateTime ProblemBeganDate { get; set; }
        public DateTime PayingToProblemBeganDate { get; set; }
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
}
