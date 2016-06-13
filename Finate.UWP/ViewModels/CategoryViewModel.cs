using Windows.UI.Xaml.Media;
using Prism.Windows.Mvvm;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The category view model.
    /// </summary>
    public class CategoryViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category color.
        /// </summary>
        public Brush Color { get; set; }
    }
}