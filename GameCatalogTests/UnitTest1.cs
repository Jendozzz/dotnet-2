using System;
using System.IO;
using Xunit;
using GameCatalogServer.Repositories;
using GameCatalog;
using System.Collections.Generic;

namespace GameCatalogTests
{
    public class UnitTests
    {
        /*private static GamesRepository CreateTestRepository()
        {
            Game game1 = new Game();
            game1.Name = "PUBG";
            game1.Price = 1337;
            var testGamesRepository = new GamesRepository();
            testGamesRepository.AddGame(game1);
            testGamesRepository.
            return testGamesRepository;
        }*/

        private static Game CreateTestGame()
        {
            Game game = new Game();
            game.Name = "PUBG";
            game.Price = 1337;
            
            return game;
        }

        [Fact]
        public void WriteToFileTest()
        {
            var gamesRepositoryTest = new GamesRepository();
            var game = CreateTestGame();
            var games = new List<Game>();
            games.Add(game);
            gamesRepositoryTest.WriteFile(games);
            Assert.True(File.Exists("database.json"));
        }

        [Fact]
        public async void ReadFromFileTest()
        {
            var gamesRepositoryTest = new GamesRepository();
            var game = CreateTestGame();
            var games = new List<Game>();
            games.Add(game);
            gamesRepositoryTest.WriteFile(games);
            var ReadFiles = await gamesRepositoryTest.ReadFile();
            Assert.True(ReadFiles.Count != 0);
        }

        [Fact]
        public async void AddGameTest()
        {
            var gamesRepositoryTest = new GamesRepository();
            var game = CreateTestGame();
            await gamesRepositoryTest.AddGame(game);
            Assert.True(await gamesRepositoryTest.CheckGame(game));
        }

        [Fact]
        public async void RemoveGameTest()
        {
            var gamesRepositoryTest = new GamesRepository();
            var game = CreateTestGame();
            await gamesRepositoryTest.AddGame(game);
            await gamesRepositoryTest.RemoveGame(game);
            Assert.True(!(await gamesRepositoryTest.CheckGame(game)));
        }

        [Fact]
        public async void GetAllTest()
        {
            var gamesRepositoryTest = new GamesRepository();
            var game = CreateTestGame();
            await gamesRepositoryTest.AddGame(game);
            await gamesRepositoryTest.GetAll();
            Assert.True(await gamesRepositoryTest.CheckGame(game));
        }

        [Fact]
        public async void GetGameByNameTest()
        {
            var gamesRepositoryTest = new GamesRepository();
            var game = CreateTestGame();
            await gamesRepositoryTest.AddGame(game);
            var result = await gamesRepositoryTest.GetGameByName(game.Name);
            Assert.True(result == game);
        }

        [Fact]
        public async void GetPriceByNameTest()
        {
            var gamesRepositoryTest = new GamesRepository();
            var game = CreateTestGame();
            await gamesRepositoryTest.AddGame(game);
            var result = await gamesRepositoryTest.GetPriceByName(game.Name);
            Assert.True(result == game.Price);
        }
    }
}
