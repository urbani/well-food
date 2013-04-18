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


namespace trpo
{
    public partial class CourierForm : Form
    {
        public CourierForm()
        {
            InitializeComponent();

            OleDbConnection m_objConnection = null;
            string m_CONN_STR = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}";

            try//CONNECT
            {
                string connStr = string.Format(m_CONN_STR, "main_db.accdb");

                m_objConnection = new OleDbConnection(connStr);

                m_objConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_objConnection = null;
            }//--connect

            try//SELECT
            {
                if (m_objConnection == null)
                {
                    return;
                }

                OleDbCommand objCommand = new OleDbCommand();
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = headerList1.Text;
                objCommand.Connection = m_objConnection;

                OleDbDataReader dataReader = objCommand.ExecuteReader();

                DataTable dtTab = new DataTable();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    dtTab.Columns.Add(dataReader.GetName(i), dataReader.GetFieldType(i));
                }
                do
                {
                    while (dataReader.Read())
                    {
                        DataRow newRow = dtTab.NewRow();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            newRow[i] = dataReader.GetValue(i);
                        }
                        dtTab.Rows.Add(newRow);
                    }
                }
                while (dataReader.NextResult());
                dataReader.Close();

                this.dataGridView1.DataSource = dtTab;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//--select

        }

        private void GeneralForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        
    }
}
