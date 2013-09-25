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
        delegate void proprietyEvent();
        bool systemChange = false; //флаг изменение списка прозошло в системых целях (не по вызову пользователя)
        DishesTypes curTypeMenu; //если подчеркивает зеленым - то это чушь
        List<ListView> listViewList = new List<ListView>();


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
            listViewList.Add(menuList1);
            listViewList.Add(menuList2);
            listViewList.Add(menuList3);
            listViewList.Add(menuList4);
            


        }
        public void showMsg(String msg, GlobalObj.ErrorLevels levels)
        {
            showMsg(msg, levels, MessageBoxButtons.OK);
        }

        public void showMsg(String msg,  GlobalObj.ErrorLevels levels, MessageBoxButtons buttons = MessageBoxButtons.OK, String title="")
        {
           if (title!="")
           {
                if (levels == GlobalObj.ErrorLevels.Info)
                    title = "Уведомление";
                else if (levels == GlobalObj.ErrorLevels.Critical)
                    title = "Ошибка";
                else
                    throw new NotImplementedException("showMsg courier не предвиденный ErrorLevel");
           }
            notifyValue = MessageBox.Show(msg, title, buttons);

        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }

        void handlerNewOrderEvent(proprietyEvent func, int tabIndex)
        {
            if (tabControl1.SelectedIndex==tabIndex || tabIndex==-1)
                func();
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

            showMsg("Выбрано несколько блюд, сбросить список и продолжить?", GlobalObj.ErrorLevels.Info, MessageBoxButtons.OKCancel, "Создание новогого заказа");
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

        /// <summary>
        /// обработчик вызова(вызывать или нет?) viewReadyOrder
        /// </summary>
        void callHandlerViewReadyOrder()
        {
            if (tabControl1.SelectedIndex == 1)
            {
                ordersController.updatePlacedOrderMenu();
            }
        }



        private void headerList2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            ordersController.clientId = getEmplId(true);
            ordersController.updateActiveMenu();
            ordersController.updateOrderMenu();
            ordersController.updatePlacedOrderMenu();
        }
        private void headerList1_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void headerList1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            clientManagementController.fillEmployList();
            try { ordersController.clientId = getEmplId(true); }
            catch (KeyNotFoundException) { }
            
            ordersController.updateOrderMenu();
            ordersController.updatePlacedOrderMenu();

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


        /// <summary>
        /// обновление доступного меню
        /// </summary>
        /// <param name="listDishes"></param>
        public void updateMenuList(List<ListViewItem[]> listDishes)
        {
            foreach (int i in Enumerable.Range(0, listViewList.Count))
            {
                listViewList[i].Clear();
                listViewList[i].Items.AddRange(listDishes[i]);
            }  
        }


        public void updateOrderMenu(ListViewItem[] dishesInOrder)
        {
            buyOrderMenu.Items.Clear();
            buyOrderMenu.Items.AddRange(dishesInOrder);
        }


        private void employEditButton_Click(object sender, EventArgs e)
        {
            ClientManagerDialog cmd = new ClientManagerDialog();
            cmd.ShowDialog();
        }

        private void orderMenu_DoubleClick(object sender, EventArgs e) 
        {
            ordersController.removeDishFromOrder(buyOrderMenu.SelectedItems[0].Text, Convert.ToSingle(buyOrderMenu.SelectedItems[0].SubItems[1].Text));
            ordersController.updateOrderMenu();
            changeTotalLabel(ordersController.getTotalPrice());
        }

        void changeTotalLabel(float total)
        {
            totalLabel.Text = String.Format("{0} руб.", total);
        }

        public int getSelectedDishType()
        {
            return tabControl1.SelectedIndex;
        }

        public int getIndexSelectedDish()
        {
            return curList.SelectedIndices[0];
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
            ordersController.dishindex = curList.SelectedIndices[0];
            ordersController.addDishToOrder(curList.SelectedItems[0].Text, Convert.ToSingle(curList.SelectedItems[0].SubItems[1].Text));

            ordersController.updateOrderMenu();
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
            ordersController.createOrder();
        }
        public void clearOrderMenu()
        {
            buyOrderMenu.Items.Clear();
        }
        public int getEmplId(bool silent=false)
        {
            if (Equals(null,headerList2.SelectedIndex))
            {
                if(!silent)
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
            int t = headerList2.SelectedIndex;
            return headerList2.SelectedIndex;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void updatePlacedOrderMenu(ListViewItem[] orderArr)
        {
            placedOrderMenu.Items.Clear();
            placedOrderMenu.Items.AddRange(orderArr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ordersController.checkoutOrder();
        }

        public void updatePlaceOrderTotalPrice(float totalPrice)
        {
            placedOrderTotalSum.Text = String.Format("{0} руб", totalPrice);
        }

        public void updatePlecedStatusOrder(String statusMsg)
        {
            placedStatusOrder.Text = statusMsg;
        }

        private void leftBodyTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            ordersController.typeIndex = leftBodyTable.SelectedIndex;
        }






    }


}
