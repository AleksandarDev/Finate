using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Finate.Models;
using Newtonsoft.Json;

namespace Finate.Data
{
    /// <summary>
    /// The Local database context.
    /// </summary>
    public class LocalDbContext : ILocalDbContext
    {
        private static int databaseLocked;
        private const string DatabasePath = "Database.json";

        /// <summary>
        /// Gets or sets the value that detemines whether this database was seeded already.
        /// </summary>
        public bool IsSeeded { get; set; }

        /// <summary>
        /// Gets or sets the accounts database set.
        /// </summary>
        public List<Account> Accounts { get; set; } = new List<Account>();

        /// <summary>
        /// Gets or sets the categories database set.
        /// </summary>
        public List<Category> Categories { get; set; } = new List<Category>();

        /// <summary>
        /// Gets or sets the groups database set.
        /// </summary>
        public List<Group> Groups { get; set; } = new List<Group>();

        /// <summary>
        /// Gets or sets the monthly budgets database set.
        /// </summary>
        public List<MonthlyBudget> MonthlyBudgets { get; set; } = new List<MonthlyBudget>();

        /// <summary>
        /// Gets or sets the transactions database set.
        /// </summary>
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();


        /// <summary>
        /// Saves the context.
        /// </summary>
        public async Task SaveContextAsync()
        {
            // Do not save if we are already saving
            // Locks the database if not already
            if (Interlocked.CompareExchange(ref databaseLocked, 1, 0) == 1)
                return;

            try
            {
                var databaseContent = JsonConvert.SerializeObject(this);
                var databaseFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    DatabasePath, CreationCollisionOption.OpenIfExists).AsTask();
                await FileIO.WriteTextAsync(databaseFile, databaseContent).AsTask();
            }
            catch
            {
                // TODO: Log
            }
            finally
            {
                // Mark database as unlocked
                Interlocked.Exchange(ref databaseLocked, 0);
            }
        }

        /// <summary>
        /// Loads the context.
        /// </summary>
        /// <returns>Returns the context.</returns>
        public static async Task<ILocalDbContext> LoadAsync()
        {
            var databaseFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                DatabasePath, CreationCollisionOption.OpenIfExists).AsTask();
            var databaseContent = await FileIO.ReadTextAsync(databaseFile);
            if (string.IsNullOrWhiteSpace(databaseContent))
                return new LocalDbContext();
            return JsonConvert.DeserializeObject<LocalDbContext>(databaseContent);
        }
    }
}
