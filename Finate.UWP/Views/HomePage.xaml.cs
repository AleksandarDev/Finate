using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Finate.UWP.Annotations;
using Finate.UWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Finate.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : INotifyPropertyChanged
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.DataContextChanged += this.HomePageDataContextChanged;
        }

        private void HomePageDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.OnPropertyChanged(nameof(ConcreteDataContext));
        }

        public HomePageViewModel ConcreteDataContext => this.DataContext as HomePageViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
