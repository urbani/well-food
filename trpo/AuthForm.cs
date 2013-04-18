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

namespace trpo
{
    public partial class AuthForm : Form
    {

        private OleDbConnection m_objConnection = null;
        private string m_CONN_STR = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}";

        public AuthForm()
        {
            InitializeComponent();
            /*
            //Для сворачивания формы авторизации
            //Удалить когда потребуется          
            ChiefForm g = new ChiefForm();
            g.FormClosed += new FormClosedEventHandler(g_FormClosed);
            g.Show();

            CourierForm c = new CourierForm();
            c.FormClosed += new FormClosedEventHandler(c_FormClosed);
            c.Show();
            this.WindowState = FormWindowState.Minimized;
            //------------------------------------------

            */


            try//CONNECT
            {
                string connStr = string.Format(m_CONN_STR, "main_db.accdb");

                m_objConnection = new OleDbConnection(connStr);

                m_objConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_objConnection = null;
            }//--connect

            }


        //Для сворачивания формы авторизации
        private void g_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void c_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        //------------------------------


        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void enterButton_Click(object sender, EventArgs e) //Нажатие кнопки 
        {
            if(checkAuth(loginTextBox.Text, passTextBox.Text)) //Проверка авторизации
            {
                switch (loginTextBox.Text)
                {
                    case ("admin"):
                        AdminForm af = new AdminForm(m_objConnection);
                        af.Show();

                        break;
                    case ("chief"):
                        ChiefForm chiF = new ChiefForm(m_objConnection);
                        chiF.Show();
                        break;
                    case ("manager"):
                        ManagerForm mf = new ManagerForm(m_objConnection);
                        mf.Show();
                        break;
                    case ("courier"):
                        CourierForm couF = new CourierForm(m_objConnection);
                        couF.Show();
                        break;
                }
            }
            //TODO: ПОКАЗ текста ошибки при неверном логин-пароле 
        }

        private bool checkAuth(String login, String pass)
        {
            bool res = false;
            
            try//SELECT
            {
                    if (m_objConnection != null)
                    {
                    
                        OleDbCommand objCommand = new OleDbCommand();
                        objCommand.CommandType = CommandType.Text;
                        objCommand.CommandText = "SELECT Password FROM Users WHERE Login = \"" + login + "\"";
                        objCommand.Connection = m_objConnection;
                        OleDbDataReader reader = objCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            res = (pass.Equals(reader[0].ToString()));
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }//--select
                return res;
        }
    }
}
