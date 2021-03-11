using HeadWorkProject.Model;
using System;
using System.Threading.Tasks;
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
            Start();
        }

        private async void Start()
        {
            await Task.Delay(2000);
            if (listView.ItemTemplate == null)
            {
                listView.IsVisible = false;
                IsEmptyList.IsVisible = true;
            }
            else
            {
                listView.IsVisible = true;
                IsEmptyList.IsVisible = false;
            }
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
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                NickName = profile.NickName,
                DateCreation = profile.DateCreation,
                UserId = profile.UserId,
                Icon = profile.Icon
            };
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            listView.RefreshCommand.Execute(sender);
            Task.Delay(5000);
            UpdateChildrenLayout();
        }

    }
}