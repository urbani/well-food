using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRPO.Structures
{
    public struct orderEnrty
    {
        String dish; //название
        public String Dish { get { return dish; } }
        
        float price; //цена за шт.
        
        float cost; //общая цена
        public float Cost { get { return cost; } }
        
        int count; //количество
        public int Count { get { return count; } }
        //полный конструктор
        public orderEnrty(String dish, float price, float cost, int count)
        {
            this.dish=dish;
            this.price=price;
            this.cost=cost;
            this.count=count;
        }
        //конструктор ноого объекта, когда больше не чего не знаем/не нужно
        public orderEnrty(String dish, float price)
        {
            this.dish = dish;
            this.price = price;
            this.cost = price;
            this.count = 1;
        }
        //конструктор копии
        public orderEnrty(orderEnrty entry)
        {
            this.dish = entry.Dish;
            this.cost = entry.Cost;
            this.count = entry.Count;
            this.price = this.cost / this.count;
        }

        public void changeOf(int off = 1)
        {
            if (count+off<1)
                throw new ArgumentException();
            this.count += off;
            this.cost = this.price * this.count;
        }
        public void inreament()
        {
            changeOf(1);
        }
        public void decreament()
        {
            changeOf(-1);
        }

        

    }
}
