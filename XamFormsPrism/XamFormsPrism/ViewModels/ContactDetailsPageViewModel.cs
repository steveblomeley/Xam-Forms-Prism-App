using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using XamFormsPrism.Constants;
using XamFormsPrism.Helpers;
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

            if (parameters.TryGetParam<Contact>(ContactDetailsPage.Contact, out var contact))
            {
                Contact = contact;
            }
        }

        private async Task GoBack()
        {
            await NavigationService.GoBackAsync();
        }
    }
}