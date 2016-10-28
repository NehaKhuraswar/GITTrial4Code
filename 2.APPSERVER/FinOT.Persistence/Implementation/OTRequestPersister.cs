using System;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using RAP.Core.DataModels;
using RAP.Core.Persisters;
using RAP.Persistence.ADO;


namespace RAP.Persistence.Implementation
{
    internal class OTRequestPersister : IOTRequestPersister
    {
        public string CorrelationId { get; set; }
        Database _db;
        public OTRequestPersister(Database db, string connectionName)
        {
            this._db = db;
            _db.CreateConnection(connectionName);
        }

        public DataSet GetOTRequest(int ReqID, int? FY, string Username)
        {
            try
            {
                IDbParameters parameters = _db.CreateDBParameters();
                parameters.AddInParameter("ReqID", SqlDbType.Int, ReqID);
                parameters.AddInParameter("FY", SqlDbType.Int, FY);
                parameters.AddInParameter("Username", SqlDbType.VarChar, Username);
                parameters.AddOutParameter("Message", SqlDbType.VarChar, -1);
               // Neha TBD Add all the parameters of OT request table

                DataSet ds = new DataSet();
                _db.FillDataSet(ds, Procedure.GetOTRequest, CommandType.StoredProcedure, parameters);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNotes(int ReqID, string Username)
        {
            try
            {
                IDbParameters parameters = _db.CreateDBParameters();
                parameters.AddInParameter("ReqID", SqlDbType.Int, ReqID);

                // Neha TBD add all the parameters to the table

                //parameters.AddInParameter("Username", SqlDbType.VarChar, Username);
                //parameters.AddInParameter("correlationId", SqlDbType.VarChar, CorrelationId);
                //parameters.AddOutParameter("Message", SqlDbType.VarChar, -1);

                DataTable dt = new DataTable();
                _db.FillDataTable(dt, Procedure.GetOTReqNotes, CommandType.StoredProcedure, parameters);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SaveOTRequest(ref int? ReqID, string OTCode, int RequestTypeID, string BriefDescription,
            string DetailDescription, string CashOrComp, string BureauOwner, DateTime StartDate, DateTime EndDate, Decimal AuthorizedOTAmount,
            Decimal EstimatedOTHours, Decimal AuthorizedOTHours, bool ActiveOTCode, string Username)   
        {
            try
            {
                IDbParameters parameters = _db.CreateDBParameters();
                parameters.AddInOutParameter("ReqID", SqlDbType.Int, ReqID);
                parameters.AddInParameter("OTCode", SqlDbType.VarChar, OTCode);
                parameters.AddInParameter("RequestTypeID", SqlDbType.Int, RequestTypeID);
                parameters.AddInParameter("BriefDescription", SqlDbType.VarChar, BriefDescription);
                parameters.AddInParameter("DetailDescription", SqlDbType.VarChar, DetailDescription);
                parameters.AddInParameter("CashOrComp", SqlDbType.VarChar, CashOrComp);
                parameters.AddInParameter("BureauOwner", SqlDbType.VarChar, BureauOwner);
                parameters.AddInParameter("StartDate", SqlDbType.Date, StartDate);
                parameters.AddInParameter("EndDate", SqlDbType.Date, EndDate);
                parameters.AddInParameter("AuthorizedOTAmount", SqlDbType.Decimal, AuthorizedOTAmount);
                parameters.AddInParameter("EstimatedOTHours", SqlDbType.Decimal, EstimatedOTHours);
                parameters.AddInParameter("AuthorizedOTHours", SqlDbType.Decimal, AuthorizedOTHours);
                parameters.AddInParameter("ActiveOTCode", SqlDbType.Bit, ActiveOTCode);
                parameters.AddInParameter("Username", SqlDbType.VarChar, Username);
                parameters.AddOutParameter("Message", SqlDbType.VarChar, -1);

                DataTable dt = new DataTable();
                _db.FillDataTable(dt, Procedure.SaveOTReqHeader, CommandType.StoredProcedure, parameters);
                ReqID = (int)(parameters["ReqID"]).Value;

                return dt;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveNotes(int ReqID, string NoteDescription, string Context, string CreatedBy)
        {
            try
            {
                IDbParameters parameters = _db.CreateDBParameters();
                parameters.AddInParameter("ReqID", SqlDbType.Int, ReqID);
                parameters.AddInParameter("NoteDescription", SqlDbType.VarChar, NoteDescription);
                parameters.AddInParameter("Context", SqlDbType.VarChar, Context);
                parameters.AddInParameter("CreatedBy", SqlDbType.VarChar, CreatedBy);
                parameters.AddOutParameter("Message", SqlDbType.VarChar, -1);

                _db.ExecuteNonQuery(Procedure.SaveOTReqNotes, CommandType.StoredProcedure, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //implements all methods from IOTRequestPersister
    }

}
