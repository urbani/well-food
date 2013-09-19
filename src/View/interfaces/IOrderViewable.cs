using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;
namespace TRPO.View
{
    public interface IOrderViewable : IInterractable
    {
        void updateOrderList(List<ChiefListEntry> list);
    }
}
