using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.GlobalObj
{
   public static class Time
    {
       public static String getCurrentTime()
       {

           //Удобное использование времени
           //DateTime.Parse("01.09.2013").ToLongDateString();
           //DateTime.Parse("01.09.2013").AddDays(1);
           //Сегодня: DateTime.Now.ToShortDateString();
           
           return DateTime.Parse("1.09.2013").ToShortDateString();
       }
    }
}
