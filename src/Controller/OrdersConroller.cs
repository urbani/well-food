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
        User user; //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        public int clientId { get; set; } //id-клиента с которым мы рабдотали в предывдущий раз
        public int typeIndex = 0;
        public int dishindex = 0;
        

        Dictionary<int, List<CourierListEntry>> currentMenuList = new Dictionary<int, List<CourierListEntry>>();
        public List<CourierListEntry> currentMenu
        {
            get { return currentMenuList[typeIndex]; }
            set { value = currentMenuList[typeIndex]; }
        }

        Dictionary<int, List<orderEnrty>> currentOrderList = new Dictionary<int, List<orderEnrty>>();
        //List<orderEnrty> currentOrder = new List<orderEnrty>(); //текущий заказ в системном виде
        //геттеры и сеттеры творят чудеса!!!
        List<orderEnrty> currentOrder 
        {
            get 
            {
                if (!currentOrderList.ContainsKey(clientId))
                    currentOrderList.Add(clientId, new List<orderEnrty>());
                 return currentOrderList[clientId];
                
            } 
            set { value = currentOrderList[clientId]; } 
        }

        List<orderEnrty> placedOrderList = new List<orderEnrty>(); //выполненный заказ текущего клиента
        

        OrderManager orderManager = new OrderManager(); //модель курьера
        
        /// <summary>
        /// добавление блюда во внутренний список заказа
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="price"></param>
        public void addDishToOrder(String dish, float price)
        {

            int index = view.getIndexSelectedDish();
            //currentMenu[index].
            foreach (int i in Enumerable.Range(0, currentOrder.Count))
            {
                if (currentOrder[i].id == currentMenu[dishindex].id)
                {
                    orderEnrty temp = new orderEnrty(currentOrder[i]);
                    temp.inreament();
                    currentOrder[i] = temp;
                    return;
                }
            }
            currentOrder.Add(currentMenu[dishindex].ToOrderEntry());

            //List<CourierListEntry> t = currentMenuList[view.getSelectedDishType()];
            //List<orderEnrty> t2 = currentOrderList[1];
            //foreach (CourierListEntry entry in currentMenuList[view.getSelectedDishType()])
            //{
            //    if(entry.id==view.
            //}


            
            //foreach (int i in Enumerable.Range(0,currentOrder.Count))
            //{
            //    if (currentOrder[i].id == view.)
            //    {
            //        orderEnrty temp = new orderEnrty(currentOrder[i]);
            //        temp.inreament();
            //        currentOrder[i] = temp;
            //        return;
            //    }
            //}
            
            //currentOrder.Add(new orderEnrty(dish, price, 1, findIdDish(dish)));
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
            
            placedOrderList = orderManager.getPlacedOrder(clientId, true);
            ListViewItem[] viewOrder = new ListViewItem[placedOrderList.Count];

            int ptr = 0;
            float totalPrice = 0;
            ListViewItem temp = new ListViewItem();
            foreach (orderEnrty entry in placedOrderList)
            {
                temp = new ListViewItem(entry.Dish);
                temp.SubItems.Add(entry.Cost.ToString());
                temp.SubItems.Add(entry.Count.ToString());
                temp.SubItems.Add(entry.Price.ToString());
                viewOrder[ptr] = temp;
                ptr++;
                totalPrice += entry.Cost;
            }

            view.updatePlacedOrderMenu(viewOrder);
            view.updatePlaceOrderTotalPrice(totalPrice);
            String status = (placedOrderList.Count > 0) ? "Открыт" : "Нет заказа";
            view.updatePlecedStatusOrder(status);
        }


        /// <summary>
        /// возвращает список блюд выбранных для заказа, подготовленный для добавление в листВью
        /// </summary>
        /// <returns></returns>
        public void updateOrderMenu()
        {
            view.updateOrderMenu(Convertor.orderListToViewArr(currentOrder));
        }

        public OrdersConroller(User u)
        {
            user = u;
            clientId = -1;
            //создаем индекс во внутреннем представлении меню



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

        public void createOrder()
        {
            try
            {
                orderManager.createOrder(view.getEmplId(), currentOrder);
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
            currentMenuList = Convertor.dishListToMenuList(orderManager.getActiveMenu());
            view.updateMenuList(Convertor.menuDictToViewArr(currentMenuList));

        }

        /// <summary>
        /// возвращает сумму заказа
        /// </summary>
        /// <returns></returns>
        public float getTotalPrice()
        {
            float total = 0;
            foreach (orderEnrty entry in currentOrder)
            {
                total += entry.Cost;
            }
            return total;
        }

        public void checkoutOrder()
        {
            if (placedOrderList.Count == 0)
                return;
            bool result = orderManager.checkoutOrder(clientId);
            if (result)
            {
                updatePlacedOrderMenu();
                view.updatePlecedStatusOrder("Выполнен");
            }
            else
                view.showMsg("Не удалось провести заказ", ErrorLevels.Info);



        }

    }
}
