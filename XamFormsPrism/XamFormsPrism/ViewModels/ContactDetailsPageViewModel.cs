using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using XamFormsPrism.Models;

namespace XamFormsPrism.ViewModels
{
    // ReSharper disable once UnusedMember.Global
    public class ContactDetailsPageViewModel : ViewModelBase
    {
        private Contact _contact;

        public Contact Contact
        {
            get => _contact;
            private set => SetProperty(ref _contact, value);
        }
        public ICommand GoBackCommand { get; }

        public ContactDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoBackCommand = new DelegateCommand(async () => await GoBack());
        }

	    public override void OnNavigatingTo(NavigationParameters parameters)
	    {
	        base.OnNavigatingTo(parameters);

	        if (parameters.TryGetValue<object>("Contact", out var contactParameter))
	        {
	            if (contactParameter is Contact contact)
	            {
	                Contact = contact;
	            }
	        }
	    }

	    private async Task GoBack()
	    {
	        await NavigationService.GoBackAsync();
	    }
	}
}