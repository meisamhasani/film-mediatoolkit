using MediaToolkit;
using MediaToolkit.HLSOptions;
using MediaToolkit.Model;

namespace Basic
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parameters = new EngineParameters()
            {
                HLSOptions = new HLSGeneratingOptions(@"D:\test\", "360p_%03d.ts", "360p.m3u8"),
                InputFile = new MediaFile(@"E:\World_s_Most_Breathtaking_Piano_Pieces_Contemporary_Music_Mix_Vol._1.135.mp4"),
                Task = FFmpegTask.GenerateHLS
            };

            using (var engine = new Engine(@"E:\cmd\ffmpeg.exe"))
            {
                engine.GenerateHLS(parameters);
            }
        }
    }
}
