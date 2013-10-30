using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.Structures
{
    public struct ChiefListEntry
    {
        public String name; //Название блюда
        public int need; //Необходимо приготовить
        public int ready;//Уже приготовлено
        public int left;//Осталось приготовить
        public int inStock;//На складе
        public ChiefListEntry(String s, int n, int r, int l, int st)
        {
            name = s;
            need = n;
            ready = r;
            left = l;
            inStock = st;
        }
    }
}
