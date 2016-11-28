using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
   public class EmailM
    {
      public string[] RecipientAddress { get; set; }
      public string Subject { get; set; }
      public string MessageBody { get; set; }
      public string[] CC { get; set; }
      public string[] BCC { get; set; }
      public string[] Attachments { get; set; }
    }
}
