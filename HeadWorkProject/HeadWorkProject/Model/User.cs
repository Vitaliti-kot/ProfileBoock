using SQLite;

namespace HeadWorkProject.Model
{
    public class User : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
