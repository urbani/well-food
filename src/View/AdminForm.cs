using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Data.Common;
using System.Collections;

namespace TRPO.View
{
    public partial class AdminForm : Form
    {
        OleDbConnection connection = null;

        public AdminForm()
        {

            InitializeComponent();

            string m_CONN_STR = Properties.Settings.Default.main_dbConnectionString;// "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}";
            String connStr = String.Format(m_CONN_STR, Properties.Settings.Default.db_path);


            connection = new OleDbConnection(connStr);
            connection.Open();
        }

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
                    throw new ApplicationException(String.Format("Ошибка при выполнении запроса: {0}.\n Original error: {1}", query, ex.ToString()));
                }
            }
            else
            {
                throw new ApplicationException("Попытка выполнения запроса при отстутствии открытого соединения с базой.");
            }

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OleDbDataReader reader;
            reader = executeQuery("SELECT u.Surname, u.Name, u.Patronymic, r.Role, u.Login FROM Users AS u INNER JOIN Roles AS r ON u.Role = r.ID_R");
            //executeNonQuery("INSERT blabla");
            while (reader.Read())
            {
                usersDataGrid.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4]);
            }
            reader.Close();
        }

    }
}
