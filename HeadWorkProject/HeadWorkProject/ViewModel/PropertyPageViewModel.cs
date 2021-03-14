using Prism.Mvvm;
using Prism.Navigation;

namespace HeadWorkProject.ViewModel
{
   public class PropertyPageViewModel:BindableBase, INavigationAware
    {
        public readonly INavigationService _navigationService;
        public PropertyPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
