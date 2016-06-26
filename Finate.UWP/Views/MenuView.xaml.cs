using System.ComponentModel;
using Windows.UI.Xaml;
using Finate.UWP.ViewModels;
namespace Finate.UWP.Views
{
    public sealed partial class MenuView : INotifyPropertyChanged
    {
        public MenuView()
        {
            this.InitializeComponent();

            this.DataContextChanged += this.MenuViewDataContextChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MenuViewModel ConcreteDataContext => this.DataContext as MenuViewModel;

        private void MenuViewDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
        }
    }
}
