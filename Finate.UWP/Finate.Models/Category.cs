namespace Finate.Models
{
    /// <summary>
    /// The category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category group.
        /// </summary>
        public Group Group { get; set; }
    }
}