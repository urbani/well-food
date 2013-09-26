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
            Dictionary<String, Double> productPrices = new Dictionary<String, Double>();
            connector.openConnection();
            OleDbDataReader reader;
            reader = connector.executeQuery("SELECT pr.Name_Prod, SUM(pi.Amount), pr.Price FROM Prod_in pi INNER JOIN Products pr ON pi.ID_Prod = pr.ID_Prod GROUP BY pr.Name_Prod, pr.Price");
            while (reader.Read())
            {
                productsIn.Add(reader[0].ToString(), Convert.ToDouble(reader[1].ToString()));
                productPrices.Add(reader[0].ToString(), Convert.ToDouble(reader[2].ToString()));
            }

            reader = connector.executeQuery("SELECT pr.Name_Prod, SUM(po.Amount) FROM Prod_out po INNER JOIN Products pr ON po.ID_Prod = pr.ID_Prod GROUP BY pr.Name_Prod");
            while (reader.Read())
            {
                productsOut.Add(reader[0].ToString(), Convert.ToDouble(reader[1].ToString()));
            }
            
            reader.Close();
            
            List<String> addedProducts = new List<String>();

            List<ProductListEntry> result = new List<ProductListEntry>();
            double left = 0;
            foreach (KeyValuePair<String, Double> e in productsIn)
            {
                left = e.Value - (productsOut.ContainsKey(e.Key) ? productsOut[e.Key] : 0);
                if (left > 0)
                {
                    result.Add(new ProductListEntry(e.Key, left, productPrices[e.Key]));
                    addedProducts.Add(e.Key);
                }
            }


            reader = connector.executeQuery("SELECT Name_Prod, Price FROM Products");
            while (reader.Read())
            {
                if (!addedProducts.Contains(reader[0].ToString()))
                result.Add(new ProductListEntry(reader[0].ToString(), 0, Convert.ToDouble(reader[1])));
            }

            connector.closeConnection();
            return result;
        }

        public List<ProductListEntry> getReqProducts()
        {
            List<ProductListEntry> prodLeft = getProductsLeft();
            Dictionary<String, Double> productsInStock = new Dictionary<String, Double>();
            foreach (ProductListEntry p in prodLeft)
            {
                if (p.Count > 0)
                {
                    productsInStock.Add(p.Name, p.Count);
                }
            }
            
            List<ProductListEntry> result = new List<ProductListEntry>();
            connector.openConnection();
            OleDbDataReader reader;
            reader = connector.executeQuery(@"
                SELECT pr.Name_Prod, sel2.ProdLeft, pr.Price
                FROM 
	                Products pr 
                INNER JOIN
	                (
		                SELECT pd.ID_Prod, SUM(pd.Product_Count * sel1.Di_Left) AS ProdLeft 
		                FROM 
			                Products_Dishes pd 
		                INNER JOIN
			                (
					                SELECT di.Name_Dish, dio.ID_Dish, SUM(dio.Dish_Count - dio.Ready_Count) as Di_Left 
					                FROM 
						                Dishes_Order dio 
					                INNER JOIN 
						                Dishes di 
					                ON di.ID_Dish = dio.ID_Dish 
					                GROUP BY dio.ID_Dish, di.Name_Dish
			                ) AS sel1
		                ON sel1.ID_Dish = pd.ID_Dish
		                GROUP BY pd.ID_Prod
	                ) AS sel2
                ON pr.ID_Prod = sel2.ID_Prod 
                ORDER BY pr.Name_Prod
            ");

            Double tmpAmount = 0;
            while (reader.Read())
            {
                tmpAmount = Convert.ToDouble(reader[1].ToString()) - (productsInStock.ContainsKey(reader[0].ToString()) ?  productsInStock[reader[0].ToString()] : 0);
                
                if (tmpAmount > 0)
                {
                    result.Add(
                                new ProductListEntry(reader[0].ToString(),
                                Convert.ToDouble(tmpAmount), 
                                Convert.ToDouble(reader[2].ToString()))
                            );
                }
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

            connector.executeNonQuery("INSERT INTO Products VALUES (" + (lastID + 1) + ", \"" + prodName+ "\", -1, FALSE)");
            connector.closeConnection();
        }

        public void addProduct(String prodName, Double price)
        {
            throw new NotImplementedException();
        }

        public void addIncomeProducts(List<ProductListEntry> products)
        {
            connector.openConnection();
            foreach (ProductListEntry p in products)
            {
                connector.executeNonQuery("INSERT INTO Prod_in (ID, ID_Prod, Amount, IN_date, Price) SELECT (SELECT MAX(pin.ID) + 1 FROM Prod_in pin), pr.ID_Prod , " + p.Count + ", \"" + DateTime.Now.ToShortDateString() + "\", " + p.Price + " FROM Products pr WHERE pr.Name_Prod = \"" + p.Name + "\"");
            }
            connector.closeConnection();
        }

        public void deleteProduct(String prodName)
        {
            connector.openConnection();
            connector.executeNonQuery("DELETE FROM Products Where ID_Prod = (SELECT ID_Prod FROM Products WHERE Name_Prod = \"" + prodName + "\")");
            connector.closeConnection();
        }
    }
}
