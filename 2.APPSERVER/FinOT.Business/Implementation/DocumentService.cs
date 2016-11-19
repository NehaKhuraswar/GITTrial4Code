using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RAP.Core.Common;
using RAP.Core.DataModels;
using RAP.Core.Services;
using RAP.Business.Proxy;

namespace RAP.Business.Implementation
{
  public class DocumentService : IdocumentService
    {
      public ReturnResult<DocumentM> UploadDocument(DocumentM doc)
      {
          ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
          try
          {
              var serviceObj = ConvertToServiceObj(doc);
              string endpoint = ConfigurationManager.AppSettings["WebcenterEndPoint"];
              
              CheckInSoapClient checkInService = new CheckInSoapClient();              
              checkInService.ClientCredentials.UserName.UserName = "intrapapp01";
              //checkInServiceClientCredentials.UserName.Password = "intrapapp01";
              var serviceResult =  checkInService.CheckInUniversal(doc.DocName, doc.DocTitle, doc.DocType, "RAP", "", "", null, serviceObj, null, null);
              if(serviceResult == null)
              {
                  result.status = new OperationStatus() { Status = StatusEnum.UploadFailed };
                  return result;
              }
              doc.DocThirdPartyID = serviceResult.dID;
              result.status = new OperationStatus() { Status = StatusEnum.Success };
              return result;
          }
          catch(Exception ex)
          {
              IExceptionHandler eHandler = new ExceptionHandler();
              result.status = eHandler.HandleException(ex);
              return result;
          }    
      }

      private IdcFile ConvertToServiceObj(DocumentM doc)
      {
          IdcFile serviceObj = new IdcFile();

          if(!string.IsNullOrEmpty(doc.DocName) && doc.Content != null)
          {
              serviceObj.fileName = doc.DocName;
              serviceObj.fileContent = doc.Content;
          }
          return serviceObj;
      }
    }
}
