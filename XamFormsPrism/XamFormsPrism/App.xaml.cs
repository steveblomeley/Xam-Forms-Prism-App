﻿using Prism;
using Unity;
using Prism.Ioc;
using XamFormsPrism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Unity.Injection;
using XamFormsPrism.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamFormsPrism
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync("NavigationPage/MainPage");
            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<AnotherPage>();
            containerRegistry.RegisterForNavigation<ContactDetailsPage>();

            var ctr = containerRegistry.GetContainer();

            ctr.RegisterType<IDataService, DataService>(
                new InjectionConstructor(
                    new ResolvedParameter<IPlatformSpecificService>(), "injected string"));

            //dryIoc code to inject string as parameter - doesn't appear to work though ...
            //ctr.Register<IDataService, DataService>();
            //ctr.Register<DataService>(made: Parameters.Of.Type<string>(serviceKey: "injectedString"));
            //ctr.RegisterInstance("Injected Fragment", serviceKey: "injectedString");

            ctr.RegisterType<IContactService, ContactService>();
        }
    }
}