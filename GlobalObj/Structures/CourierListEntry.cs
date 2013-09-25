using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.GlobalObj;

namespace TRPO.Structures
{
    public struct CourierListEntry
    {
        
        public int id;
        public float price;
        public String dish;
        public String type;
        public bool isSpecial;
        public String linkToPhoto;
        public int intType
        {
            get
            {
                if (isSpecial)
                    return 3;
                else if (type == DishesTypes.Первое.ToString())
                    return 0;
                else if (type == DishesTypes.Второе.ToString())
                    return 1;
                else if (type == DishesTypes.Третье.ToString())
                    return 2;
                else
                    throw new ArgumentException("courier list enry error type");
            }

        }




        public CourierListEntry(int i, String d, int p, String type, bool isSpecial, String linkToPhoto)
        {
            id = i;
            dish = d;
            price = p;
            this.type = type;
            this.isSpecial = isSpecial;
            this.linkToPhoto = linkToPhoto;

        }
        public CourierListEntry(int i, String d, int p, String type, bool isSpecial)
        {
            id = i;
            dish = d;
            price = p;
            this.type = type;
            this.isSpecial = isSpecial;
            this.linkToPhoto = "";

        }
        public orderEnrty ToOrderEntry()
        {
            return new orderEnrty(this.dish, this.price, 1, this.id, this.linkToPhoto);
            
        }
    }
}
