using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.Business.Helper;
using RAP.DAL;

namespace RAP.Business.Implementation
{
    public class DashboardService : IDashboardService
    {
        public string CorrelationId { get; set; }
        private readonly IDashboardDBHandler _dbHandler;
        private readonly IDocumentService _documentService;
        private readonly ICommonService _commonService;
        private readonly IExceptionHandler _eHandler = new ExceptionHandler();
        private readonly IEmailService _emailService;
        //TBD
        //public ApplicationProcessingService()
        //{
        //    _dbHandler = new ApplicationProcessingDBHandler();
        //}
        public DashboardService(IDashboardDBHandler dbHandler, IDocumentService documentService, ICommonService commonService, IEmailService emailService)
        {
            this._dbHandler = dbHandler;
            this._documentService = documentService;
            this._commonService = commonService;
            this._emailService = emailService;
        }

        public ReturnResult<bool> SaveNewActivityStatus(ActivityStatus_M activityStatus, int C_ID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SaveNewActivityStatus(activityStatus, C_ID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<ActivityStatus_M>> GetActivityStatusForCase(int C_ID)
        {
            ReturnResult<List<ActivityStatus_M>> result = new ReturnResult<List<ActivityStatus_M>>();
            try
            {
                result = _dbHandler.GetActivityStatusForCase(C_ID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<Activity_M>> GetActivity()
        {
            ReturnResult<List<Activity_M>> result = new ReturnResult<List<Activity_M>>();
            try
            {
                result = _dbHandler.GetActivity();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<List<CityUserAccount_M>> GetHearingOfficers()
        {
            ReturnResult<List<CityUserAccount_M>> result = new ReturnResult<List<CityUserAccount_M>>();
            try
            {
                result = _dbHandler.GetHearingOfficers();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<CityUserAccount_M>> GetAnalysts()
        {
            ReturnResult<List<CityUserAccount_M>> result = new ReturnResult<List<CityUserAccount_M>>();
            try
            {
                result = _dbHandler.GetAnalysts();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<bool> AssignHearingOfficer(int cID, int HearingOfficerUserID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.AssignHearingOfficer(cID, HearingOfficerUserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<bool> AssignAnalyst(int cID, int AnalystUserID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.AssignAnalyst( cID,  AnalystUserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<Status_M>> GetStatus(int activityID)
        {
            ReturnResult<List<Status_M>> result = new ReturnResult<List<Status_M>>();
            try
            {
                result = _dbHandler.GetStatus(activityID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }


        public ReturnResult<List<DocumentM>> GetCaseDocuments(int c_id)
        {
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            try
            {
                result = _commonService.GetCaseDocuments(c_id);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }
        public ReturnResult<List<DocumentM>> SaveCaseDocuments(List<DocumentM> documents)
        {
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            List<DocumentM> _documents = new List<DocumentM>();
            try
            {
                if (documents != null && documents.Any())
                {
                    foreach (var doc in documents)
                    {
                        if (!doc.isUploaded)
                        {
                            doc.DocCategory = DocCategory.AdditionalStaffDocument.ToString();
                            var docUploadResut = _documentService.UploadDocument(doc);
                            if (docUploadResut.status.Status == StatusEnum.Success)
                            {
                                _documents.Add(doc);
                            }
                        }
                        else
                        {
                            _documents.Add(doc);
                        }
                    }
                }
                result.result = _documents;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CustomEmailM> GetCustomEmail(int c_id)
        {
            ReturnResult<CustomEmailM> result = new ReturnResult<CustomEmailM>();
            result.result = new CustomEmailM();
            result.status = new OperationStatus() { Status = StatusEnum.Success };
            return result;
        }

        public ReturnResult<bool> SubmitCustomEmail(CustomEmailM cMail)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _emailService.SendEmail(cMail.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }


        }
        //implements all methods from IDashboardService
    }
}
