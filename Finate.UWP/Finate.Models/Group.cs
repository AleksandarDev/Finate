using System;
using System.Diagnostics.CodeAnalysis;

namespace Finate.Models
{
    /// <summary>
    /// The group.
    /// </summary>
    public class Group : IEquatable<Group>
    {
        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the group name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the transaction account identifier.
        /// </summary>
        public string AccountId { get; set; }


        /// <summary>
        /// Compares the two instances of group.
        /// </summary>
        /// <param name="other">The group instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both groups have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both groups.
        /// </returns>
        public bool Equals(Group other)
        {
            // Not equal if identifier is not set
            if (other.Id == null || this.Id == null)
                return false;

            // Equal if identifiers match
            return other.Id == this.Id;
        }

        /// <summary>
        /// Compares the two instances of group.
        /// </summary>
        /// <param name="obj">The group instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both groups have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both groups.
        /// </returns>
        public override bool Equals(object obj)
        {
            // Ignore if null or not transaction
            var other = obj as Group;
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
