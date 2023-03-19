using System;
using System.Collections.Generic;

namespace WpfApp1
{

    [Serializable]
    public class Data
    {
        public Data()
        {
            Entries = new Dictionary<string, List<Entry>>();
            Tags = new List<string> ();
        }
        public Dictionary<string, List<Entry>> Entries { get; set; } = new Dictionary<string, List<Entry>>();
        public List<string> Tags { get; set; } = new List<string>();
        public long Calculate()
        {
            long result = 0;
            if (Entries == null || Entries.Count == 0)
                return 0;

            foreach (var entry in Entries)
                foreach (var item in entry.Value)
                    if (item.IsIncome)
                        result += item.Money;
                    else
                        result -= item.Money;

            return result;
        }
    }
}
