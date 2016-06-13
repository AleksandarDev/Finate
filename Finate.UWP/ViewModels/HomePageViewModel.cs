using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Finate.Models;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace Finate.UWP.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private QuickTransactionViewModel quickTransaction = new QuickTransactionViewModel();


        public HomePageViewModel()
        {
        }

        public void QuickTransactionConfirmedCommandExecute()
        {
            
        }

        public QuickTransactionViewModel QuickTransaction
        {
            get { return this.quickTransaction; }
            set { this.SetProperty(ref this.quickTransaction, value); }
        }

        public ObservableCollection<TransactionViewModel> TodaysTransactions { get; } = 
            new ObservableCollection<TransactionViewModel>
        {
            new TransactionViewModel
            {
                Name = "Test",
                Category = new CategoryViewModel
                {
                    Name = "Food",
                    Color = new SolidColorBrush(Colors.Crimson)
                },
                Amount = 125,
                Date = DateTime.Now,
                Type = "Income",
                TypeColor = new SolidColorBrush(Colors.LightSeaGreen)
            }
        };

        public ObservableCollection<CategoryViewModel> Categories { get; } = new ObservableCollection<CategoryViewModel>()
        {
            new CategoryViewModel
            {
                Name = "Food",
                Color = new SolidColorBrush(Colors.Crimson)
            },
            new CategoryViewModel
            {
                Name = "Gas",
                Color = new SolidColorBrush(Colors.DarkSeaGreen)
            },
            new CategoryViewModel
            {
                Name = "Shopping",
                Color = new SolidColorBrush(Colors.Purple)
            }
        };

        public ObservableCollection<Transaction> WeeklyExpenses { get; } = new ObservableCollection<Transaction>()
        {
            new Transaction()
            {
                Amount = 80,
                Date = new DateTime(2016, 6, 2)
            },
            new Transaction()
            {
                Amount = 180,
                Date = new DateTime(2016, 6, 3)
            },
            new Transaction()
            {
                Amount = 120,
                Date = new DateTime(2016, 6, 4)
            },
            new Transaction()
            {
                Amount = 90,
                Date = new DateTime(2016, 6, 5)
            },
            new Transaction()
            {
                Amount = 70,
                Date = new DateTime(2016, 6, 6)
            },
            new Transaction()
            {
                Amount = 20,
                Date = new DateTime(2016, 6, 7)
            }
        };

        public ObservableCollection<Transaction> PreviousWeeklyExpenses { get; } = new ObservableCollection<Transaction>()
        {
            new Transaction()
            {
                Amount = 120,
                Date = new DateTime(2016, 6, 2)
            },
            new Transaction()
            {
                Amount = 70,
                Date = new DateTime(2016, 6, 3)
            },
            new Transaction()
            {
                Amount = 80,
                Date = new DateTime(2016, 6, 4)
            },
            new Transaction()
            {
                Amount = 100,
                Date = new DateTime(2016, 6, 5)
            },
            new Transaction()
            {
                Amount = 140,
                Date = new DateTime(2016, 6, 6)
            },
            new Transaction()
            {
                Amount = 80,
                Date = new DateTime(2016, 6, 7)
            },
            new Transaction()
            {
                Amount = 120,
                Date = new DateTime(2016, 6, 8)
            }
        };
    }
}
