using System.Collections.Generic;
using System.Threading.Tasks;
using Finate.Models;

namespace Finate.Data
{
    /// <summary>
    /// The local database context.
    /// </summary>
    public interface ILocalDbContext
    {
        /// <summary>
        /// Gets or sets the value that detemines whether this database was seeded already.
        /// </summary>
        bool IsSeeded { get; set; }

        /// <summary>
        /// Gets or sets the accounts database set.
        /// </summary>
        List<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the categories database set.
        /// </summary>
        List<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the groups database set.
        /// </summary>
        List<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the monthly budgets database set.
        /// </summary>
        List<MonthlyBudget> MonthlyBudgets { get; set; }

        /// <summary>
        /// Gets or sets the transactions database set.
        /// </summary>
        List<Transaction> Transactions { get; set; }


            /// <summary>
        /// Saves the context.
        /// </summary>
        Task SaveContextAsync();
    }
}