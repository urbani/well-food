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
        void setDishesList(Dictionary<String, String> dishesTypes);
        //void setMenuList1(Dictionary<String, String> dishesTypes);
        //void setMenuList2(Dictionary<String, String> dishesTypes);
        //void setMenuList3(Dictionary<String, String> dishesTypes);
        //void setMenuList4(Dictionary<String, String> dishesTypes);
        //void setOrderList(Dictionary<String, String> dishesTypes);
        void setProductsList(List<String> pList);
        void updateContents(Dictionary<String, Double> cont);

        void addProductToContence(String s, Double d);
        String getSelectedProductName();
        String getSelectedContenceName();
        Dish getCreatedDish();
        String getAddingProductName();
    }
}
