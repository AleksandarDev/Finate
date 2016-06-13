using System;
using Windows.UI.Xaml.Media;
using Prism.Windows.Mvvm;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The transaction view model.
    /// </summary>
    public class TransactionViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the transaction view model.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the transaction category.
        /// </summary>
        public CategoryViewModel Category { get; set; }

        /// <summary>
        /// Gets or sets the transaction date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the transaction type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the transaction type color brush.
        /// </summary>
        public Brush TypeColor { get; set; }
    }
}