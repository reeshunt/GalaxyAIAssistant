using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApp
{
    public abstract class InitializeServices
    {
        public static void Configure(IServiceCollection services)
        {


            // Register services
            services.AddScoped<ITeamsCommunicator, TeamsCommunicator>();
            services.AddScoped<IVoiceService, VoiceService>();



            //Register the Main Window Service
            services.AddSingleton<MainWindow>();
        }
    }
}
