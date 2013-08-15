using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public class Product
    {
        private String name;
        private Double price;
        private int idInDB;

        public Product() : this(0, 0, "") { }

        public Product(int i, Double p, String n)
        {
            idInDB = i;
            name = n;
            price = p;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder(255);
            res.Append("ID:");
            res.Append(idInDB);
            res.Append("; Product name:");
            res.Append(name);
            res.Append("%; Price:");
            res.Append(price);
            res.Append(".");
            return res.ToString();
        }

        public int IdInDB
        {
            get { return idInDB; }
            set { idInDB = value; }
        }

        public Double Price
        {
            get { return price; }
            set { price = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        
    }
}
