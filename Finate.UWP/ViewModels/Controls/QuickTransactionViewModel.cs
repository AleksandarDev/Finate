using Prism.Windows.Mvvm;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The quick transaction view model.
    /// </summary>
    public class QuickTransactionViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the transaction name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the transaction category.
        /// </summary>
        public CategoryViewModel Category { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether transaction is expense.
        /// </summary>
        public bool IsExpense { get; set; } = true;
    }
}