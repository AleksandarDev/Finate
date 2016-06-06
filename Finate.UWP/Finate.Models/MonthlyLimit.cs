using System;

namespace Finate.Models
{
    /// <summary>
    /// The monthly limit.
    /// </summary>
    public class MonthlyLimit
    {
        /// <summary>
        /// Gets or sets the monthly limit.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the limit amount.
        /// </summary>
        public double Limit { get; set; }

        /// <summary>
        /// Gets or sets the monthly limit month and year.
        /// </summary>
        public DateTime Month { get; set; }
    }
}