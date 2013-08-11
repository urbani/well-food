using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IOrderManagable : IInterractable
    {
     void showMenuList();
     void chowCurMenu();
    
    }
}
