using System.Threading.Tasks;
using Finate.UWP.Models;

namespace Finate.UWP.DAL
{
    /// <summary>
    /// The <see cref="Transaction"/> repository.
    /// </summary>
    public interface ITransactionsRepository
    {
        /// <summary>
        /// Adds the transaction to the repository.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Returns <c>True</c> if transaction was successfully added to the repository; <c>False</c> otherwise.</returns>
        Task<bool> AddAsync(Transaction transaction);

        /// <summary>
        /// Removes the transaction from the repository.
        /// </summary>
        /// <param name="transaction">The transaction to remove.</param>
        /// <returns>Returns <c>True</c> if transaction was successfully removed from the repository; <c>False</c> otherwise.</returns>
        Task<bool> RemoveAsync(Transaction transaction);
    }
}