using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using TRPO.Structures;

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

        public List<ProductListEntry> getProductsLeft()
        {
            Dictionary<String, Double> productsIn = new Dictionary<String, Double>();
            Dictionary<String, Double> productsOut = new Dictionary<String, Double>();
            connector.openConnection();
            OleDbDataReader reader;
            reader = connector.executeQuery("SELECT pr.Name_Prod, SUM(pi.Amount) FROM Prod_in pi INNER JOIN Products pr ON pi.ID_Prod = pr.ID_Prod GROUP BY pr.Name_Prod");
            while (reader.Read())
            {
                productsIn.Add(reader[0].ToString(), Convert.ToDouble(reader[1].ToString()));
            }

            reader = connector.executeQuery("SELECT pr.Name_Prod, SUM(po.Amount) FROM Prod_out po INNER JOIN Products pr ON po.ID_Prod = pr.ID_Prod GROUP BY pr.Name_Prod");
            while (reader.Read())
            {
                productsOut.Add(reader[0].ToString(), Convert.ToDouble(reader[1].ToString()));
            }
            
            reader.Close();

            List<ProductListEntry> result = new List<ProductListEntry>();
            foreach (KeyValuePair<String, Double> e in productsIn)
            {
                result.Add(new ProductListEntry(e.Key, e.Value - (productsOut.ContainsKey(e.Key) ? productsOut[e.Key] : 0 )));
            }


            connector.closeConnection();
            return result;
        }

        public void addProduct(String prodName)
        {
            connector.openConnection();

            int lastID = 0;
            OleDbDataReader reader = connector.executeQuery("SELECT MAX(ID_Prod) FROM Products");
            if (reader.Read())
            {
                lastID = Convert.ToInt32(reader[0]);
            }

            connector.executeNonQuery("INSERT INTO Products VALUES (" + (lastID + 1) + ", \"" + prodName+ "\", -1)");
            connector.closeConnection();
        }

        public void addProduct(String prodName, Double price)
        {
            throw new NotImplementedException();
        }

        public void deleteProduct(String prodName)
        {
            connector.openConnection();
            connector.executeNonQuery("DELETE FROM Products Where ID_Prod = (SELECT ID_Prod FROM Products WHERE Name_Prod = \"" + prodName + "\")");
            connector.closeConnection();
        }
    }
}
