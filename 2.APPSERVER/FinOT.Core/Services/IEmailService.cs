using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using RAP.Core.Common;
using RAP.Core.DataModels;
using RAP.Core.Services;
using System.Configuration;

namespace RAP.Core.Services
{
   public interface IEmailService
    {
       ReturnResult<bool> SendEmail(EmailM message);
    }
}
