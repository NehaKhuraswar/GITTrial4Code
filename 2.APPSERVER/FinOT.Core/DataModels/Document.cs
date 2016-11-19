using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
    public class Document : DocumentContent
    {
        public int? DocID { get; set; }        
        public IDDescription Section { get; set; }
        public int KeyID { get; set; }
        public string AdditionalKey { get; set; }
        public IDDescription Category { get; set; }
        public string Description { get; set; }
        public int? Version { get; set; }
        public int? Rank { get; set; }
        public Audit Audit { get; set; }
        public bool PostCompleteIndicator { get; set; }
    }

    public class DocumentContent
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
    }

     public class DocumentM
     {
         public int? DocID { get; set; }
         public string DocName { get; set; }
         public string Extension { get; set; }
         public string DocType { get; set; }
         public string DocTitle { get; set; }
         public byte[] Content { get; set; }
         public int DocThirdPartyID { get; set; }
         
     }
}
