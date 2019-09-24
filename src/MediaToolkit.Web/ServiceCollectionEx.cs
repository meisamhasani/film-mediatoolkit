using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace MediaToolkit.Web
{
    public static class ServiceCollectionEx
    {
        public static void AddMediaEngine(this IServiceCollection services, string ffmpegPath)
        {
            if (string.IsNullOrEmpty(ffmpegPath))
            {
                throw new ArgumentNullException("ffmpeg path must be specified");
            }

            if (!File.Exists(ffmpegPath))
            {
                throw new InvalidOperationException("FFMPEG Path is not valid");
            }

            services.AddTransient(_ => new MediaEngine(ffmpegPath));
            services.AddTransient<IMediaEngine, InternalMediaEngine>();
        }
    }
}
