using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI
{
    internal interface IGameCatalogService
    {
        public int AddGame(string name, string processName, TextBox offsets);
        public Game GetSelectedGameData(string selectedGameName);
        public void LoadNames(ComboBox cmb);
    }
}
