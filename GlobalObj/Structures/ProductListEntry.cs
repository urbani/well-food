using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public struct ProductListEntry
    {
        public String name; //Название
        public Double count;//Осталось на складе 

        public ProductListEntry(String s, Double c)
        {
            name = s;
            count = c;
        }
    }
    
}
