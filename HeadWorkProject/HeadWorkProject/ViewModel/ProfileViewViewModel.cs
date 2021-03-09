using HeadWorkProject.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeadWorkProject.ViewModel
{
   public class ProfileViewViewModel:BindableBase
    {
        private Profile profile;
        public Profile Profile
        {
            get { return profile; }
            set
            {
                SetProperty(ref profile, value);
            }
        }
        public ProfileViewViewModel(Profile profile)
        {
            Profile = profile;
        }
    }
}
