using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Model;
using TRPO.View;

namespace TRPO.Controller
{
    public class CourierController
    {
        User user;
        ICourierForm view;

        public CourierController(User u)
        {
            user = u;
        }

        public void addForm(ICourierForm cf)
        {
            view = cf;
        }
    }
}
