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
        enum dishTriger { insert, update, delete };
        //
        public List<ChiefListEntry> getActiveOrders()
        {
            List<ChiefListEntry> list = new List<ChiefListEntry>();
            ChiefListEntry tmpEntry = new ChiefListEntry();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery(
            @"SELECT 
                sel.Name_Dish, 
                sel.Need, 
                sel.Ready, 
                sel.Left_,
                sel2.StockLeft
            FROM
            (
                SELECT 
                    d.Name_Dish, 
                    SUM(do.Dish_Count) as Need, 
                    SUM(do.Ready_Count) as Ready, 
                    (Need - Ready) as Left_ 
                FROM 
                    Dishes_Order AS do 
                INNER JOIN 
                    Dishes AS d ON d.ID_Dish = do.ID_Dish 
                WHERE 
                    do.Dish_Count - do.Ready_Count > 0 
                GROUP BY 
                    d.Name_Dish
            ) as sel 
            LEFT JOIN
            (
                SELECT
                    lcd.Name_Dish, SUM(lcd.Amount - lcd.Out_Amount) as StockLeft
                FROM
                    Left_Cooked_Dishes as lcd
                GROUP BY lcd.Name_Dish
            ) as sel2
            ON sel.Name_Dish = sel2.Name_Dish"
            );

            while (reader.Read())
            {
                tmpEntry.name = reader[0].ToString();
                tmpEntry.need = Convert.ToInt32(reader[1]);
                tmpEntry.ready = Convert.ToInt32(reader[2]);
                tmpEntry.left = Convert.ToInt32(reader[3]);

                tmpEntry.inStock = !DBNull.Value.Equals(reader[4]) ? Convert.ToInt32(reader[4]) : 0;
                list.Add(tmpEntry);
            }
            reader.Close();
            connector.closeConnection();
            return list;
        }

        /// <summary>
        /// возвращает меню на сегодня
        /// </summary>
        /// <returns></returns>
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
        /// открыть/провести/изменить заказ
        /// </summary>
        /// <param name="clientId">id клиента</param>
        /// <param name="crudeOrderList">список блюд в заказе</param>
        public int createOrder(int clientId, List<OrderEntry> crudeOrderList)
        {
            int timesChanges = 0; //индекс для самопроверки
            connector.openConnection(); 
            int idOrd = serviceFunction.getOrderId(clientId); //получить ид заказа
            //List<List<orderEnrty>> intellyOrderList = new List<List<orderEnrty>>(); //заказ, в котором блюда отсортированы по типу 

            //если заказ еще не создан, то создать
            if (idOrd == -1)
            {
                connector.executeNonQuery(String.Format("INSERT INTO Orders (ID_Emp, Status) VALUES ({0},  1)", clientId));
                idOrd = serviceFunction.getOpenOrderFromEmloy(clientId);
                if (idOrd == -1)
                {
                    throw new ApplicationException(String.Format("id order все еще -1. id empl={0}", clientId));
                }

            }

            //List<int> oldOrderListIds = serviceFunction.getDishIdesFronClient(clientId); //получаем блюда на апдейт
            List<OrderEntry> oldOrderList = new List<OrderEntry>();
            oldOrderList = getPlacedOrderFromIdOrder(idOrd);
            List<OrderEntry> updateListDishes = new List<OrderEntry>();
            List<OrderEntry> insertList = new List<OrderEntry>();
            List<OrderEntry> unchangeList = new List<OrderEntry>();
            List<int> removeList = new List<int>();

            //заполняем updateList && unchangeList
            foreach (OrderEntry entry in crudeOrderList)
            {
                foreach( OrderEntry oldEntry in oldOrderList)
                {
                    if (Equals(entry, oldEntry))
                    {
                        unchangeList.Add(entry);
                        break;
                    }

                    if (entry.id==oldEntry.id)
                    {
                        updateListDishes.Add(entry);
                        break;

                    }
                }
                
            }
            
            //заполняем removeList
            bool found = false;
            foreach (OrderEntry oldEntry in oldOrderList)
            {
                found = false;
                foreach (OrderEntry entry in crudeOrderList)
                {
                    if (oldEntry.id == entry.id)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    removeList.Add(oldEntry.id);
            }

            //заполняем insertList
            foreach (OrderEntry rawEntry in crudeOrderList)
            {
                found = false;
                foreach (OrderEntry entry in updateListDishes)
                {
                    if (Equals(entry,rawEntry))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    foreach (int id in removeList)
                    {
                        if (rawEntry.id == id)
                        {
                            found = true;
                            break;
                        }
                    }
                if (!found)
                    foreach (OrderEntry entry in unchangeList)
                    {
                        if (Equals(entry, rawEntry))
                        {
                            found = true;
                            break;
                        }
                    }
                if (!found)
                    insertList.Add(rawEntry);
            }

            foreach (OrderEntry entry in updateListDishes)
            {
                connector.executeNonQuery(String.Format("UPDATE Dishes_Order SET DISH_count={0} WHERE ID_order={1} AND id_dish={2}", entry.Count, idOrd, entry.id));
            }


            //делаем инсерт
            foreach(OrderEntry dish in insertList)
            {
                timesChanges += connector.executeNonQuery(String.Format("INSERT INTO Dishes_Order (Id_dish, id_order, dish_count, ready_count) VALUES ({0},  {1} , {2}, 0)", dish.id, idOrd, dish.Count));

            }
            foreach (int id in removeList)
            {
                connector.executeNonQuery(String.Format("DELETE FROM Dishes_Order WHERE ID_dish={0} AND id_order={1}", id, idOrd));
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
        public List<OrderEntry> getPlacedOrder(int emplId, bool readyOrder = true)
        {
            String readyOrderSymbol = readyOrder ? "=" : "<>"; //должны ли все блюда в заказе быть выполнены
            List<OrderEntry> order = new List<OrderEntry>();
            connector.openConnection(true);
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
                order.Add(new OrderEntry(serviceFunction.getDishName(id), serviceFunction.getPriceByDishId(id), count, id,serviceFunction.getLinkToPhoto(id)));
            }
            connector.closeConnection(true);
            return order;
        }

        public List<OrderEntry> getPlacedOrderFromIdOrder(int idOrd)
        {
            List<OrderEntry> order = new List<OrderEntry>();
            connector.openConnection(true);
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT id_dish, dish_count from dishes_order WHERE id_order={0} and ready_count<>0", idOrd));
            int id = 0;
            int count = 0;
            while (reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
                count = Convert.ToInt32(reader[1]);
                order.Add(new OrderEntry(serviceFunction.getDishName(id), serviceFunction.getPriceByDishId(id), count, id, serviceFunction.getLinkToPhoto(id)));
            }
            connector.closeConnection(true);
            return order;

        }



        public void closeOrder(int clientId)
        {
            connector.openConnection(true);
            int orderId = serviceFunction.getOrderId(clientId, true);
            int result;
            result = connector.executeNonQuery(String.Format(@"UPDATE orders SET Status=2 WHERE id_emp={0}", clientId));

            connector.closeConnection(true);

        }



        public bool checkoutOrder(int emplId)
        {
            return true;
            connector.openConnection(true);
            int orderId = serviceFunction.getOrderId(emplId, true);
            int result;
            result = connector.executeNonQuery(String.Format(@"UPDATE Orders SET status=2 WHERE id_ord={0}", orderId));
            connector.closeConnection(true);
            if (result > 0)
                return true;
            else
                return false;
            
        }
     
    }
}
