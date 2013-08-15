using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.Structures;

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
            view.setDishInfo(tmpDish);
        }

        public void addReadyDishes()
        {
            int readyDishes = view.getReadyDishesAmount();
            String readyDish = view.getSelectedDishName();
            int redundantDishes = dishesManager.addReadyDishes(readyDish, readyDishes);
            if (redundantDishes > 0)
            {
                view.showMsg("Все заказы на блюдо [" + readyDish + "] закрыты. Осталось неучтенных блюд: [" + readyDish + " : " + redundantDishes + "шт.]", GlobalObj.ErrorLevels.Info);
            }
        }
    }
}
