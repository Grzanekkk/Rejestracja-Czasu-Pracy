using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

//  Microsoft.Data.SqlClient NuGet packet requiered 

namespace DatabaseConnection        // Class used to execute commands and connect to database
{
    public class DBAccess
    {
        private SqlCommand command = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private SqlConnection connection = new SqlConnection();
        private IConfiguration configuration;


        #region Connection Managment

        public void CreateConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    //ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[configuration.GetConnectionString("DPKSystemDB")];
                    //ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[];
                    //SqlConnectionStringBuilder build = new SqlConnectionStringBuilder(setting.ConnectionString);
                    //connection = new SqlConnection(setting.ConnectionString);

                    //connection.ConnectionString = setting.ConnectionString;
                    connection.ConnectionString = "Data Source=JASIO1\\JANEKSQL;Initial Catalog=DPKSystem;Integrated Security=True";
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CloseConnection()
        {
            connection.Close();
        }

        private void CheckConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                CreateConnection();
            }
        }

        #endregion Connection Managment


        #region Execute Queries


        public int ExecuteDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                CheckConnection();

                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(adapter);


                string insert = DbCommandBuilder.GetInsertCommand().CommandText.ToString();
                string update = DbCommandBuilder.GetUpdateCommand().CommandText.ToString();
                string delete = DbCommandBuilder.GetDeleteCommand().CommandText.ToString();


                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ReadDataThroughAdapter(string query, DataTable tblName)
        {
            try
            {
                CheckConnection();

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SqlDataReader ReadDataThroughReader(string query)
        {
            //DataReader used to sequentially read data from a data source
            SqlDataReader reader;

            try
            {
                CheckConnection();

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int ExecuteQuery(SqlCommand dbCommand)
        {
            try
            {
                CheckConnection();

                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion Execute Queries
    }
}