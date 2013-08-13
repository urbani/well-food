using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace TRPO.Model
{
    class DBConnector
    {
        private OleDbConnection connection = null;
        private static string m_CONN_STR = Properties.Settings.Default.main_dbConnectionString;// "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}";

        public DBConnector()
        {
            String connStr = String.Format(m_CONN_STR, Properties.Settings.Default.db_path);
            connection = new OleDbConnection(connStr);
        }

        // Открывает соединение с базой
        public void openConnection()  
        {
            if ((connection != null) && (connection.State == ConnectionState.Closed))
            {
                connection.Open();
            }
        }

        // Закрывает соединение с базой
        public void closeConnection()
        {
            if ((connection != null) && (connection.State != ConnectionState.Closed))
            {
                connection.Close();
            }
        }

        // Выполняет переданный SQL запрос
        public OleDbDataReader executeQuery(String query)
        {
            OleDbDataReader result = null;
            if (connection != null && connection.State == ConnectionState.Open)
            {
                OleDbCommand objCommand = new OleDbCommand();
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = query;
                objCommand.Connection = connection;
                OleDbDataReader reader = objCommand.ExecuteReader();
                result = reader;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("WARNING! Попытка выполнения запроса при отстутствии открытого соединения с базой.");
            }
            return result;
        }

        //Выполняет UPDATE\DELETE\INSERT. Возвращает количество измененных строк
        public int executeNonQuery(String query)
        {
            int res = 0;
            if (connection != null && connection.State == ConnectionState.Open)
            {
                try
                {
                    OleDbCommand objCommand = new OleDbCommand();
                    objCommand.CommandType = CommandType.Text;
                    objCommand.CommandText = query;
                    objCommand.Connection = connection;
                    res = objCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("WARNING! Ошибка при выполнении запроса: " + query + ".\n Original error: " + ex.ToString());
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("WARNING! Попытка выполнения запроса при отстутствии открытого соединения с базой.");
            }
            return res;
        }
    }
}
