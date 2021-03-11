using Acr.UserDialogs;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Verification;
using HeadWorkProject.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace HeadWorkProject.ViewModel
{
    public class MainPageViewModel : BindableBase, INavigationAware, IDestructible
    {
        #region ---Fields---
        private string _login;
        private string _password;
        private int _id;
        IRepository rep;
        ILoginValidation loginValidation;
        private DelegateCommand _navigateCommand;
        private DelegateCommand _navigateCommand1;
        private readonly INavigationService _navigationService;
        #endregion
        #region ---SetProperty---
        public string Login
        {
            get { return _login; }
            set
            {
                SetProperty(ref _login, value);
            }
        }
        public int UserId
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

        #endregion
        public DelegateCommand NavigateToPageSignIn => _navigateCommand1 ?? (_navigateCommand1 = new DelegateCommand(ExecuteNavigateCommand));
        public DelegateCommand NavigateToMainList => _navigateCommand ?? (_navigateCommand = new DelegateCommand(TapButtonLogin));
        #region ---Methods---
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
       
        private async void TapButtonLogin()
        {
            UserId = loginValidation.Success(Login, Password);
            await Task.Delay(2000);
            if (UserId == -1) UserDialogs.Instance.Alert("Неверный логин или пароль");
            else
            {
                var parameters = new NavigationParameters()
                {
                    { nameof(UserId), UserId }
                };
                await _navigationService.NavigateAsync($"{nameof(ProfileList)}", parameters);
            }
        }
      
      
       
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
            loginValidation = new LoginValidation(rep);
        }
        public MainPageViewModel(INavigationService navigationService, IRepository repository, ILoginValidation login)
        {
            _navigationService = navigationService;
            rep = repository;
            loginValidation = login;
        }
        #endregion
    }
}
