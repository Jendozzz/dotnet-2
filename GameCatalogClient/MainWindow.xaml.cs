using GameCatalogClient.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GameCatalogClient
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            Icon = FindResource("AppIcon") as System.Windows.Media.ImageSource;

            InitializeComponent();
            ViewModel = new MainViewModel();

            ViewModel.UpdateGames().Wait();
        }
    }
}

