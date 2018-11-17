using System;
using System.Collections.Generic;

namespace MediaToolkit.Model
{
    public class Metadata
    {
        internal Metadata() => this.ErrorCollection = new List<string>();

        public TimeSpan Duration { get; internal set; }
        public Video VideoData { get; internal set; }
        public Audio AudioData { get; internal set; }
        public List<string> ErrorCollection { get; internal set; }

        public override string ToString()
        {
            return $"{this.Duration} {Environment.NewLine} {this.VideoData} {Environment.NewLine} {this.AudioData}";
        }

        public class Video
        {
            internal Video() { }
            public string Format { get; internal set; }
            public string ColorModel { get; internal set; }
            public string FrameSize { get; internal set; }
            public int? BitRateKbs { get; internal set; }
            public double Fps { get; internal set; }

            public int Width => int.Parse(this.FrameSize.Split('x')[0]);
            public int Height => int.Parse(this.FrameSize.Split('x')[1]);

            public override string ToString()
            {
                return $"Video) Format: {this.Format}," +
                    $" ColorModel: {this.ColorModel}, " +
                    $"FrameSize: {this.FrameSize}, " +
                    $"BitrateKBS: {this.BitRateKbs}, " +
                    $"FPS: {this.Fps}";
            }
        }

        public class Audio
        {
            internal Audio() { }

            public string Format { get; internal set; }
            public string SampleRate { get; internal set; }
            public string ChannelOutput { get; internal set; }
            public int BitRateKbs { get; internal set; }

            public override string ToString()
            {
                return $"Audio) Format: {this.Format}, " +
                    $"SampleRate: {this.SampleRate}, " +
                    $"ChannelOutput: {this.ChannelOutput}, " +
                    $"BitrateKBS: {this.BitRateKbs}";
            }
        }
    }
}
