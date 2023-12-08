using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace UI
{
    internal class GameCatalogService : IGameCatalogService
    {
        private readonly IDbContextFactory<GameDbContext> _contextFactory;
        public GameCatalogService(IDbContextFactory<GameDbContext> contextFactory) 
        {
            _contextFactory = contextFactory;
        }
        public int AddGame(string name, string processName, TextBox offsets)
        {
            using var context = _contextFactory.CreateDbContext();
            Game game = new Game (Name : name, ProcessName : processName, Offsets : offsets.Text);
            context.Game.Add(game);
            return context.SaveChanges();
        }
        public Game GetSelectedGameData(string selectedGameName)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Game.FirstOrDefault(game => game.Name == selectedGameName);
        }
        public void LoadNames(ComboBox cmb)
        {
            using var context = _contextFactory.CreateDbContext();
            var games =  context.Game.Select(game => game.Name).ToList();
            foreach (var name in games)
            {
                cmb.Items.Add(name);
            }
        }
    }
}
