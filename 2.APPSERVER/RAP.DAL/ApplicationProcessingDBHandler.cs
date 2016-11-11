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

        public ReturnResult<TenantPetitionFormInfoM> GetTenantPetitionFormInfo()
       {
            ReturnResult<TenantPetitionFormInfoM> result = new ReturnResult<TenantPetitionFormInfoM>();
            List<UnitType> _units = new List<UnitType>();
            List<CurrentOnRentM> _rentStatusItems = new List<CurrentOnRentM>();
            List<PetitionGround> _petitionGrounds = new List<PetitionGround>();
            try
            {
                using(ApplicationProcessingDataContext db = new ApplicationProcessingDataContext())
                {
                   var units = db.UnitTypes;                    
                    if(units == null)
                    {
                    result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                    return result;
                    }
                    else
                    {                       
                        foreach(var unit in units)
                        {
                            UnitType _unit = new UnitType();
                            _unit.UnitTypeID = unit.UnitTypeID;
                            _unit.Description = unit.Description;
                            _units.Add(_unit);
                        }

                    }

                    if (_units.Count == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                        return result;
                    }

                    var rentStausItems = db.CurrentOnRentStatus;
                    if(rentStausItems ==null)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                        return result;
                    }
                    else
                    {
                        foreach(var rentStatusItem in rentStausItems)
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
                   if(petitionGrounds == null)
                   {
                       result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                       return result;
                   }
                    else
                   {
                       foreach (var petitionGround in petitionGrounds)
                       {
                           PetitionGround _petitionGround = new PetitionGround();
                           _petitionGround.PetitionGroundID = petitionGround.PetitionGroundID;
                           _petitionGround.PetitionDescription = petitionGround.PetitionDescription;
                           _petitionGrounds.Add(_petitionGround);
                       }
                   }

                    if(_petitionGrounds.Count == 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                        return result;
                    }

                  //  result.result.UnitTypes = _units 
                   
                }

                return result;

            }
            catch(Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

            
       }
    }
}
