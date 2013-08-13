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
        IDishManagable view;
        User user;
        DishesManager dishesManager;

        public DishesManagementController(User u)
        {
            user = u;
            dishesManager = new DishesManager();
        }

        public void setForm(IDishManagable f)
        {
            view = f;
        }

        public void updateDishInfo()
        {
            Dish tmpDish = dishesManager.getDish(view.getSelectedDishName());
            view.setDishInfo(tmpDish.Name, tmpDish.DishType, tmpDish.LinkToPhoto, tmpDish.Recipe);
        }

        public void addReadyDishes()
        {
            int readyDishes = view.getReayDishesAmount();
            String readyDish = view.getSelectedDishName();
            int changes = dishesManager.addReadyDishes(readyDish, readyDishes);

            if (changes < 0)
            {
                System.Diagnostics.Debug.WriteLine("WARNING! При добавлении готовых продуктов поля в базе не изменились!");
            }
        }
    }
}
