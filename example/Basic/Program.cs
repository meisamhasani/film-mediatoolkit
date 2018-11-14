﻿using MediaToolkit;
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
                //var file = new MediaFile(@"E:\1.mp4");

                Metadata(engine, @"E:\00003.mp4");
                //HealthCheck(engine, file);
                //foreach (var item in file.Metadata.ErrorCollection)
                //{
                //    Console.WriteLine(item);
                //}
            }
        }

        private static void HealthCheck(MediaEngine engine, MediaFile input)
        {
            engine.HealthCheck(input);
        }

        private static void Metadata(MediaEngine engine, string path)
        {
            var input = new MediaFile(path);

            engine.GetMetadata(input);
        }

        private static void GIF(MediaEngine engine)
        {
            var param = EngineParameters.GIF(
                new MediaFile(@"E:\1.mp4"),
                new MediaFile(@"E:\output.gif"),
                GifGenerationOptions.Default());
            var result = CommandBuilder.GetGIF(param);
            Console.WriteLine(result);
            engine.GenerateGIF(param);
        }

        private static void HLS(MediaEngine engine)
        {
            var parameters = new EngineParameters()
            {
                HLSOptions = new HLSGeneratingOptions(@"D:\test\", "360p_%03d.ts", "360p.m3u8"),
                InputFile = new MediaFile(@"E:\1.mp4"),
                Task = FFmpegTask.GenerateHLS
            };

            engine.GenerateHLS(parameters);
        }
    }
}
