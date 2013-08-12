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
                clientManagementController.idCompanyList.Add(kv.Key);
            }
        }

        public void setEmployList(Dictionary<int, String> employList)
        {
            foreach (KeyValuePair<int, String> kv in employList)
            {
                headerList2.Items.Add(kv.Value);
            }
        }

        public int getIndexSelectedCompany()
        {
            return headerList1.SelectedIndex;
        }

        public void showMenuList(){}
        public void chowCurMenu(){}

        private void headerSearchButton2_Click(object sender, EventArgs e)
        {
            clientManagementController.setEmployList();
        }
        //public void setEmployList() { }

    }
}
