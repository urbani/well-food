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
        void setCreateDishInfo(Dish d);
        int getReadyDishesAmount();
        void setDishesList(Dictionary<String, String> dishesTypes);
        void setProductsList(List<String> pList);
        void updateContents(Dictionary<String, Double> cont);
    }
}
