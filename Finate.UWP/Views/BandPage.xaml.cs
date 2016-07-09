using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Finate.UWP.Annotations;
using Finate.UWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Finate.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BandPage : INotifyPropertyChanged
    {
        public BandPage()
        {
            this.InitializeComponent();
            this.DataContextChanged += this.BandPageDataContextChanged;
        }

        private void BandPageDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.OnPropertyChanged(nameof(ConcreteDataContext));
        }


        /// <summary>
        /// Gets the concrete data context for this page.
        /// </summary>
        public BandPageViewModel ConcreteDataContext => this.DataContext as BandPageViewModel;


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
