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

        String company, surname, name, patr;

        public ClientManagerDialog(ClientManagementConroller cmc, int emplId, int compId)
        {
            InitializeComponent();
            clientManagementConroller = cmc;
            companyId = compId;
            employId = emplId;
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
            clientManagementConroller.updateEmployDate();
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
    }
}
