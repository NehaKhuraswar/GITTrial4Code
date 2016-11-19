using System;
using System.Collections.Generic;
using RAP.Core.DataModels;
using RAP.Core.Common;

namespace RAP.Core.Services
{
    public interface IdocumentService
    {
        ReturnResult<DocumentM> UploadDocument(DocumentM doc);
    }
}
