using MediaToolkit;
using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options.GIF;
using System;

namespace Basic
{
    public static class Program
    {
        const string FFMPEG = @"E:\cmd\ffmpeg.exe";

        static void Main(string[] args)
        {
            using (var engine = new Engine(FFMPEG))
            {
                GIF(engine);
            }
        }

        private static void GIF(Engine engine)
        {
            var param = EngineParameters.GIF(
                new MediaFile(@"E:\1.mp4"),
                new MediaFile(@"E:\output.gif"),
                GifGenerationOptions.Default());
            var result = CommandBuilder.GetGIF(param);
            Console.WriteLine(result);
            engine.GenerateGIF(param);
        }

        private static void HLS(Engine engine)
        {
            var parameters = new EngineParameters()
            {
                HLSOptions = new HLSGeneratingOptions(@"D:\test\", "360p_%03d.ts", "360p.m3u8"),
                InputFile = new MediaFile(@"E:\World_s_Most_Breathtaking_Piano_Pieces_Contemporary_Music_Mix_Vol._1.135.mp4"),
                Task = FFmpegTask.GenerateHLS
            };

            engine.GenerateHLS(parameters);
        }
    }
}
