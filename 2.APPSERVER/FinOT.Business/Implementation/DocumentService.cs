using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using System.ServiceModel;
using RAP.Core.Common;
using RAP.Core.DataModels;
using RAP.Core.Services;



namespace RAP.Business.Implementation
{
  public class DocumentService : IDocumentService
    {
      private readonly IExceptionHandler _eHandler;
      private readonly ICommonService _commonService;
      public DocumentService(IExceptionHandler eHandler, ICommonService commonService)
      {
          this._eHandler = eHandler;
          this._commonService = commonService;
      }
      public ReturnResult<DocumentM> UploadDocument(DocumentM doc)
      {
          ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
          try
          {             
              string endpoint = ConfigurationManager.AppSettings["WebcenterEndPoint"];
              BasicHttpBinding myBinding = new BasicHttpBinding();
              myBinding.Security.Mode = BasicHttpSecurityMode.Transport;
              myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
              myBinding.MaxReceivedMessageSize =Convert.ToInt64(ConfigurationManager.AppSettings["MaxReceivedMessageSize"]);
              myBinding.MaxBufferSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxBufferSize"]);
              myBinding.MaxBufferPoolSize = Convert.ToInt64(ConfigurationManager.AppSettings["MaxBufferPoolSize"]);
              EndpointAddress ea = new EndpointAddress(endpoint);
              CheckInService.CheckInSoapClient checkInService = new CheckInService.CheckInSoapClient(myBinding,ea);
              checkInService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WebcenterUserName"];
              checkInService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WebcenterPassword"];
              string docType = ConfigurationManager.AppSettings["DocType"];
              string docAuthor = ConfigurationManager.AppSettings["DocAuthor"];
              string securityGroup = ConfigurationManager.AppSettings["SecurityGroup"]; 
              string docAccount = ConfigurationManager.AppSettings["DocAccount"];                    
              var customDocMetaData = GetCustomDocMetaData(doc);
              var serviceObj = ConvertToServiceObj(doc);            
              var serviceResult = checkInService.CheckInUniversal(null, doc.DocName.Replace(" ", "").Trim(), docType, docAuthor, securityGroup, docAccount, customDocMetaData.ToArray(), serviceObj, null, null);
              if(serviceResult == null)
              {
                  throw new Exception("Document upload failed for the document" + doc.DocName);               
              }
              doc.DocThirdPartyID = serviceResult.dID;
              if (doc.DocThirdPartyID > 0)
              {
                  doc.isUploaded = true;
                  if (doc.C_ID != null && doc.IsPetitonFiled == true)
                  {
                      var saveDocumentResult = _commonService.SaveCaseDocument(doc);
                      if (saveDocumentResult.status.Status != StatusEnum.Success)
                      {
                          throw new Exception("SaveCaseDocument for the document" + doc.DocName);
                      }
                      doc.DocID = saveDocumentResult.result.DocID;
                  }
                  else
                  {
                      var saveDocumentResult = _commonService.SaveDocument(doc);
                      if (saveDocumentResult.status.Status != StatusEnum.Success)
                      {
                          throw new Exception("Save document for the document" + doc.DocName);
                      }
                      doc.DocID = saveDocumentResult.result.DocID;
                  }
              }
              doc.Base64Content = string.Empty;
              doc.Content = null;
              result.status = new OperationStatus() { Status = StatusEnum.Success };
              result.result = doc;
              return result;
          }
          catch(Exception ex)
          {
              result.status = _eHandler.HandleException(ex);
              _commonService.LogError(result.status);
              return result;
          }    
      }

      public ReturnResult<DocumentM> DownloadDocument(DocumentM doc)
      {
          ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
          try
          {
              string endpoint = ConfigurationManager.AppSettings["WebcenterEndPoint"];
              BasicHttpBinding myBinding = new BasicHttpBinding();
              myBinding.Security.Mode = BasicHttpSecurityMode.Transport;
              myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
              myBinding.MaxReceivedMessageSize = Convert.ToInt64(ConfigurationManager.AppSettings["MaxReceivedMessageSize"]);
              myBinding.MaxBufferSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxBufferSize"]);
              myBinding.MaxBufferPoolSize = Convert.ToInt64(ConfigurationManager.AppSettings["MaxBufferPoolSize"]);
              EndpointAddress ea = new EndpointAddress(endpoint);
              GetFileService.GetFileSoapClient getFileService = new GetFileService.GetFileSoapClient(myBinding, ea);
              getFileService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WebcenterUserName"];
              getFileService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WebcenterPassword"];
              GetFileService.GetFileByIDResult serviceResult = getFileService.GetFileByID(doc.DocThirdPartyID, null, null);

              if (serviceResult == null || serviceResult.downloadFile == null)
              {
                  throw new Exception("Download document failed " + doc.DocName);
              }
              var content = serviceResult.downloadFile.fileContent;
              doc.Base64Content = Convert.ToBase64String(content);
                            
              result.result = doc;
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
      private CheckInService.IdcFile ConvertToServiceObj(DocumentM doc)
      {
          CheckInService.IdcFile serviceObj = new CheckInService.IdcFile();
          doc.Content = Convert.FromBase64String(doc.Base64Content);
          if(!string.IsNullOrEmpty(doc.DocName) && doc.Content != null)
          {
              serviceObj.fileName = doc.DocName;
              serviceObj.fileContent = doc.Content;
          }
          return serviceObj;
      }

      private List<CheckInService.IdcProperty> GetCustomDocMetaData(DocumentM doc)
      {
          List<CheckInService.IdcProperty> customDocMetaData = new List<CheckInService.IdcProperty>();
          int RefID;
          customDocMetaData.Add(new CheckInService.IdcProperty() { name = "xProfileTrigger", value = ConfigurationManager.AppSettings["xProfileTrigger"] });
          customDocMetaData.Add(new CheckInService.IdcProperty() { name = "xIdcProfile", value = ConfigurationManager.AppSettings["xIdcProfile"] });
          customDocMetaData.Add(new CheckInService.IdcProperty() { name = "xAgencyDepartment", value = ConfigurationManager.AppSettings["xAgencyDepartment"] });
          customDocMetaData.Add(new CheckInService.IdcProperty() { name = "xDivision", value = ConfigurationManager.AppSettings["xDivision"] });
          var refIDResult = _commonService.GetDocReferenceID(doc);
          if(refIDResult.status.Status == StatusEnum.Success)
          {
              RefID = refIDResult.result;
          }
          else
          {
              RefID = 6312;
          }
          customDocMetaData.Add(new CheckInService.IdcProperty() { name = "xReferenceType", value = RefID.ToString() });
          return customDocMetaData;
      }
      
    }
}
