﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Finate.UWP.DAL;
using Finate.UWP.Extensions;
using Finate.UWP.Models;
using Finate.UWP.Properties;
using Prism.Windows.Mvvm;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Schedule;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The <see cref="Views.HomePage"/> view model.
    /// </summary>
    public class HomePageViewModel : ViewModelBase
    {
        private readonly ITransactionsRepository transactionsRepository;
        private readonly ILocalDbContext context;
        private QuickTransactionViewModel quickTransaction;

        /// <summary>
        /// Occurs when quick transaction was processed.
        /// </summary>
        public event EventHandler OnQuickTransactionProcessed;


        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageViewModel"/> class.
        /// </summary>
        /// <param name="transactionsRepository">The transactions repository.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">
        /// transactionsRepository
        /// or
        /// context
        /// </exception>
        public HomePageViewModel(
            [NotNull] ITransactionsRepository transactionsRepository,
            [NotNull] ILocalDbContext context)
        {
            if (transactionsRepository == null) throw new ArgumentNullException(nameof(transactionsRepository));
            if (context == null) throw new ArgumentNullException(nameof(context));
            this.transactionsRepository = transactionsRepository;
            this.context = context;

            // Populate categories
            this.context.Categories.ForEach(c => this.Categories.Add(new CategoryViewModel(c)));

            // Populate todays transactions collection
            this.context.Transactions
                .Where(t => t.Date.Date == DateTime.Now.Date)
                .ForEach(t => this.TodaysTransactions.Add(new TransactionViewModel(t)));

            // Populate graph collections
            this.PopulateGraphCollections();

            // Handle empty transactions collection property changed notification
            this.TodaysTransactions.CollectionChanged += (sender, args) =>
                this.OnPropertyChanged(() => this.IsTodaysTransactionsEmpty);

            // Initial clear of quick transaction view model
            this.ClearQuickTransaction();
        }


        /// <summary>
        /// Populates the destination graph transaction collection with 
        /// transaction view models that represent users transaction 
        /// for specified date range.
        /// </summary>
        /// <param name="source">The user transactions collection.</param>
        /// <param name="startDate">Start of the date range to populate destination collection.</param>
        /// <param name="endDate">End of the date range to populate destination collection.</param>
        /// <param name="destination">The destination transaction graph view model collection.</param>
        /// <remarks>
        /// This will populate destination collection with transaction graph view models
        /// that contain date and amount equal to sum of all users transaction for that date.
        /// If user did not make any transactions on that date, amount will be zero.
        /// </remarks>
        private static void PopulateGraphCollection(
            IReadOnlyCollection<Transaction> source, 
            DateTime startDate, 
            DateTime endDate, 
            ICollection<TransactionGraphViewModel> destination)
        {
            // Populate previous week transactions
            for (var currentDay = startDate; currentDay < endDate; currentDay += TimeSpan.FromDays(1))
            {
                // Retrieve transactions for given day of the previous week
                var dayTransactions = source
                    .Where(t => t.Date.Date == currentDay)
                    .ToList();

                // Add empty transaction to the collection if on the day no transactions are available
                if (!dayTransactions.Any())
                {
                    // Add empty transaction
                    destination.Add(new TransactionGraphViewModel(
                        new Transaction
                        {
                            Date = currentDay,
                            Amount = 0
                        }));
                }
                else
                {
                    // Add transaction whith sum of all transaction amounts
                    destination.Add(new TransactionGraphViewModel(
                        new Transaction
                        {
                            Date = currentDay,
                            Amount = dayTransactions.Sum(t => Math.Abs(t.Amount))
                        }));
                }
            }
        }

        /// <summary>
        /// Populates the graph collections.
        /// </summary>
        /// <remarks>
        /// Two graph collections will be populated. 
        /// The previous week collection is populated for all seven days,
        /// current week is populated with transaction up and including current date.
        /// </remarks>
        private void PopulateGraphCollections()
        {
            // Calculate starting date
            var currentWeekStartDate = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
            var previousWeekStartDate = (currentWeekStartDate - TimeSpan.FromDays(1)).StartOfWeek(DayOfWeek.Monday);

            // Retrieve all transactions for previous two weeks
            var transactions = this.context.Transactions
                .Where(t => t.Date.Date >= previousWeekStartDate.Date)
                .ToList();

            // Populate previous week transactions
            PopulateGraphCollection(
                transactions, 
                previousWeekStartDate,
                currentWeekStartDate,
                this.PreviousWeeklyExpenses);

            // Populate current week transactions
            PopulateGraphCollection(
                transactions,
                currentWeekStartDate,
                DateTime.Now.Date + TimeSpan.FromDays(1),
                this.WeeklyExpenses);
        }

        /// <summary>
        /// Clears the quick transaction view model.
        /// This is called after user has added new transaction and view needs to be reset.
        /// </summary>
        private void ClearQuickTransaction()
        {
            this.QuickTransaction = new QuickTransactionViewModel
            {
                Category = this.Categories.FirstOrDefault()
            };
        }

        /// <summary>
        /// Handles the quick transaction confirmed command execution.
        /// </summary>
        public async void QuickTransactionConfirmedCommandExecute()
        {
            // Instantiate and fill new transaction model
            var transaction = new Transaction
            {
                Amount = double.Parse(this.quickTransaction.Amount) * (this.quickTransaction.IsExpense ? -1 : 1),
                Date = DateTime.Now,
                CategoryId = this.quickTransaction.Category.Id,
                Name = this.quickTransaction.Name
            };

            // Add new transaction to the repository
            await this.transactionsRepository.AddAsync(transaction);

            // Add transaction to the todays transaction collection
            var transactionViewModel = new TransactionViewModel(transaction);
            this.TodaysTransactions.Insert(0, transactionViewModel);

            // Add transaction to the current weekly expense graph collection
            this.WeeklyExpenses
                .First(t => t.Index == transaction.Date.GetDayOfWeekIndex(true))
                .Amount += Math.Abs(transaction.Amount);

            // Invoke event
            this.OnQuickTransactionProcessed?.Invoke(this, null);

            // Clear the quick transaction view model
            this.ClearQuickTransaction();
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
        /// Gets the current weekly transactions collection.
        /// </summary>
        public ObservableCollection<TransactionGraphViewModel> WeeklyExpenses { get; } = 
            new ObservableCollection<TransactionGraphViewModel>();

        /// <summary>
        /// Gets the previous weekly transactions collection.
        /// </summary>
        public ObservableCollection<TransactionGraphViewModel> PreviousWeeklyExpenses { get; } =
            new ObservableCollection<TransactionGraphViewModel>();

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

        /// <summary>
        /// Gets the value that determines whether todays transactions collectio is empty.
        /// </summary>
        /// <value>
        /// <c>True</c> if todays transactions collection is empty; <c>False</c> otherwise.
        /// </value>
        public bool IsTodaysTransactionsEmpty => !this.TodaysTransactions.Any();
    }
}
