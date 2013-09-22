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
        
        public List<CourierListEntry> currentMenu = new List<CourierListEntry>(); //текущее меню в системном виде
        List<orderEnrty> currentOrder = new List<orderEnrty>(); //текущий заказ в системном виде
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
                        currentOrder.Remove(new orderEnrty(dish, price));
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
        /// возвращает список блюд выбранных для заказа, подготовленный для добавление в листВью
        /// </summary>
        /// <returns></returns>
        public ListViewItem[] getOrderMenuForView()
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
            return viewOrder;
        }

        public OrdersConroller(User u)
        {
            user = u;

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
            orderManager.checkoutOrder(view.getEmplId(), currentOrder);
           
            view.clearOrderMenu();

        }
        

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
