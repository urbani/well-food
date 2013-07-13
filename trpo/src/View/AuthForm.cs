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

namespace trpo.View
{
    public partial class AuthForm : Form
    {

        private OleDbConnection m_objConnection = null;
        private string m_CONN_STR = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}";

        public AuthForm()
        {
            InitializeComponent();
            
            //Для сворачивания формы авторизации
            //Удалить когда потребуется          
            ChiefForm g = new ChiefForm(m_objConnection);
            g.FormClosed += new FormClosedEventHandler(g_FormClosed);
            g.Show();

           /* CourierForm c = new CourierForm(m_objConnection);
            c.FormClosed += new FormClosedEventHandler(c_FormClosed);
            c.Show();*/
            this.WindowState = FormWindowState.Minimized;
            //------------------------------------------
            


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
//TODO Удалить после завершения работы с формой повара
/*
 * ChiefForm chief = new ChiefForm(m_objConnection);
 * chief.Show();
 */
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

        private String getSqlResp(String sqlReq)
        {
            String result = "";
            try//SELECT
            {
                if (m_objConnection != null)
                {
                    OleDbCommand objCommand = new OleDbCommand();
                    objCommand.CommandType = CommandType.Text;
                    objCommand.CommandText = sqlReq;
                    objCommand.Connection = m_objConnection;
                    OleDbDataReader reader = objCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader[0].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//--select
            return result;
        }

        private void enterButton_Click(object sender, EventArgs e) //Нажатие кнопки 
        {
            
            if(checkAuth(loginTextBox.Text, passTextBox.Text)) //Проверка авторизации
            {
                String req = "SELECT r.Role FROM Users u, Roles r WHERE u.Login='" + loginTextBox.Text + "' AND u.Role = r.ID_R";

                switch (getSqlResp(req))
                {
                    case ("Administrator"):
                        AdminForm af = new AdminForm(m_objConnection);
                        af.Show();
                        break;
                    case ("Chief"):
                        ChiefForm chief = new ChiefForm(m_objConnection);
                        chief.Show();
                        break;
                    case ("Manager"):
                        ManagerForm mf = new ManagerForm(m_objConnection);
                        mf.Show();
                        break;
                    case ("Courier"):
                        CourierForm couF = new CourierForm(m_objConnection);
                        couF.Show();
                        break;
                }
            }
            else
            {
                errorLabel.Text = "Неверный логин/пароль";
                errorLabel.Visible = true;
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

        private void passTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            errorLabel.Visible = false;
            if (e.KeyCode == Keys.Enter)
		    {
			    enterButton_Click(sender, new EventArgs());
		    }
        }
    }
}
