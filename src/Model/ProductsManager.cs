using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace TRPO.Model
{
    public class ProductsManager
    {
        DBConnector connector;

        public ProductsManager()
        {
            connector = new DBConnector();
        }

        public List<String> getProdNames()
        {
            List<String> result = new List<String>();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT p.Name_Prod FROM Products as p");
            while (reader.Read())
            {
                result.Add(reader[0].ToString());
            }

            reader.Close();

            connector.closeConnection();
            return result;
        }
    }
}
