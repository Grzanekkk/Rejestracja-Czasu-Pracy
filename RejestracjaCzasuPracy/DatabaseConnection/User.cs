using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseConnection
{
    public class User
    {
        public string id, name, password;

        static string query;
        static DataTable dtUsers = new DataTable();


        #region Constructors


        public User(string _id, string _name, string _password)
        {
            id = _id;
            name = _name;
            password = _password;
        }

        public User(string _id, string _name)
        {
            id = _id;
            name = _name;
        }


        #endregion Constructors


        public static User GetUserWithNameAndPassword(string name, string password)
        {
            dtUsers = new DataTable();

            query = $"Select * from Users Where Name = '{name}' AND Password = '{password}'";

            DBAccess.ReadDataThroughAdapter(query, dtUsers);

            if (dtUsers.Rows.Count == 1)
            {
                User currentUser = new User
                (
                    dtUsers.Rows[0]["ID"].ToString(),
                    dtUsers.Rows[0]["Name"].ToString(),
                    dtUsers.Rows[0]["Password"].ToString()
                );

                return currentUser;
            }
            else
            {
                return null;    // DO ZMIANY !!!!!!!!!!!!!!!!!!
            }
        }

        public static User GetUserWithName(string name)
        {
            dtUsers = new DataTable();

            query = $"Select * from Users Where Name = '{name}'";

            DBAccess.ReadDataThroughAdapter(query, dtUsers);

            if (true) // dtUsers.Rows.Count == 1
            {
                User currentUser = new User
                (
                    dtUsers.Rows[0]["ID"].ToString(),
                    dtUsers.Rows[0]["Name"].ToString()
                );

                return currentUser;
            }
            else
            {
                return null;    // DO ZMIANY !!!!!!!!!!!!!!!!!!
            }
        }

        public static List<User> GetAllUsers()
        {
            dtUsers = new DataTable();

            List<User> listOfAllUsers = new List<User>();
            query = $"Select * from Users";

            DBAccess.ReadDataThroughAdapter(query, dtUsers);

            int i = 0;
            foreach(DataRow row in dtUsers.Rows)
            {
                User user = new User
                (
                    dtUsers.Rows[i]["ID"].ToString(),
                    dtUsers.Rows[i]["Name"].ToString()
                );

                listOfAllUsers.Add(user);
                i++;
            }

            return listOfAllUsers;
        }
    }
}
