using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{

     public class DocumentM
     {
         public int DocID { get; set; }
         public string DocName { get; set; }
         public string DocCategory { get; set; }
         public string DocTitle { get; set; }
         public string DocDescription { get; set; }
         public string MimeType { get; set; }
         public byte[] Content { get; set; }
         public string Base64Content { get; set; }
         public int DocThirdPartyID { get; set; }
         public int CustomerID { get; set; }
         public int CityUserID { get; set; }
         public int? C_ID { get; set; }
         public bool IsPetitonFiled { get; set; }
         public bool isUploaded { get; set; }
         public string UploadedBy { get; set; }
         public DateTime CreatedDate { get; set; }
     }
     public enum DocCategory
     {
         TenantPetition,
         OwnerPetition,
         OwnerResponse,
         TenantResponse,
         Appeal,
         AdditionalStaffDocument,
         EmailAttachment,
         MailAttachment
     }
}
