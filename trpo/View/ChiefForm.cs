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

namespace trpo.View
{
    public partial class ChiefForm : Form
    {
        private OleDbConnection dbConnection = null;//соединение



        public ChiefForm(OleDbConnection con)
        {
            InitializeComponent();
            dbConnection = con;


            String result = "";
            try//SELECT
            {
                if (dbConnection != null)
                {
                    OleDbCommand objCommand = new OleDbCommand();
                    objCommand.CommandType = CommandType.Text;
                    objCommand.CommandText = "SELECT tab1.Name_Dish, tab2.Need, tab2.Ready, tab2.D_Left FROM (SELECT dis.Name_Dish, dis.ID_Dish FROM Dishes dis) AS tab1 INNER JOIN (SELECT do.ID_Dish, SUM(do.Dish_Count) AS Need, SUM(do.Ready_Count) AS Ready, (Need-Ready) AS D_Left FROM Dishes_Order AS do INNER JOIN (SELECT ord.ID_Ord FROM Orders AS ord INNER JOIN OrderStatuses AS ost ON ost.ID_Ord=ord.Status WHERE ost.Status='Открыт')  AS tab ON do.ID_Order=tab.ID_Ord GROUP BY do.ID_Dish) AS tab2 ON tab1.ID_Dish=tab2.ID_Dish";
                    objCommand.Connection = dbConnection;
                    OleDbDataReader reader = objCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader[0].ToString();
Console.WriteLine(result);
                        //reader.GetEnumerator()
                        
//TODO: заполнение таблицы запросом из базы
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//--select
//     MessageBox.Show(result);


        }
        
    }
}
