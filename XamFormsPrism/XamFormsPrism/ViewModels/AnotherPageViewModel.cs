using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using XamFormsPrism.Services;

namespace XamFormsPrism.ViewModels
{
	public class AnotherPageViewModel : ViewModelBase
	{
        public ICommand GoBackCommand { get; }
        private IDataService _dataService;

        public AnotherPageViewModel(INavigationService navigationService, IDataService dataService) : base (navigationService)
        {
            _dataService = dataService;
            Title = "Another Page";
            GoBackCommand = new DelegateCommand(async () => await GoBack());
        }

	    private async Task GoBack()
	    {
	        var newPageTitle = _dataService.GetData();

	        var anotherPageParams = new NavigationParameters
	        {
	            {"AnotherParameter", newPageTitle}
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