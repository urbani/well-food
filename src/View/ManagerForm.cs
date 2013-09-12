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
                    statusStrip1.Text = msg;
                    statusStrip1.Visible = true;
                    break;
            }
        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }

        public void setProductsList(List<ProductListEntry> plist)
        {
            foreach (ProductListEntry p in plist)
            {
                storeDataGrid.Rows.Add(p.name, p.count);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        /*    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                ExcelWorkSheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    ExcelWorkSheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }

            ExcelApp.Visible = true;*/
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            prodController.updateProductsList();
        }
        
    }
}
