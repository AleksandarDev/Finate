using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Finate.Data;
using Finate.Models;
using Finate.UWP.Annotations;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Syncfusion.Data.Extensions;

namespace Finate.UWP.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly ITransactionsRepository transactionsRepository;
        private readonly ILocalDbContext context;
        private QuickTransactionViewModel quickTransaction;

        public event EventHandler OnQuickTransactionProcessed;


        public HomePageViewModel(
            [NotNull] ITransactionsRepository transactionsRepository,
            [NotNull] ILocalDbContext context)
        {
            if (transactionsRepository == null) throw new ArgumentNullException(nameof(transactionsRepository));
            if (context == null) throw new ArgumentNullException(nameof(context));
            this.transactionsRepository = transactionsRepository;
            this.context = context;

            // Populate collections
            this.context.Categories.ForEach(c => this.Categories.Add(new CategoryViewModel(c)));
            this.context.Transactions.Where(t => t.Date.Date == DateTime.Now.Date).ForEach(t => this.TodaysTransactions.Add(new TransactionViewModel(t)));

            this.CleatQuickTransaction();
        }

        private void CleatQuickTransaction()
        {
            this.QuickTransaction = new QuickTransactionViewModel {Category = this.Categories.FirstOrDefault()};
        }

        /// <summary>
        /// Handles the quick transaction confirmed command execution.
        /// </summary>
        public async void QuickTransactionConfirmedCommandExecute()
        {
            // Instantiate and fill new transaction model
            var transaction = new Transaction
            {
                Amount = double.Parse(this.quickTransaction.Amount),
                Date = DateTime.Now,
                CategoryId = this.quickTransaction.Category.Id,
                Name = this.quickTransaction.Name
            };

            // Add new transaction to the repository
            await this.transactionsRepository.AddAsync(transaction);

            // Add transaction to the todays transaction collection
            var transactionViewModel = new TransactionViewModel(transaction);
            this.TodaysTransactions.Add(transactionViewModel);

            // Invoke event
            this.OnQuickTransactionProcessed?.Invoke(this, null);

            // Clear the quick transaction view model
            this.CleatQuickTransaction();
        }

        /// <summary>
        /// Gets or sets the quick transaction view model.
        /// </summary>
        public QuickTransactionViewModel QuickTransaction
        {
            get { return this.quickTransaction; }
            set { this.SetProperty(ref this.quickTransaction, value); }
        }

        /// <summary>
        /// Gets or sets the todays transactions collection.
        /// </summary>
        public ObservableCollection<TransactionViewModel> TodaysTransactions { get; } =
            new ObservableCollection<TransactionViewModel>();

        /// <summary>
        /// Gets or sets the available categories collection.
        /// </summary>
        public ObservableCollection<CategoryViewModel> Categories { get; } =
            new ObservableCollection<CategoryViewModel>();

        public ObservableCollection<Transaction> WeeklyExpenses { get; } = new ObservableCollection<Transaction>()
        {
            new Transaction()
            {
                Amount = 80,
                Date = new DateTime(2016, 6, 2)
            },
            new Transaction()
            {
                Amount = 180,
                Date = new DateTime(2016, 6, 3)
            },
            new Transaction()
            {
                Amount = 120,
                Date = new DateTime(2016, 6, 4)
            },
            new Transaction()
            {
                Amount = 90,
                Date = new DateTime(2016, 6, 5)
            },
            new Transaction()
            {
                Amount = 70,
                Date = new DateTime(2016, 6, 6)
            },
            new Transaction()
            {
                Amount = 20,
                Date = new DateTime(2016, 6, 7)
            }
        };

        public ObservableCollection<Transaction> PreviousWeeklyExpenses { get; } = new ObservableCollection<Transaction>()
        {
            new Transaction()
            {
                Amount = 120,
                Date = new DateTime(2016, 6, 2)
            },
            new Transaction()
            {
                Amount = 70,
                Date = new DateTime(2016, 6, 3)
            },
            new Transaction()
            {
                Amount = 80,
                Date = new DateTime(2016, 6, 4)
            },
            new Transaction()
            {
                Amount = 100,
                Date = new DateTime(2016, 6, 5)
            },
            new Transaction()
            {
                Amount = 140,
                Date = new DateTime(2016, 6, 6)
            },
            new Transaction()
            {
                Amount = 80,
                Date = new DateTime(2016, 6, 7)
            },
            new Transaction()
            {
                Amount = 120,
                Date = new DateTime(2016, 6, 8)
            }
        };
    }
}
