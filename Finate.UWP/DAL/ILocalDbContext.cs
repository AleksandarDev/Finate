using System.Threading;
using System.Threading.Tasks;
using Finate.UWP.Models;
using Microsoft.EntityFrameworkCore;

namespace Finate.UWP.DAL
{
    /// <summary>
    /// The local database context.
    /// </summary>
    public interface ILocalDbContext
    {
        /// <summary>
        /// Gets or sets the accounts database set.
        /// </summary>
        DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the categories database set.
        /// </summary>
        DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the groups database set.
        /// </summary>
        DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the monthly budgets database set.
        /// </summary>
        DbSet<MonthlyBudget> MonthlyBudgets { get; set; }

        /// <summary>
        /// Gets or sets the transactions database set.
        /// </summary>
        DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Saves the context changes.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Saves the context changes.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}