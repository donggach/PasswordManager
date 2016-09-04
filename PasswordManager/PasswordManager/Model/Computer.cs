using SQLite;
using System;

namespace PasswordManager.Model
{
    [Table("Computer")]
    public class Computer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public int DayChangePassword { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
