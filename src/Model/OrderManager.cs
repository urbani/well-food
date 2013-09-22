using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;
using System.Data.OleDb;
using TRPO.GlobalObj;

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
            OleDbDataReader reader = connector.executeQuery(@"
                SELECT 
                    m.ID_dish, jo.price, jo.Name_dish, jo.Dish_Type, m.Is_Special
                FROM 
                    Menu as m
                INNER JOIN
                        (
                        SELECT 
                                di.ID_Dish,
                                (prices.price*(1 + di.Percent/100)/100) as price,
                                di.Name_Dish, di.Dish_Type
                        FROM 
                            Dishes AS di 
                        INNER JOIN
                            (
                                SELECT 
                                    pd.ID_Dish, 
                                    SUM(pd.Product_Count * pr.Price) AS Price 
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
                            di.ID_Dish = prices.ID_Dish
                        ) as jo
                        ON m.ID_Dish=jo.ID_Dish
                WHERE 
	            m.Date_Menu like('" + TRPOGlobal.getCurrentTime() + "');");
            while (reader.Read())
            {
                tmpDish.id = Convert.ToInt32(reader[0]);
                tmpDish.price = Convert.ToSingle(reader[1]);
                tmpDish.dish = reader[2].ToString();
                tmpDish.type = reader[3].ToString();
                tmpDish.isSpecial = Convert.ToBoolean(reader[4]);
                


                resultList.Add(tmpDish);
            }

            connector.closeConnection();
            return resultList;
        }
        /// <summary>
        /// провести заказ
        /// </summary>
        /// <param name="id_empl"></param>
        /// <param name="orderList"></param>
        public int checkoutOrder(int id_empl, List<orderEnrty> orderList)
        {
            int timesChanges = 0;
            connector.openConnection();
            timesChanges += connector.executeNonQuery(String.Format("INSERT INTO Orders (ID_Emp, Status) VALUES ({0},  1)", id_empl));
            OleDbDataReader reader = connector.executeQuery(String.Format("SELECT MAX(o.id_ord) AS curOrdId FROM Orders AS o"));
            int idOrd=0;   
            int ptr =0;
            while (reader.Read())
            {
                idOrd = Convert.ToInt32(reader[0]);
                if (ptr>1)
                    throw new SystemException();
                ptr ++;
            }
            foreach(orderEnrty dish in orderList)
            {
                timesChanges += connector.executeNonQuery(String.Format("INSERT INTO Dishes_Order (Id_dish, id_order, dish_count, ready_count) VALUES ({0},  {1} , {2}, 0", dish.id, idOrd, dish.Count));

            }

            connector.closeConnection();
            return timesChanges;
        }
    }
}
