using HeadWorkProject.Model;
using HeadWorkProject.Srvices.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HeadWorkProject.Srvices.Verification
{
    public class LoginValidation : ILoginValidation
    {
        public IRepository _repository { get; set; }
        public ObservableCollection<User> allUsers {get;set;}

        public LoginValidation(IRepository repository)
        {
            _repository = repository;
        }
        public int Success(string login, string password)
        {
            int res = -1;
            if (allUsers == null)
            {
                GetUser(login,password);
                
            }
            else
            {
                foreach (User user in allUsers)
                {
                    if (user.Login == login && user.Password == password) return res = user.Id;
                }
            }
            
            return res;
        }

        public async void GetUser(string login, string password)
        {
                var users = await _repository.GetAllAsync<User>();
                allUsers = new ObservableCollection<User>(users);
                Success(login, password);
        }
    }
}
