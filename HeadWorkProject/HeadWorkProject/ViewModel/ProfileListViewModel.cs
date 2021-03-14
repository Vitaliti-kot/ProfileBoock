using HeadWorkProject.Dialog;
using HeadWorkProject.Model;
using HeadWorkProject.Srvices.Repository;
using HeadWorkProject.Srvices.Repositoryi;
using HeadWorkProject.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeadWorkProject.ViewModel
{
    public class ProfileListViewModel : BindableBase, INavigationAware, IDestructible
    {
        #region ---Fields---
        private int _id;
        private string _icon;
        public bool NavFromMainPage = true;
        private int userId;
        private bool isBusy;
        private bool isVisibleListView;
        private bool isVisibleEmptyALert;
        private ICommand propertyCommand;
        private ICommand goBackCommand;
        private ICommand refreshListCommand;
        public ICommand EditProfileCommand { protected set; get; }
        public ICommand DeleteProfileCommand { protected set; get; }
        public ICommand AddNewProfile { protected set; get; }
        public ICommand ShowDialog { protected set; get; }
        private readonly INavigationService _navigationService;
        private IRepositoryProfile _repositoryProfile;
        private INavigationParameters _parameters;
        private IDialogService _dialogService { get; }
        public ObservableCollection<Profile> Profiles { get; set; }
        #endregion

        #region ---SetProperty---
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }
        public int UserId
        {
            get { return userId; }
            set { SetProperty(ref userId, value); }
        }
        public string Icon
        {
            get { return _icon; }
            set
            {
                SetProperty(ref _icon, value);
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
        public bool IsVisibleListView { get => isVisibleListView; set => SetProperty(ref isVisibleListView, value); }
        public bool IsVisibleEmptyALert { get => isVisibleEmptyALert; set => SetProperty(ref isVisibleEmptyALert, value); }
        #endregion

        public ProfileListViewModel(INavigationService navigation, IDialogService dialogService)
        {
            _navigationService = navigation;
            _repositoryProfile = new ProfilesRepository();
            _dialogService = dialogService;
            Profiles = new ObservableCollection<Profile>();
            EditProfileCommand = new Command(EditProfileComm);
            DeleteProfileCommand = new Command(DeleteProfile);
            AddNewProfile = new Command(AddProfile);
            ShowDialog = new Command(OnShowDialogExecute);
        }

        #region ---Methods---
        private void OnShowDialogExecute(object obj)
        {
            Icon = obj.ToString();
            _dialogService.ShowDialog($"{nameof(ImageDialog)}", new DialogParameters
            {
                {"Icon", Icon }
            });
        }
        private async void GetCollection()
        {
            var prfls = await _repositoryProfile.GetAllAsync<Profile>();
            Profiles.Clear();
            foreach (Profile prof in prfls)
            {
                if (prof.UserId == UserId) Profiles.Add(prof);
            }
            IsVisibleList();
        }
        public async void DeleteProfile(object obj)
        {
            var prfls = await _repositoryProfile.GetAllAsync<Profile>();
            var p = obj as Profile;
            foreach (Profile pr in prfls)
            {
                if (pr.Id == p.Id && p.UserId == pr.UserId)
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
            if (NavFromMainPage)
            {
                NavFromMainPage = false;
                UserId = parameters.GetValue<int>($"{nameof(UserId)}");
                GetCollection();
            }
            else
            {
                var res = parameters.GetValue<Profile>("profile");
                if (res.NickName != null)
                {
                    bool oldProfile = false;
                    foreach (Profile p in Profiles)
                    {
                        if (p.Id == res.Id)
                        {
                            DeleteProfile(p);
                            InsertNewProfile(res);
                            oldProfile = true;
                            return;
                        }
                    }
                    if (!oldProfile)
                    {
                        Profiles.Add(res);
                        InsertNewProfile(res);
                    }
                }
            }

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
                Id = Profiles.Count,
                Icon = "not_icon.png",
                DateCreation = DateTime.Now
            };
            var parameters = new NavigationParameters()
            {
                { "profile", newProfile }
            };
            await _navigationService.NavigateAsync($"{nameof(EditProfile)}", parameters);
        }
        private void RefreshList()
        {
            IsBusy = true;
            GetCollection();
            IsBusy = false;
        }
        private async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }
        private void IsVisibleList()
        {
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
        private async void Property()
        {
            await _navigationService.NavigateAsync($"{nameof(PropertyPageView)}");
        }
        #endregion
    }
}
