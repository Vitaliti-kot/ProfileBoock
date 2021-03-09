﻿using Acr.UserDialogs;
using HeadWorkProject.Model;
using Prism.Commands;
using Prism.Events;
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
    public class ProfileListViewModel : BindableBase, INavigationAware, IDestructible
    {
       
        private int _id;
        public DelegateCommand <object> EditProfileCommand { protected set; get; }
        public ICommand DeleteProfileCommand { protected set; get; }
        public readonly INavigationService _navigation;
        public ObservableCollection<Profile> profiles { get; set; }
        //private string iconSource;
        //private string nickName;
        //private string firstName;
        //private string lastName;
        //private DateTime creationTime;
        //private string name;
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { SetProperty(ref userId, value); }
        }
        //public string Name
        //{
        //    get { return name; }
        //    set { name = $"{firstName} {lastName}"; }
        //}
        //public DateTime DateCreation
        //{
        //    get { return creationTime; }
        //    set { SetProperty(ref creationTime, value); }
        //}
        //public string LastName
        //{
        //    get { return lastName; }
        //    set { SetProperty(ref lastName, value); }
        //}
        //public string FirstName
        //{
        //    get { return firstName; }
        //    set { SetProperty(ref firstName, value); }
        //}
        //public string NickName
        //{
        //    get { return nickName; }
        //    set { SetProperty(ref nickName, value);}
        //}
        //public string _Icon
        //{
        //    get { return iconSource; }
        //    set { SetProperty(ref iconSource, value); }
        //}
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
            profiles = new ObservableCollection<Profile>()
            {
                new Profile()
                {
                    userId=UserId,
                    _Icon="pic_profile.png",
                    NickName="VP",
                    FirstName="Vasia",
                    LastName="Pupkin",
                    DateCreation=DateTime.Now
                },
                 new Profile()
                {
                     userId=UserId,
                    _Icon="pic_profile.png",
                    NickName="VPV",
                    FirstName="Masha",
                    LastName="Rasputina",
                    DateCreation=DateTime.Now
                }
            };
            EditProfileCommand = new DelegateCommand<object>(EditProfile);
            DeleteProfileCommand = new Command(DeleteProfile);
        }

        public async void DeleteProfile(object obj)
        {
            var p = obj as Profile;
            await UserDialogs.Instance.AlertAsync($"delet{p.NickName}");
        }

        public async void EditProfile(object obj)
        {
            var p = obj as Profile;
            await UserDialogs.Instance.AlertAsync($"edit{p.NickName}");
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
