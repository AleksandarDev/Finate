using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Finate.UWP.DAL;
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
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            RaygunClient.Attach("UNQ3Nr8h83xioevBJXOM5A==");

            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            this.InitializeComponent();

            // Migrate database
            using (var db = new LocalDbContext())
                db.Database.Migrate();
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = this.Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            this.Container.RegisterInstance(this.SetupLogging());
            this.Container.RegisterType<ILocalDbContext, LocalDbContext>(new PerResolveLifetimeManager());
            this.Container.RegisterType<ITransactionsRepository, TransactionsRepository>(new PerResolveLifetimeManager());

            //Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            return base.OnInitializeAsync(args);
        }

        private ILogger SetupLogging()
        {
            return new LoggerConfiguration()
                .CreateLogger();
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // Initialize database
            var context = new LocalDbContext();
            await this.SeedContextAsync(context);
            this.Container.RegisterInstance(context);

            this.NavigationService.Navigate(PageTokens.HomePage, null);
            await Task.FromResult(true);
        }

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
