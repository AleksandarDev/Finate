using System;
using System.Linq;
using Windows.UI.Xaml.Media;
using Finate.Data;
using Prism.Windows.Mvvm;
using Finate.Models;
using Microsoft.Practices.Unity;
using Prism.Unity.Windows;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The transaction view model.
    /// </summary>
    public class TransactionViewModel : ViewModelBase
    {
        public string IncomeBrushKey = "";


        public TransactionViewModel(Transaction transaction)
        {
            this.FromTransaction(transaction);
        }

        private void FromTransaction(Transaction transaction)
        {
            this.Id = transaction.Id;
            this.Name = transaction.Name;
            this.Amount = transaction.Amount;
            this.Date = transaction.Date;
            this.Type = transaction.Amount < 0 ? "Expense" : "Income";

            // Assign type color
            object incomeBrushObject;
            PrismUnityApplication.Current.Resources.TryGetValue(this.IncomeBrushKey, out incomeBrushObject);
            this.TypeColor = (Brush)incomeBrushObject;

            // Retrieve and assign category
            var context = PrismUnityApplication.Current.Container.Resolve<ILocalDbContext>();
            var category = context.Categories.FirstOrDefault(c => c.Id == transaction.CategoryId);
            this.Category = new CategoryViewModel(category);
        }

        public Transaction ToTransaction()
        {
            return ToTransaction(this);
        }

        public static Transaction ToTransaction(TransactionViewModel viewModel)
        {
            return new Transaction
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Amount = viewModel.Amount * (viewModel.Type == "Income" ? 1 : -1),
                Date = viewModel.Date,
                CategoryId = viewModel.Category.Id
            };
        }
        

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        public string Id { get; private set; }

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