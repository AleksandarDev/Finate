using System;
using System.Diagnostics.CodeAnalysis;

namespace Finate.UWP.Models
{
    /// <summary>
    /// The monthly budget.
    /// </summary>
    public class MonthlyBudget : IEquatable<MonthlyBudget>
    {
        /// <summary>
        /// Gets or sets the monthly budget identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the monthly budget category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the monthly budget category identifier.
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the monthly budget.
        /// </summary>
        public double Budget { get; set; }

        /// <summary>
        /// Gets or sets the monthly budget month and year.
        /// </summary>
        public DateTime Month { get; set; }


        /// <summary>
        /// Compares the two instances of monthly budget.
        /// </summary>
        /// <param name="other">The monthly budget instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both monthly budgets have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both monthly budgets.
        /// </returns>
        public bool Equals(MonthlyBudget other)
        {
            // Not equal if identifier is not set
            if (other.Id == 0 || this.Id == 0)
                return false;

            // Equal if identifiers match
            return other.Id == this.Id;
        }

        /// <summary>
        /// Compares the two instances of monthly budget.
        /// </summary>
        /// <param name="obj">The monthly budget instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both monthly budgets have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both monthly budgets.
        /// </returns>
        public override bool Equals(object obj)
        {
            // Ignore if null or not transaction
            var other = obj as MonthlyBudget;
            if (other == null)
                return false;

            return this.Equals(other);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        /// <returns>Returns the has code of this instance.</returns>
        [SuppressMessage(
            "ReSharper",
            "NonReadonlyMemberInGetHashCode",
            Justification = "Identifier shouldn't change once assigned through instance lifetine.")]
        public override int GetHashCode()
        {
            if (this.Id == 0)
                return 0;

            return this.Id.GetHashCode();
        }
    }
}