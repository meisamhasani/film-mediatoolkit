using System;

namespace MediaToolkit.Options.GIF
{
    public class GifGenerationOptions
    {
        public TimeSpan StartTime { get; set; } = new TimeSpan(0, 0, 0);
        public TimeSpan StopTime { get; set; } = new TimeSpan(0, 0, 3);
        public string PixelFormat { get; set; } = "rgb24";
        public int FrameRate { get; set; } = 10;
        public Rectangle Dimension { get; set; } = new Rectangle(320, 180);

        public static GifGenerationOptions Default() => new GifGenerationOptions();
        public string Serialize()
            => this.SerializeTime()
                + this.SerializePixelFormat()
                + this.SerializeFrameRate()
                + this.SerializeSize();

        private string SerializeTime() => $" -ss {this.StartTime.ToString()} -t {this.StopTime.ToString()}";
        private string SerializePixelFormat() => $" -pix_fmt {this.PixelFormat}";
        private string SerializeFrameRate() => $" -r {this.FrameRate}";
        private string SerializeSize() => $" -s {this.Dimension}";
    }
}

// base type: ffmpeg -i e:\1.mp4 -ss 00:00:00.000 -pix_fmt rgb24 -r 10 -s 320x180 -t 00:00:06.000 e:\output.gif