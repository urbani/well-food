using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.GlobalObj;
using System.Data.OleDb;

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
            int rowsChanged = -1;
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
                reader.Close(); 

                if (amount >= dishesToCookInOrderLeft)
                {
                    rowsChanged = connector.executeNonQuery("UPDATE Dishes_Order AS do SET do.Ready_Count = " + dishesToCookInOrder + " WHERE do.ID_Order = " + currentOrder + "AND do.ID_Dish = " + currentDish);
                    amount -= dishesToCookInOrderLeft;
                }
                else
                {
                    rowsChanged = connector.executeNonQuery("UPDATE Dishes_Order AS do SET do.Ready_Count = " + (amount + dishesReadyInOrder) + " WHERE do.ID_Order = " + currentOrder + "AND do.ID_Dish = " + currentDish);
                    amount = 0;
                }

            }
                     
            connector.closeConnection();
            
            return rowsChanged;
        }
    }
}
