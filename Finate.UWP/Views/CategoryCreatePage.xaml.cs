﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Finate.UWP.Properties;
using Finate.UWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Finate.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryCreatePage : INotifyPropertyChanged
    {
        public CategoryCreatePage()
        {
            // Initialize the view
            this.InitializeComponent();

            // Attach to data context changed event of this page
            this.DataContextChanged += this.CategoryCreatePageViewModelChanged;
        }

        /// <summary>
        /// Handles the page data context changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The event arguments.</param>
        private void CategoryCreatePageViewModelChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.OnPropertyChanged(nameof(ConcreteDataContext));
        }

        /// <summary>
        /// Gets the concrete data context for this page.
        /// </summary>
        public CategoryCreatePageViewModel ConcreteDataContext => this.DataContext as CategoryCreatePageViewModel;

        /// <summary>
        /// Called when propert changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changes event for specified property name.
        /// </summary>
        /// <param name="propertyName">The name of changed property.</param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
