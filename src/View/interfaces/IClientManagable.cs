using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IClientManagable : IInterractable
    {

        /// <summary>
        /// задает список компаний из Входящего словаря в выподающий список формы
        /// </summary>
        /// <param name="companyList"></param>
     void setCompanyList(Dictionary<int, String> companyList);//Dictionary<int, String> companyList
        /// <summary>
        /// задает список сотрудников из Входящего словаря в выподающий список формы
        /// </summary>
        /// <param name="employList"></param>
     void setEmployList(Dictionary<int, String> employList);


     /// <summary>
     /// возвращает id текущей компании, если не чего не выбрано, то -1
     /// </summary>
     /// <returns></returns>
     int getIndexSelectedCompany();
    }
}
