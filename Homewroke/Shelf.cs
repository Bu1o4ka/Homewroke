using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal class Shelf
    {
        private List<string> items = new List<string>(10) { "пусто", "пусто", "пусто", "пусто", "пусто", "пусто", "пусто", "пусто", "пусто", "пусто" };

        public List<string> Items
        {
            get { return items; }
            set { items = value; }
        }

        public Shelf()
        {

        }

        public void Place(int slotNumb, string itemName)
        {
            items[slotNumb-1] = itemName;
        }

        public void Take(int slotNumb)
        {
            items[slotNumb-1] = "пусто";
        }

        public string Read(int slotNumb)
        {
            return items[slotNumb - 1];
        }
    }
}
