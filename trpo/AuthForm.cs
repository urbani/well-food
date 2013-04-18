using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace trpo
{
    public partial class AuthForm : Form
    {

        public AuthForm()
        {
            InitializeComponent();
/*
//Для сворачивания формы авторизации
//TODO:Удалить когда потребуется          
ChiefForm g = new ChiefForm();
g.FormClosed += new FormClosedEventHandler(g_FormClosed);
g.Show();

CourierForm c = new CourierForm();
c.FormClosed += new FormClosedEventHandler(c_FormClosed);
c.Show();
this.WindowState = FormWindowState.Minimized;
//------------------------------------------

*/
        }


//Для сворачивания формы авторизации
//TODO:Удалить когда потребуется
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

        private void enterButton_Click(object sender, EventArgs e)
        {
            //ChiefForm g = new ChiefForm();
           // g.Show();
            CourierForm cf = new CourierForm();
            cf.Show();
            //this.Visible = false;
        }

    }
}
