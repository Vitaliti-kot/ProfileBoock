using HeadWorkProject.Model;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HeadWorkProject.ViewModel
{
    public class ProfileListViewModel : BindableBase, INavigationAware, IDestructible
    {
        private int _id;
        public readonly INavigationService _navigation;
        public ObservableCollection<Profile> profiles;
        private string iconSource;
        private string nickName;
        private string firstName;
        private string lastName;
        private DateTime creationTime;
        private string name;
        public string Name
        {
            get { return name; }
            set { name = $"{firstName} {lastName}"; }
        }
        public DateTime DateCreation
        {
            get { return creationTime; }
            set { SetProperty(ref creationTime, value); }
        }
        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }
        public string NickName
        {
            get { return nickName; }
            set { SetProperty(ref nickName, value);}
        }
        public string Icon
        {
            get { return iconSource; }
            set { SetProperty(ref iconSource, value); }
        }
        public ObservableCollection<Profile> Profiles
        {
            get { return profiles; }
            set { SetProperty(ref profiles, value); }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public ProfileListViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            Profiles = new ObservableCollection<Profile>()
            {
                new Profile()
                {
                    Icon="pic_profile.png",
                    NickName="VP",
                    FirstName="Vasia",
                    LastName="Pupkin",
                    DateCreation=DateTime.Now
                },
                 new Profile()
                {
                    Icon="pic_profile.png",
                    NickName="VPV",
                    FirstName="Masha",
                    LastName="Rasputina",
                    DateCreation=DateTime.Now
                }
            };
        }
        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Id = parameters.GetValue<int>($"{nameof(Id)}");
        }
    }
}
