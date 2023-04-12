using BLL.Services;
using DAL.Context;
using DAL.Repositories;
using Ninject.Modules;
namespace BLL.Modules
{
    internal class GrizzlyModule : NinjectModule
    {
        public override void Load()
        {
            //Database context
            Bind<FarmacyContext>().To<FarmacyContext>();

            //Repositories
            Bind<UserRepository>().To<UserRepository>();
            Bind<RoleRepository>().To<RoleRepository>();
            Bind<ProducerRepository>().To<ProducerRepository>();
            Bind<OrderRepository>().To<OrderRepository>();
            Bind<FarmacyOfficeRepository>().To<FarmacyOfficeRepository>();
            Bind<DrugTypeRepository>().To<DrugTypeRepository>();
            Bind<DrugRepository>().To<DrugRepository>();
            Bind<DrugIndicationRepository>().To<DrugIndicationRepository>();
            Bind<ContraIndicationRepository>().To<ContraIndicationRepository>();
            Bind<CartRepository>().To<CartRepository>();
            Bind<ApplicationTypeRepository>().To<ApplicationTypeRepository>();
            Bind<ActiveIngredientRepository>().To<ActiveIngredientRepository>();

            //Services
            Bind<UserService>().To<UserService>();
            Bind<RoleService>().To<RoleService>();
            Bind<ProducerService>().To<ProducerService>();
            Bind<OrderService>().To<OrderService>();
            Bind<FarmacyOfficeService>().To<FarmacyOfficeService>();
            Bind<DrugTypeService>().To<DrugTypeService>();
            Bind<DrugService>().To<DrugService>();
            Bind<DrugIndicationService>().To<DrugIndicationService>();
            Bind<ContraIndicationService>().To<ContraIndicationService>();
            Bind<CartService>().To<CartService>();
            Bind<ApplicationTypeService>().To<ApplicationTypeService>();
            Bind<ActiveIngredientService>().To<ActiveIngredientService>();
        }
    }
}
