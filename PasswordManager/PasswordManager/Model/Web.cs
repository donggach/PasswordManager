using SQLite;

namespace PasswordManager.Model
{
    [Table("Web")]
    public class Web
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Url { get; set; }

        [MaxLength(30)]
        public string UserName { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        public string Note { get; set; }
    }
}
