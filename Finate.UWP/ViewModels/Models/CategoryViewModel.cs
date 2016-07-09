using Windows.UI.Xaml.Media;
using Prism.Windows.Mvvm;
using Finate.UWP.Extensions;
using Finate.UWP.Models;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The category view model.
    /// </summary>
    public class CategoryViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryViewModel"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        public CategoryViewModel(Category category)
        {
            this.FromCategory(category);
        }


        private void FromCategory(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Color = new SolidColorBrush(category.Color.GetColorFromHexString());
        }

        /// <summary>
        /// Converts the view model to model.
        /// </summary>
        /// <returns>Returns new populated instance of <see cref="Category"/>.</returns>
        public Category ToCategory()
        {
            return ToCategory(this);
        }

        /// <summary>
        /// Converts the view model to model.
        /// </summary>
        /// <returns>Returns new populated instance of <see cref="Category"/>.</returns>
        public static Category ToCategory(CategoryViewModel viewModel)
        {
            return new Category
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Color = ((SolidColorBrush)viewModel.Color).Color.ToString()
            };
        }

        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category color.
        /// </summary>
        public Brush Color { get; set; }
    }
}