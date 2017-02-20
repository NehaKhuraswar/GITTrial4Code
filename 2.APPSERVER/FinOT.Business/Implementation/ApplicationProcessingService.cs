using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.DAL;
using System.Configuration;
using System.Resources;
namespace RAP.Business.Implementation
{
    public class ApplicationProcessingService : IApplicationProcessingService
    {
        public string CorrelationId { get; set; }
        private readonly IApplicationProcessingDBHandler _dbHandler;
        private readonly IExceptionHandler _eHandler;
        private readonly ICommonService _commonService;
        private readonly IDocumentService _documentService;
        private readonly IEmailService _emilService;
        
        //TBD
        //public ApplicationProcessingService()
        //{
        //    _dbHandler = new ApplicationProcessingDBHandler();
        //}
        public ApplicationProcessingService(IApplicationProcessingDBHandler dbHandler, IExceptionHandler eHandler, ICommonService commonService, IDocumentService documentService, IEmailService emailService)
        {
            this._dbHandler = dbHandler;
            this._eHandler = eHandler;
            this._commonService = commonService;
            this._documentService = documentService;
            this._emilService = emailService;
        }

        public ReturnResult<TenantResponsePageSubnmissionStatusM> GetTRPageSubmissionStatus(int CustomerID)
        {
            ReturnResult<TenantResponsePageSubnmissionStatusM> result = new ReturnResult<TenantResponsePageSubnmissionStatusM>();
            try
            {
                result = _dbHandler.GetTRPageSubmissionStatus(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }
        public ReturnResult<AppealPageSubnmissionStatusM> GetAppealPageSubmissionStatus(int CustomerID)
        {
            ReturnResult<AppealPageSubnmissionStatusM> result = new ReturnResult<AppealPageSubnmissionStatusM>();
            try
            {
                result = _dbHandler.GetAppealPageSubmissionStatus(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<PetitionPageSubnmissionStatusM> GetPageSubmissionStatus(int CustomerID)
        {
            ReturnResult<PetitionPageSubnmissionStatusM> result = new ReturnResult<PetitionPageSubnmissionStatusM>();
            try
            {
                result = _dbHandler.GetPageSubmissionStatus(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> GetCaseDetails()
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetCaseDetails();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetCaseInfo(string caseID, int CustomerID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetCaseInfo(caseID,  CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetCaseDetails(int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetCaseDetails(UserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetAppealServe(int AppealID, int CustomerID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetAppealServe(AppealID,  CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<AppealGroundM>> GetAppealGroundInfo(string CaseNumber, int AppealFiledBy)
        {
            ReturnResult<List<AppealGroundM>> result = new ReturnResult<List<AppealGroundM>>();
            try
            {
                result = _dbHandler.GetAppealGroundInfo(CaseNumber, AppealFiledBy);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<List<DocumentM>> GetUploadedDocuments(int CustomerID, string DocTitle)
        {
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var docsResult = _commonService.GetDocuments(CustomerID, false, DocTitle);
                if (docsResult.status.Status == StatusEnum.Success && docsResult.result != null)
                {
                    foreach (var doc in docsResult.result)
                    {
                        if (doc != null)
                        {
                            documents.Add(doc);
                        }
                    }
                }
                result.result = documents;
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
      
        //TBD - to be removed as we dont need to save the APpeal info
        public ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo, int CustomerID)
        {

            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                result = _dbHandler.SaveTenantAppealInfo(caseInfo,  CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(TenantAppealInfoM tenantAppealInfo, int CustomerID)
        {

            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                result = _dbHandler.SaveTenantServingAppeal(tenantAppealInfo, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> AddAnotherOpposingParty(TenantAppealInfoM tenantAppealInfo)
        {

            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.AddAnotherOpposingParty( tenantAppealInfo);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantAppealInfoM> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo)
        {
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var dbResult = _dbHandler.SaveAppealGroundInfo(tenantAppealInfo);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }

                foreach (var doc in tenantAppealInfo.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.Appeal.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                dbResult.result.Documents = documents;
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            string _loginURL = ConfigurationManager.AppSettings["loginURL"];
            List<string> _receipients = new List<string>();
            try
            {
                var dbResult = _dbHandler.SubmitTenantPetition(caseInfo);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                EmailM message = new EmailM();
                message.Subject = "Tenant Petition Filed Successfully : Case No -" + caseInfo.CaseID;
                message.MessageBody = NotificationMessage.ResourceManager.GetString("PetitionMsg").Replace("CASEID", caseInfo.CaseID).Replace("LOGIN", _loginURL);
                if (caseInfo.TenantPetitionInfo.ApplicantUserInfo.Email != null)
                {
                    _receipients.Add(caseInfo.TenantPetitionInfo.ApplicantUserInfo.Email);
                }
                if (caseInfo.bCaseFiledByThirdParty)
                {
                    if (caseInfo.TenantPetitionInfo.ThirdPartyInfo.Email != null)
                    {
                        _receipients.Add(caseInfo.TenantPetitionInfo.ThirdPartyInfo.Email);
                    }
                }
                message.RecipientAddress = _receipients;
                _emilService.SendEmail(message);
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<List<DocumentM>> SaveAppeallDocuments(List<DocumentM> documents, int CustomerID)
        {
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            List<DocumentM> _documents = new List<DocumentM>();
            try
            {
                foreach (var doc in documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.Appeal.ToString();
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
                _dbHandler.AppealUpdateAdditionalDocumentsPageSubmission(CustomerID);
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

        public ReturnResult<CaseInfoM> SubmitAppeal(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            string _loginURL = ConfigurationManager.AppSettings["loginURL"];
            List<string> _receipients = new List<string>();
            try
            {
                var dbResult = _dbHandler.SubmitAppeal(caseInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                EmailM message = new EmailM();
                message.Subject = "Appeal Filed Successfully : Case No -" + caseInfo.CaseID;
                message.MessageBody = NotificationMessage.ResourceManager.GetString("AppealMsg").Replace("CASEID", caseInfo.CaseID).Replace("LOGIN", _loginURL);
                if (caseInfo.TenantAppealInfo.ApplicantUserInfo.Email != null)
                {
                    _receipients.Add(caseInfo.TenantAppealInfo.ApplicantUserInfo.Email);
                }
                if (caseInfo.bCaseFiledByThirdParty)
                {
                    if (caseInfo.TenantAppealInfo.ThirdPartyInfo.Email != null)
                    {
                        _receipients.Add(caseInfo.TenantAppealInfo.ThirdPartyInfo.Email);
                    }
                }
                message.RecipientAddress = _receipients;
                _emilService.SendEmail(message);
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.SaveCaseDetails(caseInfo);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID, int CustomerID)
        {
            ReturnResult<List<PetitionGroundM>> result = new ReturnResult<List<PetitionGroundM>>();
            try
            {
                result = _dbHandler.GetPetitionGroundInfo(petitionID, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }


        public ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId, int CustomerID)
        {
            ReturnResult<TenantRentalHistoryM> result = new ReturnResult<TenantRentalHistoryM>();
            try
            {
                result = _dbHandler.GetRentalHistoryInfo(PetitionId, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<ThirdPartyCaseInfo>> GetThirdPartyCasesForCustomer(int CustomerID)
        {
            ReturnResult<List<ThirdPartyCaseInfo>> result = new ReturnResult<List<ThirdPartyCaseInfo>>();
            try
            {
                result = _dbHandler.GetThirdPartyCasesForCustomer(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<ThirdPartyCaseInfo>> UpdateThirdPartyAccessPrivilege(List<ThirdPartyCaseInfo> ThirdPartyCaseInfo, int CustomerID)
        {
            ReturnResult<List<ThirdPartyCaseInfo>> result = new ReturnResult<List<ThirdPartyCaseInfo>>();
            try
            {
                result = _dbHandler.UpdateThirdPartyAccessPrivilege(ThirdPartyCaseInfo, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<CaseInfoM>> GetCasesForCustomer(int CustomerID)
        {
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try
            {
                result = _dbHandler.GetCasesForCustomer(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetSelectedCase(int C_ID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetSelectedCase(C_ID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst(int UserID)
        {
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try
            {
                result = _dbHandler.GetCasesNoAnalyst(UserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                result = _dbHandler.GetTenantApplicationInfo(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.SaveApplicationInfo(caseInfo, UserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<bool> SaveTenantRentalHistoryInfo(TenantRentalHistoryM rentalHistory, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SaveTenantRentalHistoryInfo(rentalHistory,  CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> SaveTenantLostServiceInfo(LostServicesPageM message, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                result = _dbHandler.SaveTenantLostServiceInfo(message,  CustomerID);

                if(result.status.Status != StatusEnum.Success)
                {
                    return result;
                }

                foreach (var doc in message.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.TenantPetition.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }                       
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                message.Documents = documents;
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetTenantAppealInfoForReview(int AppealID, int CustomerID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetTenantAppealInfoForReview(AppealID, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public  ReturnResult<CaseInfoM> GetTenantAppealInfoForView(int C_ID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetTenantAppealInfoForView(C_ID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetPetitionViewInfo(int C_ID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetPetitionViewInfo(C_ID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetTenantResponseViewInfo(int C_ID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetTenantResponseViewInfo(C_ID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int CustomerID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var dbResult = _dbHandler.GetTenantReviewInfo(CustomerID);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result = dbResult;
                    return result;
                }
                var lostdocsResult = _commonService.GetDocuments(CustomerID, false, "TP_LostService");
                if (lostdocsResult.status.Status == StatusEnum.Success && lostdocsResult.result != null)
                {
                    foreach (var doc in lostdocsResult.result)
                    {
                        if (doc != null)
                        {
                            documents.Add(doc);
                        }
                    }
                }
                var problemdocsResult = _commonService.GetDocuments(CustomerID, false, "TP_Problem");
                if (problemdocsResult.status.Status == StatusEnum.Success && problemdocsResult.result != null)
                {
                    foreach (var doc in problemdocsResult.result)
                    {
                        if (doc != null)
                        {
                            documents.Add(doc);
                        }
                    }
                }
                dbResult.result.LostServicesPage.Documents = documents;
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<LostServicesPageM> GetTenantLostServiceInfo(int PetitionID, int CustomerID)
        {
            ReturnResult<LostServicesPageM> result = new ReturnResult<LostServicesPageM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var dbResult = _dbHandler.GetTenantLostServiceInfo(PetitionID, CustomerID);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result = dbResult;
                    return result;
                }

                //if (result.result.Documents.Where(x => x.DocTitle == "TP_LostServive").Count() == 0)
                //{
                var lostdocsResult = _commonService.GetDocuments(CustomerID, false, "TP_LostService");
                if (lostdocsResult.status.Status == StatusEnum.Success && lostdocsResult.result != null)
                {
                    foreach (var doc in lostdocsResult.result)
                    {
                        if (doc != null)
                        {
                            documents.Add(doc);
                        }
                    }
                }
                var problemdocsResult = _commonService.GetDocuments(CustomerID, false, "TP_Problem");
                if (problemdocsResult.status.Status == StatusEnum.Success && problemdocsResult.result != null)
                {
                    foreach (var doc in problemdocsResult.result)
                    {
                        if (doc != null)
                        {
                            documents.Add(doc);
                        }
                    }
                }
                dbResult.result.Documents = documents;
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };

                //}
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SavePetitionGroundInfo(petition,  CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        #region TenantResponseMethods
        public ReturnResult<CaseInfoM> GetTenantResponseExemptContestedInfo(int TenantResponseID, int CustomerID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetTenantResponseExemptContestedInfo(TenantResponseID, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetTenantResponseApplicationInfo(string CaseNumber, int CustomerID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetTenantResponseApplicationInfo(CaseNumber, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetTRAdditionalDocuments(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                model = GetUploadedDocuments(model, "TR_AdditionalDocuments");
                result.result = model;
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
        public ReturnResult<CaseInfoM> SaveTenantResponseApplicationInfo(CaseInfoM caseInfo, int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.SaveTenantResponseApplicationInfo(caseInfo, UserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> SaveTenantResponseExemptContestedInfo(TenantResponseExemptContestedInfoM message, int CustomerID)            
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SaveTenantResponseExemptContestedInfo(message, CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> SaveTenantResponseRentalHistoryInfo(TenantResponseRentalHistoryM rentalHistory, int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                result = _dbHandler.SaveTenantResponseRentalHistoryInfo(rentalHistory, CustomerID);
                if (result.status.Status != StatusEnum.Success)
                {
                    return result;
                }

                foreach (var doc in rentalHistory.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.TenantResponse.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                rentalHistory.Documents = documents;
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }


        public ReturnResult<TenantResponseRentalHistoryM> GetTenantResponseRentalHistoryInfo(int TenantResponseID, int CustomerID)
        {
            ReturnResult<TenantResponseRentalHistoryM> result = new ReturnResult<TenantResponseRentalHistoryM>();
            try
            {
                result = _dbHandler.GetTenantResponseRentalHistoryInfo(TenantResponseID, CustomerID);
                if (result.status.Status != StatusEnum.Success)
                {
                    return result;
                }

                if (result.result.Documents.Where(x => x.DocTitle == "TR_RentalHistoryNotice").Count() == 0)
                {
                    var docsResult = _commonService.GetDocuments(CustomerID, false, "TR_RentalHistoryNotice");
                    if (docsResult.status.Status == StatusEnum.Success && docsResult.result != null)
                    {
                        foreach (var doc in docsResult.result)
                        {
                            if (doc != null)
                            {
                                result.result.Documents.Add(doc);
                            }
                        }
                    }
                }

                if (result.result.Documents.Where(x => x.DocTitle == "TR_RentalHistoryLease").Count() == 0)
                {
                    var docsResult = _commonService.GetDocuments(CustomerID, false, "TR_RentalHistoryLease");
                    if (docsResult.status.Status == StatusEnum.Success && docsResult.result != null)
                    {
                        foreach (var doc in docsResult.result)
                        {
                            if (doc != null)
                            {
                                result.result.Documents.Add(doc);
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetTenantResponseReviewInfo(string CaseNumber, int CustomerID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            ReturnResult<CaseInfoM> responseResult = new ReturnResult<CaseInfoM>();
            try
            {
                responseResult = _dbHandler.GetTenantResponseReviewInfo(CaseNumber, CustomerID);

                if (responseResult.status.Status != StatusEnum.Success)
                {
                    return result;
                }

                if (responseResult.result.TenantResponseInfo.TenantRentalHistory.Documents.Where(x => x.DocTitle == "TR_RentalHistoryNotice").Count() == 0)
                {
                    var docsResult = _commonService.GetDocuments(CustomerID, false, "TR_RentalHistoryNotice");
                    if (docsResult.status.Status == StatusEnum.Success && docsResult.result != null)
                    {
                        foreach (var doc in docsResult.result)
                        {
                            if (doc != null)
                            {
                                responseResult.result.TenantResponseInfo.TenantRentalHistory.Documents.Add(doc);
                            }
                        }
                    }
                }

                if (responseResult.result.TenantResponseInfo.TenantRentalHistory.Documents.Where(x => x.DocTitle == "TR_RentalHistoryLease").Count() == 0)
                {
                    var docsResult = _commonService.GetDocuments(CustomerID, false, "TR_RentalHistoryLease");
                    if (docsResult.status.Status == StatusEnum.Success && docsResult.result != null)
                    {
                        foreach (var doc in docsResult.result)
                        {
                            if (doc != null)
                            {
                                responseResult.result.TenantResponseInfo.TenantRentalHistory.Documents.Add(doc);
                            }
                        }
                    }
                }

                CaseInfoM caseInfo = new CaseInfoM();

                if (responseResult.status.Status == StatusEnum.Success)
                {
                    caseInfo.CustomerID = CustomerID;
                    caseInfo = GetTRAdditionalDocuments(caseInfo).result;
                    caseInfo.TenantResponseInfo = responseResult.result.TenantResponseInfo;
                    caseInfo.CaseID = responseResult.result.CaseID;
                }
                else
                {
                    result.result = null;
                    result.status = responseResult.status;
                }

                
                result.result = caseInfo;
                result.status = responseResult.status;
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<List<DocumentM>> SaveTenantDocuments(List<DocumentM> documents, int customerID)
        {
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            List<DocumentM> _documents = new List<DocumentM>();
            try
            {
                foreach (var doc in documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.TenantPetition.ToString();
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
                _dbHandler.TenantUpdateAdditionalDocumentsPageSubmission(customerID);
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
        public ReturnResult<CaseInfoM> SubmitTenantResponse(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            string _loginURL = ConfigurationManager.AppSettings["loginURL"];
            List<string> _receipients = new List<string>();
            try
            {
               var dbResult = _dbHandler.SubmitTenantResponse(caseInfo);
               if (dbResult.status.Status != StatusEnum.Success)
               {
                   result.status = dbResult.status;
                   return result;
               }
                EmailM message = new EmailM();
                message.Subject = "Tenant Response Filed Successfully : Case No -" + caseInfo.CaseID;
                message.MessageBody = NotificationMessage.ResourceManager.GetString("ResponseMsg").Replace("CASEID", caseInfo.CaseID).Replace("LOGIN", _loginURL);
                if (caseInfo.TenantResponseInfo.ApplicantUserInfo.Email!= null)
                {
                    _receipients.Add(caseInfo.TenantResponseInfo.ApplicantUserInfo.Email);
                }
                if (caseInfo.bCaseFiledByThirdParty)
                {
                    if (caseInfo.TenantResponseInfo.ThirdPartyInfo.Email != null)
                    {
                        _receipients.Add(caseInfo.TenantResponseInfo.ThirdPartyInfo.Email);
                    }
                }
                message.RecipientAddress = _receipients;
                _emilService.SendEmail(message);
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> SaveTRAdditionalDocuments(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.TenantResponse.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                model.Documents = documents;
                result.result = model;
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
        #endregion TenantResponseMethods

        #region Common File Petition methods
        public ReturnResult<CaseInfoM> GetPetitioncategory()
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetPetitioncategory();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        
        #endregion

        #region Get Owner Petition Methods
        public ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                string RAPFee = ConfigurationManager.AppSettings["RAPFee"];
                var dbResult = _dbHandler.GetOwnerApplicantInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                dbResult.result.OwnerPetitionInfo.ApplicantInfo.RAPFee = string.IsNullOrEmpty(RAPFee) ? "69" : RAPFee;
                model= GetUploadedDocuments(model, "OP_BusinessTaxProof");
                model = GetUploadedDocuments(model, "OP_PropertyServiceFee");

                //if (model.Documents.Where(x => x.DocTitle == "OP_BusinessTaxProof").Count() == 0)
                //{
                //    var docsResult = _commonService.GetDocuments(model.OwnerPetitionInfo.ApplicantInfo.CustomerID, false, "OP_BusinessTaxProof");
                //    if (docsResult.status.Status == StatusEnum.Success && docsResult.result !=null)
                //    {
                //        model.Documents.Add(docsResult.result);
                //    }
                //}
                //if (model.Documents.Where(x => x.DocTitle == "OP_PropertyServiceFee").Count() == 0)
                //{
                //    var docsResult = _commonService.GetDocuments(model.OwnerPetitionInfo.ApplicantInfo.CustomerID, false, "OP_PropertyServiceFee");
                //    if (docsResult.status.Status == StatusEnum.Success && docsResult.result !=null)
                //    {
                //        model.Documents.Add(docsResult.result);
                //    }
                //}
                ////string[] titles = { "OP_BusinessTaxProof", "OP_PropertyServiceFee" };
                
                //var docsResult = _commonService.GetDocuments(model.OwnerPetitionInfo.ApplicantInfo.CustomerID, false, titles);
                //if (docsResult.status.Status == StatusEnum.Success)
                //{

                //    List<DocumentM> docs = new List<DocumentM>();
                //    foreach (var doc in docsResult.result)
                //    {
                //        if (model.Documents.Where(x => x.DocTitle == doc.DocTitle).ToList().Count == 0)
                //        {
                //            var doccumentResult = _documentService.DownloadDocument(doc);
                //            if (doccumentResult.status.Status == StatusEnum.Success)
                //            {
                //                model.Documents.Add(doccumentResult.result);
                //            }
                //        }
                //    }
                    
                //}
                result.result = dbResult.result;
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

        public ReturnResult<CaseInfoM> GetRentIncreaseReasonInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<OwnerRentIncreaseReasonsM> _reasons = new List<OwnerRentIncreaseReasonsM>();
            try
            {
                var dbResult = _dbHandler.GetRentIncreaseReasonInfo(model.OwnerPetitionInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.RentIncreaseReasons = dbResult.result;
                model = GetUploadedDocuments(model, "OP_Justification");
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOwnerPropertyAndTenantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOwnerPropertyAndTenantInfo(model.OwnerPetitionInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.PropertyInfo = dbResult.result;
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOwnerRentIncreaseAndPropertyInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOwnerRentIncreaseAndPropertyInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model = dbResult.result;
                model = GetUploadedDocuments(model, "OP_RAPNotice1");
                model = GetUploadedDocuments(model, "OP_RAPNotice2");
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOwnerAdditionalDocuments(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                model = GetUploadedDocuments(model, "OP_AdditionalDocuments");
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOwnerReview(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOwnerReview(model);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model = dbResult.result;
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOwnerReviewByCaseID(int C_ID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOwnerReviewByCaseID(C_ID);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                result.result = dbResult.result;
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
        public ReturnResult<CaseInfoM> GetOResponseViewByCaseID(int C_ID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOResponseViewByCaseID(C_ID);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                result.result = dbResult.result;
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
        #endregion

        #region Save Owner Petition Methods
        public ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var dbResult = _dbHandler.SaveOwnerApplicantInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }

                foreach(var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerPetition.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                        else
                        {
                            documents.Add(doc);
                        }
                    }
                dbResult.result.Documents = documents;
                result.result = dbResult.result;
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


        public ReturnResult<CaseInfoM> SaveRentIncreaseReasonInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var dbResult = _dbHandler.SaveRentIncreaseReasonInfo(model.OwnerPetitionInfo);  
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerPetition.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                model.Documents = documents;
                result.result = model;
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

        public ReturnResult<CaseInfoM> SaveOwnerPropertyAndTenantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveOwnerPropertyAndTenantInfo(model.OwnerPetitionInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.PropertyInfo = dbResult.result;
                result.result = model;
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

        public ReturnResult<CaseInfoM> SaveOwnerRentIncreaseAndUpdatePropertyInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var dbResult = _dbHandler.SaveOwnerRentIncreaseAndUpdatePropertyInfo(model.OwnerPetitionInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }

                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerPetition.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }                       
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                model.OwnerPetitionInfo.PropertyInfo = dbResult.result;
                result.result = model;
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

        public ReturnResult<CaseInfoM> SaveOwnerAdditionalDocuments(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerPetition.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                _dbHandler.OwnerUpdateAdditionalDocumentsPageSubmission(model.CustomerID);
                model.Documents = documents;
                result.result = model;
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

        public ReturnResult<bool> SaveOwnerReviewPageSubmission(int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _dbHandler.OwnerUpdateReviewPageSubmission(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> SubmitOwnerPetition(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                string _loginURL = ConfigurationManager.AppSettings["loginURL"];
                List<string> _receipients = new List<string>();
                var dbResult = _dbHandler.SubmitOwnerPetition(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                EmailM message = new EmailM();
                message.Subject = "Owner Petition Filed Successfully : Case No -" + model.CaseID;
                message.MessageBody = NotificationMessage.ResourceManager.GetString("PetitionMsg").Replace("CASEID",model.CaseID).Replace("LOGIN",_loginURL);
                if (model.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.Email != null)
                {
                   _receipients.Add(model.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.Email);
                }
                if(model.bCaseFiledByThirdParty)
                {
                    if(model.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.Email !=null)
                    {
                        _receipients.Add(model.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.Email);
                    }
                }
                message.RecipientAddress = _receipients;
                _emilService.SendEmail(message);
                result.result = model;
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

        private CaseInfoM GetUploadedDocuments(CaseInfoM model, string DocTitle)
        {
            //if (model.Documents.Where(x => x.DocTitle == DocTitle).Count() == 0)
            //{
                var docsResult = _commonService.GetDocuments(model.CustomerID, false, DocTitle);
                if (docsResult.status.Status == StatusEnum.Success && docsResult.result != null)
                {
                    foreach (var doc in docsResult.result)
                    {
                        if (doc != null)
                        {
                            if (!model.Documents.Where(d => d.DocID == doc.DocID).Any())
                            {
                                model.Documents.Add(doc);
                            }
                        }
                    }
                }
            //}
            return model;
        }
        
        #endregion

        #region Get Owner Response Methods
        public ReturnResult<CaseInfoM> GetOResponseApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                string RAPFee = ConfigurationManager.AppSettings["RAPFee"];
                var dbResult = _dbHandler.GetOResponseApplicantInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                dbResult.result.OwnerResponseInfo.ApplicantInfo.RAPFee = string.IsNullOrEmpty(RAPFee) ? "69" : RAPFee;
                model = dbResult.result;
                model = GetUploadedDocuments(model, "OR_BusinessTaxProof");
                model = GetUploadedDocuments(model, "OR_PropertyServiceFee");              
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOResponsePropertyAndTenantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOResponsePropertyAndTenantInfo(model.OwnerResponseInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerResponseInfo.PropertyInfo = dbResult.result;
                result.result = model;
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
        public ReturnResult<CaseInfoM> GetOResponseRentIncreaseAndPropertyInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOResponseRentIncreaseAndPropertyInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model = dbResult.result;
                model = GetUploadedDocuments(model, "OR_RAPNotice1");
                model = GetUploadedDocuments(model, "OR_RAPNotice2");
                model = GetUploadedDocuments(model, "OR_Justification");            
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOResponseDecreasedHousing(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                model = GetUploadedDocuments(model, "OR_DecreasedHousing");
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOResponseExemption(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOResponseExemption(model.OwnerResponseInfo.PropertyInfo);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerResponseInfo.PropertyInfo = dbResult.result;
                result.result = model;
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

        public ReturnResult<CaseInfoM> GetOResponseAdditionalDocuments(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                model = GetUploadedDocuments(model, "OR_AdditionalDocuments");
                result.result = model;
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
        public ReturnResult<CaseInfoM> GetOResponseReview(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOResponseReview(model);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model = dbResult.result;
                result.result = model;
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

        public ReturnResult<OwnerResponsePageSubnmissionStatusM> GetOResponseSubmissionStatus(int CustomerID)
        {
            ReturnResult<OwnerResponsePageSubnmissionStatusM> result = new ReturnResult<OwnerResponsePageSubnmissionStatusM>();
            try
            {
                result = _dbHandler.GetOResponseSubmissionStatus(CustomerID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

       

        #endregion

        #region Save Owner Response Methods
        public ReturnResult<CaseInfoM> SaveOResponseApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                var dbResult = _dbHandler.SaveOResponseApplicantInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }

                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerResponse.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                model.Documents = documents;
                result.result = dbResult.result;
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

        public ReturnResult<CaseInfoM> SaveOResponsePropertyAndTenantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveOResponsePropertyAndTenantInfo(model.OwnerResponseInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerResponseInfo.PropertyInfo = dbResult.result;
                result.result = model;
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

        public ReturnResult<CaseInfoM> SaveOResponseRentIncreaseAndUpdatePropertyInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveOResponseRentIncreaseAndUpdatePropertyInfo(model.OwnerResponseInfo.PropertyInfo);
                List<DocumentM> documents = new List<DocumentM>();
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerResponseInfo.PropertyInfo = dbResult.result;

                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerResponse.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                model.Documents = documents;
                result.result = model;
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

        public ReturnResult<CaseInfoM> SaveOResponseAdditionalDocuments(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerResponse.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                _dbHandler.OResponseUpdateAdditionalDocumentsPageSubmission(model.CustomerID);
                model.Documents = documents;
                result.result = model;
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
        public ReturnResult<CaseInfoM> SaveOResponseDecreasedHousing(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<DocumentM> documents = new List<DocumentM>();
            try
            {
                foreach (var doc in model.Documents)
                {
                    if (!doc.isUploaded)
                    {
                        doc.DocCategory = DocCategory.OwnerResponse.ToString();
                        var docUploadResut = _documentService.UploadDocument(doc);
                        if (docUploadResut.status.Status == StatusEnum.Success)
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                _dbHandler.OResponseUpdateDecreasedHousingPageSubmission(model.CustomerID);
                model.Documents = documents;
                result.result = model;
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

        public ReturnResult<CaseInfoM> SaveOResponseExemption(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveOResponseExemption(model.OwnerResponseInfo.PropertyInfo);
              
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerResponseInfo.PropertyInfo = dbResult.result;               
                result.result = model;
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

        public ReturnResult<bool> SaveOResponseReviewPageSubmission(int CustomerID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _dbHandler.OResponseUpdateReviewPageSubmission(CustomerID);          
               return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> SubmitOwnerResponse(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            string _loginURL = ConfigurationManager.AppSettings["loginURL"];
            List<string> _receipients = new List<string>();
            try
            {
                var dbResult = _dbHandler.SubmitOwnerResponse(model);

                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                EmailM message = new EmailM();
                message.Subject = "Owner Response Filed Successfully : Case No -" + model.OwnerResponseInfo.ApplicantInfo.CaseRespondingTo;
                message.MessageBody = NotificationMessage.ResourceManager.GetString("ResponseMsg").Replace("CASEID", model.OwnerResponseInfo.ApplicantInfo.CaseRespondingTo).Replace("LOGIN", _loginURL);
                if (model.OwnerResponseInfo.ApplicantInfo.ApplicantUserInfo.Email != null)
                {
                    _receipients.Add(model.OwnerResponseInfo.ApplicantInfo.ApplicantUserInfo.Email);
                }
                if (model.bCaseFiledByThirdParty)
                {
                    if (model.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser.Email != null)
                    {
                        _receipients.Add(model.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser.Email);
                    }
                }
                message.RecipientAddress = _receipients;
                _emilService.SendEmail(message);
                model = dbResult.result;
                result.result = model;
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
        #endregion
    }
}
