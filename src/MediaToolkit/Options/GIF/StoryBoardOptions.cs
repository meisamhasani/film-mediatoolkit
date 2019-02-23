using System;
using System.IO;

namespace MediaToolkit.Options.Storyboard
{
    public sealed class StoryBoardOptions
    {
        public StoryBoardOptions(string outputDirectory)
        {
            Guard.NotNullOrEmpty(outputDirectory, nameof(outputDirectory));

            this.OutputDirectory = outputDirectory;
        }

        /// <summary>
        /// 0 to 10, where 0 is best quality
        /// </summary>
        public int Quality { get; set; } = 2;

        /// <summary>
        /// Number of cells in each row
        /// </summary>
        public int TileWidth { get; set; } = 5;

        /// <summary>
        /// Number of cells in each column
        /// </summary>
        public int TileHeight { get; set; } = 5;

        /// <summary>
        /// Dimension of each thumbnail, Defaults to 160x90
        /// </summary>
        public Rectangle ThumbnailSize { get; set; } = new Rectangle { Width = 160, Height = 90 };

        /// <summary>
        /// The directory in which thumbnails will be placed
        /// </summary>
        public string OutputDirectory { get; set; }

        /// <summary>
        /// Pattern foreach of thumbnail name
        /// </summary>
        public string OutputFileNamePattern { get; set; } = "board_%d.jpg";

        public string Serialize()
        {
            return $" -vf " +
                $"\"fps={1}, scale={this.ThumbnailSize.Width}:{this.ThumbnailSize.Height}, tile={this.TileWidth}*{this.TileHeight}\"" +
                $" -qscale:v {this.Quality}" +
                $" {Path.Combine(OutputDirectory, this.OutputFileNamePattern)}";
        }
    }
}

// base type: ffmpeg -i e:\1.mp4 -ss 00:00:00.000 -pix_fmt rgb24 -r 10 -s 320x180 -t 00:00:06.000 e:\output.gif