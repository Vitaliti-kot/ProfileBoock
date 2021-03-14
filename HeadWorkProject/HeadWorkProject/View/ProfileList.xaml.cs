using Acr.UserDialogs;
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
        }

        private async void Start()
        {
            await Task.Delay(2000);

        }
        private void MenuItemEdit_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            item.Command = butEdit.Command;
            Profile = item.BindingContext as Profile;
            item.Command.Execute(Profile);
        }

        private async void MenuItemDelete_Clicked(object sender, EventArgs e)
        {
            var res = await UserDialogs.Instance.ConfirmAsync("Вы уверенны, что хотите удалить пользователя?", Title = "Предупреждение.", "Продолжить", "Отмена");
           // var res = await DisplayAlert(Title = "Предупреждение.", "Вы уверенны, что хотите удалить пользователя?", "Продолжить", "Отмена");
            if (res)
            {
                var item = sender as MenuItem;
                item.Command = butDelete.Command;
                Profile = item.BindingContext as Profile;
                item.Command.Execute(Profile);
            }
           
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
           // Task.Delay(5000);
            UpdateChildrenLayout();
        }


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var profile = e.SelectedItem as Profile;
                string icon = profile.Icon;
                butDialog.Command.Execute(icon);
                listView.SelectedItem = null;
            }
            
        }

    }
}