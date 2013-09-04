using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;

namespace TRPO.View
{
    public interface IMenuManagable : IInterractable
    {
        void clearMenu();
        Menu getCreatedMenu();
        String getSelectedDishName();

        void addDishToMenu1(String dish);
        void addDishToMenu2(String dish);
        void addDishToMenu3(String dish);
        void addDishToSpecialMenu(String dish);

        bool addingToCompexMenu();
        bool inMenu1(String dish);
        bool inMenu2(String dish);
        bool inMenu3(String dish);
        bool inSpecialMenu(String dish);
    }
}
