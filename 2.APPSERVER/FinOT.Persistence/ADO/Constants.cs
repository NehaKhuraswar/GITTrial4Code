using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Persistence.ADO
{
    public struct Connection
    {
        public const string RCMANAGEMENT = "Connection_RCManagement";
        public const string FSAREFERENCE = "Connection_Fsareference";
    }

    public struct Procedure
    {
        //Get WorkQueue and Search
        public const string WorkQueue = "USP_WorkQueue";
        public const string SearchRC = "USP_SearchRC";
        public const string SearchRCRequest = "USP_SearchRCRequest";
        //Get RC Request SPs
        public const string GetRCDetails = "USP_RCDetails_Get";
        public const string GetOTRequest = "USP_OTRequest_Get";
        public const string GetRCReqAccessConfig = "USP_RCReqAccessConfig_Get";
        public const string GetRCReqMHYFunding = "USP_RCReqMHYFunding_Get";
        public const string GetRCReqExpenseCodes= "USP_RCReqExpenseCodes_Get";
        public const string GetRCReqRevenueCodes = "USP_RCReqRevenueCodes_Get";
        public const string GetRCReqJustification = "USP_RCReqJustification_Get";
        public const string GetRCReqA6Config = "USP_RCReqA6Config_Get";
        public const string GetOTReqNotes = "USP_OTReqNotes_Get";
        public const string GetRCReqReturnStatus = "USP_RCReqReturnStatus_Get";
        //Save RC Request SPs
        public const string SaveOTReqHeader = "USP_OTRequest_SaveNew";
        public const string SaveRCReqRCCode = "USP_RCReqRCCode_Save";
        public const string SaveRCReqMHYFunding = "USP_RCReqMHYFunding_Save";
        public const string SaveRCReqExpenseCodes = "USP_RCReqExpenseCodes_Save";
        public const string SaveRCReqRevenueCodes = "USP_RCReqRevenueCodes_Save";
        public const string SaveRCReqJustification = "USP_RCReqJustification_Save";
        public const string SaveRCReqA6Config = "USP_RCReqA6Config_Save";
        public const string SaveOTReqNotes = "USP_OTReqNotes_Save";
        public const string SaveWorkflowAction = "USP_RCReqWorkflow_Save";
        
        //Document SPs
        public const string GetRCReqDocuments = "USP_RCReqDocuments_Get";
        public const string SaveRCReqDocuments = "USP_RCReqDocuments_Save";
        public const string GetDocumentContent = "USP_DocumentContent_Get";

        //Article-6 Configuration
        public const string GetA6ConfigLines = "USP_A6ConfigLines_Get";
        public const string GetA6ConfigLevel = "USP_A6ConfigLevel_Get";
        public const string SaveA6ConfigLines = "USP_A6ConfigLines_Save";
        public const string GetA6ConfigNotes = "USP_A6ConfigNotes_Get";

        //Master Data
        public const string GetFiscalYear = "USP_FiscalYear_Get";
        public const string GetPendingRestructure = "USP_PendingRestructure_Get";
        public const string GetDivision = "USP_Division_Get";
        public const string GetBureau = "USP_Bureau_Get";
        public const string GetProgram = "USP_Program_Get";
        public const string GetFundingType = "USP_FundingType_Get";
        public const string GetFundingClass = "USP_FundingClass_Get";
        public const string GetFundingSource = "USP_FundingSource_Get";
        public const string GetGrants = "USP_RMSGrants_Get";
        public const string GetBCode = "USP_BCode_Get";
        public const string GetRSCode = "USP_RSCode_Get";
        public const string GetMHYSector = "USP_MHYSector_Get";
        public const string GetFundingStream = "USP_FundingStream_Get";
        public const string GetA6ReimbursementRate = "USP_A6ReimbursementRate_Get";
        public const string GetDocumentCategory = "USP_DocumentCategory_Get";
        public const string GetDocumentExtension = "USP_DocumentExtension_Get";
        public const string GetFourthCharacter = "USP_FourthCharacter_Get";
        public const string GetStatusList = "USP_StatusList_Get";
        public const string GetExpenseType = "USP_ExpenseType_Get";

    }
}
