using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
   public class EmailM
    {
       public EmailM()
       {
           RecipientAddress = new List<string>();
           CC = new List<string>();           
       }
      private List<DocumentM> _attachments = new List<DocumentM>();
      public List<string> RecipientAddress { get; set; }
      public string Subject { get; set; }
      public string MessageBody { get; set; }
      public List<string> CC { get; set; }
      public string BCC { get; set; }
      public List<DocumentM> Attachments
      {
          get
          {
              return _attachments;
          }
          set
          {
              _attachments = value;
          }
      }
    }
}
