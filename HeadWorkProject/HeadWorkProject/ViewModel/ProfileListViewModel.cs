using Acr.UserDialogs;
using HeadWorkProject.Model;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Repositoryi;
using HeadWorkProject.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeadWorkProject.ViewModel
{
    public class ProfileListViewModel : BindableBase, INavigationAware, IDestructible
    {

        private int _id;
        public ICommand EditProfileCommand { protected set; get; }
        public ICommand DeleteProfileCommand { protected set; get; }
        public ICommand AddNewProfile { protected set; get; }

        private readonly INavigationService _navigationService;
        private IRepositoryProfile _repositoryProfile;
        private INavigationParameters _parameters;
        public ObservableCollection<Profile> Profiles { get; set; }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { SetProperty(ref userId, value); }
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
            _navigationService = navigation;
            _repositoryProfile = new ProfilesRepository();
            EditProfileCommand = new Command(EditProfileComm);
            DeleteProfileCommand = new Command(DeleteProfile);
            AddNewProfile = new Command(AddProfile);
            IsVisibleList();
        }

        private async void GetCollection()
        {
            var prfls = await _repositoryProfile.GetAllAsync<Profile>();
            Profiles.Clear();
            foreach(Profile prof in prfls)
            {
                if(prof.UserId==UserId) Profiles.Add(prof);
            }
        }
        public async void DeleteProfile(object obj)
        {
            var prfls = await _repositoryProfile.GetAllAsync<Profile>();
            var p = obj as Profile;
            foreach(Profile pr in prfls)
            {
                if (pr.Id == p.Id&&p.UserId==pr.UserId)
                {
                    var r = await _repositoryProfile.DeleteAsync(pr);
                    
                    break;
                }
            }
            RefreshList();
        }

        public async void EditProfileComm(object obj)
        {
            var profile = obj as Profile;
            var parameters = new NavigationParameters
            {
                { "profile", profile }
            };
            var res = await _navigationService.NavigateAsync($"{nameof(EditProfile)}", parameters);
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
                UserId = parameters.GetValue<int>($"{nameof(UserId)}");
                
                var res = parameters.GetValue<Profile>("profile");
                if (res.NickName != null)
                {
                    if (res.Id >= Profiles.Count)
                    {
                        Profiles.Add(res);
                        InsertNewProfile(res);
                    }
                    else
                    {
                        foreach (Profile p in Profiles)
                        {
                            if (p.Id == res.Id)
                            {
                            DeleteProfile(p);
                                //int idx = Profiles.IndexOf(p);
                                //Profiles.RemoveAt(idx);
                                //Profiles.Insert(idx, res);
                            InsertNewProfile(res);
                                return;
                            }
                        }
                    }
                }
            RefreshList();
        }

        private async void InsertNewProfile(Profile res)
        {
            await _repositoryProfile.InsertAsync(res);
            RefreshList();
        }

        private async void UpdateProfile(Profile res)
        {
            await _repositoryProfile.UpdateAsync(res);
        }
        public async void AddProfile()
        {
            var newProfile = new Profile()
            {
                UserId = UserId,
                Id=Profiles.Count,
                Icon = "not_icon.png",
                DateCreation = DateTime.Now
        };
            var parameters = new NavigationParameters()
            {
                { "profile", newProfile }
            };
            await _navigationService.NavigateAsync($"{nameof(EditProfile)}", parameters);
        }

        private ICommand refreshListCommand;

        public ICommand RefreshListCommand
        {
            get
            {
                if (refreshListCommand == null)
                {
                    refreshListCommand = new DelegateCommand(RefreshList);
                }

                return refreshListCommand;
            }
        }


        private void RefreshList()
        {
            IsBusy = true;
            GetCollection();
            IsBusy = false;
        }

        private bool isBusy;

        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        private ICommand goBackCommand;

        public ICommand GoBackCommand
        {
            get
            {
                if (goBackCommand == null)
                {
                    goBackCommand = new DelegateCommand(GoBack);
                }

                return goBackCommand;
            }
        }

        private async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }

        private void IsVisibleList()
        {
            GetCollection();
            if (Profiles.Count == 0)
            {
                IsVisibleListView = false;
                IsVisibleEmptyALert = true;
            }
            else
            {
                IsVisibleEmptyALert = false;
                IsVisibleListView = true;
            }
        }
        private bool isVisibleListView;

        public bool IsVisibleListView { get => isVisibleListView; set => SetProperty(ref isVisibleListView,value); }

        private bool isVisibleEmptyALert;

        public bool IsVisibleEmptyALert { get => isVisibleEmptyALert; set => SetProperty(ref isVisibleEmptyALert, value); }

        private ICommand propertyCommand;

        public ICommand PropertyCommand
        {
            get
            {
                if (propertyCommand == null)
                {
                    propertyCommand = new DelegateCommand(Property);
                }

                return propertyCommand;
            }
        }

        private void Property()
        {
        }
    }
}
