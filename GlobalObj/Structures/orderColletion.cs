using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRPO.Structures
{
    /// <summary>
    /// системное представление записи о блюде добовляемом в заказ
    /// </summary>
    public struct orderEntry
    {
        String dish;
        /// <summary>
        /// название
        /// </summary>
        public String Dish { get { return dish; } }
        
        /// <summary>
        ///цена за шт.
        /// </summary>
        float price;
        public float Price { get { return price ; } }
       
        /// <summary>
        /// общая цена
        /// </summary>
        public float Cost { get { return price * count; } }
        
        int count; 
        /// <summary>
        /// количество
        /// </summary>
        public int Count { get { return count; } }

        public int id;
        String linkToPhoto;
        public String LinkToPhoto { get { return linkToPhoto; } }
        //полный конструктор
        public orderEntry(String dish, float price,  int count, int id, String linkToPhoto)
        {
            this.dish = dish;
            this.price = price;
            this.count = count;
            this.id = id;
            this.linkToPhoto = linkToPhoto;
        }


        public orderEntry(String dish, float price, int count)
        {
            this.dish=dish;
            this.price = price;
            this.count=count;
            id = 0;
            this.linkToPhoto = "";
        }



        //конструктор ноого объекта, когда больше не чего не знаем/не нужно
        public orderEntry(String dish, float price)
        {
            this.dish = dish;
            this.price = price;
            this.count = 1;
            id = 0;
            this.linkToPhoto = "";
        }
        //конструктор копии
        public orderEntry(orderEntry entry)
        {
            this.dish = entry.Dish;
            this.count = entry.Count;
            this.price = entry.price;
            this.id = entry.id;
            this.linkToPhoto = entry.linkToPhoto;
        }
        
        public void changeOf(int off = 1)
        {
            if (count+off<1)
                throw new ArgumentException();
            this.count += off;
        }
        //TODO можно была бы перегрузить оператор ++
        public void inreament()
        {
            changeOf(1);
        }
        public void decreament()
        {
            changeOf(-1);
        }
        public bool Equals(orderEntry other)
        {
            if (this.id != other.id || this.count != other.count || this.price != other.price || this.dish!=other.dish)
                return false;
            else
                return true;
        }


    }

    /// <summary>
    /// системное представление блюда из выполненного заказа
    /// </summary>
    public struct orderCheckoutEnry
    {
        public int dishId;
        public int dishCount;
        public int readyCount;
        public String dish;
        public orderCheckoutEnry(int dishId, int dishCount, int readyCount, String dish)
        {
            this.dish = dish;
            this.dishCount = dishCount;
            this.dishId = dishId;
            this.readyCount = readyCount;
        }
    }

}
