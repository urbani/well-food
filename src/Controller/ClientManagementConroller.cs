using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.GlobalObj;
using System.Windows.Forms;

namespace TRPO.Controller
{
    public class ClientManagementConroller
    {
        IClientManagable view;
        //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        User user;
        private ClientManager clientManager;

        Dictionary<int, int> companyIds = new Dictionary<int,int>();
        public List<string> companyList = new List<string>();

        Dictionary<int, int> employIds  = new Dictionary<int, int>();
        public List<string> employList = new List<string>();
        //CurrencyManager currencyManager= (CurrencyManager)this.BindingContext[listBox.DataSource]; 

        
        public ClientManagementConroller(User u)
        {
            
            user = u;
            clientManager = new ClientManager();
 
        }

        public void fillEmployList()
        {
            
            
            employIds.Clear();
            employList.Clear();
            Dictionary<int, String> rawData;
            rawData = clientManager.getEmployers(companyIds[view.getIndexSelectedCompany()]);
            int ptr=0;
            foreach (KeyValuePair<int, String> pair in rawData)
            {
                employList.Add(pair.Value);
                employIds.Add(ptr, pair.Key);
                ptr++;

            }
            view.updateEmployList();

        }

        //начальное заполнение формы данными
        public void fillCompanyList()
        {
            
            
            employIds.Clear();
            employList.Clear();
            companyIds.Clear();
            companyList.Clear();
            Dictionary<int, String> rawData;
            rawData = clientManager.getCompanies();
            int ptr = 0;
            foreach (KeyValuePair<int, String> pair in rawData)
            {
                companyList.Add(pair.Value);
                companyIds.Add(ptr, pair.Key);
                ptr++;
            }
        }

        public void addForm(IClientManagable c)
        {
            view = c;
        }





    }



}
