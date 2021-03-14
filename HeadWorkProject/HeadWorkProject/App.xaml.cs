using HeadWorkProject.Dialog;
using HeadWorkProject.Dialog.ViewModel;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Repositoryi;
using HeadWorkProject.Srvices.Verification;
using HeadWorkProject.View;
using HeadWorkProject.ViewModel;
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
            containerRegistry.RegisterDialog<ImageDialog, DialogViewModel>();
            containerRegistry.RegisterForNavigation<PropertyPageView, PropertyPageViewModel>();
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            App.Current.UserAppTheme = OSAppTheme.Dark;
            App.Current.SetAppThemeColor(MenuProperty, Color.Aqua, Color.Black);
            App.Current.SetAppThemeColor(ClassIdProperty, Color.Blue, Color.DarkBlue);
            App.Current.SetAppThemeColor(Button.BackgroundColorProperty, Color.Aqua, Color.Black);
            App.Current.SetAppThemeColor(StackLayout.BackgroundColorProperty, Color.Crimson, Color.DarkBlue);
            App.Current.SetAppThemeColor(Grid.BackgroundColorProperty, Color.Crimson, Color.DarkBlue);
            await NavigationService.NavigateAsync($"NavigationPage/{nameof(MainPage)}");
        }
        #endregion

    }
}
