using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Dependency resolver based on Ninject
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public NinjectDependencyResolver()
        {
            Kernel = new StandardKernel();
        }

        /// <summary>
        /// Ninject kernel
        /// </summary>
        public IKernel Kernel { get; }

        /// <inheritdoc />
        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        /// <inheritdoc />
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}
