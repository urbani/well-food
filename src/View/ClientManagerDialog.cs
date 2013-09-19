using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace TRPO.View
{
    public partial class ClientManagerDialog : Form
    {
        public ClientManagerDialog()
        {
            InitializeComponent();

            // Use a connection string.
            DataContext db = new DataContext(Properties.Settings.Default.db_path);




        }

        public ClientManagerDialog(int EmployId)
        {
            InitializeComponent();
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
            //actions
            this.Close();


        }
    }


        
    
}
