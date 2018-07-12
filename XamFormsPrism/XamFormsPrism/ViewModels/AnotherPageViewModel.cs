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
        public ICommand GetDataCommand { get; }
        private readonly IDataService _dataService;

        public AnotherPageViewModel(INavigationService navigationService, IDataService dataService) : base (navigationService)
        {
            _dataService = dataService;
            NewPageTitle = "Default Title";

            GoBackCommand = new DelegateCommand(async () => await GoBack(), () => !IsBusy);
            GetDataCommand = new DelegateCommand(async () => await GetData(), () => !IsBusy);
        }

	    private string _newPageTitle;
	    public string NewPageTitle
	    {
	        get => _newPageTitle;
            set => SetProperty(ref _newPageTitle, value);
        }

	    private bool _isBusy;

	    public bool IsBusy
	    {
            get => _isBusy;
	        set => SetProperty(ref _isBusy, value);
	    }

        private async Task GoBack()
	    {
	        var anotherPageParams = new NavigationParameters
	        {
	            {"AnotherParameter", NewPageTitle}
	        };

	        await NavigationService.GoBackAsync(anotherPageParams);
	    }

	    private async Task GetData()
	    {
	        IsBusy = true;
	        await Task.Delay(5000);
            IsBusy = false;
	        NewPageTitle = _dataService.GetData();
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