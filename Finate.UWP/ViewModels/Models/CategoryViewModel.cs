﻿using Windows.UI.Xaml.Media;
using Finate.Models;
using Prism.Windows.Mvvm;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The category view model.
    /// </summary>
    public class CategoryViewModel : ViewModelBase
    {
        public CategoryViewModel(Category category)
        {
            this.FromCategory(category);
        }

        private void FromCategory(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Color = new SolidColorBrush(category.Color);
        }

        public Category ToCategory()
        {
            return ToCategory(this);
        }

        public static Category ToCategory(CategoryViewModel viewModel)
        {
            return new Category
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Color = ((SolidColorBrush)viewModel.Color).Color
            };
        }

        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        public string Id { get; private set; }

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