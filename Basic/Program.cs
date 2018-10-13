using MediaToolkit;
using MediaToolkit.Model;
using System;

namespace Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = new MediaFile { Filename = @"e:\1.mp4" };

            using (var engine = new Engine(@"E:\cmd\ffmpeg.exe"))
            {
                engine.GetMetadata(inputFile);
            }

            Console.WriteLine(inputFile.Metadata.Duration);
        }
    }
}
