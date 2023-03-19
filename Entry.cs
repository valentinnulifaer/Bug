using System;

namespace WpfApp1
{
    [Serializable]
    public class Entry
    {
        public Entry(string name, string type, long money)
        {
            Money = money;
            Name = name;
            Type = type;
        }
        private long money;
        public long Money
        {
            get => money;
            set
            {
                money = value >= 0 ? value : value * -1;
                isIncome = value >= 0;
            }
        }

        public string Name { get; set; }
        public string Type { get; set; }
        private bool isIncome;
        public bool IsIncome
        {
            get => isIncome;           
        }

    }
}
