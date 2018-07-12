using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;

namespace XamFormsPrism.ViewModels
{
	public class AnotherPageViewModel : ViewModelBase
	{
        public ICommand GoBackCommand { get; }

        public AnotherPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            Title = "Another Page";
            GoBackCommand = new DelegateCommand(async () => await GoBack());
        }

	    private async Task GoBack()
	    {
	        var anotherPageParams = new NavigationParameters
	        {
	            {"AnotherParameter", "New Page Title"}
	        };

	        await NavigationService.GoBackAsync(anotherPageParams);
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