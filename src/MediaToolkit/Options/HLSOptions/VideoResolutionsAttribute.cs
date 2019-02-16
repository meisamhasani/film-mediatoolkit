#pragma warning disable RCS1197
// Hassan: Refer to https://docs.peer5.com/guides/production-ready-hls-vod/

using System;

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
        public int BandWidthK { get; set; }
        public int BufferSizeK { get; set; }
        public int MaxRateK { get; set; }
    }
}
#pragma warning restore RCS1197