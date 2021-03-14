using Acr.UserDialogs;
using HeadWorkProject.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HeadWorkProject.ViewModel
{
   public class EditProfileViewModel : BindableBase, INavigationAware, IDestructible
    {
        #region ---Fields---
        private readonly INavigationService _navigationService;
        public bool GoBackButOn = false;
        private string _icon;
        private Profile _profile;
        private ICommand editPhoto;
        private ICommand nickNameChangedCommand;
        private ICommand firstNameChangedCommand;
        private ICommand lastNameChangedCommand;
        private ICommand saveCommand;
        #endregion

        #region ---SetProperty---
        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }
        public Profile EditingProfile
        {
            get { return _profile; }
            set
            {
                SetProperty(ref _profile, value);
            }
        }
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
        #endregion


        public EditProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
        }
        #region ---Methods---
        private void PerformEditPhoto()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                            .SetTitle("Выберите действие")
                            .Add("Фото с камеры", OpenCamera, "ic_camera_alt_black.png")
                            .Add("Фото с галереи", OpenFolder, "ic_collections_black.png")
                            .SetCancel("Закрыть",null)
                        );
        }
        private async void OpenCamera()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                EditingProfile.Icon = photo.FullPath;
                Icon = photo.FullPath;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Сообщение об ошибке", ex.Message, "OK");
            }
        }
        private async void OpenFolder()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                EditingProfile.Icon = photo.FullPath;
                Icon = photo.FullPath;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Сообщение об ошибке", ex.Message, "OK");
            }
        }
        private void NickNameChanged(object obj)
        {
            string nickName = obj.ToString();
            EditingProfile.NickName = nickName;
        }
        private void FirstNameChanged(object obj)
        {
            string firstName = obj.ToString();
            EditingProfile.FirstName = firstName;
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
            if(GoBackButOn) parameters.Add("profile", EditingProfile);
        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
            EditingProfile = parameters.GetValue<Profile>("profile");
            Icon = EditingProfile.Icon;
        }
        private async void Save()
        {
            GoBackButOn = true;
            await _navigationService.GoBackAsync();
        }
        #endregion
    }
}
