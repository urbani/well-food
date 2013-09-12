using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.Structures;

namespace TRPO.Controller
{
    public class OrdersConroller
    {
        IOrderManagable view;
        //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        User user; 
        //конструкто, принимаем что
        OrderManager orderManager = new OrderManager();
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

        public void updateActiveMenu()
        {
            List<CourierListEntry> menuList =  orderManager.getActiveMenu();
            String[] rawSting;
            List<List<String[]>> fillMenuList = new List<List<string[]>>();
            fillMenuList.Add(new List<string[]>());
            fillMenuList.Add(new List<string[]>());
            fillMenuList.Add(new List<string[]>());
            fillMenuList.Add(new List<string[]>());
            foreach (CourierListEntry entry in menuList)
            {
                rawSting = new String[] { entry.dish, entry.price.ToString() };

                if (entry.isSpecial)
                {
                    fillMenuList[3].Add(rawSting);
                }
                else
                {


                    switch (entry.type)
                    {
                        case ("Первое"):
                            fillMenuList[0].Add(rawSting);
                            break;
                        case ("Второе"):
                            fillMenuList[1].Add(rawSting);
                            break;
                        case ("Третье"):
                            fillMenuList[2].Add(rawSting);
                            break;
                    }
                }
            }
            view.updateMenuList(menuList1, menuList2, menuList3, menuList4);
           

        }


    }
}
