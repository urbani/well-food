using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRPO.View
{
    public partial class ClientManagerDialog : Form
    {
        String name, company, surname, patronikName;

        public ClientManagerDialog()
        {
            InitializeComponent();
            
        }

        public ClientManagerDialog(String name, String company, String surname, String patronikName) : this()
        {//1
            nameEntry.Text = name;
            companyEntry.Text = company;
            surnameEntry.Text = surname;
            patronimicEntry.Text = patronikName;

        }

        private void ClientCompanyManagerDialog_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
           

            this.Close();


        }
    }
}
