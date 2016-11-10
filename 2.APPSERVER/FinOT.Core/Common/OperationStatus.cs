using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.Common
{
    public class OperationStatus
    {
        private StatusEnum _status = StatusEnum.Success;

        public StatusEnum Status
        {
            get { return _status; }
            set
            {
                _status = value;
                GetStatusDetails();
            }
        }
        
        public string StatusCode { get; set; }
        
        public string StatusMessage { get; set; }
        
        public string StatusDetails { get; set; }
        
        private void GetStatusDetails()
        {
            switch (_status)
            {
                case StatusEnum.Success:
                    StatusCode = "0000";

                    StatusMessage = "Operation done successfully";
                    break;

                case StatusEnum.NoDataFound:
                    StatusCode = "0100";
                    StatusMessage = "No Data Found";
                    break;

                #region Generic Unhandled Exceptions
                case StatusEnum.InvalidArgumentException:
                    StatusCode = "0101";
                    StatusMessage = "There was an invalid data in the argument";
                    break;

                case StatusEnum.TimeoutException:
                    StatusCode = "0102";
                    StatusMessage = "The service operation timed out";
                    break;

                case StatusEnum.FaultException:
                    StatusCode = "0103";
                    StatusMessage = "An unknown exception was received from service";
                    break;

                case StatusEnum.CommunicationException:
                    StatusCode = "0104";
                    StatusMessage = "There was a communication problem";
                    break;

                case StatusEnum.SystemException:
                    StatusCode = "0105";
                    StatusMessage = "Some system error occured";
                    break;

                case StatusEnum.AuthenticationFailed:
                    StatusCode = "0106";
                    StatusMessage = "Authentication failed";
                    break;

                case StatusEnum.NullArgumentException:
                    StatusCode = "0107";
                    StatusMessage = "The argument passed is null";
                    break;
                #endregion

                default:
                    StatusCode = "1111";
                    StatusMessage = "Unknown exception occured";
                    break;

            }
        }
    }
}
