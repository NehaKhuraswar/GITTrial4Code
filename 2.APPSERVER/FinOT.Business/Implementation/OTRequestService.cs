using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using RAP.Core.DataModels;
using RAP.Core.Services;
using RAP.Core.Persisters;
using RAP.Core.Common;
using RAP.Business.Helper;

namespace RAP.Business.Implementation
{
    internal class OTRequestService : IOTRequestService
    {
        public string CorrelationId { get; set; }
        private readonly IOTRequestPersister persister;
        public OTRequestService(IOTRequestPersister _persister)
        {
            this.persister = _persister;
        }

        public OTRequest GetOTRequest(int ReqID, int? FY, string Username)
        {
            try
            {
                this.persister.CorrelationId = this.CorrelationId;
                DataSet ds = persister.GetOTRequest(ReqID, FY, Username);
                OTRequest obj = new OTRequest();
                obj.ReqID = ReqID;
                obj.Header = (from DataRow row in ds.Tables[0].AsEnumerable()
                              select new Header()
                              {
                                  OTCode = row["OTCode"].ToString(),
                                  Status = new IDDescription()
                                  {
                                      ID=Convert.ToInt32(row["StatusID"]),
                                      Description=row["StatusName"].ToString(),
                                  },
                                  RequestType = new IDDescription()
                                  {
                                      ID = Convert.ToInt32(row["RequestTypeID"]),
                                      Description = Enum.GetName(typeof(RequestType), Convert.ToInt32(row["RequestTypeID"])),
                                  },
                                  BriefDescription = row["BriefDescription"].ToString(),
                                  DetailDescription = row["DetailDescription"].ToString(),
                                  CashOrComp = row["CashOrComp"].ToString(),
                                  BureauOwner = row["BureauOwner"].ToString(),
                                  StartDate = Convert.ToDateTime(row["StartDate"]),
                                  EndDate = Convert.ToDateTime(row["EndDate"]),
                                  IFY = Convert.ToInt32(row["IFY"]),
                                  AuthorizedOTAmount = Convert.ToDecimal(row["AuthorizedOTAmount"]),
                                  EstimatedOTHours = Convert.ToDecimal(row["EstimatedOTHours"]),
                                  AuthorizedOTHours = Convert.ToDecimal(row["AuthorizedOTHours"]),
                                  ActiveOTCode = Convert.ToBoolean(row["ActiveOTCode"])
                                  // Neha TBD copy all the variables 
                              }).FirstOrDefault();
                
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Notes> GetNotes(int ReqID, string Username)
        {
            try
            {
                this.persister.CorrelationId = this.CorrelationId;
                DataTable dt = persister.GetNotes(ReqID, Username);
                List<Notes> obj = (from DataRow row in dt.AsEnumerable()
                                   select new Notes()
                                   {
                                       ItemID = Convert.ToInt32(row["ItemID"]),
                                       //Neha TBD add all the parameter of the Notes.
                                   }).ToList();

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveOTRequest(int? ReqID, Header obj, string Username, out IList<string> Warnings)
        {
            try
            {
                this.persister.CorrelationId = this.CorrelationId;
                Warnings = null;
                string OTCode = ""; // Neha TBD generate the OTCode
                //int FY = obj.StartDate.Year;
                //if(obj.StartDate.Month > 6)
                //    FY++;

                //if (obj.RequestType.ID == 1 && obj.CashOrComp=="Cash")
                //{
                //    OTCode = "CA" + FY.ToString().Substring(2, 2);
                //}
                //else if (obj.RequestType.ID == 1 && obj.CashOrComp == "Comp")
                //{
                //    OTCode = "CA" + FY.ToString().Substring(2, 2);
                //}
                //else if (obj.RequestType.ID == 2 && obj.CashOrComp == "Cash")
                //{
                //    OTCode = "SA" + FY.ToString().Substring(2, 2);
                //}
                //else if (obj.RequestType.ID == 2 && obj.CashOrComp == "Comp")
                //{
                //    OTCode = "SA" + FY.ToString().Substring(2, 2);
                //}

                DataTable dt = persister.SaveOTRequest(ref ReqID, OTCode, obj.RequestType.ID, obj.BriefDescription, obj.DetailDescription,
                    obj.CashOrComp, obj.BureauOwner, obj.StartDate, obj.EndDate, obj.AuthorizedOTAmount, obj.EstimatedOTHours,
                    obj.AuthorizedOTHours, obj.ActiveOTCode, Username);
 
                
                //if (dt.Rows.Count > 0)
                //{
                //    Warnings = dt.AsEnumerable().Select(r => r.Field<string>("Message")).ToList();
                //}
                return (int)ReqID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool SaveNotes(int ReqID, Notes Note, string Username)
        {
            try
            {
                this.persister.CorrelationId = this.CorrelationId;
               
                return persister.SaveNotes(ReqID, Note.NoteDescription, Note.Context, Username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //implements all methods from IOTRequestService
    }
}
