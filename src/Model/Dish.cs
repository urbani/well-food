using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Model
{
    public class Dish
    {
        private int idInDB;

        public int IdInDB
        {
            get { return idInDB; }
            set { idInDB = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private String linkToPhoto;

        public String LinkToPhoto
        {
            get { return linkToPhoto; }
            set { linkToPhoto = value; }
        }

        private String dishType;

        public String DishType
        {
            get { return dishType; }
            set { dishType = value; }
        }

        private int percent;

        public int Percent
        {
            get { return percent; }
            set { percent = value; }
        }

        private String recipe;

        public String Recipe
        {
            get { return recipe; }
            set { recipe = value; }
        }

        public Dish()
        {
            idInDB = -1;
            name = "";
            linkToPhoto = "";
            dishType = "";
            percent = 0;
            recipe = "";
        }

        public Dish(int i, String n, String l, String d, int p, String r)
        {
            idInDB = i;
            name = n;
            linkToPhoto = l;
            dishType = d;
            percent = p;
            recipe = r;
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
            res.Append(".");

            return res.ToString();
        }
    }
}
