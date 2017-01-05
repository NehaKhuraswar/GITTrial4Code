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
              var serviceObj = ConvertToServiceObj(doc);
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
              string docAuthor = ConfigurationManager.AppSettings["DocAuthor"];
              string docType = ConfigurationManager.AppSettings["DocType"];
              string securityGroup = ConfigurationManager.AppSettings["SecurityGroup"];
              CheckInService.IdcProperty[] idcProperty = new CheckInService.IdcProperty[0];
              CheckInService.IdcFile idcFile = new CheckInService.IdcFile();
              var serviceResult = checkInService.CheckInUniversal(doc.DocName.Replace(" ","").Trim(), doc.DocTitle, docType, docAuthor, securityGroup, "", idcProperty, serviceObj, idcFile, idcProperty);
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
      //To be removed
      //private DocumentM getDocumentObj(HttpPostedFile file)
      //{
      //    DocumentM doc = new DocumentM();
      //    doc.DocName = file.FileName;
            
      //    BinaryReader b = new BinaryReader(file.InputStream);
      //    var content = b.ReadBytes(file.ContentLength);
      //    if(content !=null)
      //    {
      //        doc.Content = content;
      //    }

      //    return doc;
      //}
    }
}
