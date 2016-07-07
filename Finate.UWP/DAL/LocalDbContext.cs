using Finate.UWP.Models;
using Microsoft.EntityFrameworkCore;

namespace Finate.UWP.DAL
{
    /// <summary>
    /// The Local database context.
    /// </summary>
    public class LocalDbContext : DbContext, ILocalDbContext
    {
        private const string DatabasePath = "Filename=Transactions.db";
        

        /// <summary>
        /// Gets or sets the accounts database set.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the categories database set.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the groups database set.
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the monthly budgets database set.
        /// </summary>
        public DbSet<MonthlyBudget> MonthlyBudgets { get; set; }

        /// <summary>
        /// Gets or sets the transactions database set.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }


        /// <summary>
        /// Handles the context configuration.
        /// </summary>
        /// <param name="optionsBuilder">The context configuration options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DatabasePath);
        }
    }
}
