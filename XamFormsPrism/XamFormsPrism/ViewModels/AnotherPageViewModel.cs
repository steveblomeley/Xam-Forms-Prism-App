﻿using Prism.Commands;
using System.Threading.Tasks;
using Prism.Navigation;
using XamFormsPrism.Constants;
using XamFormsPrism.Helpers;
using XamFormsPrism.Services;

namespace XamFormsPrism.ViewModels
{
    // ReSharper disable once UnusedMember.Global
    public class AnotherPageViewModel : ViewModelBase
    {
        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GetDataCommand { get; }
        private readonly IDataService _dataService;

        public AnotherPageViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService)
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
            set
            {
                SetProperty(ref _isBusy, value);
                GoBackCommand.RaiseCanExecuteChanged();
                GetDataCommand.RaiseCanExecuteChanged();
            }
        }

        private async Task GoBack()
        {
            var anotherPageParams = new NavigationParameters
            {
                {MainPage.AnotherParameter, NewPageTitle}
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

            if (parameters.TryGetParam<string>(AnotherPage.AnotherParameter, out var paramString))
            {
                Title = paramString;
            }
        }
    }
}