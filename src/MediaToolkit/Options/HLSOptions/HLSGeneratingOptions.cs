#pragma warning disable RCS1197
// Hassan: Refer to https://docs.peer5.com/guides/production-ready-hls-vod/

using System;
using System.IO;
using System.Text;

namespace MediaToolkit.HLSOptions
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class VideoResolutionsAttribute : Attribute
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int BitRate_LowMotionK { get; set; }
        public int BitRate_HighMotionK { get; set; }
        public int Aduio_BitRateK { get; set; }
    }

    public enum VideoQualities
    {
        [VideoResolutions(Width = 426,
            Height = 240,
            BitRate_LowMotionK = 400,
            BitRate_HighMotionK = 600,
            Aduio_BitRateK = 64)]
        P240,

        [VideoResolutions(Width = 640,
            Height = 360,
            BitRate_LowMotionK = 700,
            BitRate_HighMotionK = 900,
            Aduio_BitRateK = 96)]
        P360,

        [VideoResolutions(Width = 854,
            Height = 480,
            BitRate_LowMotionK = 1250,
            BitRate_HighMotionK = 1600,
            Aduio_BitRateK = 128)]
        P480,

        [VideoResolutions(Width = 1280,
            Height = 720,
            BitRate_LowMotionK = 2500,
            BitRate_HighMotionK = 3200,
            Aduio_BitRateK = 128)]
        P720_HD,

        [VideoResolutions(Width = 1280,
            Height = 720,
            BitRate_LowMotionK = 3500,
            BitRate_HighMotionK = 4400,
            Aduio_BitRateK = 128)]
        P720_HD_60FPS,

        [VideoResolutions(Width = 1920,
            Height = 1080,
            BitRate_LowMotionK = 4500,
            BitRate_HighMotionK = 5300,
            Aduio_BitRateK = 192)]
        P1080_HD_FULL,

        [VideoResolutions(Width = 1920,
            Height = 1080,
            BitRate_LowMotionK = 5800,
            BitRate_HighMotionK = 7400,
            Aduio_BitRateK = 192)]
        P1080_HD_FULL_60FPS,

        [VideoResolutions(Width = 3840,
            Height = 2160,
            BitRate_LowMotionK = 14000,
            BitRate_HighMotionK = 18200,
            Aduio_BitRateK = 192)]
        FOUR_K,

        [VideoResolutions(Width = 3840,
            Height = 2160,
            BitRate_LowMotionK = 23000,
            BitRate_HighMotionK = 29500,
            Aduio_BitRateK = 192)]
        FOUR_K_60FPS
    }

    public class HLSGeneratingOptions
    {
        public HLSGeneratingOptions(string basePath, FilterConfig config)
            : this(basePath, $"{config.Height}p_%03d.ts", $"{config.Height}p.m3u8")
        {
        }

        public HLSGeneratingOptions(string basePath, string hlsFileName, string playListFileName)
        {
            Guard.NotNullOrEmpty(hlsFileName, nameof(hlsFileName));
            Guard.NotNullOrEmpty(playListFileName, nameof(playListFileName));
            Guard.NotNullOrEmpty(basePath, nameof(basePath));

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            this.HLSSegementFileName = Path.Combine(basePath, hlsFileName);
            this.HLSPlayListFileName = Path.Combine(basePath, playListFileName);
        }

        public AudioOptions AudioOptions { get; set; } = new AudioOptions();
        public VideoOptions VideoOptions { get; set; } = new VideoOptions();
        public FilterConfig FilterOptions { get; set; } = new FilterConfig();
        public BitRateOptions BitRateOptions { get; set; } = new BitRateOptions();

        public int HLSTime { get; set; } = 8;
        public string HLSPlayListType { get; set; } = "vod";

        public string HLSSegementFileName { get; set; }
        public string HLSPlayListFileName { get; set; }

        public int ConstantRateFactor { get; set; } = 20;
        public int KeyFramePerFrame { get; set; } = 48;
        public string Speed { get; set; } = " ultrafast";
        public string DefaultParams => " -profile:v main";

        public string SerializeSpeed() => $" -speed {this.Speed}";

        public string SerializeHLSConfig()
        {
            return new StringBuilder()
                .Append($" -hls_time {this.HLSTime}")
                .Append($" -hls_playlist_type {this.HLSPlayListType}")
                .Append($" -hls_segment_filename {this.HLSSegementFileName} {this.HLSPlayListFileName}")
                .ToString();
        }

        public string SerializeBitrate()
        {
            string GetVideoBitrate()
            {
                return new StringBuilder()
                    .Append(" -b:v ").Append(this.BitRateOptions.Video.Value / 1000).Append("k")
                    .Append(" -maxrate ").Append(this.BitRateOptions.Video.Max / 1000).Append("k")
                    .Append(" -bufsize ").Append(this.BitRateOptions.Video.BufferSize / 1000).Append("k ")
                    .ToString();
            }

            string GetAudioBitrate() => $"-b:a {this.BitRateOptions.Audio.Value / 1000}k";
            return $"{GetVideoBitrate()} {GetAudioBitrate()}";
        }

        public string SerializeKeyframes()
        {
            return new StringBuilder()
                .Append($" -g {this.KeyFramePerFrame}")
                .Append($" -keyint_min {this.KeyFramePerFrame}")
                .Append(" -sc_threshold 0") //Ignore keyframe generation on scene change (only consider -g config"
                .ToString();
        }

        public string SerializeCRF() => $" -crf {this.ConstantRateFactor}";

        public string SerializeVideo()
        {
            return new StringBuilder()
                .Append($" -c:v {this.VideoOptions.VideoCodec.ToString().ToLower()}")
                .ToString();
        }

        public string SerializeAudio()
        {
            string Coded() => this.AudioOptions.AudioCodec.ToString().ToLower();
            string Sampling() => $" -ar {this.AudioOptions.AudioSampling}";

            return new StringBuilder()
                .Append(" -c:a ")
                .Append(Coded())
                .Append(Sampling())
                .ToString();
        }

        public string SerializeScale()
        {
            string WidthAndHeight()
                => $"scale=w={this.FilterOptions.Width}:h={this.FilterOptions.Height}";

            string ForceAspectRatio()
            {
                if (string.IsNullOrEmpty(this.FilterOptions.ForceOriginalRatio))
                {
                    return null;
                }

                return $"force_original_aspect_ratio={this.FilterOptions.ForceOriginalRatio}";
            }

            if (this.FilterOptions == null
                ||
                this.FilterOptions.Width == null
                ||
                this.FilterOptions.Height == null)
            {
                return null;
            }

            return new StringBuilder()
                .Append($" -vf {WidthAndHeight()}{( ForceAspectRatio() != null ? string.Concat(":", ForceAspectRatio()) : null )}")
                .ToString();
        }
    }
}
#pragma warning restore RCS1197