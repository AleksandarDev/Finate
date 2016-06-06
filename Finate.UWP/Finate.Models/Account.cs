using System.Collections.Generic;

namespace Finate.Models
{
    /// <summary>
    /// The financial account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the account name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the account transactions.
        /// </summary>
        public ICollection<Transaction> Transactions { get; set; }
    }
}