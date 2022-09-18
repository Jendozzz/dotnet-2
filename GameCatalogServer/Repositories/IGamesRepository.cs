using GameCatalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalogServer.Repositories
{
    public interface IGamesRepository
    {
        Task<List<Game>> GetAll();

        Task<int> GetPriceByName(string name);
        Task AddGame(Game game);
        Task RemoveGame(Game game);
    }
}
