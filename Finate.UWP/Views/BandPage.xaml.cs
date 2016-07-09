using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="BandPage"/> class.
        /// </summary>
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
