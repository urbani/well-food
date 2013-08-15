using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public class Dish
    {
        private int idInDB;
        private String name;
        private String linkToPhoto;
        private String dishType;
        private int percent;
        private String recipe;
        private Dictionary<Product, Double> consistance;

        public Dictionary<Product, Double> Consistance
        {
            get { return consistance; }
            set { consistance = value; }
        }

        public int IdInDB
        {
            get { return idInDB; }
            set { idInDB = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String LinkToPhoto
        {
            get { return linkToPhoto; }
            set { linkToPhoto = value; }
        }

        public String DishType
        {
            get { return dishType; }
            set { dishType = value; }
        }

        public int Percent
        {
            get { return percent; }
            set { percent = value; }
        }

        public String Recipe
        {
            get { return recipe; }
            set { recipe = value; }
        }

        public Dish() : this(-1, "", "", "", 0, "", null){}

        public Dish(int i, String n, String l, String d, int p, String r, Dictionary<Product, Double> c)
        {
            idInDB = i;
            name = n;
            linkToPhoto = l;
            dishType = d;
            percent = p;
            recipe = r;
            consistance = c;
        }

        public override String ToString() 
        {
            StringBuilder res = new StringBuilder(255);
            res.Append("ID:");
            res.Append(idInDB);
            res.Append("; Dish name:");
            res.Append(name);
            res.Append("; DishType:");
            res.Append(dishType);
            res.Append("; Link to Photo:");
            res.Append(linkToPhoto);
            res.Append("; Cost percent:");
            res.Append(percent);
            res.Append("%; Recipe:");
            res.Append(recipe);
            res.Append("; Consists of ");
            res.Append(consistance.Count);
            res.Append(" products.");
            return res.ToString();
        }
    }
}
