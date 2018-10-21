namespace MediaToolkit.HLSOptions
{
    // -c:a aac -ar 48000 -b:a 128k
    public class AudioOptions
    {
        public AudioCodecs AudioCodec { get; set; } = AudioCodecs.AAC;
        public int AudioSampling { get; set; } = 48_000; // 48K HZ
        public int Bitrate { get; set; } = 128_000; // 128K
    }
}
