using GameCatalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalogServer.Repositories
{
    public interface IGamesRepository
    {
        Task<List<Game>> GetAll();
        Task<Game?> GetGameByName(string name);
        Task<int> GetPriceByName(string name);
        Task AddGame(Game game);
        Task RemoveGame(Game game);
    }
}
