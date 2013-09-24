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
        int softStack = 0;//сколько раз был вызван мегкий openCoennection

        public DBConnector()
        {
            String connStr = String.Format(m_CONN_STR, Properties.Settings.Default.db_path);
            connection = new OleDbConnection(connStr);
        }

        /// <summary>
        /// Открывает соединение с базой
        /// </summary>
        public void openConnection(bool soft=false)  
        {
            if (soft && connectIsOpen())
            {
                softStack++;
                return;
            }
            //по логике вещей должна быть проверка ConnectionState.Opened! хотя это не так
            if ((connection != null) && (connection.State == ConnectionState.Closed))
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Закрывает соединение с базой
        /// </summary>

        public void closeConnection(bool soft = false)
        {
            if (soft && softStack!=0)
            {
                //выполниться только в случае ошибки, вообще connect всегда должен закрывать hard closeConnection
                softStack--;
                return;
 
            }
            if ((connection != null) && (connection.State != ConnectionState.Closed))
            {
                connection.Close();
            }
        }

        /// <summary>
        /// открыто ли соединение с бд?
        /// </summary>
        /// <returns></returns>
        bool connectIsOpen()
        {
            if (connection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Выполняет переданный SQL запрос (SELECT)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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
                throw new ApplicationException("Попытка выполнения запроса при отстутствии открытого соединения с базой.");
            }
            return result;
        }

        /// <summary>
        /// Выполняет UPDATE\DELETE\INSERT. Возвращает количество измененных строк
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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
                catch (OleDbException ex)
                {
                    connection.Close(); //думаю, правильно в случае чего закрывать соединение тут, что бы не где об этом не заморачиваться
                    throw new ApplicationException(String.Format("Ошибка при выполнении запроса: {0}.\n Original error: {1}",query, ex.ToString() ));
                }
            }
            else
            {
                throw new ApplicationException("Попытка выполнения запроса при отстутствии открытого соединения с базой.");
            }
            return res;
        }
    }
}
