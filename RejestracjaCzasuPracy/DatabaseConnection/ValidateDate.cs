using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DatabaseConnection
{
    public static class ValidateDate        // Class used to Validate login data like name, password, email
    {
        static DataTable dtValidate = new DataTable();

        public static bool NamePassword(string name, string password)
        {
            if(!Password(password))
            {
                return false;
            }
            else if (!Name(name))
            {
                return false;
            }

            return true;
        }

        public static bool Password(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Name(string name)
        {
            string query = $"Select * from Users Where Name = '{name}'";

            DBAccess.ReadDataThroughAdapter(query, dtValidate);

            if (dtValidate.Rows.Count != 0)
            {
                return false;
            }
            else if (name.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckWorkHours(DateTime startDate, DateTime finishtDate)
        {
            TimeSpan workTime = finishtDate - startDate;

            if (workTime.TotalHours != 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
