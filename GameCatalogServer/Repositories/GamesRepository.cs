using GameCatalog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameCatalogServer.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private const string _filePath = "database.json";
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<List<Game>> GetAll()
        {
            await _semaphore.WaitAsync();
            try
            {
                return await ReadFile();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<Game?> GetGameByName(string name)
        {
            await _semaphore.WaitAsync();
            try
            {
                var games = await ReadFile();
                return games.FirstOrDefault(x => x.Name == name);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<int> GetPriceByName(string name)
        {
            await _semaphore.WaitAsync();
            try
            {
                var games = await ReadFile();
                return games.First(x => x.Name == name).Price;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task AddGame(Game game)
        {
            await _semaphore.WaitAsync();
            try
            {
                var games = await ReadFile();
                if (games.Contains(game))
                    return;
                games.Add(game);
                WriteFile(games);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task RemoveGame(string gameName)
        {
            await _semaphore.WaitAsync();
            try
            {
                var games = await ReadFile();
                var game = games.FirstOrDefault(x => x.Name == gameName);
                if (game is null)
                    return;
                games.Remove(game);
                WriteFile(games);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task RemoveAll()
        {
            await _semaphore.WaitAsync();
            try
            {
                var games = await ReadFile(); 
                games.Clear();
                WriteFile(games);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private static async Task<List<Game>> ReadFile()
        {
            if (!File.Exists(_filePath))
                File.Create(_filePath).Close();
            string jsonString = await File.ReadAllTextAsync(_filePath);
            var games = jsonString.Length != 0 ? JsonSerializer.Deserialize<List<Game>>(jsonString) : new List<Game>();
            if (games is null)
                throw new IOException($"File {_filePath} cannot be read");
            return games;
        }

        private static async void WriteFile(List<Game> games)
        {
            using FileStream createStream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(createStream, games);
            await createStream.DisposeAsync();
        }
    }
}
