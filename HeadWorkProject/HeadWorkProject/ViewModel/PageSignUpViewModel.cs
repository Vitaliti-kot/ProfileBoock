using Acr.UserDialogs;
using HeadWorkProject.Model;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Verification;
using HeadWorkProject.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace HeadWorkProject.ViewModel
{
    public class PageSignUpViewModel : BindableBase, INavigationAware, IDestructible
    {
        private string _login = "";
        private string _password1 = "";
        private string _password2 = "";
        IRepository _repository { get; set; }
        INewUserVerification _verification;
        public string Login
        {
            get { return _login; }
            set
            {
                SetProperty(ref _login, value);
            }
        }

        public string Password1
        {
            get { return _password1; }
            set
            {
                SetProperty(ref _password1, value);
            }
        }
        public string Password2
        {
            get { return _password2; }
            set
            {
                SetProperty(ref _password2, value);
            }
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        private DelegateCommand _navigateCommand;
        public DelegateCommand NavigateToMainPage => _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        private readonly INavigationService _navigationService;
        private async void ExecuteNavigateCommand()
        {
            var usersFromDB = await _repository.GetAllAsync<User>();
            var usersCollection = new ObservableCollection<User>(usersFromDB);
            _verification._users = usersCollection;
            var res = _verification.IsCorrect(Login, Password1);
            if (res != null) UserDialogs.Instance.Alert(res);
            else
            {
                var user = new User
                {
                    Login = Login,
                    Password = Password1
                };
                var id = await _repository.InsertAsync(user);
               // UserDialogs.Instance.Alert($"id={id}");
                user.Id = id;
                var parameters = new NavigationParameters
                {
                    { nameof(Login), Login }
                };
                await _navigationService.GoBackAsync(parameters);
               // await _navigationService.NavigateAsync($"{nameof(MainPage)}", parameters);
                
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }
        public PageSignUpViewModel(INavigationService navigationService, IRepository repository, INewUserVerification userVerification)
        {
            _navigationService = navigationService;
            _verification = userVerification;
            _repository = repository;
        }
    }
}
