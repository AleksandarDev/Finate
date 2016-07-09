using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Finate.UWP.Band;
using Finate.UWP.DAL;
using Finate.UWP.Logging.Sinks.Raygun;
using Finate.UWP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.Unity;
using Mindscape.Raygun4Net;
using Serilog;

namespace Finate.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        private const string RaygunApplicationKey = "UNQ3Nr8h83xioevBJXOM5A==";

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            RaygunClient.Attach(RaygunApplicationKey);

            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            this.InitializeComponent();

            // Migrate database
            using (var db = new LocalDbContext())
                db.Database.Migrate();
        }


        /// <summary>
        /// Creates the shell of the app.
        /// </summary>
        /// <param name="rootFrame"></param>
        /// <returns>
        /// The shell of the app.
        /// </returns>
        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = this.Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        /// <summary>
        /// Override this method with the initialization logic of your application. Here you can initialize services, repositories, and so on.
        /// </summary>
        /// <param name="args">The <see cref="T:Windows.ApplicationModel.Activation.IActivatedEventArgs" /> instance containing the event data.</param>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            this.Container.RegisterInstance(this.SetupLogging());
            this.Container.RegisterType<ILocalDbContext, LocalDbContext>(new PerResolveLifetimeManager());
            this.Container.RegisterType<ITransactionsRepository, TransactionsRepository>(new PerResolveLifetimeManager());
            this.Container.RegisterType<IBandConnectionsManager, BandConnectionsManager>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IFinateBandTileManager, FinateBandTileManager>(new PerResolveLifetimeManager());

            //Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            return base.OnInitializeAsync(args);
        }

        /// <summary>
        /// Setups the logging.
        /// </summary>
        /// <returns>Returns a new instance of logger.</returns>
        private ILogger SetupLogging()
        {
            return new LoggerConfiguration()
                .WriteTo.Raygun(RaygunApplicationKey)
                .CreateLogger();
        }

        /// <summary>
        /// Override this method with logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// </summary>
        /// <param name="args">The <see cref="T:Windows.ApplicationModel.Activation.LaunchActivatedEventArgs" /> instance containing the event data.</param>
        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // Initialize database
            var context = new LocalDbContext();
            await this.SeedContextAsync(context);
            this.Container.RegisterInstance(context);

            this.NavigationService.Navigate(PageTokens.HomePage, null);
            await Task.FromResult(true);
        }

        /// <summary>
        /// Seeds the context.
        /// </summary>
        /// <param name="context">The context.</param>
        private async Task SeedContextAsync(ILocalDbContext context)
        {
            // Prepare accounts
            if (!context.Accounts.Any())
            {
                var account = new Account
                {
                    Name = "Account"
                };

                context.Accounts.Add(account);
                await context.SaveChangesAsync();
            }

            // Prepare groups
            if (!context.Groups.Any())
            {
                var shoppingCategory = new Group
                {
                    Name = "Shopping",
                    AccountId = context.Accounts.First().Id
                };

                context.Groups.Add(shoppingCategory);
                await context.SaveChangesAsync();
            }

            // Prepare categories
            if (!context.Categories.Any())
            {
                var foodCategory = new Category
                {
                    Color = Colors.Purple.ToString(),
                    Name = "Food",
                    GroupId = context.Groups.First().Id
                };

                context.Categories.Add(foodCategory);
                await context.SaveChangesAsync();
            }
        }
    }
}
