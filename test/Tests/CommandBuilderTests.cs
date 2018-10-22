using MediaToolkit;
using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using Xunit;

namespace Tests
{
    public class CommandBuilderTests
    {
        [Fact]
        public void Should_Create_Default()
        {
            var parameters = new EngineParameters()
            {
                HLSOptions = new HLSGeneratingOptions(@"D:\test\", "360p_%03d.ts", "360p.m3u8"),
                InputFile = new MediaFile(@"E:\World_s_Most_Breathtaking_Piano_Pieces_Contemporary_Music_Mix_Vol._1.135.mp4"),
                Task = FFmpegTask.GenerateHLS
            };

            var result = CommandBuilder.GetHLS(parameters);
        }
    }
}
