using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeadWorkProject.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyPageView : ContentPage
    {
        bool strt = true;
        public PropertyPageView()
        {
            InitializeComponent();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!strt) setTheme.Command.Execute(e.Value);
            else strt = false;
        }
    }
}