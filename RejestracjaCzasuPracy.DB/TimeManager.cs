using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DatabaseConnection
{
    public class TimeManager
    {
        private DataTable dataTable = new DataTable();
        private DBAccess dbAccess = new DBAccess();
        private string query;

        public bool AddNewEvent(string memberID, int minutesToCatchUp)
        {
            SqlCommand insertCommand = new SqlCommand($"INSERT into Events(EventID, Date, MinutesToCatchUp, MemberID, BreakTime) " +
                $"values(@EventID, @Date, @Minutes, @MemberID, @BreakTime)");

            insertCommand.Parameters.AddWithValue("@EventID", Guid.NewGuid());
            insertCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")));
            insertCommand.Parameters.AddWithValue("@MemberID", memberID);
            insertCommand.Parameters.AddWithValue("@BreakTime", 0);

            if (minutesToCatchUp == 0) 
            {
                insertCommand.Parameters.AddWithValue("@Minutes", DBNull.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Minutes", minutesToCatchUp);
            }

            int row = dbAccess.ExecuteQuery(insertCommand);

            if(row == 1)
            {
                return true;    // Event added successfully
            }
            else
            {
                return false;   // FAILED to add an Event
            }
        }

        public void DeleteEvent(string eventID)
        {
            dataTable = new DataTable();
            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Events Where eventID='{eventID}'");

            dbAccess.ExecuteQuery(deleteCommand);
        }

        public void UpdateEvents(DataTable changes)
        {
            query = $"SELECT * from Events";

            dbAccess.ExecuteDataAdapter(changes, query);
        }

        public int CountUserTimeToCatchUp(string memberID)
        {
            dataTable = new DataTable();
            query = $"SELECT sum(MinutesToCatchUp) Bilans, MemberID from Events where MemberID = '{memberID}' group by MemberID";
            int minutesToCatchUp = 0;

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            if (dataTable.Rows.Count != 0 && !dataTable.Rows[0].IsNull("Bilans") && dataTable.Rows[0]["Bilans"] != DBNull.Value)
            {              
                minutesToCatchUp = Convert.ToInt32(dataTable.Rows[0]["Bilans"]);
            }

            return minutesToCatchUp;
        }

        public int CountMinutesToCatchUpFromNow(User currentUser)
        {
            TimeSpan timeSpan = DateTime.Now - currentUser.finishWorkHour;

            int minutesToCatchUp = Convert.ToInt32(timeSpan.TotalMinutes) * -1;

            return minutesToCatchUp;
        }


        public DataTable GetSummaryForAllUsers()
        {
            dataTable = new DataTable();
            query = $"SELECT sum(MinutesToCatchUp) Bilans, MemberID FROM Events group by MemberID";
            dbAccess.ReadDataThroughAdapter(query, dataTable);

            query = $"Select * From CRMember";
            DataTable usersTabel = new DataTable();
            dbAccess.ReadDataThroughAdapter(query, usersTabel);

            DataTable summaryTable = dataTable.Clone();
            summaryTable.Columns.Add("Name", typeof(string));

            foreach (DataRow row in dataTable.Rows)
            {
                summaryTable.ImportRow(row);
            }

            foreach (DataRow row in summaryTable.Rows)
            { 
                for (int n = 0; n < usersTabel.Rows.Count; n++)
                    if (row["MemberID"].ToString() == usersTabel.Rows[n]["MemberID"].ToString())
                    {
                        row["Name"] = $"{usersTabel.Rows[n]["FirstName"]} {usersTabel.Rows[n]["SurName"]}";
                    }
            }

            return summaryTable;
        }

        public DataTable GetUserEvents(string memberID)
        {
            dataTable = new DataTable();

            query = $"SELECT * from Events Where MemberID = '{memberID}' ORDER BY Date DESC";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            DataTable date = dataTable.Clone();
            date.Columns["Date"].DataType = typeof(string);

            foreach (DataRow row in dataTable.Rows)
            {
               date.ImportRow(row);
            }

            int i = 0;
            foreach (DataRow row in date.Rows)
            {
                row["Date"] = Convert.ToDateTime(dataTable.Rows[i]["Date"]).ToString("dd-MM-yyyy HH:mm:ss");
            }

            return date;
        }


        #region Work Button


        public bool IsWorking(string memberID)
        {
            dataTable = new DataTable();
            query = $"SELECT * from Events where MemberID = '{memberID}'";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            foreach(DataRow row in dataTable.Rows)
            {
                if (row.IsNull("MinutesToCatchUp"))
                {
                    return true;
                }        
            }

            return false;
        }

        public void StopWorking(string memberID)
        {
            dataTable = new DataTable();
            int minutes = GetMinutesOfWorkSinceStart(memberID);

            minutes -= (480 + GetUserMinutesOnBreak(memberID)); // 8 hours, odejmujemy 8 godzin oraz czas spędzony na przerwie aby sprawdzić różnice i dodać reszte do nadrobienia
            minutes = minutes * -1;

            dataTable.Rows[0]["MinutesToCatchUp"] = minutes;

            UpdateEvents(dataTable);
        }

        public void StartWorking(string memberID)
        {
            if(GetMinutesOfWorkSinceStart(memberID) > 840); // 14 godzin, sprawdzamy czy nie ma recordu z wczoraj
            {
                DeleteLetestNullRow(memberID);
            }

            AddNewEvent(memberID, 0);
        }

        private int GetMinutesOfWorkSinceStart(string memberID)     
        {           
            return GetMinutesFromDateTimeInDataBase(memberID, "MinutesToCatchUp", "Date");
        }


        #endregion Work Button

        #region Break Button


        public void StartBreak(string memberID)
        {
            dataTable = new DataTable();
            query = $"SELECT * from Events where MinutesToCatchUp IS NULL AND MemberId = '{memberID}'";
            dbAccess.ReadDataThroughAdapter(query, dataTable);

            dataTable.Rows[0]["BeginningOfTheLatestBreak"] = DateTime.Now;

            UpdateEvents(dataTable);
        }

        public void FinishBreak(string memberID)
        {
            dataTable = new DataTable();
            query = $"SELECT * from Events where MinutesToCatchUp IS NULL AND MemberId = '{memberID}'";
            dbAccess.ReadDataThroughAdapter(query, dataTable);


            int minutesOnBreak = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(dataTable.Rows[0]["BeginningOfTheLatestBreak"])).TotalMinutes);

            dataTable.Rows[0]["BreakTime"] = Convert.ToInt32(dataTable.Rows[0]["BreakTime"]) + minutesOnBreak;
            dataTable.Rows[0]["BeginningOfTheLatestBreak"] = DBNull.Value;

            UpdateEvents(dataTable);
        }


        public bool IsOnBreak(string memberID)
        {
            dataTable = new DataTable();
            query = $"SELECT * from Events where MinutesToCatchUp IS NULL AND MemberId = '{memberID}'";
            dbAccess.ReadDataThroughAdapter(query, dataTable);

            if (dataTable.Rows.Count != 0 && dataTable.Rows[0]["BeginningOfTheLatestBreak"] != DBNull.Value)
            {
                return true;
            }

            return false;
        }

        private int GetUserMinutesOnBreak(string memberID)
        {
            dataTable = new DataTable();
            query = $"SELECT * from Events where MinutesToCatchUp IS NULL AND MemberId = '{memberID}'";
            dbAccess.ReadDataThroughAdapter(query, dataTable);

            int minutesOnBreak = Convert.ToInt32(dataTable.Rows[0]["BreakTime"]);

            return minutesOnBreak;
        }

        #endregion Break Button


        private int GetMinutesFromDateTimeInDataBase(string memberID, string nullColumnName, string dateColumnName)    // return 0 if there is no null record
        {
            query = $"SELECT * from Events where {nullColumnName} IS NULL AND MemberID = '{memberID}'";

            dbAccess.ReadDataThroughAdapter(query, dataTable);

            if (dataTable.Rows.Count == 0)
            {
                return 0;
            }

            DateTime startTime = Convert.ToDateTime(dataTable.Rows[0][dateColumnName]);

            if (startTime.Day == DateTime.Today.Day)
            {
                int minutesOfWork = Convert.ToInt32((DateTime.Now - startTime).TotalMinutes);

                return minutesOfWork;
            }

            return 0;
        }

        private void DeleteLetestNullRow(string memberID)
        {
            query = $"DELETE from Events where MinutesToCatchUp IS NULL";
            SqlCommand deleteCommand = new SqlCommand(query);

            dbAccess.ExecuteQuery(deleteCommand);
        }
    }

}
