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
    public partial class ChiefForm : Form
    {
        private OleDbConnection dbConnection = null;//соединение

        public ChiefForm(OleDbConnection con)
        {
            InitializeComponent();
            dbConnection = con;
            orderItemBox.Items.Add("Hi!");
            orderItemBox.Items.Add("Hi!");
            orderItemBox.Items.Add("Hi!");
            orderItemBox.Items.Add("Hi!");
        }
        
    }
}
