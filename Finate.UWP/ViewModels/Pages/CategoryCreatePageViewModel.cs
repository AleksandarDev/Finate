using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Finate.Data;
using Finate.Models;
using Finate.UWP.Annotations;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace Finate.UWP.ViewModels
{
    public class CategoryCreatePageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly ILocalDbContext context;
        private string name;
        private Brush color;


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
                Id = Guid.NewGuid().ToString(),
                Name = this.name,
                Color = solidColorBrush.Color,
                GroupId = this.context.Groups.FirstOrDefault()?.Id
            });
            await this.context.SaveContextAsync();

            // Navigate away from this page
            // Navigate back if possible; to categories page if can't go back
            if (this.navigationService.CanGoBack())
                this.navigationService.GoBack();
            else this.navigationService.Navigate(PageTokens.CategoriesPage, null);
        }


        public string Name
        {
            get { return this.name; }
            set
            {
                this.SetProperty(ref this.name, value);
                (this.CreateCategoryCommand as DelegateCommand)?.RaiseCanExecuteChanged();
            }
        }

        public Brush Color
        {
            get { return this.color; }
            set { this.SetProperty(ref this.color, value); }
        }

        public IEnumerable<Brush> AvailableColor { get; }

        /// <summary>
        /// Gets the create categoy command.
        /// </summary>
        public ICommand CreateCategoryCommand { get; }
    }
}
