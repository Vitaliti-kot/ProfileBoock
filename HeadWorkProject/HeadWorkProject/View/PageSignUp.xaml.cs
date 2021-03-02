using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeadWorkProject.View
{
     [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSignUp : ContentPage

    {
        string _login="";
        string _password1="";
        string _password2="";
        public PageSignUp()
        {
            InitializeComponent();
        }
        private void entryLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            _login = entryLogin.Text;
            CheckInputData();
        }

        private void entryPassword1_TextChanged(object sender, TextChangedEventArgs e)
        {
            _password1 = entryPassword1.Text;
            CheckInputData();
        }

        private void entryPassword2_TextChanged(object sender, TextChangedEventArgs e)
        {
            _password2 = entryPassword2.Text;
            CheckInputData();
        }
        private void CheckInputData()
        {
            if (_login.Length >= 4 && _password1.Length >= 8 && _password2 == _password1)
            {
                buttSignUp.IsEnabled = true;
            }
            else
            {
                buttSignUp.IsEnabled = false;
            }
        }

    }
}