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

        public Dictionary<int, ClientData> companyList = new Dictionary<int,ClientData>();
        public Dictionary<int, ClientData> employList = new Dictionary<int,ClientData>();

        public IEnumerator<String> getCompanyList()
        {

            foreach (var kv in companyList)
            {
                yield return kv.Value.data;
            }
        }


        public ClientManagementConroller(User u)
        {
            user = u;
            clientManager = new ClientManager();
        }

        public void fillEmployList()
        {
            Dictionary<int, String> rawData;
            rawData = clientManager.getEmployers( companyList[view.getIndexSelectedCompany()].id );
            int ptr=0;
            foreach (KeyValuePair<int, String> pair in rawData)
            {
                employList.Add(ptr, new ClientData(pair.Key,pair.Value));
                ptr++;

            }


        }

        //начальное заполнение формы данными
        public void fillCompList()
        {
           // view.setCompanyList(clientManager.getCompanies());
            Dictionary<int, String> rawData;
            rawData = clientManager.getCompanies();
            int ptr=0;
            foreach (KeyValuePair<int, String> pair in rawData)
            {
                companyList.Add(ptr, new ClientData(pair.Key,pair.Value));
                ptr++;

            }
        }

        public void addForm(IClientManagable c)
        {
            view = c;
        }

    }

    public struct ClientData
    {
        public int id;
        public String data;
        public ClientData(int id_, String data_)
        {
            id = id_;
            data = data_;
            
        }
    }
    //public class SimpleList : IList<ClientData>
    //{
    //    ref ClientData dataConteiner;
    //    SimpleList()
    //    {
           
    //    }

    //    public int Add(object value)
    //    {
    //        if (_count < _contents.Length)
    //        {
    //            _contents[_count] = value;
    //            _count++;

    //            return (_count - 1);
    //        }
    //        else
    //        {
    //            return -1;
    //        }
    //    }
    //}
}
