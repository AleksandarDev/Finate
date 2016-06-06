using System;

namespace Finate.Models
{
    /// <summary>
    /// The monthly budget.
    /// </summary>
    public class MonthlyBudget
    {
        /// <summary>
        /// Gets or sets the monthly budget category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the monthly budget.
        /// </summary>
        public double Budget { get; set; }

        /// <summary>
        /// Gets or sets the monthly budget month and year.
        /// </summary>
        public DateTime Month { get; set; }
    }
}