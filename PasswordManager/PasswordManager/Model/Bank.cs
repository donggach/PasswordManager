using SQLite;
using System;

namespace PasswordManager.Model
{
    [Table("Bank")]
    public class Bank
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Account { get; set; }

        [MaxLength(20)]
        public string ATMPassword { get; set; }

        [MaxLength(30)]
        public string InternetUser { get; set; }

        [MaxLength(20)]
        public string InternetPassword { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswern { get; set; }

        public int DayChangePassword { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Note { get; set; }
    }
}