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
        public CustomerInfo GetCustomer(CustomerInfo message)
        {
            try
            {
                CustomerInfo custinfo ;
                using (OAKRAPDataContext db = new OAKRAPDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.email == message.email && x.Password == message.Password)
                                                            .Select(c => new CustomerInfo() {
                                                             FirstName = c.FirstName, 
                                                             LastName = c.LastName,
                                                             email = c.email,
                                                             Address1 = c.AddressLine1,
                                                            Address2 = c.AddressLine2,
                                                            City = c.City,
                                                            PhoneNumber = c.PhoneNumber,
                                                            State = c.State,
                                                            Zip = c.Zip,
                                                            UserTypeID = (int)c.UserTypeID,
                                                            custID = (int)c.CustomerID}).FirstOrDefault();
                    if (custdetails != null)
                    {
                        custinfo = new CustomerInfo();
                        custinfo.FirstName = custdetails.FirstName;
                        custinfo.LastName = custdetails.LastName;
                        custinfo.Address1 = custdetails.Address1;
                        custinfo.Address2 = custdetails.Address2;
                        custinfo.City = custdetails.City;
                        custinfo.PhoneNumber = custdetails.PhoneNumber;
                        custinfo.State = custdetails.State;
                        custinfo.Zip = custdetails.Zip;
                        custinfo.email = custdetails.email;
                        custinfo.UserTypeID = custdetails.UserTypeID;
                        custinfo.custID = custdetails.custID;
                    }
                    else
                    {
                        custinfo = null;
                    }
                }
                return custinfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public CustomerInfo SearchInviteThirdPartyUser(String message)
        {
            try
            {
                CustomerInfo custinfo;
                using (OAKRAPDataContext db = new OAKRAPDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.email == message)
                                                            .Select(c => new CustomerInfo()
                                                            {
                                                                FirstName = c.FirstName,
                                                                LastName = c.LastName,
                                                                email = c.email,                                                               
                                                                custID = (int)c.CustomerID
                                                            }).FirstOrDefault();
                    if (custdetails != null)
                    {
                        custinfo = new CustomerInfo();
                        custinfo.FirstName = custdetails.FirstName;
                        custinfo.LastName = custdetails.LastName;
                        custinfo.email = custdetails.email;
                        custinfo.custID = custdetails.custID;
                    }
                    else
                    {
                        custinfo = null;
                    }
                }
                return custinfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool AuthorizeThirdPartyUser(int CustID, int thirdpartyCustID)
        {
            try
            {
               // CustomerInfo custinfo;
                
                    using (OAKRAPDataContext db = new OAKRAPDataContext(_connString))
                    {

                        ThirdPartyRepresentation thirdpartyTable = new ThirdPartyRepresentation();
                        thirdpartyTable.CustomerID = CustID;
                        thirdpartyTable.ThirdPartyCustomerID = thirdpartyCustID;

                        db.ThirdPartyRepresentations.InsertOnSubmit(thirdpartyTable);
                        db.SubmitChanges();
                    }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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

                   // RAP-TBD
                 //  custTable.EmailNotificationFlag = message.EmailNotificationFlag;
                   //custTable.EmailNotificationFlag = message.MailNotificationFlag;
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
