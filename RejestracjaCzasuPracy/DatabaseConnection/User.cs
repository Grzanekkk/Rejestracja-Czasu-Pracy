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
        static DataTable dataTable = new DataTable();

        public DateTime startWorkHour;
        public DateTime finishWorkHour;


        #region Constructors


        public User(string _id, string _name, string _password)
        {
            id = _id;
            name = _name;
            password = _password;

            startWorkHour = DateTime.Today.AddHours(9);
            finishWorkHour = startWorkHour.AddHours(8);
        }

        public User(string _id, string _name)
        {
            id = _id;
            name = _name;

            startWorkHour = DateTime.Today.AddHours(9);
            finishWorkHour = startWorkHour.AddHours(8);
        }


        #endregion Constructors


        public static User GetUserWithNameAndPassword(string name, string password)
        {
            dataTable = new DataTable();

            query = $"Select * from Users Where Name = '{name}' AND Password = '{password}'";

            DBAccess.ReadDataThroughAdapter(query, dataTable);

            if (dataTable.Rows.Count == 1)
            {
                User currentUser = new User
                (
                    dataTable.Rows[0]["UserID"].ToString(),
                    dataTable.Rows[0]["Name"].ToString(),
                    dataTable.Rows[0]["Password"].ToString()
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
            dataTable = new DataTable();

            query = $"Select * from Users Where Name = '{name}'";

            DBAccess.ReadDataThroughAdapter(query, dataTable);

            if (true) // dtUsers.Rows.Count == 1
            {
                User currentUser = new User
                (
                    dataTable.Rows[0]["UserID"].ToString(),
                    dataTable.Rows[0]["Name"].ToString()
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
            dataTable = new DataTable();

            List<User> listOfAllUsers = new List<User>();
            query = $"Select * from Users";

            DBAccess.ReadDataThroughAdapter(query, dataTable);

            int i = 0;
            foreach(DataRow row in dataTable.Rows)
            {
                User user = new User
                (
                    dataTable.Rows[i]["UserID"].ToString(),
                    dataTable.Rows[i]["Name"].ToString()
                );

                listOfAllUsers.Add(user);
                i++;
            }

            return listOfAllUsers;
        }

        public static DataTable GetAllUserEvents(string userID)
        {
            dataTable = new DataTable();

            query = $"SELECT Date, MinutesToCatchUp, UserID from Events Where UserID = '{userID}'";

            DBAccess.ReadDataThroughAdapter(query, dataTable);

            return dataTable;
        }
    }
}
