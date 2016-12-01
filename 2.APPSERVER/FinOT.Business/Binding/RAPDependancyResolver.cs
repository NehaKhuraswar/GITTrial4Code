using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace RAP.Business.Binding
{
   public sealed class RAPDependancyResolver
    {
        private static volatile RAPDependancyResolver instance;
        private static object syncRoot = new Object();
        private static IKernel kernel;

        private RAPDependancyResolver()
        {
            kernel = new StandardKernel(new RAPBinding());
        }

        public static RAPDependancyResolver Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RAPDependancyResolver();
                    }
                }

                return instance;
            }
        }
        public IKernel GetKernel()
        {
            return kernel;
        }
    }
}
