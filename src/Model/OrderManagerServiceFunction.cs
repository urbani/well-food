using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;
using System.Data.OleDb;
using TRPO.GlobalObj;

namespace TRPO.Model
{
   public class OrderManagerServiceFunction
    {
       private DBConnector connector;
       public OrderManagerServiceFunction(DBConnector con)
       {
           connector = con;
       }
        /// <summary>
        /// сообщает № заказа по ID клиента и статусу заказа
        /// </summary>
        /// <param name="emlId"></param>
        /// <returns></returns>
        public int getOpenOrderFromEmloy(int emlId, bool status = true)
        {
            int intFormStatus = status ? 1 : 0;
            connector.openConnection(true);
            OleDbDataReader reader = connector.executeQuery(String.Format("SELECT * FROM orders WHERE id_emp={0} and Status=1", emlId));
            int id_ord = -1;
            while (reader.Read())
            {
                id_ord = Convert.ToInt32(reader[0]);
            }
            connector.closeConnection(true);
            return id_ord + 1;
        }

        public int getOpenOrderFromEmloy2(int emlId, bool status = true)
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
        public float getPriceByDishId(int dishId)
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
        public String getDishName(int dishId)
        {
            String dishName = "";
            connector.openConnection(true);
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT name_dish FROM dishes WHERE id_dish={0}", dishId));
            while (reader.Read())
            {
                dishName = Convert.ToString(reader[0]);
            }
            connector.closeConnection(true);
            return dishName;

        }

        /// <summary>
        /// сообщает id заказа (выполненного или не выполненного)
        /// </summary>
        /// <param name="emplId"></param>
        /// <returns></returns>
        public int getOrderId(int emplId, bool ready = true)
        {
            connector.openConnection(true);
            int id = -1;
            String readySymbol = ready ? "=" : "<>";
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT id_ord FROM Orders AS o INNER JOIN 
                (SELECT do.ID_Dish, do.ID_Order, do.Dish_Count, do.Ready_Count FROM Dishes_Order AS do ) AS do ON o.ID_Ord=do.ID_Order 
                WHERE o.Status=1 AND do.Dish_Count{1}do.Ready_Count AND o.ID_Emp={0}", emplId, readySymbol));
            while (reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
            }
            connector.closeConnection(true);
            return id;
        }


        public String getLinkToPhoto(int idDish)
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

        public List<int> getDishIdesFronClient(int clientId)
        {
            connector.openConnection(true);
            List<int> dishesIds = new List<int>();
            OleDbDataReader reader = connector.executeQuery(String.Format(@"SELECT id_dish FROM Orders AS o INNER JOIN 
                (SELECT do.ID_Dish, do.ID_Order, do.Dish_Count, do.Ready_Count FROM Dishes_Order AS do ) AS do ON o.ID_Ord=do.ID_Order 
                WHERE o.Status=1 AND o.ID_Emp={0}", clientId));
            while (reader.Read())
            {
                dishesIds.Add(Convert.ToInt32(reader[0].ToString()));
            }
            return dishesIds;
        }

    }
}
