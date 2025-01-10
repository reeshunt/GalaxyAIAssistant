using System.Windows;
using System.Windows.Media.Animation;

namespace WindowsApp
{
    public partial class MainWindow : Window
    {
        private bool isMenuExpanded = false;

        public MainWindow()
        {
            InitializeComponent();
            PositionOnRightCenter();
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
        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleMenuState();
        }

        // Toggle menu state: expand or collapse
        private void ToggleMenuState()
        {
            var storyboardKey = isMenuExpanded ? "CollapseMenu" : "ExpandMenu";
            var storyboard = (Storyboard)Resources[storyboardKey];
            storyboard.Begin();

            // Toggle the menu state
            isMenuExpanded = !isMenuExpanded;
        }
    }
}
