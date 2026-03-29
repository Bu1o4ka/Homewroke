using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal class TakenEvent: IJournalEntry
    {
        public string ToLogLine()
        {
            return $"{itemName}-{slotNumb}-{shelf}";
        }

        public string ToScreenLine()
        {
            return $"Предмет [{itemName}] забрали с полки [{shelf}] из слота [{slotNumb}]";
        }

        private char shelf;
        private int slotNumb;
        private string itemName;

        public char Shelf
        {
            get { return shelf; }
        }
        public int SlotNumb
        {
            get { return slotNumb; }
        }
        public string ItemName
        {
            get { return itemName; }
        }

        public TakenEvent(string itemName, int slotNumb, char shelf) 
        { 
            this.shelf = shelf;
            this.slotNumb = slotNumb;
            this.itemName = itemName;
        }
    }
}
