using System;
using System.IO;
using System.Text;

namespace MediaToolkit.HLSOptions
{
    public class HLSGeneratingOptions
    {
        public HLSGeneratingOptions(string basePath, string hlsFileName, string playListFileName)
        {
            Guard.NotNullOrEmpty(basePath, nameof(basePath));
            Guard.NotNullOrEmpty(hlsFileName, nameof(hlsFileName));
            Guard.NotNullOrEmpty(playListFileName, nameof(playListFileName));

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

        public int HLSTime { get; set; } = 8;
        public string HLSPlayListType { get; set; }
        public string HLSSegementFileName { get; set; }
        public string HLSPlayListFileName { get; set; }
        public int ConstantRateFactor { get; set; } = 20;
        public int KeyFramePerFrame { get; set; } = 48;
        public bool CreateKeyFrameOnScneneChange { get; set; } = false;
        public string Speed { get; set; } = "ultrafast";
        public string DefaultParams => "-profile:v main -sc_threshold 0";

        /// /////////////////// // تا -c:a اضافه شد
        /// ادامه ی کار بیت ریت و تنظیمات ویدئو
        private string SerializeVideo()
        {
            return new StringBuilder()
                .Append($" -c:v {this.VideoOptions.VideoCodec.ToString().ToLower()}")
                .ToString();
        }

        private string SerializeAudio()
        {
            string GetCoded() => this.AudioOptions.AudioCodec.ToString().ToLower();
            string Sampling() => $" -ar {this.AudioOptions.AudioSampling}";

            return new StringBuilder()
                .Append($"-c:a ")
                .Append(GetCoded())
                .Append(Sampling())
                .ToString();
        }

        private string SerializeScale()
        {
            string GetWidthAndHeight()
            {
                if (this.FilterOptions == null || this.FilterOptions.Width == null || this.FilterOptions.Height == null)
                {
                    return null;
                }

                return $"scale=w={this.FilterOptions.Width}:h={this.FilterOptions.Height}";
            }

            string GetForceAspectRatio()
            {
                if (this.FilterOptions == null)
                {
                    return null;
                }

                return $"force_original_aspect_ratio={this.FilterOptions.ForceOriginalRatio}";
            }

            return new StringBuilder()
                .Append($" -vf {GetWidthAndHeight()} {( GetForceAspectRatio() != null ? string.Concat(":", GetForceAspectRatio()) : null )}")
                .ToString();
        }
    }
}
