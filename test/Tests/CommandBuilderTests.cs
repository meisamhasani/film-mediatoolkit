using MediaToolkit;
using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options.GIF;
using Xunit;

namespace Tests
{
    public class CommandBuilderTests
    {
		private readonly string FFMPEG = @"E:\cmd\ffmpeg.exe";

		public CommandBuilderTests()
		{
		}

		[Theory]
		[InlineData(3720, @"E:\test\01.mp4", @"E:\test\out")]
		[InlineData(3024, @"E:\test\02.mp4", @"E:\test\out")]
		[InlineData(2176, @"E:\test\03.mp4", @"E:\test\out")]
		[InlineData(260, @"E:\test\04.mp4", @"E:\test\out")]
		[InlineData(54, @"E:\test\05.mp4", @"E:\test\out")]
		[InlineData(68, @"E:\test\06.mp4", @"E:\test\out")]
		[InlineData(61, @"E:\test\07.mp4", @"E:\test\out")]
		[InlineData(55, @"E:\test\08.mp4", @"E:\test\out")]
		[InlineData(131, @"E:\test\09.mp4", @"E:\test\out")]
		[InlineData(27, @"E:\test\10.mp4", @"E:\test\out")]
		[InlineData(17, @"E:\test\11.mp4", @"E:\test\out")]
		[InlineData(11, @"E:\test\12.mp4", @"E:\test\out")]
		[InlineData(21, @"E:\test\13.mp4", @"E:\test\out")]
		public void Should_Generate_TmbConfig(int duration, string inputPath, string outputDirectory)
		{
			// 60 sec = 20
			var ratio = duration / 3;
			var options = new ThumbnailOptions(ratio, inputPath, outputDirectory)
			{
			};

			using (var engine = new MediaEngine(FFMPEG))
			{
				engine.GetThumbnail(options);
			}
		}

		[Theory]
		[InlineData(@"E:\test\01.mp4", @"E:\test\out\output01.gif")]
		[InlineData(@"E:\test\02.mp4", @"E:\test\out\output02.gif")]
		[InlineData(@"E:\test\03.mp4", @"E:\test\out\output03.gif")]
		[InlineData(@"E:\test\04.mp4", @"E:\test\out\output04.gif")]
		[InlineData(@"E:\test\05.mp4", @"E:\test\out\output05.gif")]
		[InlineData(@"E:\test\06.mp4", @"E:\test\out\output06.gif")]
		[InlineData(@"E:\test\07.mp4", @"E:\test\out\output07.gif")]
		[InlineData(@"E:\test\08.mp4", @"E:\test\out\output08.gif")]
		[InlineData(@"E:\test\09.mp4", @"E:\test\out\output09.gif")]
		[InlineData(@"E:\test\10.mp4", @"E:\test\out\output10.gif")]
		[InlineData(@"E:\test\11.mp4", @"E:\test\out\output11.gif")]
		[InlineData(@"E:\test\12.mp4", @"E:\test\out\output12.gif")]
		[InlineData(@"E:\test\13.mp4", @"E:\test\out\output13.gif")]
		public void Should_Generate_GifConfig(string inputPath, string outputDirectory)
		{
			var options = EngineParameters.GIF(new MediaFile(inputPath), new MediaFile(outputDirectory), GifGenerationOptions.Default());

			using (var engine = new MediaEngine(FFMPEG))
			{
				engine.GenerateGIF(options);
			}
		}

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
