using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace UI
{
    internal class DbContextHelper
    {
        private static readonly IHost _host;

        static DbContextHelper()
        {

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGameCatalogService, GameCatalogService>();

                    services.AddPooledDbContextFactory<GameDbContext>(options =>
                    {
                        options.UseSqlite("Data Source = HackedGames.db");
                    });
                })
                .Build();
        }

    }
}
