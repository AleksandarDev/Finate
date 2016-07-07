using System;
using System.Linq;
using System.Threading.Tasks;
using Finate.UWP.Models;

namespace Finate.UWP.DAL
{
    /// <summary>
    /// The <see cref="Transaction"/> repository.
    /// </summary>
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ILocalDbContext context;


        /// <summary>
        /// Instantiates a new instance of <see cref="TransactionsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public TransactionsRepository(ILocalDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            this.context = context;
        }


        /// <summary>
        /// Adds the transaction to the repository.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Returns <c>True</c> if transaction was successfully added to the repository; <c>False</c> otherwise.</returns>
        public async Task<bool> AddAsync(Transaction transaction)
        {
            // Assign default account
            if (transaction.Account == null || transaction.AccountId == 0)
                transaction.AccountId = this.context.Accounts.First().Id;

            // Add to context
            this.context.Transactions.Add(transaction);

            // Save changes
            return await this.context.TrySaveContextAsync();
        }

        /// <summary>
        /// Removes the transaction from the repository.
        /// </summary>
        /// <param name="transaction">The transaction to remove.</param>
        /// <returns>Returns <c>True</c> if transaction was successfully removed from the repository; <c>False</c> otherwise.</returns>
        public async Task<bool> RemoveAsync(Transaction transaction)
        {
            // Validate transaction
            if (transaction == null || transaction.Id == 0)
                return false;

            // Try to remove from context
            this.context.Transactions.Remove(transaction);

            // Save changes
            return await this.context.TrySaveContextAsync();
        }
    }
}
