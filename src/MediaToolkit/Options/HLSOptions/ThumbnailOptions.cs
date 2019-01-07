#pragma warning disable RCS1197
// Hassan: Refer to https://docs.peer5.com/guides/production-ready-hls-vod/

using System;
using System.IO;

namespace MediaToolkit.HLSOptions
{
    public sealed class ThumbnailOptions
    {
        /// <summary>
        /// Initialize a new instance of thumbnail options.
        /// </summary>
        /// <param name="frameRateRatio">Usually we have seconds/outputcount.</param>
        /// <param name="input">The path to input video file.</param>
        /// <param name="output">The directory in which thumbnails will be placed.</param>
        public ThumbnailOptions(
            int frameRateRatio,
            string input,
            string output)
        {
            Guard.Positive(frameRateRatio, nameof(frameRateRatio));
            Guard.NotNullOrEmpty(input, nameof(input));
            Guard.NotNullOrEmpty(output, nameof(output));

            this.FrameRateRatio = frameRateRatio;
            this.InputFile = input;
            this.OutputDirectory = output;
        }

        public string InputFile { get; private set; }
        public string OutputDirectory { get; private set; }

        // Discard first n frame to skip logos and other stuff
        public int Discards { get; private set; } = 3;

        public double FrameDiff { get; set; } = 0.4;
        public int OutputCount { get; set; } = 3;

        public int Width { get; set; }
        public int Height { get; set; }

        public string OutputFiles
        {
            get
            {
                return Path.Combine(this.OutputDirectory, this.OutputPath);
            }
        }

        public string OutputPath
        {
            get
            {
                var outputFileName =
                        Path.GetFileNameWithoutExtension(this.InputFile);

                return $"{outputFileName}%02d.jpg";
            }
        }

        public int FrameRateRatio { get; }

        public string Serialize()
        {
            return $"-ss {this.Discards}" +
                $" -i {this.InputFile}" +
                $" -vf \"select=gt(scene\\,{this.FrameDiff})\" " +
                $" -frames:v {this.OutputCount} " +
                $" -vsync vfr " +
                $" -vf fps=fps=1/{this.FrameRateRatio},scale=w={this.Width}:h={this.Height}:force_original_aspect_ratio=decrease" +
                $" {this.OutputFiles}";
        }
    }
}
#pragma warning restore RCS1197