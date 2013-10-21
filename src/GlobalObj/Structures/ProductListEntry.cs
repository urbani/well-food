using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public class ProductListEntry
    {
        private String name;     //Название
        private Double count;    //Количество 
        private Double price;    //Цена

        public System.String Name
        {
            get { return name; }
            set { name = value; }
        }

        public System.Double Count
        {
            get { return count; }
            set { count = value; }
        }

        public System.Double Price
        {
            get { return price; }
            set { price = value; }
        }


        public ProductListEntry(String s, Double c)
        {
            name = s;
            count = c;
            price = 0;
        }

        public ProductListEntry(String s, Double c, Double p)
        {
            name = s;
            count = c;
            price = p;
        }

    }
    
}
