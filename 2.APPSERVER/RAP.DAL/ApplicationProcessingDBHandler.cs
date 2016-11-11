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

        public ReturnResult<TenantPetitionInfoM> GetTenantPetitionFormInfo()
       {
           ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            List<UnitTypeM> _units = new List<UnitTypeM>();
            List<CurrentOnRentM> _rentStatusItems = new List<CurrentOnRentM>();
            List<PetitionGroundM> _petitionGrounds = new List<PetitionGroundM>();
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
                           PetitionGroundM _petitionGround = new PetitionGroundM();
                           _petitionGround.PetitionGroundID = petitionGround.PetitionGroundID;
                           _petitionGround.PetitionGroundDescription = petitionGround.PetitionDescription;
                           _petitionGrounds.Add(_petitionGround);
                       }
                   }

                    if(_petitionGrounds.Count == 0)
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
            catch(Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }

            
       }
    }
}
