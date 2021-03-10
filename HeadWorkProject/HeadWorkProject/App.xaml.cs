using HeadWorkProject.Srvices;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Repositoryi;
using HeadWorkProject.Srvices.Verification;
using HeadWorkProject.View;
using HeadWorkProject.ViewModel;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace HeadWorkProject
{
    public partial class App:PrismApplication
    {
        public App()
        {
        }
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
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IRepositoryProfile>(Container.Resolve<ProfilesRepository>());
            containerRegistry.RegisterInstance<INewUserVerification>(Container.Resolve<NewUserVerification>());
            containerRegistry.RegisterInstance<ILoginValidation>(Container.Resolve<LoginValidation>());
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PageSignUp, PageSignUpViewModel>();
            containerRegistry.RegisterForNavigation<ProfileList, ProfileListViewModel>();
            containerRegistry.RegisterForNavigation<EditProfile, EditProfileViewModel>();
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"NavigationPage/{nameof(MainPage)}");
        }
        #endregion

    }
}
