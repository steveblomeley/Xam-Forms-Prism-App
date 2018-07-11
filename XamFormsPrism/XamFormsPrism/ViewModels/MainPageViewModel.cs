using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XamFormsPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
            GoToNextPageCommand = new DelegateCommand(async () => await GoToNextPage());
        }

        private INavigationService _navigationService;
        public ICommand GoToNextPageCommand { get; }

        private async Task GoToNextPage()
        {
            await NavigationService.NavigateAsync("AnotherPage");
        }
    }
}