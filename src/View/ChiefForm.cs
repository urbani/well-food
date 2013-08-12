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
using System.IO;

namespace TRPO.View
{
    public partial class ChiefForm : Form, IOrderViewable, IDishViewable
    {
        OrderCookController ordCookContr;
        DishesManagementController dishesManagementContr;

        public ChiefForm(OrderCookController occ, DishesManagementController dmc)
        {
            InitializeComponent();
            ordCookContr = occ;
            dishesManagementContr = dmc;
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

            if (listView1.Items.Count > 0) 
            {
                listView1.Items[0].Selected = true;
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

        public String getSelectedDishName()
        {
            return listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0].Text : "";
        }

        public void setDishInfo(String name, String dType, String linkToPh, String rec)
        {
            dishName.Text = name;
            dishTypeLabel.Text = dType;
            receipeText.Text = rec;
            try
            {
                if (linkToPh != "")
                {
                    dishPicture.Image = Image.FromFile(Properties.Settings.Default.dishesImagesFolderPath + linkToPh);
                }
                else
                {
                    dishPicture.Image = null;
                }
            }
            catch (FileNotFoundException ex)
            {
                dishPicture.Image = null;
                System.Diagnostics.Debug.WriteLine("WARNING! File with image:" + linkToPh + " of the dish: " + name + " not found!");
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            dishesManagementContr.updateDishInfo();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
