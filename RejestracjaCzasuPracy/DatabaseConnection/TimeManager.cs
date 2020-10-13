using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DatabaseConnection;
using System.Data;

namespace DatabaseConnection
{
    public static class TimeManager
    {
        private static DataTable dataTable = new DataTable();
        private static string query;


        //public static GoHomo(DateTime)

        public static bool AddNewEvent(string userID, int minutesToCatchUp)
        {
            SqlCommand insertCommand = new SqlCommand($"INSERT into Events(EventID, Date, MinutesToCatchUp, UserID) values(@EventID, @Date, @Minutes, @UserID)");

            insertCommand.Parameters.AddWithValue("@EventID", Guid.NewGuid());
            insertCommand.Parameters.AddWithValue("@Date", DateTime.Now);
            insertCommand.Parameters.AddWithValue("@Minutes", minutesToCatchUp);
            insertCommand.Parameters.AddWithValue("@UserID", userID);

            int row = DBAccess.ExecuteQuery(insertCommand);

            if(row == 1)
            {
                return true;    // Event added successfully
            }
            else
            {
                return false;   // FAILED to add an Event
            }
        }

        public static int CountMinutesToCatchUp(string userID)
        {
            dataTable = new DataTable();
            int minutesToCatchUp = 0;
            query = $"SELECT MinutesToCatchUp from Events Where UserID = '{userID}'";

            DBAccess.ReadDataThroughAdapter(query, dataTable);

            foreach(DataRow row in dataTable.Rows)
            {
                minutesToCatchUp += Convert.ToInt32(row["MinutesToCatchUp"]);
            }

            return minutesToCatchUp;
        }

        public static int GoHome(User currentUser)
        {
            TimeSpan timeSpan = DateTime.Now - currentUser.finishWorkHour;

            int minutesToCatchUp = Convert.ToInt32(timeSpan.TotalMinutes);

            // AddNewEvent(currentUser.id, minutesToCatchUp);

            return minutesToCatchUp;
        }
    }

}
