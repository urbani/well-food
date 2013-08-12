using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.GlobalObj
{
    public struct ChiefListEntry
    {
        public String name; //Название блюда
        public int need; //Необходимо приготовить
        public int ready;//Уже приготовлено
        public int left;//Осталось приготовить
        public ChiefListEntry(String s, int n, int r, int l)
        {
            name = s;
            need = n;
            ready = r;
            left = l;
        }
    }
}
