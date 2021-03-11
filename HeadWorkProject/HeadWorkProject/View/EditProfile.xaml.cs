
using HeadWorkProject.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeadWorkProject.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfile : ContentPage
    {
        public delegate void BackButtonPressedEvent();
        public event BackButtonPressedEvent BackButtonPressed;
        public EditProfile()
        {
            InitializeComponent();
            if(this.OnBackButtonPressed()) BackButtonPressed += EditProfile_BackButtonPressed ;
        }

        private void EditProfile_BackButtonPressed()
        {
            //this.SetValue("EditingProfile", null);
        }
    }
}