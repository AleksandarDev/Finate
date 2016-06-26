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
        private bool canNavigateToBudgetPage;


        public MenuViewModel([NotNull] INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            this.navigationService = navigationService;

            // Set initial state
            this.canNavigateToHomePage = false;
            this.canNavigateToCategoriesPage = true;
            this.canNavigateToBudgetPage = true;

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
                },
                new MenuItemViewModel
                {
                    DisplayName = "Budget",
                    Command = new DelegateCommand(this.NavigateToBudgetPage, this.CanNavigateToBudgetPage)
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

            // Set new navigation states
            this.canNavigateToHomePage = false;
            this.canNavigateToCategoriesPage = true;
            this.canNavigateToBudgetPage = true;

            // Raise the can execute changed event
            this.RaiseCanExecuteChanged();
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

            // Set new navigation states
            this.canNavigateToHomePage = true;
            this.canNavigateToCategoriesPage = false;
            this.canNavigateToBudgetPage = true;
            
            // Raise the can execute changed event
            this.RaiseCanExecuteChanged();
        }

        private bool CanNavigateToCategoriesPage()
        {
            return this.canNavigateToCategoriesPage;
        }

        private void NavigateToBudgetPage()
        {
            // Check if request is valid
            if (!this.CanNavigateToBudgetPage()) return;

            // Request the navigation;
            // If navigation was successfull - proceed
            if (!this.navigationService.Navigate(PageTokens.BudgetPage, null)) return;

            // Set new navigation states
            this.canNavigateToHomePage = true;
            this.canNavigateToCategoriesPage = true;
            this.canNavigateToBudgetPage = false;

            // Raise the can execute changed event
            this.RaiseCanExecuteChanged();
        }

        private bool CanNavigateToBudgetPage()
        {
            return this.canNavigateToBudgetPage;
        }

        /// <summary>
        /// Raises the can execute changed event on all menu items.
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            foreach (var item in this.MenuItems)
                (item.Command as DelegateCommand)?.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Gets the menu items collection.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> MenuItems { get; }
    }
}
