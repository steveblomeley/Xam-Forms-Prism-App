using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using XamFormsPrism.Helpers;
using XamFormsPrism.Models;
using XamFormsPrism.Services;

namespace XamFormsPrism.ViewModels
{
    // ReSharper disable once UnusedMember.Global
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();
        public ICommand GoToNextPageCommand { get; }
        public ICommand ShowContactDetailsCommand { get; }

        private readonly IContactService _contactService;

        public MainPageViewModel(INavigationService navigationService, IContactService contactService)
            : base(navigationService)
        {
            _contactService = contactService;

            Title = "Welcome to Xamarin Forms and Prism!";

            GoToNextPageCommand = new DelegateCommand(
                async () => await GoToNextPage());
            ShowContactDetailsCommand = new DelegateCommand<Contact>(
                async contact => await ShowContactDetails(contact));
        }

        private async Task GoToNextPage()
        {
            var anotherPageParams = new NavigationParameters
            {
                {"AnotherParameter", "New Page Title"}
            };

            await NavigationService.NavigateAsync("AnotherPage", anotherPageParams);
        }

        private async Task ShowContactDetails(Contact contact)
        {
            var contactDetailsPageParameters = new NavigationParameters
            {
                {"Contact", contact}
            };

            await NavigationService.NavigateAsync("ContactDetailsPage", contactDetailsPageParameters);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.TryGetParam<string>("AnotherParameter", out var paramString))
            { 
                Title = paramString;
            }

            if (!Contacts.Any())
            {
                var contacts = _contactService.GetContacts();
                foreach (var contact in contacts)
                {
                    //TODO: opportunity to use enhanced observable collection from MVVM Helpers package?
                    Contacts.Add(contact);
                }
}
        }
    }
}