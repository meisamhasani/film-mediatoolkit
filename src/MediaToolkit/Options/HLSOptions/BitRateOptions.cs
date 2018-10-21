namespace MediaToolkit.HLSOptions
{
    public class BitRateOptions
    {
        public class VideoBitrate
        {
            public int Value { get; set; } = 2_500_000;
            public int Max { get; set; } = 2675_000;
            public int BufferSize { get; set; } = 3750_000;
        }

        public class AudioBitrate
        {
            public int Vlaue { get; set; } = 128 * 1000;
        }

        public AudioBitrate Audio { get; set; } = new AudioBitrate();
        public VideoBitrate Video { get; set; } = new VideoBitrate();
    }
}
