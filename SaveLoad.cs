using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace WpfApp1
{
    internal static class SaveLoad
    {
        private const string fileName = "Data.bin";
        public static void SaveData(Data arg)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, arg);
            }
        }
        public static Data LoadData()
        {
            if (File.Exists(fileName))
            {

                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    try
                    {
                        Data result = (Data)formatter.Deserialize(fs);
                        return result ?? new Data();
                    }
                    catch
                    {
                        MessageBox.Show("Error", "ошибка чтения файла", MessageBoxButton.OK, MessageBoxImage.Error);
                        return new Data();
                    }
                }
            }
            else
            {
                return new Data();
            }
        }
    }
}
