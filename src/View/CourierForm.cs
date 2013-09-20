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
using TRPO.View;

namespace TRPO.View
{
    public partial class CourierForm : Form, IClientManagable, IOrderManagable
    {
        ClientManagementConroller clientManagementController;
        OrdersConroller ordersController;
        ListView curList = new ListView();
        /// <summary>
        /// продолжить ли выполнение операции или сбросить?
        /// </summary>
        bool tryContinue = false;
        bool notSetEmploy = false;
        DialogResult notifyValue=DialogResult.Yes;
        int lastEmployIndex = 0;
        int lastCompanyIndex = 0;
        bool systemChange = false;

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
            buyOrderMenu.Items.Clear();



        }
        public void showMsg(String msg, GlobalObj.ErrorLevels levels)
        {
            MessageBoxButtons butttons = MessageBoxButtons.YesNo;
            String titile;
            if (levels == GlobalObj.ErrorLevels.Info)
                titile = "Уведомление";
            else
                titile = "Ошибка";
            notifyValue = MessageBox.Show(msg, titile, butttons);
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
        /// <summary>
        /// продолжить выполнение запроса, в случае если заполнены уже часть заказа?
        /// </summary>
        /// <returns></returns>
        bool requestFromContinue()
        {

            showMsg("выбрано несколько блюд, сбросить список и продолжить?", GlobalObj.ErrorLevels.Info);
            if (notifyValue == DialogResult.Yes)
            {
                handlerContinueChoose();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// обрабочик смены клиенты
        /// </summary>
        void handlerContinueChoose()
        {
            buyOrderMenu.Items.Clear();
        }

        private void headerList1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            

        }
        private void headerList2_MouseClick(object sender, MouseEventArgs e) {}

        private void headerList2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (buyOrderMenu.Items.Count != 0)
            {
                if (requestFromContinue())
                {
                    buyOrderMenu.Items.Clear();
                }
                else
                {
                    headerList2.SelectedIndex = lastEmployIndex;
                }
            }
            lastEmployIndex = headerList2.SelectedIndex;
        }

        private void headerList1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if (systemChange)
            {
                systemChange = false;
                return;
            }
            if (buyOrderMenu.Items.Count == 0)
                clientManagementController.fillEmployList();
            else
            {
                if (requestFromContinue())
                {
                    buyOrderMenu.Items.Clear();
                    clientManagementController.fillEmployList();
                }
                else
                {
                    systemChange = true;
                    headerList1.SelectedIndex = lastCompanyIndex;
                }
            }
            lastCompanyIndex = headerList1.SelectedIndex;
        }

        private void headerList2_SelectedIndexChanged(object sender, EventArgs e) { }

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
                {
                    menuList4.Items.Add(newElem(rawSting));
                }
                else
                {


                    switch (dishWithPrice.type)
                    {
                        case ("Первое"):
                            menuList1.Items.Add(newElem(rawSting));
                            break;
                        case ("Второе"):
                            menuList2.Items.Add(newElem(rawSting));
                            break;
                        case ("Третье"):
                            menuList3.Items.Add(newElem(rawSting));
                            break;
                    }
                }
                
            }
        }
        ListViewItem newElem(String[] dish)
        {
            return new ListViewItem(dish);

        }

        void positivStatusHandler(String messege="Готово")
        {
            messege = "Состояние: " + messege;
            toolStripStatusLabel1.Text = messege;
            toolStripStatusLabel1.ForeColor = Color.Black;
        }

        void negativeStatusHandler(String messege = "Что-то пошло не так")
        {

            messege = "Ошибка: " + messege;
            
            toolStripStatusLabel1.Text = messege;
            toolStripStatusLabel1.ForeColor = Color.DarkRed;
        }
        private void s(object sender, EventArgs e)
        {}
        private void headerList2_EnabledChanged(object sender, EventArgs e)
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
            buyOrderMenu.Items.RemoveAt(buyOrderMenu.SelectedIndices[0]);
        }

        void handlerMenuList_DoubleClick()
        {
            if (headerList2.SelectedItem == null)
            {
                negativeStatusHandler("Выбирите пожалуйста сотрудника");
                return;
            }
            ListViewItem dishEnty = new ListViewItem();
            ListViewItem.ListViewSubItem priceEntry = new ListViewItem.ListViewSubItem();
            dishEnty.Text = curList.SelectedItems[0].Text;
            priceEntry.Text = curList.SelectedItems[0].SubItems[1].Text;
            dishEnty.SubItems.Add(priceEntry);
            buyOrderMenu.Items.Add(dishEnty);
            positivStatusHandler();
        }

        private void menuList1_DoubleClick(object sender, EventArgs e)
        {
            curList = menuList1;
            handlerMenuList_DoubleClick();

        }

        private void menuList2_DoubleClick(object sender, EventArgs e)
        {
            curList = menuList2;
            handlerMenuList_DoubleClick();
        }

        private void menuList3_DoubleClick(object sender, EventArgs e)
        {
            curList = menuList3;
            handlerMenuList_DoubleClick();
        }

        private void menuList4_DoubleClick(object sender, EventArgs e)
        {
            curList = menuList4;
            handlerMenuList_DoubleClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void menuList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CourierForm_Load(object sender, EventArgs e)
        {

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
