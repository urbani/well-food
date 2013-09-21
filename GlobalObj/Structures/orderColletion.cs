using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRPO.Structures
{
    public struct orderColletion
    {
        int number; //номер в коллекции
        ListViewItem item; //запись в listView
        public orderColletion(int number, ListViewItem item)
        {
            this.number = number;
            this.item = item;
        }

    }
}
