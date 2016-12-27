using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.Business.Helper;
using RAP.DAL;
using System.Configuration;
namespace RAP.Business.Implementation
{
    public class ApplicationProcessingService : IApplicationProcessingService
    {
        public string CorrelationId { get; set; }
        private readonly IApplicationProcessingDBHandler _dbHandler;
        private readonly IExceptionHandler _eHandler;
        private readonly ICommonService _commonService;
        private readonly IDocumentService _documentService;
        //TBD
        //public ApplicationProcessingService()
        //{
        //    _dbHandler = new ApplicationProcessingDBHandler();
        //}
        public ApplicationProcessingService(IApplicationProcessingDBHandler dbHandler, IExceptionHandler eHandler, ICommonService commonService, IDocumentService documentService)
        {
            this._dbHandler = dbHandler;
            this._eHandler = eHandler;
            this._commonService = commonService;
            this._documentService = documentService;
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
        public ReturnResult<CaseInfoM> GetAppealServe(int AppealID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetAppealServe(AppealID);
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
            try
            {
                result = _dbHandler.SaveAppealGroundInfo(tenantAppealInfo);
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
            try
            {
                result = _dbHandler.SubmitTenantPetition(caseInfo);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> SubmitAppeal(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.SubmitAppeal(caseInfo);
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
        public ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID)
        {
            ReturnResult<List<PetitionGroundM>> result = new ReturnResult<List<PetitionGroundM>>();
            try
            {
                result = _dbHandler.GetPetitionGroundInfo(petitionID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

       
        public ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId)
        {
            ReturnResult<TenantRentalHistoryM> result = new ReturnResult<TenantRentalHistoryM>();
            try
            {
                result = _dbHandler.GetRentalHistoryInfo(PetitionId);
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

        public ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst()
        {
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try
            {
                result = _dbHandler.GetCasesNoAnalyst();
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
        public ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int CustomerID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                result = _dbHandler.GetTenantReviewInfo(CustomerID);
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
            try
            {
                result = _dbHandler.GetTenantLostServiceInfo(PetitionID);
                if(result.status.Status != StatusEnum.Success)
                {
                return result;
            }
                
                if (result.result.Documents.Where(x => x.DocTitle == "TP_LostServive").Count() == 0)
                {
                    var docsResult = _commonService.GetDocuments(CustomerID, false, "TP_LostServive");
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
                model = GetUploadedDocuments(model, "OP_RAPNotice");
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
        public ReturnResult<CaseInfoM> SubmitOwnerPetition(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SubmitOwnerPetition(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }               
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
            if (model.Documents.Where(x => x.DocTitle == DocTitle).Count() == 0)
            {
                var docsResult = _commonService.GetDocuments(model.CustomerID, false, DocTitle);
                if (docsResult.status.Status == StatusEnum.Success && docsResult.result != null)
                {
                    foreach (var doc in docsResult.result)
                    {
                        if (doc != null)
                        {
                            model.Documents.Add(doc);
                        }
                    }
                }
            }
            return model;
        }
        
        #endregion
    }
}
