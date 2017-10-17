using EPBusinessLogic;
using EPBusinessLogic.Helpers.IocContainer;
using EPCommon.Contracts;
using EPPractice.Helpers.Mappers;
using Microsoft.Practices.Unity;
using NLog;

namespace EPPractice.Helpers
{
    public class RegisterDependency
    {
        public static void Configure(IUnityContainer container)
        {

            var mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
            container.RegisterInstance(mapper);

            container.RegisterType<ILogger>(new InjectionFactory(l => LogManager.GetCurrentClassLogger()));  // Register NLog

            container.AddNewExtension<DependencyInjectionExtension>();  // define dependency between BL and DL

            container.RegisterType<IEmailSender, EmailSender>();
        }
    }
}