﻿using System;
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
        OrderManagerServiceFunction serviceFunction;
        public OrderManager()
        {
            connector = new DBConnector();
            serviceFunction = new OrderManagerServiceFunction(connector);
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
        /// сообщает выполняется ли уже заказ для данного клиента 
        /// </summary>
        /// <param name="id_empl"></param>
        /// <returns></returns>
        public bool orderRunning(int id_empl)
        {
            int idOrd = serviceFunction.getOrderId(id_empl);
            bool orderRunning = idOrd == serviceFunction.getOrderId(id_empl, false) ? false : true;
            return orderRunning;
        }

        /// <summary>
        /// открыть заказ
        /// </summary>
        /// <param name="clientId">id клиента</param>
        /// <param name="orderList">список блюд в заказе</param>
        public int createOrder(int clientId, List<orderEnrty> orderList)
        {
            int timesChanges = 0;
            connector.openConnection();
            int idOrd = serviceFunction.getOrderId(clientId);
            
            if (idOrd == -1)
            {
                connector.executeNonQuery(String.Format("INSERT INTO Orders (ID_Emp, Status) VALUES ({0},  1)", clientId));
                idOrd = serviceFunction.getOpenOrderFromEmloy(clientId);
                if (idOrd == -1)
                {
                    throw new ApplicationException(String.Format("id order все еще -1. id empl={0}", clientId));
                }

            }
            List<int> updateList = getDishIdesFronClient
            
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
                order.Add(new orderEnrty(serviceFunction.getDishName(id), serviceFunction.getPriceByDishId(id), count, id,serviceFunction.getLinkToPhoto(id)));
            }
            


            connector.closeConnection();
            return order;
        }




  

        public bool checkoutOrder(int emplId)
        {
            connector.openConnection(true);
            int orderId=serviceFunction.getOrderId(emplId, true);
            int result;
            result = connector.executeNonQuery(String.Format(@"UPDATE Orders SET status=2 WHERE id_ord={0}", orderId));
            if (result > 0)
                return true;
            else
                return false;
        }

     
    }
}
