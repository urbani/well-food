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
using TRPO.Controller;

namespace TRPO.View
{
    public partial class AuthForm : Form, IAuthentification, IInterractable
    {
        private LoginController loginController;

        public AuthForm()
        {
            InitializeComponent();
            loginController = new LoginController(this);
        }

        public string getLogin()    { return loginTextBox.Text;}
        public string getPassword() { return passTextBox.Text;}
        public void clearLogin()    { loginTextBox.Text = "";}
        public void clearPassword() { passTextBox.Text = "";}

        public void showMsg(String msg)
        {
            MessageBox.Show(msg);
        }

        public void showErrorText(String text)
        {
            errorLabel.Text = text;
            errorLabel.Visible = true;
        }

        public void hideErrorText()
        {
            errorLabel.Visible = false;
        }

        public void clearAuthData()
        {
            clearLogin();
            clearPassword();
        }

        private void enterButton_Click(object sender, EventArgs e) //Нажатие кнопки 
        {
            loginController.Login();
        }

        private void passTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            errorLabel.Visible = false;
            if (e.KeyCode == Keys.Enter)
		    {
                loginController.Login();
		    }
        }
    }
}
