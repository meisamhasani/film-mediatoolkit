using MediaToolkit;
using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options.GIF;
using Xunit;

namespace Tests
{
    public class CommandBuilderTests
    {
        [Fact]
        public void Should_Create_Default_HLS()
        {
            var parameters = new EngineParameters()
            {
                HLSOptions = new HLSGeneratingOptions(@"D:\test\", "360p_%03d.ts", "360p.m3u8"),
                InputFile = new MediaFile(@"E:\World_s_Most_Breathtaking_Piano_Pieces_Contemporary_Music_Mix_Vol._1.135.mp4"),
                Task = FFmpegTask.GenerateHLS
            };

            var result = CommandBuilder.GetHLS(parameters);
        }

        [Fact]
        public void Should_Create_GIF()
        {
            var parameters = new EngineParameters()
            {
                HLSOptions = new HLSGeneratingOptions(@"D:\test\", "360p_%03d.ts", "360p.m3u8"),
                InputFile = new MediaFile(@"E:\World_s_Most_Breathtaking_Piano_Pieces_Contemporary_Music_Mix_Vol._1.135.mp4"),
                Task = FFmpegTask.GenerateHLS
            };

            EngineParameters.GIF(
                new MediaFile(@"E:\1.mp4"),
                new MediaFile(@"E:\output.gif"),
                GifGenerationOptions.Default());

            var result = CommandBuilder.GetHLS(parameters);
        }
    }
}
