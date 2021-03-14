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
        #region ---Fields---
        private string _login;
        private string _password1;
        private string _password2;
        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;
        IRepository _repository { get; set; }
        INewUserVerification _verification;
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
        #endregion

        #region ---Methods---
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
        public DelegateCommand NavigateToMainPage => _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));


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
                user.Id = id;
                var parameters = new NavigationParameters
                {
                    { nameof(Login), Login }
                };
                await _navigationService.GoBackAsync(parameters);

            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        #endregion
        public PageSignUpViewModel(INavigationService navigationService, IRepository repository, INewUserVerification userVerification)
        {
            _navigationService = navigationService;
            _verification = userVerification;
            _repository = repository;
        }
    }
}
