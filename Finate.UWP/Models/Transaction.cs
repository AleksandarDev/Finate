using System;
using System.Diagnostics.CodeAnalysis;

namespace Finate.UWP.Models
{
    /// <summary>
    /// The transaction.
    /// </summary>
    public class Transaction : IEquatable<Transaction>
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        public long Id { get; set; }

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
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the transaction category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the transaction category.
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the transaction account.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Gets or sets the transaction account identifier.
        /// </summary>
        public long AccountId { get; set; }


        /// <summary>
        /// Compares the two instances of transaction.
        /// </summary>
        /// <param name="other">The transaction instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both transactions have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both transaction.
        /// </returns>
        public bool Equals(Transaction other)
        {
            // Not equal if identifier is not set
            if (other.Id == 0 || this.Id == 0)
                return false;

            // Equal if identifiers match
            return other.Id == this.Id;
        }

        /// <summary>
        /// Compares the two instances of transaction.
        /// </summary>
        /// <param name="obj">The transaction instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both transactions have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both transaction.
        /// </returns>
        public override bool Equals(object obj)
        {
            // Ignore if null or not transaction
            var other = obj as Transaction;
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