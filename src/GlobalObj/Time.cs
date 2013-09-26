using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Model;
using System.Windows.Forms;
using TRPO.Structures;

namespace TRPO.GlobalObj
{
   public static class TRPOGlobal
    {
       public static String getCurrentTime()
       {

           //Удобное использование времени
           //DateTime.Parse("01.09.2013").ToLongDateString();
           //DateTime.Parse("01.09.2013").AddDays(1);
           //Сегодня: DateTime.Now.ToShortDateString();
           
           return DateTime.Parse("1.09.2013").ToShortDateString();
       }

       public static String makeTitle(User userObj)
       {
           return String.Format("{0} - {1} {2} {3}", userObj.role.ToString(), userObj.surname, userObj.name, userObj.patronymic);
       }

 

    }

    /// <summary>
    /// класс конвертируий различные внутринние представлния из одного в другой.
    /// </summary>
   public static class Convertor
   {
       /// <summary>
       /// конвертирует список блюд из системного представления в View-прикодный тип
       /// </summary>
       public static ListViewItem[] orderListToViewArr(List<orderEnrty> dishList, bool incudePrice = false)
       {
           ListViewItem[] dishArr = new ListViewItem[dishList.Count];
           int ptr = 0;
           ListViewItem temp = new ListViewItem();
           foreach (orderEnrty entry in dishList)
           {
               temp = new ListViewItem(entry.Dish);
               temp.SubItems.Add(entry.Cost.ToString());
               temp.SubItems.Add(entry.Count.ToString());
               if (incudePrice)
                   temp.SubItems.Add(entry.Price.ToString());
               dishArr[ptr] = temp;
               ptr++;
           }
           return dishArr;
       }

       static List<int> DishTypeIndex = new List<int> { 1, 2, 3, 4 };
       public static List<int> dishTypeIndex { get { return DishTypeIndex; } }
       public static int countTypeDish { get { return DishTypeIndex.Count; } }

       /// <summary>
       /// сортируют блюда по типу
       /// </summary>
       public static Dictionary<int, List<CourierListEntry>> dishListToMenuList(List<CourierListEntry> dishList)
       {
           Dictionary<int, List<CourierListEntry>> sortedMenuList = new Dictionary<int, List<CourierListEntry>>();
           foreach (int itype in Enumerable.Range(1, countTypeDish))
               sortedMenuList.Add(itype, new List<CourierListEntry>());
           foreach (CourierListEntry entry in dishList)
           {
               switch (entry.intType)
               {
                   case(1):
                       sortedMenuList[1].Add(entry);
                       break;
                   case (2):
                       sortedMenuList[2].Add(entry);
                       break;
                   case (3):
                       sortedMenuList[3].Add(entry);
                       break;
                   case (4):
                       sortedMenuList[4].Add(entry);
                       break;
                   default:
                       throw new ArgumentException("DIshListToMenuList: error entry index");
               }
           }
           return sortedMenuList;

       }
       public static List<ListViewItem[]> menuDictToViewArr(Dictionary<int, List<CourierListEntry>> sortedDishList)
       {
           List<ListViewItem[]> viewDishList = new List<ListViewItem[]>();
           ListViewItem temp = new ListViewItem();
           foreach (int index in DishTypeIndex)
           {
               viewDishList.Add(menuListToViewMenuArr(sortedDishList[index]));
           }
           return viewDishList;

       }

       static ListViewItem[] menuListToViewMenuArr(List<CourierListEntry> dishList)
       {
           ListViewItem[] dishArr = new ListViewItem[dishList.Count];
           int ptr = 0;
           ListViewItem temp = new ListViewItem();
           foreach (CourierListEntry entry in dishList)
           {
               temp = new ListViewItem(entry.dish);
               temp.SubItems.Add(entry.price.ToString());;
               dishArr[ptr] = temp;
               ptr++;
           }
           return dishArr;
       }




       static ListViewItem[] viewListToViewArr (List<ListViewItem> listView)
       {
           ListViewItem[] viewArr = new ListViewItem[listView.Count];
           foreach (int i in Enumerable.Range(1, listView.Count))
           {
               viewArr[i] = viewArr[i];
           }
           return viewArr;

       }

   }
}
