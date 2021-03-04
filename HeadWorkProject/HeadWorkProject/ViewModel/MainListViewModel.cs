using HeadWorkProject.Model;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeadWorkProject.ViewModel
{
    public class MainListViewModel:BindableBase, INavigatedAware
    {
        private ObservableCollection<Profile> _profiles;
        private int _userId;
        private string _profileImage;
        private string _nickName;
        private string _lastName;
        private string _firstName;
        private DateTime _dateCreating;
        INavigationService _navigationService;

        public MainListViewModel(INavigationService navigationService, INavigationParameters parameters)
        {
            _navigationService = navigationService;
            
        }
        public string ProfileImage
        {
            get { return _profileImage; }
            set { SetProperty(ref _profileImage, value); }
        }
        public string NickName
        {
            get { return _nickName; }
            set { SetProperty(ref _nickName, value); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        public int Id
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
        public DateTime DataCreating
        {
            get { return _dateCreating; }
            set { SetProperty(ref _dateCreating, value); }
        }
        public ObservableCollection<Profile> Profiles
        {
            get { return _profiles; }
            set { SetProperty(ref _profiles, value); }
        }
        public ICommand LogOutCommand => new Command(LogOut);

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _userId = parameters.GetValue<int>("id");
        }

        private void LogOut(object obj)
        {
        }
    }
}
