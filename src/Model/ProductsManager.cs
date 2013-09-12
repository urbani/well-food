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
            List<ProductListEntry> result = new List<ProductListEntry>();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT p.Name_Prod, 3 FROM Products as p");
            while (reader.Read())
            {
                result.Add(new ProductListEntry(reader[0].ToString(), Convert.ToDouble(reader[1].ToString())));
            }

            reader.Close();

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
