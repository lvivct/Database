using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    class UserData
    {
        public UserData() { }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public bool Corect()
        {
            if (Login == "" || Password == "")
                return false;
            return true;
        }
    }
}
