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
        /// Gets or sets the accounts database set.
        /// </summary>
        List<Account> Accounts { get; set; }


        /// <summary>
        /// Saves the context.
        /// </summary>
        Task SaveContextAsync();
    }
}