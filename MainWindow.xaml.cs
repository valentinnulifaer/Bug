using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data data;
        private List<Entry> curEntries = new  List<Entry>();
        private Entry curEntry;
        public MainWindow()
        {
            data = SaveLoad.LoadData();
            InitializeComponent();
                lb_result.Content = $"Итог: {data.Calculate()}";

        }
        private void AddEntries(params string[] names)
        {
            foreach (string name in names)
            {
                CB_Entries.Items.Add(name);
                data.Tags.Add(name);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
         {
            var win = new AddEntryType();
            win.ShowDialog();
            if (win.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                AddEntries(win.EntryName);
            }
        }
        private void AddEntry(Entry entry)
        {
            string key = DT.SelectedDate.Value.ToLongDateString();
            if (data.Entries.ContainsKey(key))
            {
                data.Entries[key].Add(entry);
            }
            else
            {
                List<Entry> list = new List<Entry>();
                list.Add(entry);
                data.Entries.Add(key, list);

            }
            LoadData();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            long money = 0;
            if (TB_EntryName.Text.Length > 0 && CB_Entries.SelectedIndex >= 0 && long.TryParse(TB_Money.Text, out money))
            {
                var entry = new Entry(TB_EntryName.Text, CB_Entries.SelectedItem.ToString(), money);
               
                AddEntry(entry);
                lb_result.Content = $"Итог: {data.Calculate()}";
            }
        }

        private void TB_EntryName_Copy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(long.TryParse(e.Text, out _) || e.Text == "-");
        }

        private void DG_Entries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_Entries.SelectedIndex >=0)
            {
                Entry entry = data.Entries[DT.SelectedDate.Value.ToLongDateString()].ToList()[DG_Entries.SelectedIndex];
                TB_EntryName.Text = entry.Name;
                TB_Money.Text = entry.Money.ToString();
                CB_Entries.SelectedIndex = -1;
                curEntry = entry;
                for (int i = 0; i< CB_Entries.Items.Count;i++ )
                {
                    if (CB_Entries.Items[i] == entry.Type)
                    {
                        CB_Entries.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                curEntry = null;
            }
        }
        private void LoadData()
        {
            string key = DT.SelectedDate.Value.ToLongDateString();
            if (data.Entries.ContainsKey(key))
            {
                DG_Entries.ItemsSource = data.Entries[key];

            }
            else
            {
                DG_Entries.ItemsSource = null;
            }
            DG_Entries.Items.Refresh();
            lb_result.Content = $"Итог: {data.Calculate()}";
        }
        private void DT_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.Entries.ContainsKey(DT.SelectedDate.Value.ToLongDateString()))
            {
                curEntries = data.Entries[DT.SelectedDate.Value.ToLongDateString()];               
                DG_Entries.ItemsSource = data.Entries[DT.SelectedDate.Value.ToLongDateString()];
            }
            else
            {
                DG_Entries.ItemsSource= null;
            }
            DG_Entries.Items.Refresh();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveLoad.SaveData(data);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in data.Tags)
            {
                CB_Entries.Items.Add(item);
            }
            DT.SelectedDate = DateTime.Now;
        }      
        private void Button_Click_2(object sender, RoutedEventArgs e)//изменение выбранной записи
        {
            if(curEntry!=null && CB_Entries.SelectedIndex>=0)
            {
                curEntry.Name = TB_EntryName.Text;
                curEntry.Money = Convert.ToInt64(TB_Money.Text);
                curEntry.Type = CB_Entries.SelectedItem.ToString();
                lb_result.Content = $"Итог: {data.Calculate()}";
            }
            DG_Entries.Items.Refresh();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (DG_Entries.SelectedIndex >= 0)
            {
                data.Entries[DT.SelectedDate.Value.ToLongDateString()].RemoveAt(DG_Entries.SelectedIndex);
                DG_Entries.Items.Refresh();
            }
        }
    }
}
