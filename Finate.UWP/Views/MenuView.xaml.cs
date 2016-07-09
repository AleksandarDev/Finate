using System.ComponentModel;
using Windows.UI.Xaml;
using Finate.UWP.ViewModels;
namespace Finate.UWP.Views
{
    /// <summary>
    /// The menu view.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public sealed partial class MenuView : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView"/> class.
        /// </summary>
        public MenuView()
        {
            this.InitializeComponent();

            this.DataContextChanged += this.MenuViewDataContextChanged;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the concrete data context.
        /// </summary>
        /// <value>
        /// The concrete data context.
        /// </value>
        public MenuViewModel ConcreteDataContext => this.DataContext as MenuViewModel;

        /// <summary>
        /// Triggers the menu view data context property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DataContextChangedEventArgs"/> instance containing the event data.</param>
        private void MenuViewDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
        }
    }
}
