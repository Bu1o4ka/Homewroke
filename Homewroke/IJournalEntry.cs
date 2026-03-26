using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal interface IJournalEntry
    {
        string ToLogLine();

        string ToScreenLine();
    }
}
