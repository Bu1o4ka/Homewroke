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

        public void Add(T entry)
        {
            notices.Add(entry);
        }
        public List<T> Notices
        {
            get { return notices; }
        }

        public void SaveToFile(string path)
        {
            string logs = "";
            foreach (var item in Notices)
            {
                logs += item.ToLogLine() + '\n';
            }
            FileHelper.WriteFile(path, logs.Trim());
        }
    }
}
