using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.GlobalObj;
using System.Data.OleDb;
using TRPO.Structures;

namespace TRPO.Model
{
    public class DishesManager
    {
        DBConnector connector;

        public DishesManager()
        {
            connector = new DBConnector();
        }

        public Dish getDish(int id)
        {
            Dish result = new Dish();
            connector.openConnection();

            OleDbDataReader reader = connector.executeQuery("SELECT d.ID_Dish, d.Name_Dish, d.Link_To_Photo, d.Dish_Type, d.Percent, d.Recipe FROM Dishes as d WHERE d.ID_Dish = " + id);
            if (reader.Read())
            {
                result.IdInDB = Convert.ToInt32(reader[0]);
                result.Name = reader[1].ToString();
                result.LinkToPhoto = reader[2].ToString();
                result.DishType = reader[3].ToString();
                result.Percent = Convert.ToInt32(reader[4]);
                result.Recipe = reader[5].ToString();
            }

            reader = connector.executeQuery("SELECT pr.Name_Prod, pd.Product_Count FROM Products_Dishes pd INNER JOIN Products pr ON pd.ID_Prod = pr.ID_Prod WHERE ID_Dish = " + id);
            while (reader.Read())
            {
                result.Consistance.Add(reader[0].ToString(), Convert.ToDouble(reader[1]));
            }

            reader.Close();

            connector.closeConnection();
            return result;
        }

        public Dish getDish(String name)
        {
            Dish result = new Dish();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT d.ID_Dish, d.Name_Dish, d.Link_To_Photo, d.Dish_Type, d.Percent, d.Recipe FROM Dishes as d WHERE d.Name_Dish = \"" + name + "\"");
            if (reader.Read())
            {
                result.IdInDB = Convert.ToInt32(reader[0]);
                result.Name = reader[1].ToString();
                result.LinkToPhoto = reader[2].ToString();
                result.DishType = reader[3].ToString();
                result.Percent = Convert.ToInt32(reader[4]);
                result.Recipe = reader[5].ToString();
            }

            reader = connector.executeQuery("SELECT pr.Name_Prod, pd.Product_Count FROM Products_Dishes pd INNER JOIN Products pr ON pd.ID_Prod = pr.ID_Prod WHERE ID_Dish = (SELECT dis.ID_Dish FROM Dishes dis WHERE dis.Name_Dish = \"" + name + "\")");
            while (reader.Read())
            {
                result.Consistance.Add(reader[0].ToString(), Convert.ToDouble(reader[1]));
            }

            reader.Close();

            connector.closeConnection();
            return result;
        }

        
        public int addReadyDishes(String dishName, int amount)
        {
            if ((dishName == "") || (amount <= 0)) 
            {
                return 0;
            }

            connector.openConnection();

            OleDbDataReader reader = null;
            int currentOrder = -1;
            int currentDish = -1;
            int dishesToCookInOrder = -1;
            int dishesReadyInOrder = -1;
            int dishesToCookInOrderLeft = -1;
            int rowsChanged = 0;
            while (amount > 0)
            {
                reader = connector.executeQuery("SELECT TOP 1 d.ID_Dish, do.ID_Order, do.Dish_Count, do.Ready_Count FROM Dishes d INNER JOIN Dishes_Order do ON d.ID_Dish = do.ID_Dish WHERE do.Dish_Count > do.Ready_Count AND d.Name_Dish = \"" + dishName + "\"");
                if (reader.Read())
                {
                    currentDish = Convert.ToInt32(reader[0]);
                    currentOrder = Convert.ToInt32(reader[1]);
                    dishesToCookInOrder = Convert.ToInt32(reader[2]);
                    dishesReadyInOrder = Convert.ToInt32(reader[3]);
                    dishesToCookInOrderLeft = dishesToCookInOrder - dishesReadyInOrder;
                }
                else
                {
                    return amount;
                }
                reader.Close(); 

                if (amount >= dishesToCookInOrderLeft)
                {
                    rowsChanged += connector.executeNonQuery("UPDATE Dishes_Order AS do SET do.Ready_Count = " + dishesToCookInOrder + " WHERE do.ID_Order = " + currentOrder + "AND do.ID_Dish = " + currentDish);
                    amount -= dishesToCookInOrderLeft;
                }
                else
                {
                    rowsChanged += connector.executeNonQuery("UPDATE Dishes_Order AS do SET do.Ready_Count = " + (amount + dishesReadyInOrder) + " WHERE do.ID_Order = " + currentOrder + "AND do.ID_Dish = " + currentDish);
                    amount = 0;
                }
                
            }


            reader = connector.executeQuery("SELECT pd.ID_Prod, SUM(pd.Product_Count) AS needProd FROM Products_Dishes pd INNER JOIN Dishes di ON di.ID_Dish = pd.ID_Dish WHERE di.Name_Dish = \"" + dishName + "\" GROUP BY pd.ID_Prod");

            Dictionary<int, Double> prodUsed = new Dictionary<int, Double>();
            while (reader.Read())
            {
                prodUsed.Add(Convert.ToInt32(reader[0].ToString()), (Convert.ToDouble(reader[1]) * amount));
            }

            foreach (KeyValuePair<int, Double> p in prodUsed)
            {
                connector.executeNonQuery("INSERT INTO Prod_out (ID, ID_Prod, Amount, Out_Date) SELECT MAX(pout.ID) + 1 , " + p.Key + " , " + p.Value + ", \"" + DateTime.Now.ToShortDateString() + "\" FROM Prod_out pout");
            }
            //TODO:Prod_out update  
            
            connector.closeConnection();

            return amount;
        }

        public Dictionary<String, String> getDishNamesWithTypes()
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT d.Name_Dish, d.Dish_Type FROM Dishes as d");
            while (reader.Read())
            {
                result.Add(reader[0].ToString(), reader[1].ToString());
            }

            reader.Close();
            connector.closeConnection();
            return result;

        }

        public Dictionary<string, double> getDishContents(String name)
        {
            Dictionary<String, double> result = new Dictionary<String, double>();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT p.Name_Prod, j.Product_Count FROM Products p INNER JOIN  (SELECT pd.ID_Prod, pd.Product_Count FROM Products_Dishes AS pd WHERE pd.ID_Dish = (SELECT d.ID_Dish FROM Dishes d WHERE d.Name_Dish = \"" + name + "\")) AS j ON p.ID_Prod = j.ID_Prod");
            while (reader.Read())
            {
                result.Add(reader[0].ToString(), Convert.ToDouble(reader[1]));
            }

            reader.Close();

            connector.closeConnection();
            return result;
        }

        public void deleteProductFromDish(String dishName, String prodName)
        {
            connector.openConnection();
            connector.executeNonQuery("DELETE FROM Products_Dishes pd WHERE pd.ID_Dish = (SELECT d.ID_Dish FROM Dishes d WHERE d.Name_Dish=\"" + dishName + "\") AND pd.ID_Prod = (SELECT p.ID_Prod FROM Products p WHERE p.Name_Prod = \"" + prodName + "\")");
            connector.closeConnection();
        }

        public void createNewDish(Dish d)
        {
            if (d.Name != "")
            {
                connector.openConnection();
                OleDbDataReader reader = connector.executeQuery("SELECT MAX(ID_Dish) FROM Dishes");
                int lastI = 0;
                if (reader.Read())
                {
                    lastI = Convert.ToInt32(reader[0]) + 1;
                }
                connector.executeNonQuery("INSERT INTO Dishes VALUES (" + lastI + ", \"" + d.Name + "\", \"" + d.LinkToPhoto + "\", \"" + d.DishType + "\", " + 30 + ", \"" + d.Recipe + "\")");

                foreach (KeyValuePair<String, Double> p in d.Consistance)
                {
                    connector.executeNonQuery("INSERT INTO Products_Dishes (ID_Prod, ID_Dish, Product_Count) SELECT p.ID_Prod, " + lastI + ", " + p.Value.ToString().Replace(",", ".") + " FROM Products p WHERE p.Name_Prod = \"" + p.Key + "\"");
                }
                connector.closeConnection();
            }
        }

        public void updateDish(Dish d)
        {
            connector.openConnection();
            connector.executeNonQuery("UPDATE Dishes AS d SET d.Link_To_Photo = \"" + d.LinkToPhoto + "\", d.Dish_Type = \"" + d.DishType + "\", d.Recipe = \"" + d.Recipe + "\" WHERE d.Name_Dish = \"" + d.Name + "\"");

            connector.executeNonQuery("DELETE FROM Products_Dishes Where ID_Dish = (SELECT d.ID_Dish FROM Dishes d WHERE Name_Dish = \"" + d.Name + "\")");
            foreach (KeyValuePair<String, Double> p in d.Consistance)
            {
                connector.executeNonQuery("INSERT INTO Products_Dishes (ID_Prod, ID_Dish, Product_Count) SELECT p.ID_Prod, (SELECT d.ID_Dish FROM Dishes d WHERE d.Name_Dish = \"" + d.Name + "\"),  " + p.Value.ToString().Replace(",", ".") + " FROM Products p WHERE p.Name_Prod = \"" + p.Key + "\"");
            }
            connector.closeConnection();
        }
    }
}
