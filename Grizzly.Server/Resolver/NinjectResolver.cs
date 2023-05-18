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
            kernel.Bind<ActiveIngredientService>().To<ActiveIngredientService>().InSingletonScope();
            kernel.Bind<UserService>().To<UserService>().InSingletonScope();
            kernel.Bind<ProducerService>().To<ProducerService>().InSingletonScope();
            kernel.Bind<OrderService>().To<OrderService>().InSingletonScope();
            kernel.Bind<FarmacyOfficeService>().To<FarmacyOfficeService>().InSingletonScope();
            kernel.Bind<DrugTypeService>().To<DrugTypeService>().InSingletonScope();
            kernel.Bind<DrugService>().To<DrugService>().InSingletonScope();
            kernel.Bind<DrugIndicationService>().To<DrugIndicationService>().InSingletonScope();
            kernel.Bind<ContraIndicationService>().To<ContraIndicationService>().InSingletonScope();
            kernel.Bind<CartService>().To<CartService>().InSingletonScope();
            kernel.Bind<ApplicationTypeService>().To<ApplicationTypeService>().InSingletonScope();
            return kernel;
        }
    }
}