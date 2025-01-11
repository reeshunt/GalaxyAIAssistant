using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.CognitiveServices.Speech;


namespace Infrastructure.Services
{
    public class VoiceService : IVoiceService
    {
        private readonly string _speechKey = "JPcOJgBEw8yxf05cOwOXNUtF53WkrFbGZxRXfLeQxIu04TYJ7YBUJQQJ99ALACqBBLyXJ3w3AAAYACOGnBc5";
        private readonly string _speechRegion = "southeastasia";
        private SpeechRecognizer? _recognizer;

        public async Task StartSpeechRecognitionAsync()
        {
            var speechConfig = SpeechConfig.FromSubscription(_speechKey, _speechRegion);
            _recognizer = new SpeechRecognizer(speechConfig);

            // Subscribe to the recognized event
            _recognizer.Recognized += OnSpeechRecognized;

            // Start continuous recognition
            await _recognizer.StartContinuousRecognitionAsync();

        }

        public async Task StopSpeechRecognitionAsync()
        {
            if (_recognizer != null)
            {
                // Stop continuous recognition
                await _recognizer.StopContinuousRecognitionAsync();

                // Unsubscribe from the recognized event
                _recognizer.Recognized -= OnSpeechRecognized;

                // Dispose of the recognizer
                _recognizer.Dispose();
                _recognizer = null;
            }
        }

        // Separate method for handling recognized events
        private void OnSpeechRecognized(object sender, SpeechRecognitionEventArgs e)
        {
            if (e.Result.Reason == ResultReason.RecognizedSpeech)
            {
                string recognizedCommand = e.Result.Text;
                ProcessVoiceCommand(recognizedCommand);
            }
            else
            {
                // Handle cases where no speech is recognized
                Console.WriteLine("No speech recognized.");
            }
        }

        // Method to process the voice command
        public void ProcessVoiceCommand(string command)
        {
            switch (command.ToLower())
            {
                case "play":
                    Console.WriteLine("play command recognized");
                    break;
                case "pause":
                    Console.WriteLine("pause command recognized");
                    break;
                case "stop":
                    Console.WriteLine("stop command recognized");
                    break;
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }
    }

}
