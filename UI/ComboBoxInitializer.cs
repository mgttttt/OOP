using System.Diagnostics;
using System.Windows.Controls;
using System.Data.SQLite;

namespace UI
{
    public class ComboBoxInitializer
    {

        public ComboBoxInitializer ()
        {

        }

        public void InitializeProcessComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            foreach (var process in Process.GetProcesses())
            {
                comboBox.Items.Add(process.ProcessName);
            }
        }

        public void InitializeGamesComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            DatabaseService ds = new DatabaseService();

            ds.LoadNames(comboBox);
        }
        
    }
}
