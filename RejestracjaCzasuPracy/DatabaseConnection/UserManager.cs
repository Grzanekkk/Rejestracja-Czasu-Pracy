using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseConnection
{
    public class UserManager
    {
        string query;
        DataTable dataTable = new DataTable();
        DBAccess dbAccess = new DBAccess();

        public User GetUserWithNameAndPassword(string name, string password)
        {
            dataTable = new DataTable();

            query = $"Select * from CRMember Where Name = '{name}' AND Password = '{password}'";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            if (dataTable.Rows.Count == 1)
            {
                User currentUser = new User
                (
                    dataTable.Rows[0]["MemberID"].ToString(),
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

        public User GetUserWithName(string name)
        {
            dataTable = new DataTable();
            string[] names = SplitUserName(name);

            query = $"Select * from CRMember Where FirstName = '{names[0]}' AND SurName = '{names[1]}'";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            if (dataTable.Rows.Count == 1)
            {
                User currentUser = new User
                (
                    dataTable.Rows[0]["MemberID"].ToString(),
                    dataTable.Rows[0]["FirstName"].ToString(),
                    dataTable.Rows[0]["SurName"].ToString()
                );

                return currentUser;
            }
            else
            {
                return null;    // DO ZMIANY !!!!!!!!!!!!!!!!!!
            }
        }

        public List<User> GetAllUsers()
        {
            dataTable = new DataTable();

            List<User> listOfAllUsers = new List<User>();
            query = $"Select * from CRMember";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                User user = new User
                (
                    dataTable.Rows[i]["MemberID"].ToString(),
                    dataTable.Rows[i]["FirstName"].ToString(),
                    dataTable.Rows[i]["SurName"].ToString()
                );

                listOfAllUsers.Add(user);
                i++;
            }

            return listOfAllUsers;
        }

        public DataTable GetUserEvents(string memberID)
        {
            dataTable = new DataTable();

            query = $"SELECT * from Events Where MemberID = '{memberID}' ORDER BY Date DESC";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            return dataTable;
        }

        public string[] SplitUserName(string name)
        {
            // [0] = firstName, [1] = surName
            string[] names = name.Split(' ');
            return names;
        }
    }
}
