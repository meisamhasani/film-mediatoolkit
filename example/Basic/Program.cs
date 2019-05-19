using MediaToolkit;
using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options.GIF;
using MediaToolkit.Options.Storyboard;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Basic
{
    public static class Program
    {
        private const string FFMPEG = @"E:\cmd\ffmpeg.exe";

        public static void Main(string[] args)
        {
            using (var engine = new MediaEngine(FFMPEG))
            {
                //var file = new MediaFile();
                //Storyboard(engine, @"E:\\00003.mp4");
                //Console.WriteLine("done");
                //engine.GetThumbnail(new ThumbnailOptions(1100, @"E:\\00003.mp4", @"\\192.168.20.100\Internal\Hashemi\film")
                //{
                //    Width = 320,
                //    Height = 240
                //});
           
                // HLS(engine, VideoQualities.P720_HD, inputPath, direcotry);
                //Console.WriteLine("Done");
                //var tasks = new List<Task>();
                //while (true)
                //{
                //    Console.Write("Enter input file path or X to exit:>");
                //    var inputPath = Console.ReadLine();
                //    if (inputPath == "X")
                //    {
                //        break;
                //    }
                //    var item = Task.Run(() =>
                //    {
                //        HLS(engine, VideoQualities.P720_HD, inputPath, Path.GetDirectoryName(inputPath));
                //    });

                //    tasks.Add(item);
                //}

                //Task.WhenAll(tasks).Wait();
                //Metadata(engine, @"d:\temp\1.mp4");

                //HealthCheck(engine, file);
                //GIF(engine);
                //Task.Run(() => GIF(engine));
                //Task.Run(() => GIF(engine));
                //Task.Run(() => GIF(engine)).wa;

                GIF(engine);
            }
        }

        private static void Storyboard(MediaEngine engine, string inputFile)
        {
            engine.GenerateStoryboard(inputFile, new StoryBoardOptions("D:\\temp\\test"));
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

            Console.WriteLine(input.Metadata.Duration);
        }

        private static void GIF(MediaEngine engine)
        {
            var param = EngineParameters.GIF(
                new MediaFile(@"D:\1.mp4"),
                new MediaFile($@"d:\\{Guid.NewGuid()}.gif"),
                GifGenerationOptions.Default());
            var result = CommandBuilder.GetGIF(param);
            Console.WriteLine(result);
            engine.GenerateGIF(param);
        }

        private static void HLS(MediaEngine engine, VideoQualities quality, string inputFile, string outputDirectory)
        {
            VideoResolutionsAttribute GetResolutionInfo(Enum value)
            {
                var filedInfo = value.GetType().GetField(value.ToString());

                var attrs = (VideoResolutionsAttribute[])filedInfo
                    .GetCustomAttributes(typeof(VideoResolutionsAttribute), false);

                return attrs[0];
            }

            var profile = GetResolutionInfo(quality);
            var hls = new HLSGeneratingOptions(outputDirectory, new FilterConfig
            {
                Height = profile.Height,
                Width = profile.Width,
                ForceOriginalRatio = ForceOriginalAspectRatio.Increase
            })
            {
                BitRateOptions = new BitRateOptions
                {
                    Audio = new BitRateOptions.AudioBitrate
                    {
                        Value = profile.Aduio_BitRateK * 1000
                    },
                    Video = new BitRateOptions.VideoBitrate
                    {
                        Value = profile.BitRate_HighMotionK * 1000,
                        Max = profile.MaxRateK * 1000,
                        BufferSize = profile.BufferSizeK * 1000
                    }
                }
            };

            var parameters = new EngineParameters
            {
                HLSOptions = hls,
                InputFile = new MediaFile(inputFile),
                Task = FFmpegTask.GenerateHLS
            };

            engine.GenerateHLS(parameters);
        }
    }
}
