using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
namespace TRPO.View
{
    public interface IClientManagable : IInterractable
    {

        /// <summary>
        /// сообщает списку компаний, что источник данных обновился
        /// </summary>
        /// <param name="companyList"></param>
         void updateCompanyList();//Dictionary<int, String> companyList
        /// <summary>
        /// сообщает списку сотрудников, что источник данных обновился
        /// </summary>
        /// <param name="employList"></param>
         void updateEmployList();
        //void clearCompanyList();
        //void clearEmployList();


     /// <summary>
     /// возвращает id текущей компании, если не чего не выбрано, то кидает исключение
     /// </summary>
     /// <returns></returns>
     int getIndexSelectedCompany();
    }
}
