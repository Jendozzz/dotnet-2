using GameCatalog;
using GameCatalogClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCatalogClient.Views
{
    public partial class GameView 
    {
        public GameView(Client client) : this(client, new Game())
        {
        }

        public GameView(Client client, Game game)
        {
            Icon = FindResource("PlusIcon") as System.Windows.Media.ImageSource;

            InitializeComponent();

            ViewModel = new GameViewModel(client, game);
        }
    }
}
