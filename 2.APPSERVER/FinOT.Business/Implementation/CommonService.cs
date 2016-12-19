using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.Business.Helper;
using RAP.DAL;

namespace RAP.Business.Implementation
{
    public class CommonService : ICommonService
    {
        public string CorrelationId { get; set; }
        private readonly ICommonDBHandler _dbHandler;
        private readonly IExceptionHandler _eHandler;
    
        //TBD
        //public CommonService()
        //{
        //    _dbHandler = new CommonDBHandler();            
        //}
        public CommonService(ICommonDBHandler dbHandler, IExceptionHandler eHandler)
        {
            this._dbHandler = dbHandler;
            this._eHandler = eHandler;
        }
        public void LogError(OperationStatus status)
        {
            try
            {
                _dbHandler.SaveErrorLog(status);
            }
            catch(Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                eHandler.HandleException(ex);               
            }     
                        
        }

       public ReturnResult<DocumentM> SaveDocument(DocumentM doc)
        {
            ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
             try
             {
                 result = _dbHandler.SaveDocument(doc);
                 return result;
             }
             catch (Exception ex)
             {
                 result.status = _eHandler.HandleException(ex);
                 LogError(result.status);
                 return result;
             }
        }
        
    }
}
