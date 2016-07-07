using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Finate.UWP.DAL;
using Finate.UWP.Properties;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The CategoriesPage view model.
    /// </summary>
    public class CategoriesPageViewModel : ViewModelBase
    {
        private readonly ILocalDbContext context;
        private readonly INavigationService navigationService;


        public CategoriesPageViewModel(
            [NotNull] ILocalDbContext context, 
            [NotNull] INavigationService navigationService)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            this.context = context;
            this.navigationService = navigationService;

            this.CreateCategoryCommand = new DelegateCommand(this.CreateCategoryCommandExecute);
        }


        /// <summary>
        /// Handles the create category command execution.
        /// </summary>
        private void CreateCategoryCommandExecute()
        {
            this.navigationService.Navigate(PageTokens.CategoryCreatePage, null);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            this.RefreshPageData();
            base.OnNavigatedTo(e, viewModelState);
        }

        private void RefreshPageData()
        {
            // Populate the categories collection from context
            this.CategoriesCollection.Clear();
            foreach (var category in this.context.Categories)
                this.CategoriesCollection.Add(new CategoryViewModel(category));
        }

        /// <summary>
        /// Gets the categories collection.
        /// </summary>
        public ObservableCollection<CategoryViewModel> CategoriesCollection { get; } = 
            new ObservableCollection<CategoryViewModel>();

        /// <summary>
        /// Gets the create categoy command.
        /// </summary>
        public ICommand CreateCategoryCommand { get; }
    }
}
