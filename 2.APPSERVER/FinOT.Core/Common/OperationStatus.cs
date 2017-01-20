using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

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
                case StatusEnum.AccountAlreadyExist:
                    StatusCode = "0001";
                    StatusMessage = "The user account already exists";
                    break;

                case StatusEnum.NoDataFound:
                    StatusCode = "0100";
                    StatusMessage = "No Data Found";
                    break;
                case StatusEnum.PinError:
                    StatusCode = "0002";
                    StatusMessage = "PIN doesnot match, please renter the pin ";
                    break;
                case StatusEnum.OwnerResponseSubmissionFailed:
                    StatusCode = "0003";
                    StatusMessage = "Owner Resonponse petition submit failed";
                    break;
                case StatusEnum.PetitionGroundRequired:
                    StatusCode = "0004";
                    StatusMessage = Message.ResourceManager.GetString("ID_0004");
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
                    StatusMessage =  Message.ResourceManager.GetString("ID_0106");
                    break;
                case StatusEnum.EmailDoesnotExist:
                    StatusCode = "0107";
                    StatusMessage = Message.ResourceManager.GetString("ID_0107");
                    break;

                case StatusEnum.NullArgumentException:
                    StatusCode = "0107";
                    StatusMessage = "The argument passed is null";
                    break;

                case StatusEnum.DatabaseException:
                    StatusCode = "0108";
                    StatusMessage = "Error occured in database operation ";
                    break;

               

                case StatusEnum.DatabaseMessage:
                    StatusCode = "0109";
                    break;

                #endregion

                #region System Errors
                case StatusEnum.UploadFailed:
                    StatusCode = "0201";
                    StatusMessage = "Document upload failed";
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
