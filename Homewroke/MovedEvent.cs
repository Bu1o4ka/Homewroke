using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal class MovedEvent: IJournalEntry
    {
        public string ToLogLine()
        {
            return $"{itemName}-{shelfOut}-{slotNumbOut}-{shelfIn}-{SlotNumbIn}";
        }

        public string ToScreenLine()
        {
            return $"Предмет {itemName} переложили с полки {shelfOut} из слота {slotNumbOut} на полку {shelfIn} в слот {slotNumbIn}";
        }

        private char shelfOut;
        private char shelfIn;
        private int slotNumbOut;
        private int slotNumbIn;
        private string itemName;

        public char ShelfOut { get { return shelfOut; } }
        public char ShelfIn { get { return shelfIn; } }
        public int SlotNumbOut { get { return slotNumbOut; } }
        public int SlotNumbIn {  get { return slotNumbIn; } }
        public string ItemName { get { return itemName; } }

        public MovedEvent( char shelfOut, char shelfIn, int slotNumbOut, int slotNumbIn, string itemName)
        {
            this.shelfOut = shelfOut;
            this.shelfIn = shelfIn;
            this.slotNumbOut = slotNumbOut;
            this.slotNumbIn = slotNumbIn;
            this.itemName = itemName;
        }
    }
}
