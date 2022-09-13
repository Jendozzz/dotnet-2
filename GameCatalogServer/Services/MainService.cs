using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalog;
using System.Runtime.InteropServices;
using GameCatalogServer.Repositories;

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
            var games =  await _gamesRepository.GetAll();
            replay.Games = new GamesReplay { Games = { games } };

            return replay;
        }
        public override async Task<Reply> GetGameByName(Request request, ServerCallContext context)
        {
            Reply replay = new Reply { };
            var game = await _gamesRepository.GetGameByName(request.GetGame.Name);
            replay.Games = new GamesReplay { Games = { game } };

            return replay;
        }
        public override async Task<Reply> GetPriceByName(Request request, ServerCallContext context)
        {
            Reply replay = new Reply { };
            var price = await _gamesRepository.GetPriceByName(request.GetPrice.Name);
            replay.Price = new PriceReplay{ Price = price };

            return replay;
        }
        public override async Task<Reply> AddGame(Request request, ServerCallContext context)
        {
            Reply replay = new Reply { };
            await _gamesRepository.AddGame(request.AddGame.Game);

            return replay;
        }

        public override async Task<Reply> RemoveGame(Request request, ServerCallContext context)
        {
            Reply replay = new Reply { };
            await _gamesRepository.RemoveGame(request.RemGame.Name);

            return replay;
        }

        public override async Task<Reply> RemoveAll(Request request, ServerCallContext context)
        {
            Reply replay = new Reply { };
            await _gamesRepository.RemoveAll();

            return replay;
        }
    }
}

