using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Finate.Data;
using Finate.UWP.Annotations;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The CategoriesPage view model.
    /// </summary>
    public class CategoriesPageViewModel : ViewModelBase
    {
        private readonly ILocalDbContext context;


        public CategoriesPageViewModel([NotNull] ILocalDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            this.context = context;
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
    }
}
