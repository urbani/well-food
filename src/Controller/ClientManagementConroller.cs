using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;

namespace TRPO.Controller
{
    public class ClientManagementConroller
    {
        IClientManagable view;
        User user;

        public ClientManagementConroller(User u)
        {
            user = u;
        }

        public void addForm(IClientManagable c)
        {
            view = c;
        }
    }
}
