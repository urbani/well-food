using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace TRPO.View
{
    public partial class ClientManagerDialog : Form
    {
        public ClientManagerDialog()
        {
            InitializeComponent();

            // Use a connection string.
            DataContext db = new DataContext(Properties.Settings.Default.db_path);
            
            // Get a typed table to run queries.
            Table<Employee> Customers = db.GetTable<Employee>();
            // Query for customers who have placed orders.
            //var custQuery =
            //    from cust in Employee
            //    where cust.Orders.Any()
            //    select cust;

            //foreach (Customer cust in custQuery)
            //{
            //    Console.WriteLine("ID={0}, City={1}", cust.CustomerID,
            //        cust.City);
            //}



        }

        public ClientManagerDialog(int EmployId)
        {
            InitializeComponent();
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
            //actions
            this.Close();


        }
    }


    [Table(Name = "Employee")]  //Свойство Name задает имя таблицы в базе данных.
    public class Employee
    {
        [Column(IsPrimaryKey = true, Storage = "ID_Emp")]
        public string ID_Emp;
        


        


        [Column(Storage = "Name_Emp")]
        public string Name_Emp;


    }
        //public string City
        //{
        //    get
        //    {
        //        return this._City;
        //    }
        //    set
        //    {
        //        this._City=value;
        //    }
        //}

        //    }

        
    
}
