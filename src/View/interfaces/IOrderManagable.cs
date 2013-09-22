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
        void updateMenuList(List<CourierListEntry> listDishes);
        void showCurMenu();
        /// <summary>
        /// возвращает список блюд, выбранных клиентов. Первый id - id-клиента
        /// </summary>
        /// <returns></returns>
        List<ListViewItem> getOrderMenu();
        void clearOrderMenu();
        int getEmplId();
        void setWindowTitile(String Title);

    
    }
}
