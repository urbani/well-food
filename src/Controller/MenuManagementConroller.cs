using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.GlobalObj;
using System.Windows.Forms;

namespace TRPO.Controller
{
    public class MenuManagementConroller
    {
        IMenuManagable view;
        //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        
        private MenuManager menuManager;
        User user;

        public MenuManagementConroller(User u)
        {
            menuManager = new MenuManager();
            user = u;
        }

        public void setForm(IMenuManagable f)
        {
            view = f;
        }

        public void addMenu()
        {
            int i = menuManager.addMenu(view.getCreatedMenu());

            view.clearMenu();
            if (i > 0)
            {
                view.showMsg("Меню занесено в базу.", ErrorLevels.Info);
            }
            else
            {
                view.showMsg("Ошибка! Меню не занесено в базу. Возможно уже есть меню на эту дату", ErrorLevels.Info);
            }

        }

        public void addDish()
        {
            String dishName = view.getSelectedDishName();
            String type = menuManager.getDishType(dishName);
            if (!view.addingToCompexMenu())
            {
                switch (type)
                {
                    case "Первое":
                        if (!view.inMenu1(dishName)) view.addDishToMenu1(dishName);
                        break;
                    case "Второе":
                        if (!view.inMenu2(dishName)) view.addDishToMenu2(dishName);
                        break;
                    case "Третье":
                        if (!view.inMenu3(dishName)) view.addDishToMenu3(dishName);
                        break;
                }
                
            }
            else
            {
                if (!view.inSpecialMenu(dishName))
                {
                    view.addDishToSpecialMenu(dishName);
                }
            }
            
        }

    }



}
