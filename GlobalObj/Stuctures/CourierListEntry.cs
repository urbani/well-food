using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public struct CourierListEntry
    {
        
        public int id;
        public String dish;
        public int price;

        public CourierListEntry(int i, String d, int p)
        {
            id = i;
            dish = d;
            price = p;
        }
    }
}
