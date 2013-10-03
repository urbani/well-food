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
using Microsoft.Office.Interop.Excel;
using TRPO.Controller;
using TRPO.Structures;

namespace TRPO.View
{
    //	получение сведений о количестве продуктов на складе 
    //	формирование списка продуктов для закупки на основании сделанных заказов

    public partial class ManagerForm : Form, IProductManagable
    {
        ProductsManagementController prodController;
        public ManagerForm(ProductsManagementController p)
        {
            InitializeComponent();
            prodController = p;
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels el)
        {
            switch (el)
            {
                case GlobalObj.ErrorLevels.Critical:
                    MessageBox.Show(msg);
                    break;
                case GlobalObj.ErrorLevels.Info:
                    toolStripStatusLabel1.Text = msg;
                    toolStripStatusLabel1.Visible = true;
                    break;
            }
        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }

        public ProductListEntry getSelectedProd()
        {
            if(storeDataGrid.Focused)
            {
                return new ProductListEntry(
                    storeDataGrid.SelectedRows[0].Cells[0].Value.ToString(),
                    Convert.ToDouble(storeDataGrid.SelectedRows[0].Cells[1].Value.ToString()),
                    Convert.ToDouble(storeDataGrid.SelectedRows[0].Cells[2].Value.ToString()));
            } else if (reqProdDataGrid.Focused)
            {
                return new ProductListEntry(
                    reqProdDataGrid.SelectedRows[0].Cells[0].Value.ToString(), 
                    Convert.ToDouble(reqProdDataGrid.SelectedRows[0].Cells[1].Value.ToString()), 
                    Convert.ToDouble(reqProdDataGrid.SelectedRows[0].Cells[2].Value.ToString())
                                            );
            }
            else
            {
                return null;
            }
        }

        public void addProductToBuyList(ProductListEntry p)
        {
            boughtProducts.Rows.Add(p.Name, p.Count, p.Price);
        }

        public void clearLists()
        {
            storeDataGrid.Rows.Clear();
            reqProdDataGrid.Rows.Clear();
            boughtProducts.Rows.Clear();
        }

        public void setProductsList(List<ProductListEntry> plist)
        {
            foreach (ProductListEntry p in plist)
            {
                storeDataGrid.Rows.Add(p.Name, p.Count, p.Price);
            }
        }

        public void setReqProductsList(List<ProductListEntry> plist)
        {
            foreach (ProductListEntry p in plist)
            {
                reqProdDataGrid.Rows.Add(p.Name, p.Count, p.Price);
            }
        }

        public List<ProductListEntry> getBoughtProducts()
        {
            List<ProductListEntry> res = new List<ProductListEntry>();
            if (boughtProducts.Rows.Count > 0)
            {
                foreach (DataGridViewRow r in boughtProducts.Rows)
                {
                    if (r.Cells[0].Value != null && r.Cells[1].Value != null && r.Cells[2].Value != null)
                    {
                        res.Add(new ProductListEntry(r.Cells[0].Value.ToString(), Convert.ToDouble(r.Cells[1].Value.ToString()), Convert.ToDouble(r.Cells[2].Value.ToString())));
                    }
                }
            }
            return res;
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            prodController.updateProductsList();
            prodController.updateReqProductsList();
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Visible = false;
            prodController.addBoughtProducts();
        }

        private void ManagerForm_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Visible = false;
        }

        private void storeDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            prodController.addProdToBuy();
        }

        private void reqProdDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            prodController.addProdToBuy();
        }

        private void boughtProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            boughtProducts.Rows.Remove(boughtProducts.SelectedRows[0]);
        }

        private void boughtProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buyButton_Click(sender, new EventArgs());
            }
        }
        
    }
}
