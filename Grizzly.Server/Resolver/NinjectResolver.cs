using BLL.Services;
using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Grizzly.Server.Resolver
{
    public class NinjectResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
        }
        public NinjectResolver(IKernel ninjectKernel, bool scope = false)
        {
            _kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(_kernel);
            }
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectResolver(AddRequestBindings(new ChildKernel(_kernel)), true);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void Dispose()
        {

        }

        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            kernel.Bind<RoleService>().To<RoleService>().InSingletonScope();
            return kernel;
        }
    }
}