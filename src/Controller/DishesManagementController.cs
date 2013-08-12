using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;

namespace TRPO.Controller
{
    public class DishesManagementController
    {
        IDishViewable view;
        User user;
        DishesManager dishesManager;

        public DishesManagementController(User u)
        {
            user = u;
            dishesManager = new DishesManager();
        }

        public void setForm(IDishViewable f)
        {
            view = f;
        }

        public void updateDishInfo()
        {
            Dish tmpDish = dishesManager.getDish(view.getSelectedDishName());
            view.setDishInfo(tmpDish.Name, tmpDish.DishType, tmpDish.LinkToPhoto, tmpDish.Recipe);
        }
    }
}
