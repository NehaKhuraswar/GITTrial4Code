using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Core.Services;
using RAP.Core.Common;
using RAP.Core.DataModels;
using RAP.DAL;
using RAP.Business.Implementation;

namespace RAP.Business.Binding
{
   public class RAPBinding :  Ninject.Modules.NinjectModule
    {
       public override void Load()
       {
           Bind<IApplicationProcessingService>().To<ApplicationProcessingService>();
           Bind<IDocumentService>().To <DocumentService>();
           Bind<ICommonService>().To<CommonService>();
           Bind<IExceptionHandler>().To<ExceptionHandler>();
           Bind<ICommonDBHandler>().To<CommonDBHandler>();
           Bind<IApplicationProcessingDBHandler>().To<ApplicationProcessingDBHandler>();
           Bind<IDashboardDBHandler>().To<DashboardDBHandler>();
           Bind<IDashboardService>().To<DashboardService>();
       }
    }
}
