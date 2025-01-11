using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IVoiceService
    {
        void ProcessVoiceCommand(string command);
        Task StartSpeechRecognitionAsync();
        Task StopSpeechRecognitionAsync();
    }
}
