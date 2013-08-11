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
using TRPO.GlobalObj;

namespace TRPO.View
{
    public partial class ChiefForm : Form, IOrderViewable
    {
        OrderCookController ordCookContr;

        public ChiefForm(OrderCookController occ)
        {
            InitializeComponent();
            ordCookContr = occ;
        }

        public void updateOrderList(List<ChiefListEntry> list)
        {
            listView1.Items.Clear();
            String[] s;
            

            foreach (ChiefListEntry entry in list)
            {
                s = new String[] { entry.name, entry.need.ToString(), entry.ready.ToString(), entry.left.ToString() };
                ListViewItem tmp = new ListViewItem(s);
                listView1.Items.Add(tmp);
            }
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels el)
        {
            toolStripStatusLabel.Text = msg;
            toolStripStatusLabel.Visible = true;
        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }
        
    }
}
