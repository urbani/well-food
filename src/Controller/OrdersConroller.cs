using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.Structures;
using System.Windows.Forms;
using TRPO.GlobalObj;
using System.IO;

namespace TRPO.Controller
{


    public class OrdersConroller
    {
        IOrderManagable view;
        User user; //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        public int clientId { get; set; } //id-клиента с которым мы рабдотали в предывдущий раз
        int TypeIndex;
        public int typeIndex{ get { return TypeIndex; } set { TypeIndex = value + 1; } }

        public int dishindex=0;
        public int orderIndex=0;
        public String orderDiahNameCrutch; //:)
        

        Dictionary<int, List<CourierListEntry>> currentMenuList = new Dictionary<int, List<CourierListEntry>>();
        public List<CourierListEntry> currentMenu
        {
            get 
            {
                if (!currentMenuList.ContainsKey(typeIndex))
                    currentMenuList.Add(typeIndex, new List<CourierListEntry>());
                return currentMenuList[typeIndex]; 
            }
            set { value = currentMenuList[typeIndex]; }
        }

        Dictionary<int, List<orderEntry>> currentOrderList = new Dictionary<int, List<orderEntry>>();
        //List<orderEnrty> currentOrder = new List<orderEnrty>(); //текущий заказ в системном виде
        //геттеры и сеттеры творят чудеса!!!
        List<orderEntry> currentOrder 
        {
            get 
            {
                if (!currentOrderList.ContainsKey(clientId))
                    currentOrderList.Add(clientId, new List<orderEntry>());
                 return currentOrderList[clientId];
                
            } 
            set { value = currentOrderList[clientId]; } 
        }

        List<orderEntry> placedOrderList = new List<orderEntry>(); //выполненный заказ текущего клиента
        

        OrderManager orderManager = new OrderManager(); //модель курьера
        
        /// <summary>
        /// добавление блюда во внутренний список заказа
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="price"></param>
        public void addDishToOrder()
        {

            int index = view.getIndexSelectedDish();
            bool nothing = true;
            foreach (int i in Enumerable.Range(0, currentOrder.Count))
            {
                if (currentOrder[i].id == currentMenu[dishindex].id)
                {
                    orderEntry temp = new orderEntry(currentOrder[i]);
                    temp.inreament();
                    currentOrder[i] = temp;
                    nothing = false; ;
                }
            }
            if (nothing)
                currentOrder.Add(currentMenu[dishindex].ToOrderEntry());
            updateDishPhoto();
            //void setDishPhoto(String path)

        }

        int findDishOrder(String dishName)
        {
            int index = 0;
            foreach (int i in Enumerable.Range(0, currentOrder.Count))
                if (currentOrder[i].Dish == orderDiahNameCrutch)
                {
                    index = i;
                    break;
                }
            return index;
        }

        public void updateDishPhoto(bool isOrderChange=true)
        {
            String link = "";
            if (isOrderChange)
                link = currentMenu[dishindex].linkToPhoto;
            else
                link = currentOrder[findDishOrder(orderDiahNameCrutch)].LinkToPhoto;
            if (link != "" && File.Exists(Properties.Settings.Default.dishesImagesFolderPath + link))
            {

                view.setDishPhoto(Properties.Settings.Default.dishesImagesFolderPath + link);
            }
            else
            {
                view.setDishPhoto(null);
            }
        }

        /// <summary>
        /// удаление блюда из внутреннего списка меню
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="price"></param>
        public void removeDishFromOrder()
        {
            int index = findDishOrder(orderDiahNameCrutch);
            orderEntry temp = new orderEntry(currentOrder[index]);
            if (temp.Count == 1)
                currentOrder.Remove(temp);
            else
            {
                temp.decreament();
                currentOrder[index] = temp;
            }
            return;

            
            
        }

        /// <summary>
        /// в список выбранных блюд, добавляет, те которые уже были выбранны в прошлый раз
        /// </summary>
        public void updateOpenedMenu()
        {
            clientId = view.getEmplId();
            placedOrderList = orderManager.getPlacedOrder(clientId, false); //зачем тру?
            currentOrder.Clear();
            foreach(orderEntry entry in placedOrderList)
                currentOrder.Add(entry);
            updateOrderMenu();
            return;
            
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
            foreach (orderEntry entry in placedOrderList)
            {
                temp = new ListViewItem(entry.Dish);
                temp.SubItems.Add(entry.Cost.ToString());
                temp.SubItems.Add(entry.Count.ToString());
                temp.SubItems.Add(entry.Price.ToString());
                viewOrder[ptr] = temp;
                ptr++;
                totalPrice += entry.Cost;
            }
            orderManager.closeOrder(clientId);
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
            orderManager.createOrder(view.getEmplId(), currentOrder);
            updateOpenedMenu();

        }
        
        /// <summary>
        /// обновлет во view теущиий список блюд в меню
        /// </summary>
        public void updateActiveMenu()
        {
            currentMenuList = new Dictionary<int, List<CourierListEntry>>();
            currentMenuList = Convertor.dishListToMenuList(orderManager.getActiveMenu());
            currentMenuList.Remove(0);
            view.updateMenuList(Convertor.menuDictToViewArr(currentMenuList));

        }

        /// <summary>
        /// возвращает сумму заказа
        /// </summary>
        /// <returns></returns>
        public float getTotalPrice()
        {
            float total = 0;
            foreach (orderEntry entry in currentOrder)
            {
                total += entry.Cost;
            }
            return total;
        }

        public void checkoutOrder()
        {
                updatePlacedOrderMenu();
                view.updatePlecedStatusOrder("Выполнен");



        }


    }
}
