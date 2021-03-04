using Acr.UserDialogs;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Verification;
using HeadWorkProject.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeadWorkProject.ViewModel
{
    public class MainPageViewModel : BindableBase, INavigationAware, IDestructible
    {
        private string _login;
        private string _password;
        private int _id;
        IRepository rep;
        ILoginValidation loginValidation;
        public string Login
        {
            get { return _login; }
            set
            {
                SetProperty(ref _login, value);
            }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }


        private DelegateCommand _navigateCommand;
        public DelegateCommand NavigateToMainList => _navigateCommand ?? (_navigateCommand = new DelegateCommand(TapButtonLogin));
        private async void TapButtonLogin()
        {
            Id = loginValidation.Success(Login, Password);
            if (Id == -1) UserDialogs.Instance.Alert("Неверный логин или пароль");
            else{
                //UserDialogs.Instance.Alert($"Id={Id}");
                var parameters = new NavigationParameters();
                parameters.Add(nameof(Id), Id);
                await _navigationService.NavigateAsync($"/NavigationPage/{nameof(ProfileList)}", parameters);
            }
        }
        private DelegateCommand _navigateCommand1;
        public DelegateCommand NavigateToPageSignIn => _navigateCommand1 ?? (_navigateCommand1 = new DelegateCommand(ExecuteNavigateCommand));

        private readonly INavigationService _navigationService;
        public async void ExecuteNavigateCommand()
        {
            await _navigationService.NavigateAsync($"{nameof(PageSignUp)}");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Login = parameters.GetValue<string>(nameof(Login));
            rep = new Repository();
        }
        public MainPageViewModel(INavigationService navigationService, IRepository repository, ILoginValidation login)
        {
            _navigationService = navigationService;
            rep = repository;
            loginValidation = login;
        }
    }
}
