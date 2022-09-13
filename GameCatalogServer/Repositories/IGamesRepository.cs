using System.Collections.Generic;
using System.Threading.Tasks;

using GameCatalog;
using Grpc.Core;

namespace GameCatalogServer.Repositories
{
    public interface IGamesRepository
    {
        Task<List<Game>> GetAll();
        Task<Game?> GetGameByName(string name);
        Task<int> GetPriceByName(string name);
        Task AddGame(Game game);
        Task RemoveGame(string gameName);
        Task RemoveAll();
    }
}
