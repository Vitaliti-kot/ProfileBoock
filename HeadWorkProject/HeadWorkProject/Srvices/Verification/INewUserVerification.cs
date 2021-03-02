using HeadWorkProject.Model;
using HeadWorkProject.Srvices.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HeadWorkProject.Srvices.Verification
{
    public interface INewUserVerification
    {
        ObservableCollection<User> _users { get; set; }
        string IsCorrect(string login, string password);
    }
}
