using SQLite;
using System;
using System.Windows.Input;
using Prism.Commands;

namespace HeadWorkProject.Model
{
    [Table("Profile")]
    public class Profile : IEntityBaseProfile
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string _Icon { get; set; }
        public string NickName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateCreation { get; set; }
        public int Id { get; set; }

        public string Comments { get; set; }
    }
}
