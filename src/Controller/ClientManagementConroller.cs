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
        IDialog dialog;
        //класс над объектом пользователь-сотрудник (ФИО фото роль и т.д.)
        User user;
        private ClientManager clientManager;

        public Dictionary<int, int> companyIds = new Dictionary<int, int>(); //храним id`шники - id - в базе
        public List<string> companyList = new List<string>(); //название компании, для вывода в списке

        public Dictionary<int, int> employIds = new Dictionary<int, int>();
        public List<string> employList = new List<string>();
 
        public ClientManagementConroller(User u)
        {
            
            user = u;
            clientManager = new ClientManager();
 
        }

        public int getEmployId()
        {
            //TODO отладить: при смени компании где-то вызывается этот код
            int index = view.getIndexSelectedEmploy();
            if (employIds.ContainsKey(index))
                return employIds[index];
            else 
                return -1;

        }

        public int getCompanyId()
        {
            //TODO отладить: при смени компании где-то вызывается этот код
            int index = view.getIndexSelectedCompany();
            if (companyIds.ContainsKey(index))
                return companyIds[index];
            else
                return -1;

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
        public void fillCompanyList(bool system=false)
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
//            if (system)
               // view.updateCompanyList();
        }

        public void addForm(IClientManagable somethingView)
        {
            view = somethingView;
        }

        public void addDialog(IDialog someDialog)
        {
            dialog = someDialog;

        }
        public void selectEmployDat()
        {
            dialog.fillFiled(clientManager.selectEmployData(dialog.getEmployId()));

        }

        public void updateEmployDate()
        {
            clientManager.updateEmployData(dialog.getFileds(), dialog.getEmployId());
            fillEmployList();

        }
        public void insertEmployData()
        {
            clientManager.insertEmployData(dialog.getFileds(), dialog.getCompanyId());
            fillEmployList();
        }

        public void deleteEmploy()
        {
            clientManager.deleteEmploy(getEmployId());
            fillEmployList();
        }

        public void createCompany()
        {
            if(dialog.getCompanyName()=="")
                return;
            clientManager.createCompany(dialog.getCompanyName());
            fillCompanyList();
            fillEmployList();
        }

        public void editCompany()
        {
            if (dialog.getCompanyName() == "")
                return;

            clientManager.editCompany(dialog.getCompanyName(), dialog.getCompanyId());
            fillCompanyList();
            fillEmployList();

        }




    }



}
