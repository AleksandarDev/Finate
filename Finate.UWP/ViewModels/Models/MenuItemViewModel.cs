using System.Windows.Input;
using Prism.Windows.Mvvm;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The manu item view model.
    /// </summary>
    public class MenuItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the menu item display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the menu item command.
        /// </summary>
        public ICommand Command { get; set; }
    }
}
