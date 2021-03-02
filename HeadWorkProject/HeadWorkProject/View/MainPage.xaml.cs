using HeadWorkProject.Model;
using HeadWorkProject.Srvices;
using HeadWorkProject.Srvices.Repository;
using System;
using Xamarin.Forms;

namespace HeadWorkProject.View
{
    public partial class MainPage : ContentPage
    {
        string _entryLogin="";
        string _entryPassword="";
        public MainPage()
        {
            InitializeComponent();
            this.UpdateChildrenLayout();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Command = buttonSign.Command;
            buttonSignUp.GestureRecognizers.Add(tap);
        }

        private void EntryLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            _entryLogin = entryLogin.Text;
            Enabled_Disabled_Button();
        }

        private void EntryPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            _entryPassword = entryPassword.Text;
            Enabled_Disabled_Button();
        }

        private void Enabled_Disabled_Button()
        {
                if (_entryLogin.Length >= 4 && _entryPassword.Length >= 8)
                {
                    buttonAutorization.IsEnabled = true;
                }
                else
                {
                    buttonAutorization.IsEnabled = false;
                }
        }
    }
}