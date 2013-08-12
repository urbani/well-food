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
        List<String> companyList_ = new List<string>();
        List<String> employlist_  = new List<string>();
        public CourierForm(ClientManagementConroller cmc, OrdersConroller oc)
        {
            InitializeComponent();
            clientManagementController = cmc;
            clientManagementController.addForm(this);
            ordersController = oc;
            ordersController.addForm(this);
            clientManagementController.fillCompList();

            //headerList1.DataSource = temp;
            headerList1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            headerList1.AutoCompleteSource = AutoCompleteSource.ListItems;
            
        
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
            headerList1.Items.Clear();
            clientManagementController.idCompanyList.Clear();
            foreach (KeyValuePair<int, String> kv in companyList)
            {
                companyList_.Add(kv.Value);
                clientManagementController.idCompanyList.Add(kv.Key);
            }
        }

        public void setEmployList(Dictionary<int, String> employList)
        {
            headerList2.Items.Clear();

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

        private void s(object sender, EventArgs e)
        {

        }

        private void headerList2_EnabledChanged(object sender, EventArgs e)
        {
            
        }

        private void headerList2_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void headerList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientManagementController.setEmployList();
        }
        //public void setEmployList() { }

    }
}
