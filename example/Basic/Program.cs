using MediaToolkit;
using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options.GIF;
using System;

namespace Basic
{
    public static class Program
    {
        private const string FFMPEG = @"E:\cmd\ffmpeg.exe";

        public static void Main(string[] args)
        {
            using (var engine = new MediaEngine(FFMPEG))
            {
                var file = new MediaFile();
                engine.GetThumbnail(new ThumbnailOptions(1100, @"E:\\00003.mp4", @"\\192.168.20.100\Internal\Hashemi\film")
                {
                    Width = 320,
                    Height = 240
                });

                Console.WriteLine("Done");

                //HLS(engine);
                //Metadata(engine, @"E:\1.mp4");
                //HealthCheck(engine, file);
                //GIF(engine);
                //Task.Run(() => GIF(engine));
                //Task.Run(() => GIF(engine));
                //Task.Run(() => GIF(engine));

                //GIF(engine);
            }
        }

        private static void HealthCheck(MediaEngine engine, MediaFile input)
        {
            engine.HealthCheck(input);
            foreach (var item in input.Metadata.ErrorCollection)
            {
                Console.WriteLine(item);
            }
        }

        private static void Metadata(MediaEngine engine, string path)
        {
            var input = new MediaFile(path);

            engine.GetMetadata(input);
        }

        private static void GIF(MediaEngine engine)
        {
            var param = EngineParameters.GIF(
                new MediaFile(@"E:\00003.mp4"),
                new MediaFile($@"E:\\{Guid.NewGuid()}.gif"),
                GifGenerationOptions.Default());
            var result = CommandBuilder.GetGIF(param);
            Console.WriteLine(result);
            engine.GenerateGIF(param);
        }

        private static void HLS(MediaEngine engine)
        {
            var parameters = new EngineParameters()
            {
                HLSOptions = new HLSGeneratingOptions(@"D:\test\", new FilterConfig
                {
                    Width = 640,
                    Height = 360
                }),
                InputFile = new MediaFile(@"E:\1.mp4"),
                Task = FFmpegTask.GenerateHLS
            };

            engine.GenerateHLS(parameters);
        }
    }
}
