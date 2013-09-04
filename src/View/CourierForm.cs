﻿using System;
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
using TRPO.View;

namespace TRPO.View
{
    public partial class CourierForm : Form, IClientManagable, IOrderManagable
    {
        ClientManagementConroller clientManagementController;
        OrdersConroller ordersController;

        public CourierForm(ClientManagementConroller cmc, OrdersConroller oc)
        {
            InitializeComponent();
            clientManagementController = cmc;
            clientManagementController.addForm(this);
            ordersController = oc;
            ordersController.addForm(this);
            clientManagementController.fillCompanyList();
            
            //включаем автозаполнение и подключаем источник данных
            headerList1.DataSource = clientManagementController.companyList;  
            headerList1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            headerList1.AutoCompleteSource = AutoCompleteSource.ListItems;

            headerList2.DataSource = clientManagementController.employList;
            headerList2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            headerList2.AutoCompleteSource = AutoCompleteSource.ListItems;
            ordersController.updateActiveMenu();
            orderMenu.Items.Clear();
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels el)
        {
            //throw ("не умею");
        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }




        public int getIndexSelectedCompany()
        {
            return headerList1.SelectedIndex;
        }

        public void showMenuList(){}
        public void showCurMenu(){}

        private void headerSearchButton2_Click(object sender, EventArgs e)
        {
            clientManagementController.fillEmployList();
        }



        private void headerList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientManagementController.fillEmployList();
            
        }

        public void updateCompanyList()
        {
            headerList1.DataSource = null;
            headerList1.DataSource = clientManagementController.employList;
        }

        public void updateEmployList()
        {
            headerList2.DataSource = null;
            headerList2.DataSource = clientManagementController.employList;
        }



        public void updateMenuList(List<CourierListEntry> listDishes)
        {
            menuList1.Items.Clear();
            menuList2.Items.Clear();
            menuList3.Items.Clear();
            menuList4.Items.Clear();
            String[] rawSting;


            foreach (CourierListEntry dishWithPrice in listDishes)
            {
                rawSting = new String[] { dishWithPrice.dish, dishWithPrice.price.ToString() };
                ListViewItem tmp = new ListViewItem(rawSting);
                if (dishWithPrice.isSpecial)
                    menuList4.Items.Add(newElem(rawSting));

                switch (dishWithPrice.type)
                {
                    case("Первое"):
                        menuList1.Items.Add(newElem(rawSting));
                        break;
                    case("Второе"):
                        menuList2.Items.Add(newElem(rawSting));
                        break;
                    case ("Третье"):
                        menuList3.Items.Add(newElem(rawSting));
                        break;
                }
                
            }
        }
        ListViewItem newElem(String[] dish)
        {
            return new ListViewItem(dish);

        }


        private void s(object sender, EventArgs e)
        {}
        private void headerList2_EnabledChanged(object sender, EventArgs e)
        {}
        private void headerList2_MouseClick(object sender, MouseEventArgs e)
        {}

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void employEditButton_Click(object sender, EventArgs e)
        {
            ClientManagerDialog cmd = new ClientManagerDialog();
            cmd.ShowDialog();
        }

        private void orderMenu_DoubleClick(object sender, EventArgs e) 
        {
            orderMenu.Items.RemoveAt(orderMenu.SelectedIndices[0]);
        }


        private void menuList1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem dishEnty = new ListViewItem();
            ListViewItem.ListViewSubItem priceEntry = new ListViewItem.ListViewSubItem();
            dishEnty.Text = menuList1.SelectedItems[0].Text;
            priceEntry.Text = menuList1.SelectedItems[0].SubItems[1].Text;
            dishEnty.SubItems.Add(priceEntry);
            orderMenu.Items.Add(dishEnty);
        }

        private void menuList2_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem dishEnty = new ListViewItem();
            ListViewItem.ListViewSubItem priceEntry = new ListViewItem.ListViewSubItem();
            dishEnty.Text = menuList2.SelectedItems[0].Text;
            priceEntry.Text = menuList2.SelectedItems[0].SubItems[1].Text;
            dishEnty.SubItems.Add(priceEntry);
            orderMenu.Items.Add(dishEnty);
        }

        private void menuList3_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem dishEnty = new ListViewItem();
            ListViewItem.ListViewSubItem priceEntry = new ListViewItem.ListViewSubItem();
            dishEnty.Text = menuList3.SelectedItems[0].Text;
            priceEntry.Text = menuList3.SelectedItems[0].SubItems[1].Text;
            dishEnty.SubItems.Add(priceEntry);
            orderMenu.Items.Add(dishEnty);
        }

        private void menuList4_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem dishEnty = new ListViewItem();
            ListViewItem.ListViewSubItem priceEntry = new ListViewItem.ListViewSubItem();
            dishEnty.Text = menuList4.SelectedItems[0].Text;
            priceEntry.Text = menuList4.SelectedItems[0].SubItems[1].Text;
            dishEnty.SubItems.Add(priceEntry);
            orderMenu.Items.Add(dishEnty);
        }

    }

    public struct ClientData
    {
        public int id;
        public String data;
        public ClientData(int id_, String data_)
        {
            id = id_;
            data = data_;

        }
    }
}
