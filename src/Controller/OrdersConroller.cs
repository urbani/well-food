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
        User user;

        public OrdersConroller(User u)
        {
            user = u;
        }

        public void addForm(IOrderManagable c)
        {
            view = c;
        }
    }
}
