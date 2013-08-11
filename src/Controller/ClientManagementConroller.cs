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
        private ClientManager clientManager;

        public ClientManagementConroller(User u)
        {
            user = u;
            clientManager = new ClientManager();
        }

        public void fillCompList()
        {
            view.setCompanyList(clientManager.getCompanies());
            
        }

        public void addForm(IClientManagable c)
        {
            view = c;
        }
    }
}
