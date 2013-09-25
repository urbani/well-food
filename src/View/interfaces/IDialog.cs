using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IDialog
    {
         int getCompanyId();
         int getEmployId();
         List<String> getFileds();
         void fillFiled(List<String> listStr);
    }
}
