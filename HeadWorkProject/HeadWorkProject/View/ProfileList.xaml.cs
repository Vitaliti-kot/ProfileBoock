using Acr.UserDialogs;
using HeadWorkProject.Model;
using HeadWorkProject.ViewModel;
using Syncfusion.ListView.XForms;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeadWorkProject.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileList : ContentPage
    {
        public Profile Profile { get; set; }
        public ProfileList()
        {
            InitializeComponent();
        }

        private void MenuItemEdit_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            item.Command = butEdit.Command;
                Profile = item.BindingContext as Profile;
            item.Command.Execute(Profile);
        }

        private void MenuItemDelete_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            item.Command = butDelete.Command;
            Profile = item.BindingContext as Profile;
            item.Command.Execute(Profile);
        }
        private void ListView_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var profile = e.Item as Profile;
            Profile = new Profile()
            {
                Id=profile.Id,
                FirstName=profile.FirstName,
                LastName=profile.LastName,
                NickName=profile.NickName,
                DateCreation=profile.DateCreation,
                userId=profile.userId,
                _Icon=profile._Icon
            };
        }

    }
}