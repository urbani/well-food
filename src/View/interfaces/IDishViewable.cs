using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IDishManagable : IInterractable
    {
        String getSelectedDishName();
        void setDishInfo(String name, String dishType, String linkToPhoto, String recipe);
        int getReayDishesAmount();
    }
}
