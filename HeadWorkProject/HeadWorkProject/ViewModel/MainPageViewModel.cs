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
        private string _login="";
        private string _password="";
       
        public string Login
        {
            get { return _login; }
            set
            {
                SetProperty(ref _login, value);
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
            IRepository rep = new Repository();
            ILoginValidation loginValidation = new LoginValidation(rep);
            var Id = loginValidation.Success(Login, Password);
            if (Id != -1) ToMainList(Id);
            else UserDialogs.Instance.ShowError("Неверный логин или пароль");
        }

        public async void ToMainList(int id)
        {
            var param = new NavigationParameters();
            param.Add("id", id);
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
          //  Login = parameters.GetValue<string>(nameof(Login));

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Login = parameters.GetValue<string>(nameof(Login));
        }
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
