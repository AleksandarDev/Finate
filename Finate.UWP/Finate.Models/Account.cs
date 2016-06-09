using System;
using System.Diagnostics.CodeAnalysis;

namespace Finate.Models
{
    /// <summary>
    /// The financial account.
    /// </summary>
    public class Account : IEquatable<Account>
    {
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the account name.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Compares the two instances of account.
        /// </summary>
        /// <param name="other">The account instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both accounts have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both accounts.
        /// </returns>
        public bool Equals(Account other)
        {
            // Not equal if identifier is not set
            if (other.Id == null || this.Id == null)
                return false;

            // Equal if identifiers match
            return other.Id == this.Id;
        }

        /// <summary>
        /// Compares the two instances of account.
        /// </summary>
        /// <param name="obj">The account instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both accounts have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both accounts.
        /// </returns>
        public override bool Equals(object obj)
        {
            // Ignore if null or not transaction
            var other = obj as Account;
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
            if (this.Id == null)
                return 0;

            return this.Id.GetHashCode();
        }
    }
}