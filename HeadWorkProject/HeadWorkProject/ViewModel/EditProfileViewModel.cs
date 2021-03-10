using HeadWorkProject.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace HeadWorkProject.ViewModel
{
   public class EditProfileViewModel : BindableBase, INavigationAware, IDestructible
    {
        private readonly INavigationService _navigationService;
        private Profile _profile;
        public Profile EditingProfile
        {
            get { return _profile; }
            set
            {
                SetProperty(ref _profile, value);
            }
        }
        public EditProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            EditingProfile = new Profile();
            
        }
       

        private ICommand editPhoto;

        public ICommand EditPhoto
        {
            get
            {
                if (editPhoto == null)
                {
                    editPhoto = new DelegateCommand(PerformEditPhoto);
                }

                return editPhoto;
            }
        }

        private void PerformEditPhoto()
        {
        }

        private ICommand nickNameChangedCommand;

        public ICommand NickNameChangedCommand
        {
            get
            {
                if (nickNameChangedCommand == null)
                {
                    nickNameChangedCommand = new DelegateCommand<object>(NickNameChanged);
                }

                return nickNameChangedCommand;
            }
        }

        private void NickNameChanged(object obj)
        {
            string nickName = obj.ToString();
            EditingProfile.NickName = nickName;
        }

        private ICommand firstNameChangedCommand;

        public ICommand FirstNameChangedCommand
        {
            get
            {
                if (firstNameChangedCommand == null)
                {
                    firstNameChangedCommand = new DelegateCommand<object>(FirstNameChanged);
                }

                return firstNameChangedCommand;
            }
        }

        private void FirstNameChanged(object obj)
        {
            string firstName = obj.ToString();
            EditingProfile.FirstName = firstName;
        }

        private ICommand lastNameChangedCommand;

        public ICommand LastNameChangedCommand
        {
            get
            {
                if (lastNameChangedCommand == null)
                {
                    lastNameChangedCommand = new DelegateCommand<string>(LastNameChanged);
                }

                return lastNameChangedCommand;
            }
        }

        private void LastNameChanged(string lastName)
        {
            EditingProfile.LastName = lastName;
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("profile", EditingProfile);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
            EditingProfile = parameters.GetValue<Profile>("profile");
        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new DelegateCommand(Save);
                }

                return saveCommand;
            }
        }

        private async void Save()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
