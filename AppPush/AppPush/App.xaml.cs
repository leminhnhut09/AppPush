
using AppPush.ViewModels;
using AppPush.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;

namespace AppPush
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
            InitializeComponent();
        }

        protected async override void OnInitialized()
        {
            var rs = await NavigationService.NavigateAsync(nameof(PushPageViewModel));
            if (!rs.Success)
            {
                Console.WriteLine("Error");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PushPage, PushPageViewModel>(nameof(PushPageViewModel));
        }
    }
}
