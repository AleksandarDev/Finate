using System;
using System.Diagnostics.CodeAnalysis;
using Windows.UI;

namespace Finate.Models
{
    /// <summary>
    /// The category.
    /// </summary>
    public class Category : IEquatable<Category>
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the category group identifier.
        /// </summary>
        public string GroupId { get; set; }


        /// <summary>
        /// Compares the two instances of category.
        /// </summary>
        /// <param name="other">The category instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both categories have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both categories.
        /// </returns>
        public bool Equals(Category other)
        {
            // Not equal if identifier is not set
            if (other.Id == null || this.Id == null)
                return false;

            // Equal if identifiers match
            return other.Id == this.Id;
        }

        /// <summary>
        /// Compares the two instances of category.
        /// </summary>
        /// <param name="obj">The category instance to compare this instance to.</param>
        /// <returns>
        /// Returns <c>True</c> if both categories have same identifier; 
        /// <c>False</c> if identifiers don't match or identifier is not assigned to both categories.
        /// </returns>
        public override bool Equals(object obj)
        {
            // Ignore if null or not transaction
            var other = obj as Category;
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