using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Finate.Data;
using Finate.Models;
using Microsoft.Practices.Unity;
using Mindscape.Raygun4Net;

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
            RaygunClient.Attach("YOUR_APP_API_KEY");

            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            this.InitializeComponent();
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = this.Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            this.Container.RegisterType<ITransactionsRepository, TransactionsRepository>(new PerResolveLifetimeManager());

            //Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            return base.OnInitializeAsync(args);
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // Initialize database
            //var context = await LocalDbContext.LoadAsync();
            var context = (ILocalDbContext)new LocalDbContext();
            if (!context.IsSeeded)
                await this.SeedContextAsync(context);
            this.Container.RegisterInstance(context);

            this.NavigationService.Navigate(PageTokens.HomePage, null);
            await Task.FromResult(true);
        }

        private async Task SeedContextAsync(ILocalDbContext context)
        {
            context.IsSeeded = true;

            // Prepare groups
            var shoppingCategory = new Group
            {
                Name = "Shopping",
                Id = Guid.NewGuid().ToString()
            };

            context.Groups.Add(shoppingCategory);

            // Prepare categories
            var foodCategory = new Category
            {
                Color = Colors.Purple,
                Name = "Food",
                Id = Guid.NewGuid().ToString(),
                GroupId = shoppingCategory.Id
            };

            context.Categories.Add(foodCategory);

            // Save context changes
            await context.SaveContextAsync();
        }
    }
}
