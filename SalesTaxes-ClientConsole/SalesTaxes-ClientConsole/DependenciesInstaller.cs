using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SalesTaxes_Library;
using SalesTaxes_Library.Presentation;
using SalesTaxes_Library.Storage;
using System.Reflection;

namespace SalesTaxes_ClientConsole
{
    /// <summary>
    /// Dependencies configuration for this client.
    /// The most of the services are requested to be Singleton since they don't
    /// need to rely on a particular state.
    /// </summary>
    public class DependenciesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IConfigurationRepository>()
                .ImplementedBy<InMemoryConfigurationRepository>()
                .LifestyleSingleton());

            container.Register(Component
                .For<IShoppingCartRepository>()
                .ImplementedBy<InMemoryShoppingCartRepository>()
                .LifestyleSingleton());

            // Transient because I want a new instance of the shopping cart editor,
            // because it represents a new shopping cart
            container.Register(Component
                .For<ShoppingCartEditor>()
                .LifestyleTransient());

            container.Register(Component
                .For<IReceiptCodeProvider>()
                .ImplementedBy<RandomStringWithPrefixReceiptCodeProvider>()
                .LifestyleSingleton());

            container.Register(Component
                .For<ReceiptFormatter>()
                .LifestyleSingleton());

            container.Register(Component
                .For<ReceiptGenerator>()
                .LifestyleSingleton());

            // Registers automatically each IInterviewInput
            container.Register(Classes.FromAssembly(Assembly.GetExecutingAssembly())
                .BasedOn<IInterviewInput>()
                .LifestyleTransient());
        }
    }
}
