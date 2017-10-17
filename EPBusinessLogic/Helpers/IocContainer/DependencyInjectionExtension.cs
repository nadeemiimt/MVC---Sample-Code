using EPCommon.Contracts;
using EPDataLayer.Entities;
using EPDataLayer.Repository;
using Microsoft.Practices.Unity;

namespace EPBusinessLogic.Helpers.IocContainer
{
    public class DependencyInjectionExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<EPTestEntities>();
            var dbContext = Container.Resolve<EPTestEntities>();
            Container.RegisterType<IRepository, Repository>(new InjectionConstructor(dbContext));
            Container.RegisterType<IRegister, RegisterLogic>();
        }
    }
}
