using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Media.Animation;

namespace WindowsApp
{
    public partial class MainWindow : Window
    {
        private bool isMenuExpanded = false;
        private readonly ITeamsCommunicator _teamsService;

        public MainWindow(ITeamsCommunicator teamsService)
        {
            InitializeComponent();
            PositionOnRightCenter();
            _teamsService = teamsService;
        }

        // Method to position window on the right-center of the screen
        private void PositionOnRightCenter()
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            // Set window position: 10px margin from the right edge, vertically centered
            this.Left = screenWidth - this.Width;
            this.Top = (screenHeight - this.Height) / 2;
        }

        // Method to toggle the menu expansion and collapse
        private async void MainButton_Click(object sender, RoutedEventArgs e)
        {
            await _teamsService.GetAllMessagesAsync(
                "19%3A8bac10eaf382472483700247341adfaa%40thread.skype"
                , "19%3Aa0f7b452794e4ed099dd23e9efa6fdba%40thread.skype"
                );
            await ToggleMenuState();
        }

        // Toggle menu state: expand or collapse
        private async Task ToggleMenuState()
        {
            var storyboardKey = isMenuExpanded ? "CollapseMenu" : "ExpandMenu";
            var storyboard = (Storyboard)Resources[storyboardKey];
            storyboard.Begin();

            // Toggle the menu state
            isMenuExpanded = !isMenuExpanded;
        }
    }
}

