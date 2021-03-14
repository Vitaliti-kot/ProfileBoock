using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Commands;

namespace HeadWorkProject.Dialog.ViewModel
{
    public class DialogViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogParameters> RequestClose;
        private string _selectedImage;
        public string SelectedImage
        {
            get { return _selectedImage; }
            set { SetProperty(ref _selectedImage, value); }
        }
        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            SelectedImage = parameters.GetValue<string>("Icon");
        }
    }
}
