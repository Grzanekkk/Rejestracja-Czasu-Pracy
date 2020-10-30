using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime;

namespace DatabaseConnection
{
    public class UserManager
    {
        string query;
        DataTable dataTable = new DataTable();
        DBAccess dbAccess = new DBAccess();


        public User GetUserWithName(string name)
        {
            dataTable = new DataTable();
            string[] names = SplitUserName(name);

            query = $"Select * from CRMember Where FirstName = '{names[0]}' AND SurName = '{names[1]}'";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            return CreateNewUserFromDataBase(dataTable);
        }

        public User GetUserWithID(string memberID)
        {      
            if (!String.IsNullOrEmpty(memberID))
            {
                dataTable = new DataTable();
                query = $"SELECT * from CRMember Where MemberID = '{memberID}'";

                dbAccess.ReadDataThroughAdapter(query, dataTable);
                
                return CreateNewUserFromDataBase(dataTable);    // can be null
            }

            return null;
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

        public string[] SplitUserName(string name)
        {
            // [0] = firstName, [1] = surName
            string[] names = name.Split(' ');
            return names;
        }

        User CreateNewUserFromDataBase(DataTable userTable)
        {
            if (userTable.Rows.Count == 1)
            {
                User currentUser = new User
                (
                    userTable.Rows[0]["MemberID"].ToString(),
                    userTable.Rows[0]["FirstName"].ToString(),
                    userTable.Rows[0]["SurName"].ToString()
                );

                return currentUser;
            }
            else
            {
                return null;  
            }
        }
    }
}
