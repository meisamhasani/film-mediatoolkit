#pragma warning disable RCS1197
// Hassan: Refer to https://docs.peer5.com/guides/production-ready-hls-vod/

using Extensions;
using System;
using System.IO;
using System.Text;

namespace MediaToolkit.HLSOptions
{
    public class HLSGeneratingOptions
    {
        public HLSGeneratingOptions(string basePath, FilterConfig config)
            : this(basePath, $"{config.Height}p_%03d.ts", $"{config.Height}p.m3u8")
        {
            this.FilterOptions = config;
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

        public int HLSTime { get; set; } = 4;
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
                // TODO: this throws 
                return null;
                var aspectRatio = EnumExtensions.GetEnumDescription(this.FilterOptions.ForceOriginalRatio);
                Guard.NotNullOrEmpty(aspectRatio, nameof(aspectRatio));

                return $"force_original_aspect_ratio={aspectRatio}";
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