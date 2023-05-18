using Grizzly.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Grizzly.ViewModels
{
    public class MainViewModel
    {
        public bool ForwardArrowEnabled { get; set; } = false;
        public bool BackwardArrowEnabled { get; set; } = false;
        public Page CurrentPage { get; set; } = new DrugListPage();
        
        public ObservableCollection<string> UserMenuOptions = new ObservableCollection<string>();

        public MainViewModel()
        {
            UserMenuOptions.Add("Log in");
            UserMenuOptions.Add("Sign in");
        }
    }
}
