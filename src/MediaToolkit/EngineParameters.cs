using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit.Options.GIF;
using MediaToolkit.Util;

namespace MediaToolkit
{
    public class EngineParameters
    {
        public bool HasCustomArguments => !this.CustomArguments.IsNullOrWhiteSpace();

        /// <summary>   Gets or sets options for controlling the conversion. </summary>
        /// <value> Options that control the conversion. </value>
        public ConversionOptions ConversionOptions { get; set; }

        public HLSGeneratingOptions HLSOptions { get; set; }

        public string CustomArguments { get; set; }

        /// <summary>   Gets or sets the input file. </summary>
        /// <value> The input file. </value>
        public MediaFile InputFile { get; set; }

        /// <summary>   Gets or sets the output file. </summary>
        /// <value> The output file. </value>
        public MediaFile OutputFile { get; set; }

        /// <summary>   Gets or sets the task. </summary>
        /// <value> The task. </value>
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