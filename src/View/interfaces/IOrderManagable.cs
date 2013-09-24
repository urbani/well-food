using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;
using System.Windows.Forms;

namespace TRPO.View
{
    public interface IOrderManagable : IInterractable
    {
        /// <summary>
        /// обновляет меню в табе "новый заказ"
        /// </summary>
        /// <param name="listDishes"></param>
        void updateMenuList(List<CourierListEntry> listDishes);

        /// <summary>
        /// обновляет блюда выбранные для заказа текущим клинтом
        /// </summary>
        /// <param name="dishesInOrder"></param>
        void updateOrderMenu(ListViewItem[] dishesInOrder);

        /// <summary>
        /// обновляет  список блюд приготовленных для текущего клиента
        /// </summary>
        /// <param name="viewPlacedOrder"></param>
        void updatePlacedOrderMenu(ListViewItem[] viewPlacedOrder);

        /// <summary>
        /// обновляет итоговую сумму сделанного заказа
        /// </summary>
        /// <param name="totalPrice"></param>
        void updatePlaceOrderTotalPrice(float totalPrice);

        /// <summary>
        /// обновляет статус зделанного заказа во view
        /// </summary>
        /// <param name="statusMsg"></param>
        void updatePlecedStatusOrder(String statusMsg);
        /// <summary>
        /// возвращает список блюд, выбранных клиентов. Первый id - id-клиента
        /// </summary>
        /// <returns></returns>
        List<ListViewItem> getOrderMenu();
        


        void clearOrderMenu();
        int getEmplId(bool silent=false);
        void setWindowTitile(String Title);


    
    }
}
