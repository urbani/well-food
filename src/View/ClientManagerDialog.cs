using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TRPO.Controller;
namespace TRPO.View
{
    public partial class ClientManagerDialog : Form, IDialog
    {
        ClientManagementConroller clientManagementConroller;
        int companyId;
        int employId;
        bool newempl;
        int compEdit = 0;

        String company, surname, name, patr;

        public ClientManagerDialog(ClientManagementConroller cmc, int emplId, int compId, bool newempl=false)
        {
            InitializeComponent();
            clientManagementConroller = cmc;
            companyId = compId;
            employId = emplId;
            clientManagementConroller.addDialog(this);
            this.newempl = newempl;
            if (!newempl)
                clientManagementConroller.selectEmployDat();
            //clientManagementConroller.();
        }

        public ClientManagerDialog(ClientManagementConroller cmc, int compId, int status)
        {
            InitializeComponent();
            clientManagementConroller = cmc;
            companyId = compId;
            clientManagementConroller.addDialog(this);
            groupBox1.Visible=false;
            label3.Visible = false;
            label4.Visible = false;
            surnameBox.Visible = false;
            nameBox.Visible = false;
            patrBox.Visible = false;
            label2.Visible = false;
            compEdit = status;
            companyBox.Enabled = true;

        }

        private void ClientCompanyManagerDialog_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (compEdit == 1)
            {
                clientManagementConroller.createCompany();
                this.Close();
            }
            if (compEdit == 2)
            {
                clientManagementConroller.editCompany();
                this.Close();
            }
            if (!newempl)
                clientManagementConroller.updateEmployDate();
            else
                clientManagementConroller.insertEmployData();
            this.Close();
        }
        public int getCompanyId() { return companyId; }
        public int getEmployId() { return employId; }
        public void fillFiled(List<String> listStr)
        {
            companyBox.Text = listStr[0];
            surnameBox.Text = listStr[1];
            nameBox.Text = listStr[2];
            patrBox.Text = listStr[3];
        }
        public List<String> getFileds()
        {
            return new List<string>() { companyBox.Text, surnameBox.Text, nameBox.Text, patrBox.Text };
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public String getCompanyName()
        {
            return companyBox.Text;
        }
    }
}
