using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddEntryType.xaml
    /// </summary>
    public partial class AddEntryType : Window
    {
        public new DialogResult DialogResult { get; private set; }
        public string EntryName { get;private set; }
        public AddEntryType()
        {
            InitializeComponent();
            btn.IsEnabled = false;
            tb.Focus();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            EntryName = tb.Text;
            Close();
            DialogResult = DialogResult.OK;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn.IsEnabled = tb.Text.Length > 0;
        }
    }
}
