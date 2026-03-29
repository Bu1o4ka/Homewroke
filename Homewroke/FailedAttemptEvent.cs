using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal class FailedAttemptEvent: IJournalEntry
    {
        public string ToLogLine()
        {
            return $"{type}-{shelf}-{slotNumb}-{description}";
        }
        public string ToScreenLine()
        {
            return $"Произошла ошибка типа: [{type}] в полке [{shelf}] в слоте [{slotNumb}]. Подробности: [{description}]";
        }

        private string type;
        private char shelf;
        private int slotNumb;
        private string description;

        public char Shelf
        {
            get { return shelf; }
        }

        public int SlotNumb
        {
            get { return slotNumb; }
        }

        public string Type
        {
            get { return type; }
        }

        public string Description
        {
            get { return description; }
        }

        public FailedAttemptEvent(string type, char shelf, int slotNumb, string description)
        {
            this.type = type;
            this.shelf = shelf;
            this.slotNumb = slotNumb;
            this.description = description;
        }
    }
}
