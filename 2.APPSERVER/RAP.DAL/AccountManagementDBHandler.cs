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

                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == message.email && x.Password == message.Password)
                                                            .Select(c => new CustomerInfo() {
                                                             email = c.Email,
                                                            UserID = (int)c.UserID,
                                                            custID = (int)c.CustomerID}).FirstOrDefault();
                    
                    if (custdetails != null)
                    {
                        custinfo = new CustomerInfo();
                        custinfo.email = custdetails.email;
                        custinfo.UserID = custdetails.UserID;
                        custinfo.custID = custdetails.custID;
                    }
                    else
                    {
                        custinfo = null;
                    }
                }
                using (CommonDataContext db = new CommonDataContext(_connString))
                {

                    var userinfos = db.UserInfos.Where(x => x.UserID == custinfo.UserID)
                                                            .Select(c => new UserInfo()
                                                            {
                                                                FirstName = c.FirstName,
                                                                LastName = c.LastName,
                                                                AddressLine1 = c.AddressLine1,
                                                                AddressLine2 = c.AddressLine2,
                                                                City = c.City,
                                                                PhoneNumber = c.PhoneNumber,
                                                                State = c.State,
                                                                Zip = c.Zip,
                                                            }).FirstOrDefault();

                    if (userinfos != null)
                    {
                        custinfo.FirstName = userinfos.FirstName;
                        custinfo.LastName = userinfos.LastName;
                        custinfo.Address1 = userinfos.AddressLine1;
                        custinfo.Address2 = userinfos.AddressLine2;
                        custinfo.City = userinfos.City;
                        custinfo.PhoneNumber = userinfos.PhoneNumber;
                        custinfo.State = userinfos.State;
                        custinfo.Zip = userinfos.Zip;
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
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == message)
                                                            .Select(c => new CustomerInfo()
                                                            {
                                                                //FirstName = c.FirstName,
                                                                //LastName = c.LastName,
                                                                email = c.Email,                                                               
                                                                custID = (int)c.CustomerID
                                                            }).FirstOrDefault();
                    if (custdetails != null)
                    {
                        custinfo = new CustomerInfo();
                        //custinfo.FirstName = custdetails.FirstName;
                        //custinfo.LastName = custdetails.LastName;
                        custinfo.email = custdetails.email;
                        custinfo.custID = custdetails.custID;
                    }
                    else
                    {
                        custinfo = null;
                    }
                }
                using (CommonDataContext db = new CommonDataContext(_connString))
                {

                    var userinfos = db.UserInfos.Where(x => x.UserID == custinfo.UserID)
                                                            .Select(c => new UserInfo()
                                                            {
                                                                FirstName = c.FirstName,
                                                                LastName = c.LastName,
                                                            }).FirstOrDefault();

                    if (userinfos != null)
                    {
                        custinfo.FirstName = userinfos.FirstName;
                        custinfo.LastName = userinfos.LastName;
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
                
                    using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
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
        public bool RemoveThirdParty(int CustID, int thirdPartyRepresentationID)
        {
            try
            {
                // CustomerInfo custinfo;

                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    ThirdPartyRepresentation thirdpartyTable = db.ThirdPartyRepresentations.First(i => i.ThirdPartyRepresentationID == thirdPartyRepresentationID);
                    thirdpartyTable.IsDeleted = true;
                    thirdpartyTable.ModifiedDate = DateTime.Now;
                   // thirdpartyTable.ThirdPartyCustomerID = thirdPartyRepresentationID;
                    db.SubmitChanges();
                  //  db.ThirdPartyRepresentations.InsertOnSubmit(thirdpartyTable);
                   // db.SubmitChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<ThirdPartyDetails> GetAuthorizedUsers(int custID)
        {
            
            
            try
            {
                List<ThirdPartyDetails> thirdPartyDetails;
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var custdetails = db.ThirdPartyRepresentations.Where(x => x.CustomerID == custID)
                                                            .Select(c => new ThirdPartyDetails()
                                                            {
                                                                ThirdPartyRepresentationID = c.ThirdPartyCustomerID,
                                                              //  UserID = (int)c.UserID,
                                                              //  custID = (int)c.CustomerID
                                                            }).FirstOrDefault();
                    var query =
                        db.ThirdPartyRepresentations.AsEnumerable().Join(db.CustomerDetails.AsEnumerable(),
                        t => t.ThirdPartyCustomerID,
                        c => c.CustomerID,
                        (t, c) => new
                        {
                            ID = t.ThirdPartyRepresentationID,
                            CustomerID = t.ThirdPartyCustomerID,
                            //NEW-RAP-TBD
                            //FirstName = c.FirstName,
                            //LastName = c.LastName,
                            //email = c.email
                        });

                    
                    thirdPartyDetails = new List<ThirdPartyDetails>();
                    int index = 0;

                    foreach (var CustomerDetails in query)
                                                            {
                        ThirdPartyDetails obj = new ThirdPartyDetails();
                        obj.ThirdPartyRepresentationID = CustomerDetails.ID;
                        obj.custID = CustomerDetails.CustomerID;
                        //NEW-RAP-TBD
                        //obj.FirstName = CustomerDetails.FirstName;
                        //obj.LastName = CustomerDetails.LastName;
                        //obj.email = CustomerDetails.email;

                        thirdPartyDetails.Add(obj);
                        index++;
                    }
                }
                return thirdPartyDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public bool CheckCustAccount(CustomerInfo message)
        {
            try
            {                

                    
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var custInfo = db.CustomerDetails.Where(x => x.Email == message.email)
                                    .Select(c => new CustomerInfo()
                                    {
                                        custID = c.CustomerID,
                                    }).FirstOrDefault();
                    if (custInfo != null)
                        return true;
                    else
                        return false;
                }
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
               // Account already exists
               if (CheckCustAccount(message))
                   return false;

               

                using (CommonDataContext dbCommon = new CommonDataContext(_connString))
                {
                    // Check for the user info, if all the details already match get the user id and directly create the customer account
                    // else enter the details in the userinfo table
                    var userInfos = dbCommon.UserInfos.Where(x => x.FirstName == message.FirstName
                                && x.LastName == message.LastName
                                && x.PhoneNumber == message.PhoneNumber
                                && x.AddressLine1 == message.Address1
                                && x.AddressLine2 == message.Address2
                                && x.City == message.City
                                && x.State == message.State
                                && x.Zip == message.Zip
                                ).Select(c => new CustomerInfo()
                                    {
                                        UserID = c.UserID,
                                    }).FirstOrDefault();

                    if (userInfos == null)
                    {
                        UserInfo userinfoTable = new UserInfo();
                        userinfoTable.FirstName = message.FirstName;
                        userinfoTable.LastName = message.LastName;
                        userinfoTable.PhoneNumber = message.PhoneNumber;
                        userinfoTable.AddressLine1 = message.Address1;
                        userinfoTable.AddressLine2 = message.Address2;
                        userinfoTable.City = message.City;
                        userinfoTable.State = message.State;
                        userinfoTable.Zip = message.Zip;
                        userinfoTable.CreatedDate = DateTime.Now;


                        // RAP-TBD
                        //  custTable.EmailNotificationFlag = message.EmailNotificationFlag;
                        //custTable.EmailNotificationFlag = message.MailNotificationFlag;
                        dbCommon.UserInfos.InsertOnSubmit(userinfoTable);
                        dbCommon.SubmitChanges();
                        message.UserID = userinfoTable.UserID;
                    }
                    else
                    {
                        message.UserID = userInfos.UserID;
                    }
                    
                }
               
               using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
               {

                   CustomerDetail custTable = new CustomerDetail();
                   custTable.Email = message.email;
                   custTable.Password = message.Password;
                   custTable.UserID = message.UserID;  
                   custTable.CreatedDate = DateTime.Now;
                   db.CustomerDetails.InsertOnSubmit(custTable);
                   db.SubmitChanges();
                   message.custID = custTable.CustomerID;
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
