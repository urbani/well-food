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

        OrderManager orderManager = new OrderManager(); //модель курьера

        

        public OrdersConroller(User u)
        {
            user = u;
         //TODO:
         //в загловок имя, роль
        }

        public void addForm(IOrderManagable c)
        {
            view = c;
            
        }
        /// <summary>
        /// находит номер блюда в коллекции по его названию
        /// </summary>
        /// <param name="dishEnty"></param>
        /// <returns></returns>
        CourierListEntry findDishInMenu(ListViewItem dishEnty)
        {
            
            //ищем  номер текущего блюда в списке всех блюд на сегодня
            foreach (CourierListEntry entry in currentMenu)
            {
                if (entry.dish == dishEnty.Text)
                {
                    return entry;
                }
            }
            throw new ArgumentException();
            return new CourierListEntry();
        }

        public void checkoutOrder()
        {
            List<CourierListEntry> order = new List<CourierListEntry>();
            foreach (ListViewItem entry in view.getOrderMenu())
                order.Add(findDishInMenu(entry));
            //TODO make checkout
            view.clearOrderMenu();

        }
        

        public void updateActiveMenu()
        {
            currentMenu = orderManager.getActiveMenu();
            view.updateMenuList(currentMenu);

        }


    }
}
