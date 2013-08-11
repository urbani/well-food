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
        //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        User user;
        private ClientManager clientManager;

        public ClientManagementConroller(User u)
        {
            user = u;
            clientManager = new ClientManager();
        }

        //начальное заполнение формы данными
        public void fillCompList()
        {
            view.setCompanyList(clientManager.getCompanies());
         //TODO:дописать
           //view.getCurCompany(
            view.setEmployList( clientManager.getEmployers(1) );
            
        }

        public void addForm(IClientManagable c)
        {
            view = c;
        }
    }
}
