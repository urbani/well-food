using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;

namespace TRPO.View
{
    public interface IDishManagable : IInterractable
    {
        String getSelectedDishName();
        void setDishInfo(Dish dish);
        int getReadyDishesAmount();
    }
}
