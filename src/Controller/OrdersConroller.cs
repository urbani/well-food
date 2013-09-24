using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.Structures;
using System.Windows.Forms;
using TRPO.GlobalObj;

namespace TRPO.Controller
{


    public class OrdersConroller
    {
        IOrderManagable view;
        //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        User user;
        public int clientId { get; set; } //id-клиента с которым мы рабдотали в предывдущий раз
        
        public List<CourierListEntry> currentMenu = new List<CourierListEntry>(); //текущее меню в системном виде
        Dictionary<int, List<orderEnrty>> currentOrderList = new Dictionary<int, List<orderEnrty>>();
        //List<orderEnrty> currentOrder = new List<orderEnrty>(); //текущий заказ в системном виде
        List<orderEnrty> currentOrder 
        {
            get 
            {
                if (currentOrderList.ContainsKey(clientId))
                    return currentOrderList[clientId];
                else
                {
                    currentOrderList.Add(clientId, new List<orderEnrty>());
                    return currentOrderList[clientId];
                }
            } 
            set { value = currentOrderList[clientId]; } 
        }
        OrderManager orderManager = new OrderManager(); //модель курьера
        
        /// <summary>
        /// добавление блюда во внутренний список заказа
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="price"></param>
        public void addDishToOrder(String dish, float price)
        {
            foreach (int i in Enumerable.Range(0,currentOrder.Count))
            {
                if (currentOrder[i].Dish == dish)
                {
                    orderEnrty temp = new orderEnrty(currentOrder[i]);
                    temp.inreament();
                    currentOrder[i] = temp;
                    return;
                }
            }
            
            currentOrder.Add(new orderEnrty(dish, price,findIdDish(dish)));
        }

        /// <summary>
        /// удаление блюда из внутреннего списка меню
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="price"></param>
        public void removeDishFromOrder(String dish, float price)
        {
            foreach (int i in Enumerable.Range(0, currentOrder.Count))
            {
                if (currentOrder[i].Dish == dish)
                {
                    orderEnrty temp = new orderEnrty(currentOrder[i]);
                    if (temp.Count == 1)
                        currentOrder.Remove(temp);
                    else
                    {
                        temp.decreament();
                        currentOrder[i] = temp;
                    }
                    return;
                }
            }
            
        }

        /// <summary>
        /// обработчик выдачи заказа
        /// </summary>
        public void updatePlacedOrderMenu()
        {
            clientId = view.getEmplId();
            List<orderEnrty> placedOrderList = new List<orderEnrty>();
            placedOrderList = orderManager.getPlacedOrder(clientId, true);
            ListViewItem[] viewOrder = new ListViewItem[placedOrderList.Count];

            int ptr = 0;
            ListViewItem temp = new ListViewItem();
            foreach (orderEnrty entry in placedOrderList)
            {
                temp = new ListViewItem(entry.Dish);
                temp.SubItems.Add(entry.Cost.ToString());
                temp.SubItems.Add(entry.Count.ToString());
                viewOrder[ptr] = temp;
                ptr++;
            }

            view.updatePlacedOrderMenu(viewOrder);
        }


        /// <summary>
        /// возвращает список блюд выбранных для заказа, подготовленный для добавление в листВью
        /// </summary>
        /// <returns></returns>
        public void updateOrderMenu()
        {
            ListViewItem[] viewOrder = new ListViewItem[currentOrder.Count];
            int ptr = 0;
            ListViewItem temp = new ListViewItem();
            foreach (orderEnrty entry in currentOrder)
            {
                temp = new ListViewItem(entry.Dish);
                temp.SubItems.Add(entry.Cost.ToString());
                temp.SubItems.Add(entry.Count.ToString());
                viewOrder[ptr] = temp ;
                ptr++;
            }

            view.updateOrderMenu(viewOrder);
        }

        public OrdersConroller(User u)
        {
            user = u;
            clientId = -1;
         //TODO:
         //в загловок имя, роль
        }

        public void addForm(IOrderManagable c)
        {
            view = c;

            view.setWindowTitile(TRPOGlobal.makeTitle(user));
            
        }
        /// <summary>
        /// находит номер блюда в коллекции по его названию
        /// </summary>
        /// <param name="dishEnty"></param>
        /// <returns></returns>
        int findIdDish(String dish)
        {
            
            //ищем  номер текущего блюда в списке всех блюд на сегодня
            foreach (CourierListEntry entry in currentMenu)
            {
                if (entry.dish == dish)
                {
                    return entry.id;
                }
            }
            throw new ArgumentException();
        }

        public void checkoutOrder()
        {
            try
            {
                orderManager.checkoutOrder(view.getEmplId(), currentOrder);
                view.clearOrderMenu();
            }
            catch (ApplicationException ex)
            { 
                view.showMsg(ex.ToString(), ErrorLevels.Critical); 
            }

          
           
            

        }
        
        /// <summary>
        /// обновлет во view теущиий список блюд в меню
        /// </summary>
        public void updateActiveMenu()
        {
            currentMenu = orderManager.getActiveMenu();
            view.updateMenuList(currentMenu);

        }

        public float getTotalPrice()
        {
            float total = 0;
            foreach (orderEnrty entry in currentOrder)
            {
                total += entry.Cost;
            }
            return total;
        }

    }
}
