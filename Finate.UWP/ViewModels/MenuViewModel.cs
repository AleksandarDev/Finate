using System;
using System.Collections.ObjectModel;
using Finate.UWP.Annotations;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace Finate.UWP.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        private bool canNavigateToHomePage;
        private bool canNavigateToCategoriesPage;


        public MenuViewModel([NotNull] INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            this.navigationService = navigationService;
            
            // Set initial state
            this.canNavigateToHomePage = false;
            this.canNavigateToCategoriesPage = true;

            // Populate the menu items collection
            this.MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    DisplayName = "Home",
                    Command = new DelegateCommand(this.NavigateToHomePage, this.CanNavigateToHomePage)
                },
                new MenuItemViewModel
                {
                    DisplayName = "Categories",
                    Command = new DelegateCommand(this.NavigateToCategoriesPage, this.CanNavigateToCategoriesPage)
                }
            };
        }


        private void NavigateToHomePage()
        {
            // Check if request is valid
            if (!this.CanNavigateToHomePage()) return;

            // Request the navigation;
            // If navigation was successfull - proceed
            if (!this.navigationService.Navigate(PageTokens.HomePage, null)) return;

            this.canNavigateToHomePage = false;
            this.canNavigateToCategoriesPage = true;
        }

        private bool CanNavigateToHomePage()
        {
            return this.canNavigateToHomePage;
        }

        private void NavigateToCategoriesPage()
        {
            // Check if request is valid
            if (!this.CanNavigateToCategoriesPage()) return;

            // Request the navigation;
            // If navigation was successfull - proceed
            if (!this.navigationService.Navigate(PageTokens.CategoriesPage, null)) return;

            this.canNavigateToHomePage = true;
            this.canNavigateToCategoriesPage = false;
        }

        private bool CanNavigateToCategoriesPage()
        {
            return this.canNavigateToCategoriesPage;
        }

        /// <summary>
        /// Gets the menu items collection.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> MenuItems { get; }
    }
}
