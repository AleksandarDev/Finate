using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Finate.UWP.Controls
{
    public sealed partial class QuickTransactionView
    {
        public ICommand Confirmed
        {
            get { return (ICommand)GetValue(ConfirmedProperty); }
            set { SetValue(ConfirmedProperty, value); }
        }

        public static readonly DependencyProperty ConfirmedProperty =
            DependencyProperty.Register("ConfirmedProperty", typeof(ICommand), typeof(QuickTransactionView), new PropertyMetadata(null));
        

        public QuickTransactionView()
        {
            this.InitializeComponent();
        }


        private void IncomeToggleOnChecked(object sender, RoutedEventArgs e)
        {
            if (this.ConfirmButton != null && this.IncomeToggle.IsChecked.HasValue && this.IncomeToggle.IsChecked.Value)
                this.ConfirmButton.Background = (SolidColorBrush) App.Current.Resources["IncomeButtonBackgroundBrush"];

            if (this.ExpenseToggle != null)
                this.ExpenseToggle.IsChecked = !(this.IncomeToggle.IsChecked ?? false);
        }

        private void ExpenseToggleOnChecked(object sender, RoutedEventArgs e)
        {
            if (this.ConfirmButton != null && this.ExpenseToggle.IsChecked.HasValue && this.ExpenseToggle.IsChecked.Value)
                this.ConfirmButton.Background = (SolidColorBrush)App.Current.Resources["ExpenseButtonBackgroundBrush"];

            if (this.IncomeToggle != null)
                this.IncomeToggle.IsChecked = !(this.ExpenseToggle.IsChecked ?? false);
        }

        private void ConfirmButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Confirmed?.Execute(null);
        }
    }
}
