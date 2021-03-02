using HeadWorkProject.Srvices;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Verification;
using HeadWorkProject.View;
using HeadWorkProject.ViewModel;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace HeadWorkProject
{
    public partial class App
    {
        public App() : this(null)
        {
        }
        public App(IPlatformInitializer initializer) : base(initializer) { }
        #region --- Overrides---
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PageSignUp, PageSignUpViewModel>();
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<INewUserVerification>(Container.Resolve<NewUserVerification>());
            containerRegistry.RegisterInstance<ILoginValidation>(Container.Resolve<LoginValidation>());
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"{nameof(MainPage)}");
        }
        #endregion

    }
}
