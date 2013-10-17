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
                    m.ID_dish, jo.price, jo.Name_dish, jo.Dish_Type, m.Is_Special, jo.Link_To_Photo
                FROM 
                    Menu as m
                INNER JOIN
                        (
                        SELECT 
                                di.ID_Dish,
                                (prices.price*(1 + di.Percent/100)/100) as price,
                                di.Name_Dish, di.Dish_Type, di.Link_To_Photo
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
                tmpDish.linkToPhoto = reader[5].ToString();
                

                resultList.Add(tmpDish);
            }

            connector.closeConnection();
            return resultList;
        }
        /// <summary>
        /// открыть заказ
        /// </summary>
        /// <param name="id_empl">id клиента</param>
        /// <param name="orderList">список блюд в заказе</param>
        public int createOrder(int id_empl, List<orderEnrty> orderList)
        {
            int timesChanges = 0;
            connector.openConnection();
            int idOrd = getOpenOrderFromEmloy2(id_empl);
            if (idOrd == -1)
            {
                connector.executeNonQuery(String.Format("INSERT INTO Orders (ID_Emp, Status) VALUES ({0},  1)", id_empl));
                idOrd = getOpenOrderFromEmloy(id_empl); //!!!!===

            }
            foreach(orderEnrty dish in orderList)
            {
                timesChanges += connector.executeNonQuery(String.Format("INSERT INTO Dishes_Order (Id_dish, id_order, dish_count, ready_count) VALUES ({0},  {1} , {2}, 0)", dish.id, idOrd, dish.Count));

            }
            connector.closeConnection();
            return timesChanges;
        }

        /// <summary>
        /// выдает заказ занесенный в БД по id клиента и "статусу" заказа (вып. || не вып.)
        /// </summary>
        /// <param name="emplId">id клиента</param>
        /// <param name="readyOrder">заказ открыт?</param>
        /// <returns></returns>
        public List<orderEnrty> getPlacedOrder(int emplId, bool readyOrder = true)
        {
            String readyOrderSymbol = readyOrder ? "=" : "<>"; //должны ли все блюда в заказе быть выполнены
            List<orderEnrty> order = new List<orderEnrty>();
            connector.openConnection();
            List<int> dishIdsList = new List<int>();
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT do.ID_Dish, do.Dish_Count FROM Orders AS o INNER JOIN 
                (SELECT do.ID_Dish, do.ID_Order, do.Dish_Count, do.Ready_Count FROM Dishes_Order AS do ) AS do ON o.ID_Ord=do.ID_Order 
                WHERE o.Status=1 AND do.Dish_Count{1}do.Ready_Count AND o.ID_Emp={0}", emplId,readyOrderSymbol));
            int id=0;
            int count=0;
            while(reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
                count = Convert.ToInt32(reader[1]);
                order.Add(new orderEnrty(getDishName(id), getPriceByDishId(id), count, id,getLinkToPhoto(id)));
            }
            


            connector.closeConnection();
            return order;
        }


        /// <summary>
        /// сообщает № заказа по ID клиента и статусу заказа
        /// </summary>
        /// <param name="emlId"></param>
        /// <returns></returns>
        private int getOpenOrderFromEmloy(int emlId, bool status=true)
        {
            int intFormStatus = status?1:0;
            connector.openConnection(true);
            OleDbDataReader reader = connector.executeQuery(String.Format("SELECT MAX(id_ord) FROM Orders"));
            int id_ord = -1;
            while (reader.Read())
            {
                id_ord = Convert.ToInt32(reader[0]);
            }
            connector.closeConnection(true);
            return id_ord+1;
        }

        private int getOpenOrderFromEmloy2(int emlId, bool status = true)
        {
            int intFormStatus = status ? 1 : 0;
            connector.openConnection(true);
            OleDbDataReader reader = connector.executeQuery(String.Format("SELECT MAX(id_order) FROM dishes_Order"));
            int id_ord = -1;
            while (reader.Read())
            {
                id_ord = Convert.ToInt32(reader[0]);
            }
            connector.closeConnection(true);
            return id_ord + 1;
        }

        /// <summary>
        /// сообщает цену блюда по его ID
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        float getPriceByDishId(int dishId)
        {
            connector.openConnection(true);
            float price = 0;
            OleDbDataReader reader = connector.executeQuery(String.Format(@"
                SELECT 
                        (prices.price*(1 + di.Percent/100)/100) as price

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
                WHERE di.Id_dish={0}", dishId));
            while (reader.Read())
            {
                price = Convert.ToInt32(reader[0]);
                break;
            }

            connector.closeConnection(true);
            return price;
        }

        /// <summary>
        /// возвращает название блюда, по его ID
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        String getDishName(int dishId)
        {
            String dishName="";
            connector.openConnection(true);
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT name_dish FROM dishes WHERE id_dish={0}", dishId));
            while (reader.Read())
            {
                dishName = Convert.ToString(reader[0]);
            }
            connector.closeConnection(true);
            return dishName;

        }

        public bool checkoutOrder(int emplId)
        {
            connector.openConnection(true);
            int orderId= getOrderId(emplId, true);
            int result;
            result = connector.executeNonQuery(String.Format(@"UPDATE Orders SET status=2 WHERE id_ord={0}", orderId));
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// сообщает id заказа (выполненного или не выполненного)
        /// </summary>
        /// <param name="emplId"></param>
        /// <returns></returns>
        int getOrderId(int emplId, bool ready=true)
        {
            connector.openConnection(true);
            int id = -1;
            String readySymbol = ready ? "=" : "<>";
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT id_ord FROM Orders AS o INNER JOIN 
                (SELECT do.ID_Dish, do.ID_Order, do.Dish_Count, do.Ready_Count FROM Dishes_Order AS do ) AS do ON o.ID_Ord=do.ID_Order 
                WHERE o.Status=1 AND do.Dish_Count{1}do.Ready_Count AND o.ID_Emp={0}",emplId, readySymbol));
            while (reader.Read())
            {
                id=Convert.ToInt32(reader[0]);
            }
            connector.closeConnection(true);
            return id;
        }


        String getLinkToPhoto(int idDish)
        {
            connector.openConnection(true);
            String link = "";
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT Link_To_Photo FROM Dishes WHERE ID_Dish={0}", idDish));
            while (reader.Read())
            {
                link = reader[0].ToString();
            }
            connector.closeConnection(true);
            return link;
            
        }
    }
}
