using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finate.Models;
using Prism.Windows.Mvvm;

namespace Finate.UWP.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
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
                Amount = 140,
                Date = new DateTime(2016, 6, 7)
            },
            new Transaction()
            {
                Amount = 140,
                Date = new DateTime(2016, 6, 7)
            }
        };
    }
}
