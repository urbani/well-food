using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public class Menu
    {
        List<String> menu1;
        List<String> menu2;
        List<String> menu3;
        List<String> specialMenu;
        DateTime menuDate;

        public Menu(List<String> m1, List<String> m2, List<String> m3, List<String> sm, DateTime date)
        {
            menu1 = m1;
            menu2 = m2;
            menu3 = m3;
            specialMenu = sm;
            menuDate = date;
        }

        public DateTime MenuDate
        {
            get { return menuDate; }
            set { menuDate = value; }
        }
        public List<String> SpecialMenu
        {
            get { return specialMenu; }
            set { specialMenu = value; }
        }
        public List<String> Menu3
        {
            get { return menu3; }
            set { menu3 = value; }
        }
        public List<String> Menu2
        {
            get { return menu2; }
            set { menu2 = value; }
        }
        public List<String> Menu1
        {
            get { return menu1; }
            set { menu1 = value; }
        }

       
    }
}
