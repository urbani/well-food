using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using TRPO.Model;
using TRPO.Structures;

namespace TRPO.Model
{
    public class MenuManager
    {
        private DBConnector connector;

        public MenuManager()
        {
            connector = new DBConnector();
        }

        public int addMenu(Menu menu)
        {
            connector.openConnection();
            int changes = 0;
            foreach (String dishName in menu.Menu1)
            {
                changes += connector.executeNonQuery("INSERT INTO Menu (ID_Dish, Date_Menu, Is_Special) SELECT d.ID_Dish, \"" + menu.MenuDate.ToShortDateString() + "\", FALSE FROM Dishes d WHERE Name_Dish = \"" + dishName + "\"");
            }
            foreach (String dishName in menu.Menu2)
            {
                changes += connector.executeNonQuery("INSERT INTO Menu (ID_Dish, Date_Menu, Is_Special) SELECT d.ID_Dish, \"" + menu.MenuDate.ToShortDateString() + "\", FALSE FROM Dishes d WHERE Name_Dish = \"" + dishName + "\"");
            }
            foreach (String dishName in menu.Menu3)
            {
                changes += connector.executeNonQuery("INSERT INTO Menu (ID_Dish, Date_Menu, Is_Special) SELECT d.ID_Dish, \"" + menu.MenuDate.ToShortDateString() + "\", FALSE FROM Dishes d WHERE Name_Dish = \"" + dishName + "\"");
            }
            foreach (String dishName in menu.SpecialMenu)
            {
                changes += connector.executeNonQuery("INSERT INTO Menu (ID_Dish, Date_Menu, Is_Special) SELECT d.ID_Dish, \"" + menu.MenuDate.ToShortDateString() + "\", TRUE FROM Dishes d WHERE Name_Dish = \"" + dishName + "\"");
            }
            connector.closeConnection();
            return changes;
        }

        public String getDishType(String dishName)
        {
            String type = "Первое";
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT Dish_Type FROM Dishes WHERE Name_Dish = \"" + dishName + "\"");
            if (reader.Read())
            {
                type = reader[0].ToString();
            }
            connector.closeConnection();
            return type;
        }
    }
}
