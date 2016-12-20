using System;
using System.Collections.Generic;
using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Web;

namespace RAP.Core.Services
{
    public interface IDocumentService
    {
        ReturnResult<DocumentM> UploadDocument(DocumentM doc);
        ReturnResult<DocumentM> DownloadDocument(DocumentM doc);
    }
}
