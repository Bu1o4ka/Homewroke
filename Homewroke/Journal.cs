using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal class Journal<T> where T: IJournalEntry
    {
        private List<T> notices = new List<T>();

        void Add(T entry)
        {

        }
        public List<T> Notices
        {
            get { return notices; }
        }
    }
}
