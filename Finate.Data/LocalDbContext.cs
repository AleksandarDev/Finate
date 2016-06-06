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
    public class LocalDbContext
    {
        private static int databaseLocked;
        private const string DatabasePath = "Database.json";


        /// <summary>
        /// Gets or sets the accounts database set.
        /// </summary>
        public List<Account> Accounts { get; set; }


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
                var databaseFile = await Package.Current.InstalledLocation.CreateFileAsync(
                    DatabasePath, CreationCollisionOption.OpenIfExists).AsTask();
                await FileIO.WriteTextAsync(databaseFile, databaseContent).AsTask();
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
        public static async Task<LocalDbContext> LoadAsync()
        {
            var databaseFile = await Package.Current.InstalledLocation.CreateFileAsync(
                DatabasePath, CreationCollisionOption.OpenIfExists).AsTask();
            var databaseContent = await FileIO.ReadTextAsync(databaseFile);
            if (string.IsNullOrWhiteSpace(databaseContent))
                return new LocalDbContext();
            return JsonConvert.DeserializeObject<LocalDbContext>(databaseContent);
        }
    }
}
