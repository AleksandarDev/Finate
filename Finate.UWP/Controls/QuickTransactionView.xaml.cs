using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Finate.UWP.Controls
{
    /// <summary>
    /// The quick transaction view.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class QuickTransactionView
    {
        /// <summary>
        /// Gets or sets the confirmed command.
        /// </summary>
        /// <value>
        /// The confirmed command.
        /// </value>
        public ICommand Confirmed
        {
            get { return (ICommand)GetValue(ConfirmedProperty); }
            set { SetValue(ConfirmedProperty, value); }
        }

        /// <summary>
        /// The confirmed command property
        /// </summary>
        public static readonly DependencyProperty ConfirmedProperty =
            DependencyProperty.Register("ConfirmedProperty", typeof(ICommand), typeof(QuickTransactionView), new PropertyMetadata(null));


        /// <summary>
        /// Initializes a new instance of the <see cref="QuickTransactionView"/> class.
        /// </summary>
        public QuickTransactionView()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// Handles the income toggle button checked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void IncomeToggleOnChecked(object sender, RoutedEventArgs e)
        {
            if (this.ConfirmButton != null && this.IncomeToggle.IsChecked.HasValue && this.IncomeToggle.IsChecked.Value)
                this.ConfirmButton.Background = (SolidColorBrush) App.Current.Resources["IncomeButtonBackgroundBrush"];

            if (this.ExpenseToggle != null)
                this.ExpenseToggle.IsChecked = !(this.IncomeToggle.IsChecked ?? false);
        }

        /// <summary>
        /// Handles the expense toggle button checked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExpenseToggleOnChecked(object sender, RoutedEventArgs e)
        {
            if (this.ConfirmButton != null && this.ExpenseToggle.IsChecked.HasValue && this.ExpenseToggle.IsChecked.Value)
                this.ConfirmButton.Background = (SolidColorBrush)App.Current.Resources["ExpenseButtonBackgroundBrush"];

            if (this.IncomeToggle != null)
                this.IncomeToggle.IsChecked = !(this.ExpenseToggle.IsChecked ?? false);
        }

        /// <summary>
        /// Handles the confirm button click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ConfirmButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Confirmed?.Execute(null);
        }
    }
}
