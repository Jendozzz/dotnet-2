using Grpc.Net.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCatalog;

namespace GameCatalogClient
{
    public class Client
    {
        string _address = "http://localhost:5000";
        GameCatalogService.GameCatalogServiceClient _client;

        public Client()
        {
            var _channel = GrpcChannel.ForAddress(_address);
             _client = new GameCatalogService.GameCatalogServiceClient(_channel);
        }

        public async Task<IEnumerable<Game>> GetGames() 
        {
            var gamesRequest = new GetGamesRequest();
            var reply = _client.Connect(new Request { GetGames = gamesRequest });
            return reply.Games.Games;
        }

        public async Task UpdateGame(Game game)
        {
            var gamesRequest = new GetGamesRequest();
            var reply = _client.Connect(new Request { GetGames = gamesRequest });
        }

        public void AddGame(Game game)
        {
            var addGameRequest = new AddGameRequest() { Game = game };
            var reply = _client.Connect(new Request { AddGame = addGameRequest });
        }

        public void DeleteGame(Game game)
        {
            var deleteGameRequest = new DeleteGameRequest() { Game = game };
            var reply = _client.Connect(new Request { DeleteGame = deleteGameRequest });
        }
    }
}
