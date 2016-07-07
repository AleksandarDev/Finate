using System;
using Finate.UWP.Extensions;
using Finate.UWP.Models;
using Prism.Windows.Mvvm;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The transaction graph view model.
    /// </summary>
    public class TransactionGraphViewModel : ViewModelBase
    {
        private int index;
        private double amount;

        /// <summary>
        /// Gets or sets the transaction in graph location index.
        /// </summary>
        public int Index
        {
            get { return this.index; }
            set { this.SetProperty(ref this.index, value); }
        }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        public double Amount
        {
            get { return this.amount; }
            set { this.SetProperty(ref this.amount, value); }
        }


        /// <summary>
        /// Instantiates a new instance of <see cref="TransactionGraphViewModel"/>.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        public TransactionGraphViewModel(Transaction transaction)
        {
            this.Amount = transaction.Amount;
            this.Index = (int)transaction.Date.GetDayOfWeekIndex(true);
        }
    }
}