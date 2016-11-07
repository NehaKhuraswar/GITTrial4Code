using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RAP.Core.DataModels;

namespace RAP.DAL
{
    public class AccountManagementDBHandler
    {
        private readonly string _connString;

        public AccountManagementDBHandler()
        {
            _connString =  ConfigurationManager.AppSettings["RAPDBConnectionString"];
        }

        public bool SaveCustomer(CustomerInfo message)
       {
           try
           {
               using(OAKRAPDataContext db = new OAKRAPDataContext(_connString))
               {
                   
                   if(message.UserTypeID == 0)
                   {
                       int? userTypeResponse = null;
                           userTypeResponse = db.UserTypes.Where(t => t.Description == message.UserType).Select(p => p.UserTypeID).FirstOrDefault();

                       if (userTypeResponse == null)
                       {
                           return false;
                       }
                       message.UserTypeID = Convert.ToInt32(userTypeResponse);                   
                   }

                   CustomerDetail custTable = new CustomerDetail();
                   custTable.FirstName = message.FirstName;
                   custTable.LastName = message.LastName;
                   custTable.PhoneNumber = message.PhoneNumber;
                   custTable.email = message.email;
                   custTable.AddressLine1 = message.Address1;
                   custTable.AddressLine2 = message.Address2;
                   custTable.City = message.City;
                   custTable.State = message.State;
                   custTable.Zip = message.Zip;
                   custTable.UserTypeID = 1;
                   custTable.Password = message.Password;
                   custTable.CreatedDate = DateTime.Now;
                   custTable.EmailNotificationFlag = message.EmailNotificationFlag;
                   db.CustomerDetails.InsertOnSubmit(custTable);
                   db.SubmitChanges();
                }
               return true;
           }
           catch(Exception ex)
           {
               return false;
           }
       }
        
    }
}
