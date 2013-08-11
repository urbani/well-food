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
            clientManagementController.fillCompList();
        
            //headerList1.Items.Add("Кот котофей");
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels el)
        {
        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }

        public void setCompanyList(Dictionary<int, String> companyList)
        {
            foreach (KeyValuePair<int, String> kv in companyList)
            {
                headerList1.Items.Add(kv.Value);
            }
        }
        public void setEmployList(Dictionary<int, String> companyList)
        {
         return;
        }
        public int getCurCompany()
        {
          
          return -1;
        }
       public void showMenuList(){}
       public void chowCurMenu(){}
       public void setEmployList() { }

    }
}
