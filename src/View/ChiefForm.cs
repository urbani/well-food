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
using TRPO.Structures;
using System.IO;

namespace TRPO.View
{
    public partial class ChiefForm : Form, IOrderViewable, IDishManagable
    {
        OrderCookController ordCookContr;
        DishesManagementController dishesManagementContr;

        public ChiefForm(OrderCookController occ, DishesManagementController dmc)
        {
            InitializeComponent();
            ordCookContr = occ;
            dishesManagementContr = dmc;
            setDishInfo("", "", "", "");
        }

        public void updateOrderList(List<ChiefListEntry> list)
        {//TODO: отладить изменение количества элементов в заказе
            int selectedItem = 0;
            if (listView1.SelectedIndices.Count > 0)
            {
                selectedItem = listView1.SelectedIndices[0];
            }

            listView1.Items.Clear();
            String[] s;


            foreach (ChiefListEntry entry in list)
            {
                s = new String[] { entry.name, entry.need.ToString(), entry.ready.ToString(), entry.left.ToString() };
                ListViewItem tmp = new ListViewItem(s);
                listView1.Items.Add(tmp);
            }

            if ((listView1.Items.Count > 0) &&(!listView1.Focused || listView1.SelectedItems.Count <= 0))
            {
                if (listView1.Items.Count <= selectedItem)
                {
                    selectedItem = listView1.Items.Count - 1;
                }
                this.listView1.Focus();
                this.listView1.Items[selectedItem].Selected = true;
            }
            dishesManagementContr.updateDishInfo();
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels el)
        {
            switch (el)
            {
                case GlobalObj.ErrorLevels.Critical:
                    MessageBox.Show(msg);
                    break;
                case GlobalObj.ErrorLevels.Info:
                    toolStripStatusLabel.Text = msg;
                    toolStripStatusLabel.Visible = true;
                    break;
            }
            
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
            if (linkToPh != "" && File.Exists(Properties.Settings.Default.dishesImagesFolderPath + linkToPh))
            {
                    
                dishPicture.Image = Image.FromFile(Properties.Settings.Default.dishesImagesFolderPath + linkToPh);
            }
            else
            {
                dishPicture.Image = null;
            }

        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            dishesManagementContr.updateDishInfo();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void readyButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Visible = false;
            dishesManagementContr.addReadyDishes();
            ordCookContr.updateOrderList();
        }

        public int getReadyDishesAmount()
        {
            return Convert.ToInt32(readyDishesAmount.Value);
        }

        private void leftBodyTable_Selected(object sender, TabControlEventArgs e)
        {
            toolStripStatusLabel.Visible = false;
        }

        private void readyDishesAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                readyButton_Click(sender, new EventArgs());
                readyDishesAmount.Focus();
            }
        }
    }
}
