using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IOrderViewable : IInterractable
    {
        void updateOrderList(List<TRPO.GlobalObj.ChiefListEntry> list);
    }
}
