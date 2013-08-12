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
            connector.closeConnection();
            return result;
        }
    }
}
