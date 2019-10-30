using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    class DatabaseHelp
    {
        public static bool Insert<T>(ref T data, string db_path)
        {
            using (var conn = new SQLite.SQLiteConnection(db_path))
            {
                conn.CreateTable<T>();
                if (conn.Insert(data) != 0)
                    return true;
            }
            return false;
        }
    }
}
    