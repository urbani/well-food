using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IClientManagable : IInterractable
    {

     void setCompanyList(Dictionary<int, String> companyList);//Dictionary<int, String> companyList
        void setEmployList();
    }
}
