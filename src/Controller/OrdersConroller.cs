using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;

namespace TRPO.Controller
{
    public class OrdersConroller
    {
        IOrderManagable view;
        //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        User user; 
        //конструкто, принимаем что
        public OrdersConroller(User u)
        {
            user = u;
         //TODO:
         //в загловок имя, роль
        }

        public void addForm(IOrderManagable c)
        {
            view = c;
        }


    }
}
