namespace MediaToolkit.HLSOptions
{
    public class BitRateOptions
    {
        public class VideoBitrate
        {
            /// <summary>
            /// This value will be divied when serializing
            /// </summary>
            public int Value { get; set; } = 2_500_000;

            /// <summary>
            /// This value will be divied when serializing
            /// </summary>
            public int Max { get; set; } = 2675_000;

            /// <summary>
            /// This value will be divied when serializing
            /// </summary>
            public int BufferSize { get; set; } = 3750_000;
        }

        public class AudioBitrate
        {
            /// <summary>
            /// This value will be divied when serializing
            /// </summary>
            public int Value { get; set; } = 128 * 1000;
        }

        public AudioBitrate Audio { get; set; } = new AudioBitrate();
        public VideoBitrate Video { get; set; } = new VideoBitrate();
    }
}
