using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
    public class TenantPetitionFormInfo
    {
        private List<UnitType> _unitTypes = new List<UnitType>();
        private List<CurrentOnRent> _currentOnRent = new List<CurrentOnRent>();
        private List<PetitionGround> _petitionGround = new List<PetitionGround>();

        public List<UnitType> UnitTypes
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

        public List<CurrentOnRent> CurrentOnRent
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

        public List<PetitionGround> PetitionGrounds
        {
            get
            {
                return _petitionGround;
            }
            set
            {
                _petitionGround = value;
            }
        }
    }

    public class UnitType
    {
        public int UnitTypeID { get; set; }
        public string UnitDescription { get; set; }
    }

    public class CurrentOnRent
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }

    public class PetitionGround
    {
        public int ID { get; set; }
        public string Description { get; set; }
    }
}
