using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;
using System.Data.OleDb;

namespace TRPO.Model
{
    public class OrderManager
    {
        DBConnector connector;

        public OrderManager()
        {
            connector = new DBConnector();
        }

        public List<ChiefListEntry> getActiveOrders()
        {
            List<ChiefListEntry> list = new List<ChiefListEntry>();
            ChiefListEntry tmpEntry = new ChiefListEntry();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT d.Name_Dish, SUM(do.Dish_Count) as Need, SUM(do.Ready_Count) as Ready, (Need - Ready) as Left_ FROM Dishes_Order AS do INNER JOIN Dishes AS d ON d.ID_Dish = do.ID_Dish WHERE do.Dish_Count - do.Ready_Count > 0 GROUP BY d.Name_Dish");

            while (reader.Read())
            {
                tmpEntry.name = reader[0].ToString();
                tmpEntry.need = Convert.ToInt32(reader[1]);
                tmpEntry.ready = Convert.ToInt32(reader[2]);
                tmpEntry.left = Convert.ToInt32(reader[3]);
                list.Add(tmpEntry);
            }
            reader.Close();
            connector.closeConnection();
            return list;
        }
        public List<CourierListEntry> getActiveMenu()
        {
            List<CourierListEntry> resultList = new List<CourierListEntry>();
            CourierListEntry tmpDish = new CourierListEntry();
            connector.openConnection();
            //TODO сделать в sql вычесление цены продукта (% * себесстоимость (с учетом, того сколько продуката в блюде)
            OleDbDataReader reader = connector.executeQuery(@"SELECT 
	                                                                di.ID_Dish, di.Name_Dish, prices.Price  
                                                                FROM 
	                                                                Dishes AS di 
                                                                INNER JOIN
	                                                                (
		                                                                SELECT 
			                                                                pd.ID_Dish, 
			                                                                SUM(pd.Product_Count * pr.Price * (di.Percent / 100) ) AS Price 
		                                                                FROM 
			                                                                Products_Dishes pd 
		                                                                INNER JOIN 
			                                                                Products pr 
		                                                                ON 
			                                                                pd.ID_Prod = pr.ID_Prod 
		                                                                GROUP BY 
			                                                                pd.ID_Dish
	                                                                ) AS prices
                                                                ON 
	                                                                di.ID_Dish = prices.ID_Dish");
            while (reader.Read())
            {
                tmpDish.id = Convert.ToInt32(reader[0]);
                tmpDish.dish = reader[1].ToString(); 
                //себес * %
                tmpDish.price = Convert.ToSingle(reader[2]);// *(Convert.ToSingle(reader[3]) / 100);


                resultList.Add(tmpDish);
            }

            connector.closeConnection();
            return resultList;
        }
    }
}
