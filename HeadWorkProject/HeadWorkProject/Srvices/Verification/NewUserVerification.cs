using HeadWorkProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HeadWorkProject.Srvices.Verification
{
    public class NewUserVerification : INewUserVerification
    {
        public ObservableCollection<User> _users { get; set; }

        public NewUserVerification(ObservableCollection<User> users)
        {
            _users = users;
        }

        char[] numbers = new char[]
        {
            '1','2','3','4','5','6','7','8','9','0'
        };
        char[] letters = new char[]
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','s','t','u','v','w','q','r','y','z','x'
        };
        char[] UpLetters = new char[]
        {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','S','T','U','V','W','Q','R','Y','Z','X'
        };
        public string IsCorrect(string login, string password)
        {
            string messLogin = CheckLogin(login);
            string messPassword = CheckPassword(password);
            if (messLogin != null)
            {
                return messLogin;
            }
            else
            {
                if (messPassword != null)
                {
                    return messPassword;
                }
                else
                {
                    return null;
                }
            }
        }
        private string CheckLogin(string login)
        {
            if (login.IndexOfAny(numbers) == 0)
            {
                return "Логин не должен начинаться с цифр.";
            }
            else
            {
                if (_users != null)
                {
                    foreach (User user in _users)
                    {
                        if (user.Login == login) return "Такой логин уже существует";
                    }
                }
            }
            return null;
        }

        private string CheckPassword(string password)
        {
            if (password.IndexOfAny(numbers) == -1) return "В пароле должна быть, по-крайней мере, одна цифра";
            if (password.IndexOfAny(UpLetters) == -1) return "В пароле должна быть, по-крайней мере, одна буква в ВЕРХНЕМ регистре";
            if (password.IndexOfAny(letters) == -1) return "В пароле должна быть, по-крайней мере, одна буква в нижнем регистре";
            return null;
        }
    }
}
