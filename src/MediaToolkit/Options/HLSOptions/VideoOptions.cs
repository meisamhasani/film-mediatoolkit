namespace MediaToolkit.HLSOptions
{
    public class FilterConfig
    {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public ForceOriginalAspectRatio ForceOriginalRatio { get; set; } = ForceOriginalAspectRatio.Decrease;
    }

    // c: v
    public class VideoOptions
    {
        public VideoCodecs VideoCodec { get; set; } = VideoCodecs.H264;
    }
}
