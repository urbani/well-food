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
            
        }

        // Открывает соединение с базой
        public void openConnection()  
        {
            if (connection == null)
            {
                try//CONNECT
                {
                    string connStr = string.Format(m_CONN_STR, Properties.Settings.Default.db_path);

                    connection = new OleDbConnection(connStr);

                    connection.Open();
                }
                catch (InvalidOperationException ioe)
                {
                    connection = null;
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
            if (connection != null)
            {
                connection.Close();
            }
        }

        // Выполняет переданный SQL запрос
        public OleDbDataReader executeQuery(String query)
        {
            OleDbDataReader result = null;
            if (connection != null)
            {   
                try//SELECT
                {
                
                    OleDbCommand objCommand = new OleDbCommand();
                    objCommand.CommandType = CommandType.Text;
                    objCommand.CommandText = query;
                    objCommand.Connection = connection;
                    OleDbDataReader reader = objCommand.ExecuteReader();
                    result = reader;

                   /* if (reader.Read())
                    {
                        result = reader;
                        // result = reader[0].ToString();
                    }
                    */
                    
                }
                catch (OleDbException ex)
                {
                    throw ex;
                    //MessageBox.Show(ex.Message);
                }//--select
            }
            return result;
        }
        
    }
}
