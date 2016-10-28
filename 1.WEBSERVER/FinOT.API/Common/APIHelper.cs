using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Diagnostics;
using RAP.Core.FinServices.APIService;
using RAP.Core.Common;


namespace RAP.API.Common
{
    public class APIHelper
    {
        private string endpoint;
        private APIServiceClient apiClient;
        public APIHelper()
        {
            endpoint = WebConfigurationManager.AppSettings[Constants.FINAPISERVICE_ENDPOINT];
        }

        public ApplicationHeader GetAppHeader(string Username, string CorrelationId)
        {
            using (apiClient = new APIServiceClient(endpoint))
            {
                RequesterDetails requester = GetRequester(CorrelationId, Username);
                RetrieveApplicationHeaderResponse response = apiClient.RetrieveApplicationHeader(requester);
                if (response.StatusCode == "SUCCESS")
                {
                    return response.Header;
                }
                else
                {
                    throw new Exception(response.Messages.First().Message);
                }
            }
        }

        public IList<int> GetUserRoles(string Username, string CorrelationId)
        {
            IList<UserPrivilege> lst = GetUserPrivileges(Username, CorrelationId);
            return lst.Select(s => s.RoleId).Distinct().ToList<int>();
        }

        public IList<UserPrivilege> GetUserPrivileges(string Username, string CorrelationId)
        {
            using (apiClient = new APIServiceClient(endpoint))
            {
                RetrieveUserPrivilegesRequest request = new RetrieveUserPrivilegesRequest() { ApplicationId = Constants.APPLICATION_ID, Username = Username };
                RetrieveUserPrivilegesResponse response = apiClient.RetrieveUserPrivileges(GetRequester(CorrelationId, Username), request);
                if (response.StatusCode == "SUCCESS")
                {
                    return response.UserPrivileges.ToList<UserPrivilege>();
                }
                else
                {
                    throw new Exception(response.Messages.First().Message);
                }
            }
        }

        public IList<JobAid> GetJobAids(string Username, string CorrelationId)
        {
            using (apiClient = new APIServiceClient(endpoint))
            {
                RetrieveJobAidsRequest request = new RetrieveJobAidsRequest() { ApplicationId = Constants.APPLICATION_ID };
                RetrieveJobAidsResponse response = apiClient.RetrieveJobAids(GetRequester(CorrelationId, Username), request);
                if (response.StatusCode == "SUCCESS")
                {
                    return response.JobAids;
                }
                else
                {
                    throw new Exception(response.Messages.First().Message);
                }
            }
        }

        public RequesterDetails GetRequester(string CorrelationId, string Username, string Domain = "HEALTH")
        {
            return new RequesterDetails()
            {
                ApplicationId = Constants.APPLICATION_ID,
                CorrelationId = (string.IsNullOrEmpty(CorrelationId) ? Guid.NewGuid().ToString() : CorrelationId),
                MachineName = Environment.MachineName,
                ProcessName = Process.GetCurrentProcess().ProcessName,
                Domain = Domain,
                Username = Username
            };
        }

    }
}