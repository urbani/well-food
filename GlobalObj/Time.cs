using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Model;

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
}
