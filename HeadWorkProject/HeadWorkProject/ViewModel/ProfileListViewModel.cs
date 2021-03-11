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
        public ObservableCollection<Profile> Profiles { get; set; }
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
        //public string Icon
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
            _navigationService = navigation;
            _repositoryProfile = new ProfilesRepository();
            Profiles = new ObservableCollection<Profile>();
            
            EditProfileCommand = new Command(EditProfileComm);
            DeleteProfileCommand = new Command(DeleteProfile);
            AddNewProfile = new Command(AddProfile);
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
            var p = obj as Profile;
            foreach(Profile pr in Profiles)
            {
                if (pr == p)
                {
                    Profiles.Remove(p);
                }
            }
            await _repositoryProfile.DeleteAsync(p);
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
            try
            {
                UserId = parameters.GetValue<int>($"{nameof(UserId)}");
                GetCollection();
            }
            catch
            {
                var res = parameters.GetValue<Profile>("profile");
                if (res != null)
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
                                int idx = Profiles.IndexOf(p);
                                Profiles.RemoveAt(idx);
                                Profiles.Insert(idx, res);
                                UpdateProfile(res);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private async void InsertNewProfile(Profile res)
        {
            await _repositoryProfile.InsertAsync(res);
        }

        private async void UpdateProfile(Profile res)
        {
            await _repositoryProfile.UpdateAsync(res);
        }
        public async void AddProfile()
        {
            var profId = Profiles.Count;
            var newProfile = new Profile()
            {
                UserId = UserId,
                Id = profId,
                Icon = "not_icon.png",
                DateCreation = DateTime.Now
        };
            var parameters = new NavigationParameters
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
    }
}
