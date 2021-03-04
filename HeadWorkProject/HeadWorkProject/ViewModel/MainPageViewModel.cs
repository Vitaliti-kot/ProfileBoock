using Acr.UserDialogs;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Verification;
using HeadWorkProject.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeadWorkProject.ViewModel
{
    public class MainPageViewModel : BindableBase, INavigationAware, IDestructible
    {
        private string _login = "";
        private string _password = "";
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

        public ICommand ButtonLogin => new Command(TapButtonLogin);

        public void TapButtonLogin(object obj)
        {
            Id = loginValidation.Success(Login, Password);
            if (Id != -1) ExecuteToMainList();
            else UserDialogs.Instance.ShowError("Неверный логин или пароль");
        }


        private async void ExecuteToMainList()
        {
            var param = new NavigationParameters();
            param.Add("id", Id);
            await _navigationService.NavigateAsync($"{nameof(MainList)}", param);
        }
        
        private DelegateCommand _navigateCommand;
        public DelegateCommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        private INavigationService _navigationService;
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
        }
        public MainPageViewModel(INavigationService navigationService, IRepository repository, ILoginValidation login)
        {
            _navigationService = navigationService;
            rep = repository;
            loginValidation = login;
        }
    }
}
