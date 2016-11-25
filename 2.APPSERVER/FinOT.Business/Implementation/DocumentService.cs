using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ServiceModel;
using RAP.Core.Common;
using RAP.Core.DataModels;
using RAP.Core.Services;
using RAP.Business.CheckInService;


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
              BasicHttpBinding myBinding = new BasicHttpBinding();
              myBinding.Security.Mode = BasicHttpSecurityMode.Transport;
              myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
              myBinding.MaxReceivedMessageSize =Convert.ToInt64(ConfigurationManager.AppSettings["MaxReceivedMessageSize"]);
              myBinding.MaxBufferSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxBufferSize"]);
              myBinding.MaxBufferPoolSize = Convert.ToInt64(ConfigurationManager.AppSettings["MaxBufferPoolSize"]);
              EndpointAddress ea = new EndpointAddress(endpoint);
              CheckInSoapClient checkInService = new CheckInSoapClient(myBinding,ea);
              checkInService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WebcenterUserName"];
              checkInService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WebcenterPassword"];
              string docAuthor = ConfigurationManager.AppSettings["DocAuthor"];
              string docType = ConfigurationManager.AppSettings["DocType"];
              string securityGroup = ConfigurationManager.AppSettings["SecurityGroup"];
              IdcProperty[] idcProperty = new IdcProperty[0];
              IdcFile idcFile = new IdcFile();
              var serviceResult = checkInService.CheckInUniversal(doc.DocName, doc.DocTitle, docType, docAuthor, securityGroup, "", idcProperty, serviceObj, idcFile, idcProperty);
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
