using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Services;
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
        private readonly IPageDialogService _dialogService;

        public MainPageViewModel(INavigationService navigationService, IContactService contactService, IPageDialogService dialogService) 
            : base (navigationService)
        {
            _contactService = contactService;
            _dialogService = dialogService;

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
            await _dialogService.
                DisplayAlertAsync("Contact Tapped", $"You tapped contact #{contact.Id}", "OK", "Cancel");
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