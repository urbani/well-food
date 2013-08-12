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
                try//CONNECT
                {
                    connection.Open();
                }
                catch (InvalidOperationException ioe)
                {
                    //connection = null;
                    throw ioe;
                } catch (OleDbException odx)
                {
                    throw odx;
                }
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
            if (connection.State == ConnectionState.Open)
            {
                try//SELECT
                {
                    OleDbCommand objCommand = new OleDbCommand();
                    objCommand.CommandType = CommandType.Text;
                    objCommand.CommandText = query;
                    objCommand.Connection = connection;
                    OleDbDataReader reader = objCommand.ExecuteReader();
                    result = reader;
                }
                catch (OleDbException ex)
                {
                    throw ex;
                }//--select
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("WARNING! Попытка выполнения запроса при отстутствии открытого соединения с базой.");
            }
            return result;
        }
        
    }
}
