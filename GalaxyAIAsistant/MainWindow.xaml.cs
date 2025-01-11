using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Media.Animation;
using System.Media;
using System.IO;
using Microsoft.CognitiveServices.Speech;


namespace WindowsApp
{
    public partial class MainWindow : Window
    {
        private bool isMenuExpanded = false;
        //private bool isListening = false;
        private readonly ITeamsCommunicator _teamsService;
        private readonly IVoiceService _voiceService;

        private TranscribeWindow transcribeWindow;

        public MainWindow(ITeamsCommunicator teamsService,IVoiceService voiceService)
        {
            InitializeComponent();
            PositionOnRightCenter();
            InitializeTranscribeWindow();

            _teamsService = teamsService;
            _voiceService = voiceService;
        }

        

        // Method to toggle the menu expansion and collapse
        private async void MainButton_Click(object sender, RoutedEventArgs e)
        {
            await ToggleMenuState();
        }


       


        private async void VoiceRecog(object sender, RoutedEventArgs e)
        {
            //Reverse the state of the listening
            //isListening = !isListening;

            // Alert sound to notify user that the app is listening
            await AlertSound("wakeup");
            // Start listening for speech
            //if(isListening)
                await _voiceService.StartSpeechRecognitionAsync();
            //else
            //await _voiceService.StopSpeechRecognitionAsync();
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

        
        private void InitializeTranscribeWindow()
        {
            transcribeWindow = new TranscribeWindow();

            // Position the SpeechWindow relative to the main button
            transcribeWindow.Left = this.Left + 110; // Adjust as needed
            transcribeWindow.Top = this.Top + 200;  // Adjust as needed
        }
        private async Task AlertSound(string position)
        {
            if (!string.IsNullOrEmpty(position))
            {
                SoundPlayer player = new SoundPlayer();

                switch (position)
                {
                    case "wakeup":
                        player = new SoundPlayer($"{Directory.GetCurrentDirectory()}\\wakeup.wav");
                        break;
                    case "sleep":
                        player = new SoundPlayer($"{Directory.GetCurrentDirectory()}\\sleep.wav");
                        break;
                    default:
                        break;
                }
                try
                {
                    // Play the sound asynchronously
                    await Task.Run(() => player.PlaySync());
                }
                catch (Exception ex)
                {
                    // Handle any errors (e.g., file not found)
                    MessageBox.Show("Error playing sound: " + ex.Message);
                }
            }
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

