using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.GlobalObj;

namespace TRPO.Controller
{
    public class OrderCookController
    {
        User user;
        IOrderViewable view;
        OrderManager ordManager;

        public OrderCookController(User u)
        {
            user = u;
            ordManager = new OrderManager();
        }

        public void setForm(IOrderViewable f)
        {
            view = f;
        }

        public void updateOrderList()
        {
            view.updateOrderList(ordManager.getActiveOrders());
        }
    }
}
