using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Finate.UWP.DAL;
using Finate.UWP.Models;
using Finate.UWP.Properties;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace Finate.UWP.ViewModels
{
    /// <summary>
    /// The category create page view model.
    /// </summary>
    /// <seealso cref="Prism.Windows.Mvvm.ViewModelBase" />
    public class CategoryCreatePageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly ILocalDbContext context;
        private string name;
        private Brush color;


        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryCreatePageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">
        /// navigationService
        /// or
        /// context
        /// </exception>
        public CategoryCreatePageViewModel(
            [NotNull] INavigationService navigationService,
            [NotNull] ILocalDbContext context)
        {
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            if (context == null) throw new ArgumentNullException(nameof(context));
            this.navigationService = navigationService;
            this.context = context;

            this.AvailableColor = new List<SolidColorBrush>
            {
                new SolidColorBrush(new Color {A=255,R = 255, G = 255, B = 0}),
                new SolidColorBrush(new Color {A=255,R = 255, G = 0, B = 255}),
                new SolidColorBrush(new Color {A=255,R = 255, G = 0, B = 0}),
                new SolidColorBrush(new Color {A=255,R = 0, G = 255, B = 255})
            };
            this.Color = this.AvailableColor.First();

            this.CreateCategoryCommand = new DelegateCommand(this.CreateCategoryCommandExecute, this.CreateCategoryCommandCanExecute);
        }

        private bool CreateCategoryCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(this.name);
        }

        /// <summary>
        /// Handles the create category command execution.
        /// </summary>
        private async void CreateCategoryCommandExecute()
        {
            // Validate name
            if (string.IsNullOrWhiteSpace(this.name))
                throw new InvalidOperationException("Provided category name is invalid.");

            // Validate color
            var solidColorBrush = this.color as SolidColorBrush;
            if (solidColorBrush == null)
                throw new InvalidOperationException("Selected color is not valid category color.");

            // Create new category
            this.context.Categories.Add(new Category
            {
                Name = this.name,
                Color = solidColorBrush.Color.ToString(),
                GroupId = this.context.Groups.FirstOrDefault().Id
            });
            await this.context.SaveChangesAsync();

            // Navigate away from this page
            // Navigate back if possible; to categories page if can't go back
            if (this.navigationService.CanGoBack())
                this.navigationService.GoBack();
            else this.navigationService.Navigate(PageTokens.CategoriesPage, null);
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.name; }
            set
            {
                this.SetProperty(ref this.name, value);
                (this.CreateCategoryCommand as DelegateCommand)?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Brush Color
        {
            get { return this.color; }
            set { this.SetProperty(ref this.color, value); }
        }

        /// <summary>
        /// Gets the collection of available colors.
        /// </summary>
        /// <value>
        /// The collection of available colors.
        /// </value>
        public IEnumerable<Brush> AvailableColor { get; }

        /// <summary>
        /// Gets the create category command.
        /// </summary>
        public ICommand CreateCategoryCommand { get; }
    }
}
