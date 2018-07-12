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
            Title = "Welcome to Xamarin Forms and Prism!";
            GoToNextPageCommand = new DelegateCommand(async () => await GoToNextPage());
        }

        public ICommand GoToNextPageCommand { get; }

        private async Task GoToNextPage()
        {
            var anotherPageParams = new NavigationParameters();
            anotherPageParams.Add("AnotherParameter", "New Page Title");

            await NavigationService.NavigateAsync("AnotherPage", anotherPageParams);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters.TryGetValue<object>("AnotherParameter", out var anotherParam))
            {
                if (anotherParam is string paramString)
                {
                    Title = paramString;
                }
            }
        }
    }
}