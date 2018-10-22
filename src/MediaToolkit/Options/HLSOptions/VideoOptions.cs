namespace MediaToolkit.HLSOptions
{
    public class FilterConfig
    {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string ForceOriginalRatio { get; set; } = "decrease";
    }

    // c: v
    public class VideoOptions
    {
        public VideoCodecs VideoCodec { get; set; } = VideoCodecs.H264;
    }
}
