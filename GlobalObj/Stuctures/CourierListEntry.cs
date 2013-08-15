using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public struct CourierListEntry
    {
        
        public int id;
        public float price;
        public String dish;
        public String type;
        


        public CourierListEntry(int i, String d, int p, String type)
        {
            id = i;
            dish = d;
            price = p;
            this.type = type;

        }
    }
}
