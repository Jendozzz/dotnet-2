using GameCatalog;
using GameCatalogClient.Commands;
using GameCatalogClient.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogClient.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        Client _client;

        [Reactive]
        public ObservableCollection<Game> Games { get; set; } = new();

        [Reactive]
        public Game? SelectedGame { get; set; }
        public Command AddGameCommand { get; }
        public Command DeleteGameCommand { get; }
        public Command UpdateGameCommand { get; }

        public MainViewModel()
        {
            _client = new Client();

            AddGameCommand = new Command(_ =>
            {
                var gameAddView = new GameView(_client);
                gameAddView.ShowDialog();
                UpdateGames();
            }, null);

            DeleteGameCommand = new Command(_ =>
            {
                if (SelectedGame == null)
                    return;
                _client.DeleteGame(SelectedGame);
                UpdateGames();
            }, null);

            UpdateGameCommand = new Command(_ =>
            {
                if (SelectedGame == null)
                    return;
                var gameAddView = new GameView(_client, SelectedGame);
                var r = gameAddView.ShowDialog();
                if (r is null || (r is not null && r == false))
                    return;
                _client.DeleteGame(SelectedGame);
                UpdateGames();
            }, null);
        }

        public async Task UpdateGames()
        {
            Games.Clear();

            foreach (var g in (await _client.GetGames()).OrderBy(x => x.Name))
            {
                Games.Add(new Game() { Name = g.Name, Price = g.Price });
            }
        }
    }
}