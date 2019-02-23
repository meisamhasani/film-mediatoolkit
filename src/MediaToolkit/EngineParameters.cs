using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit.Options.GIF;
using MediaToolkit.Options.Storyboard;
using MediaToolkit.Util;

namespace MediaToolkit
{
    public class EngineParameters
    {
        public bool HasCustomArguments => !this.CustomArguments.IsNullOrWhiteSpace();

        public StoryBoardOptions StoryBoardOptions { get; set; }
        public ThumbnailOptions ThumbnailOptions { get; set; }
        public ConversionOptions ConversionOptions { get; set; }
        public HLSGeneratingOptions HLSOptions { get; set; }
        public string CustomArguments { get; set; }
        public MediaFile InputFile { get; set; }
        public MediaFile OutputFile { get; set; }
        public FFmpegTask Task { get; set; }

        public static EngineParameters GIF(MediaFile input, MediaFile output, GifGenerationOptions options)
        {
            return new GIFParameters()
            {
                InputFile = input,
                OutputFile = output,
                GifOptions = options,
                Task = FFmpegTask.GIF
            };
        }
    }
}