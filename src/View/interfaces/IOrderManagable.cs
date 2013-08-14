using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;

namespace TRPO.View
{
    public interface IOrderManagable : IInterractable
    {
        void updateMenuList(List<CourierListEntry> listDishes);
        void showCurMenu();
    
    }
}
