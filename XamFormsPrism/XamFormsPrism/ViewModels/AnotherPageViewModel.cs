using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;

namespace XamFormsPrism.ViewModels
{
	public class AnotherPageViewModel : ViewModelBase
	{
	    public string Title => "Another Page";
        public ICommand GoBackCommand { get; }

        public AnotherPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            GoBackCommand = new DelegateCommand(async () => await GoBack());
        }

	    private async Task GoBack()
	    {
	        await NavigationService.GoBackAsync();
	    }
	}
}