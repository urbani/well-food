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
using TRPO.GlobalObj;
namespace TRPO.View
{
    public partial class CourierForm : Form, IClientManagable, IOrderManagable
    {
        ClientManagementConroller clientManagementController;
        OrdersConroller ordersController;
        ListView curList = new ListView(); //список указателей на меню (1,2,3,ланч) - для упрощения управления
        DialogResult notifyValue=DialogResult.Yes;
        int lastEmployIndex = 0; 
        int lastCompanyIndex = 0;
        bool systemChange = false; //флаг изменение списка прозошло в системых целях (не по вызову пользователя)
        DishesTypes curTypeMenu; //если подчеркивает зеленым - то это чушь

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
            showMsg(msg, levels, MessageBoxButtons.OK);
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels levels, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            ;
            String titile;
            if (levels == GlobalObj.ErrorLevels.Info)
                titile = "Уведомление";
            else if (levels == GlobalObj.ErrorLevels.Critical)
                titile = "Ошибка";
            else
                throw new NotImplementedException("showMsg courier не предвиденный ErrorLevel");
            
            notifyValue = MessageBox.Show(msg, titile, buttons);

        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }




        public int getIndexSelectedCompany()
        {
            return headerList1.SelectedIndex;
        }

        public void showMenuList(){throw new NotImplementedException();}
        public void showCurMenu() { throw new NotImplementedException(); }

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

            showMsg("выбрано несколько блюд, сбросить список и продолжить?", GlobalObj.ErrorLevels.Info, MessageBoxButtons.OKCancel);
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
            //MessageBox.Show(tabControl1.SelectedIndex.ToString());
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


        /// <summary>
        /// обновление доступного меню
        /// </summary>
        /// <param name="listDishes"></param>
        public void updateMenuList(List<CourierListEntry> listDishes)
        {
            menuList1.Items.Clear();
            menuList2.Items.Clear();
            menuList3.Items.Clear();
            menuList4.Items.Clear();
            String[] rawSting;

            
            int ptr = 0;
            //listDishes[0].id = 2;
            foreach (CourierListEntry dishWithPrice in listDishes)
            {
                rawSting = new String[] { dishWithPrice.dish, dishWithPrice.price.ToString() };
               // dishWithPrice.id = 2;
                ListViewItem tmp = new ListViewItem(rawSting);
                if (dishWithPrice.isSpecial)
                {
                    
                    menuList4.Items.Add(tmp);
                }
                else
                {


                    switch (dishWithPrice.type)
                    {
                        case ("Первое"):
                            menuList1.Items.Add(tmp);
                            break;
                        case ("Второе"):
                            menuList2.Items.Add(tmp);
                            break;
                        case ("Третье"):
                            menuList3.Items.Add(tmp);
                            break;
                    }
                }
                ptr ++;
            }
        }






        private void employEditButton_Click(object sender, EventArgs e)
        {
            ClientManagerDialog cmd = new ClientManagerDialog();
            cmd.ShowDialog();
        }

        private void orderMenu_DoubleClick(object sender, EventArgs e) 
        {
            ordersController.removeDishFromOrder(buyOrderMenu.SelectedItems[0].Text, Convert.ToSingle(buyOrderMenu.SelectedItems[0].SubItems[1].Text));
            buyOrderMenu.Items.Clear();
            buyOrderMenu.Items.AddRange(ordersController.getOrderMenuForView());
            changeTotalLabel(ordersController.getTotalPrice());
        }

        public List<ListViewItem> getOrderMenu()
        {
            List<ListViewItem> result = new List<ListViewItem>();
            foreach (ListViewItem entry in buyOrderMenu.Items)
            {
                result.Add(entry);
            }
            return result;
        }
        void changeTotalLabel(float total)
        {
            totalLabel.Text = String.Format("{0} руб.", total);
        }

        /// <summary>
        /// обработчик добавления новго элемента в список покупок
        /// </summary>
        void handlerMenuList_DoubleClick()
        {

            if (headerList2.SelectedItem == null)
            {
                return;
            }
            ordersController.addDishToOrder(curList.SelectedItems[0].Text, Convert.ToSingle(curList.SelectedItems[0].SubItems[1].Text));

            buyOrderMenu.Items.Clear();
            buyOrderMenu.Items.AddRange(ordersController.getOrderMenuForView());
            changeTotalLabel(ordersController.getTotalPrice());

            
            

        }

        private void menuList1_DoubleClick(object sender, EventArgs e)
        {
            curTypeMenu = DishesTypes.Первое;
            curList = menuList1;
            handlerMenuList_DoubleClick();
            
        }

        private void menuList2_DoubleClick(object sender, EventArgs e)
        {
            curTypeMenu = DishesTypes.Второе;
            curList = menuList2;
            handlerMenuList_DoubleClick();
        }

        private void menuList3_DoubleClick(object sender, EventArgs e)
        {
            curTypeMenu = DishesTypes.Третье;
            curList = menuList3;
            handlerMenuList_DoubleClick();
        }

        private void menuList4_DoubleClick(object sender, EventArgs e)
        {
            curTypeMenu = DishesTypes.Бизнесс;
            curList = menuList4;
            handlerMenuList_DoubleClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ordersController.checkoutOrder();
        }
        public void clearOrderMenu()
        {
            buyOrderMenu.Items.Clear();
        }
        public int getEmplId()
        {
            if (Equals(null,headerList2.SelectedIndex))
            {
                showMsg("Выбирите сотрудника", ErrorLevels.Info, MessageBoxButtons.OK);
                return -1;
            }
            return clientManagementController.getEmployId();
        }

        public void setWindowTitile(String title)
        {
            this.Text = title;
        }

        public int getIndexSelectedEmploy()
        {
            return headerList2.SelectedIndex;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ordersController.handlerViewReadyOrder();
        }

        public void updatePlacedOrderMenu(ListViewItem[] orderArr)
        {
            placedOrderMenu.Items.AddRange(orderArr);
        }





    }


}
