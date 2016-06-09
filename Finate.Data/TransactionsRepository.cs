using System;
using System.Threading.Tasks;
using Finate.Models;

namespace Finate.Data
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
            // Validate transaction
            if (transaction == null ||
                transaction.Id != null ||
                this.context.Transactions.Contains(transaction))
                return false;

            // Assign identifier to the transactions
            transaction.Id = Guid.NewGuid().ToString();

            // Add to context
            this.context.Transactions.Add(transaction);

            return await this.TrySaveContextAsync();
        }

        /// <summary>
        /// Removes the transaction from the repository.
        /// </summary>
        /// <param name="transaction">The transaction to remove.</param>
        /// <returns>Returns <c>True</c> if transaction was successfully removed from the repository; <c>False</c> otherwise.</returns>
        public async Task<bool> RemoveAsync(Transaction transaction)
        {
            // Validate transaction
            if (transaction?.Id == null)
                return false;

            // Try to remove from context
            if (!this.context.Transactions.Remove(transaction))
                return false;

            return await this.TrySaveContextAsync();
        }

        /// <summary>
        /// Saves the context.
        /// </summary>
        /// <returns>Returns <c>True</c> if context was saved successfully; <c>False</c> otherwise.</returns>
        protected async Task<bool> TrySaveContextAsync()
        {
            try
            {
                await this.context.SaveContextAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
