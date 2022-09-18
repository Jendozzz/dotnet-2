using GameCatalog;
using GameCatalogClient.Commands;
using ReactiveUI.Fody.Helpers;
using System.Windows;

namespace GameCatalogClient.ViewModels
{
    public class GameViewModel
    {
        Game _game;
        Client _client;

        [Reactive]
        public string Name { 
            get => _game.Name; 
            set
            {
                _game.Name = value ;
            }
        }

        [Reactive]
        public int Price
        {
            get => _game.Price;
            set
            {
                _game.Price = value;
            }
        }

        public Command AddGameCommand { get; }

        public GameViewModel(Client client, Game game)
        {
            _client = client;
            _game = game;

            AddGameCommand = new Command(commandParameter =>
            {
                _client.AddGame(_game);
                var window = (Window)commandParameter;
                window.DialogResult = true;
                window.Close();
            }, null);
        }
    }
}
