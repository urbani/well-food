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
        public List<int> idCompanyList = new List<int>();//{get;set;}

        public ClientManagementConroller(User u)
        {
            user = u;
            clientManager = new ClientManager();
        }

     public void setEmployList()
     {
             try
            {
             view.setEmployList(clientManager.getEmployers(idCompanyList[view.getIndexSelectedCompany()]));
            }
            catch (ArgumentOutOfRangeException)
            { }
      return;
     }

        //начальное заполнение формы данными
        public void fillCompList()
        {
            view.setCompanyList(clientManager.getCompanies());
         //TODO:дописать
           //view.getCurCompany(
            //setEmployList();
            
            
        }

        public void addForm(IClientManagable c)
        {
            view = c;
        }

    }
}
