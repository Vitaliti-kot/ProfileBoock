using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeadWorkProject.ViewModel
{
   public class PropertyPageViewModel:BindableBase, INavigationAware
    {
        #region ---Fields---
        public readonly INavigationService _navigationService;
        private bool _isDarkTheme;
        public ICommand SetTheme { protected set; get; }
        #endregion
        #region ---SetProperty---
        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set { SetProperty(ref _isDarkTheme, value); }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        private void PerformSetTheme(object obj)
        {
            IsDarkTheme = (bool)obj;
            if (IsDarkTheme) Prism.PrismApplicationBase.Current.UserAppTheme = OSAppTheme.Dark;
            else Prism.PrismApplicationBase.Current.UserAppTheme = OSAppTheme.Light;
        }
        #endregion
        public PropertyPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SetTheme = new Command(PerformSetTheme);
            if (Prism.PrismApplicationBase.Current.UserAppTheme == OSAppTheme.Light) IsDarkTheme = false;
            else IsDarkTheme = true;
        }
       
        
      
    }
}
