using System.Windows;
using System.Windows.Controls;
using EngineCoreCsharp;

namespace UI
{
    public partial class MainWindow : Window
    {
        private DatabaseService _databaseService;
        private ComboBoxInitializer cmb;
        public MainWindow()
        {
            InitializeComponent();
            cmb = new ComboBoxInitializer();
            cmb.InitializeProcessComboBox(Processes); 
            cmb.InitializeGamesComboBox(gamesComboBox);
            _databaseService = new DatabaseService();
            
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            cmb.InitializeProcessComboBox(sender as ComboBox);
        }
        private void Button_Activate_Click(object sender, RoutedEventArgs e)
        {
            Game game = _databaseService.GetSelectedGameData(gamesComboBox.Text);
            Engine.ActivateCheat(game._Offsets, game._ProcessName);
        }

        private void Button_Deactivate_Click(object sender, RoutedEventArgs e)
        {
            Engine.DeactivateCheat();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _databaseService.AddGame(NameTextBox.Text, Processes.Text, Offsets);
        }
    }
}