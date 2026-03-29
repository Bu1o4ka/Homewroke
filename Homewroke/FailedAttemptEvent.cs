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
            return $"{}";
        }
        public string ToScreenLine()
        {
            return "1234";
        }

        private string type;
        private char 

        public FailedAttemptEvent() { }
    }
}
