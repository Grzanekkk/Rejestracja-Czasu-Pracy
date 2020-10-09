using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseConnection
{
    public class User
    {
        public string id, name, password;

        private User(string _id, string _name, string _password)
        {
            id = _id;
            name = _name;
            password = _password;
        }

        public static User GetUserWithNameAndPassword(string name, string password)
        {
            DBAccess dbAccess = new DBAccess();
            DataTable dtUsers = new DataTable();

            string query = $"Select * from Users Where Name = '{name}' AND Password = '{password}'";

            dbAccess.ReadDataThroughAdapter(query, dtUsers);

            if (dtUsers.Rows.Count == 1)
            {
                User currentUser = new User
                (
                    dtUsers.Rows[0]["ID"].ToString(),
                    dtUsers.Rows[0]["Name"].ToString(),
                    dtUsers.Rows[0]["Password"].ToString()
                );

                dbAccess.CloseConnection();
                return currentUser;
            }
            else
            {
                return null;    // DO ZMIANY !!!!!!!!!!!!!!!!!!
            }
        }
    }
}
