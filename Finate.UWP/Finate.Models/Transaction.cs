using System;
using Windows.Services.Maps;

namespace Finate.Models
{
    /// <summary>
    /// The transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the transaction name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the transaction description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount, can be negative, positive or zero.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the transaction date and time.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the transaction location.
        /// </summary>
        public MapLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the transaction category.
        /// </summary>
        public Category Category { get; set; }
    }
}