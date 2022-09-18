using GameCatalog;
using GameCatalogServer.Repositories;
using Grpc.Core;
using System.Threading.Tasks;

namespace GameCatalogServer
{
    public class MainService : GameCatalogService.GameCatalogServiceBase
    {
        private readonly IGamesRepository _gamesRepository;
        public MainService(IGamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }

        public override async Task<Reply> Connect(Request request, ServerCallContext context)
        {
            Reply replay = new Reply { };
            switch (request.RequestCase)
            {
                case Request.RequestOneofCase.GetGames:
                    var games = await _gamesRepository.GetAll();
                    replay.Games = new GamesReplay { Games = { { games } } };
                    break;
                case Request.RequestOneofCase.GetPrice:
                    var price = await _gamesRepository.GetPriceByName(request.GetPrice.Name);
                    replay.Price = new PriceReplay { Price = price };
                    break;
                case Request.RequestOneofCase.AddGame:
                    await _gamesRepository.AddGame(request.AddGame.Game);
                    replay.Result = new OperationResultReplay {Result = true };
                    break;
                case Request.RequestOneofCase.DeleteGame:
                    await _gamesRepository.RemoveGame(request.DeleteGame.Game);
                    replay.Result = new OperationResultReplay { Result = true };
                    break;
            }
            return replay;
        }
    }
}
