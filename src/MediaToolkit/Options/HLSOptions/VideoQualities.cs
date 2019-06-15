#pragma warning disable RCS1197
// Hassan: Refer to https://docs.peer5.com/guides/production-ready-hls-vod/

namespace MediaToolkit.HLSOptions
{
    public enum VideoQualities
    {
        [VideoResolutions(Width = 264,
            Height = 144,
            BitRate_LowMotionK = 80,
            BitRate_HighMotionK = 100,
            Aduio_BitRateK = 80,
            BandWidthK = 200,
            BufferSizeK = 800,
            MaxRateK = 856)]
        P144 = 0,

        [VideoResolutions(Width = 426,
            Height = 240,
            BitRate_LowMotionK = 400,
            BitRate_HighMotionK = 600,
            Aduio_BitRateK = 64,
            BandWidthK = 240,
            BufferSizeK = 1000,
            MaxRateK = 856)]
        P240 = 1,

        [VideoResolutions(Width = 640,
            Height = 360,
            BitRate_LowMotionK = 700,
            BitRate_HighMotionK = 900,
            Aduio_BitRateK = 96,
            BandWidthK = 800,
            BufferSizeK = 1200,
            MaxRateK = 856)]
        P360 = 2,

        [VideoResolutions(Width = 854,
            Height = 480,
            BitRate_LowMotionK = 1250,
            BitRate_HighMotionK = 1600,
            Aduio_BitRateK = 128,
            BandWidthK = 1400,
            BufferSizeK = 2100,
            MaxRateK = 1498)]
        P480 = 3,

        [VideoResolutions(Width = 1280,
            Height = 720,
            BitRate_LowMotionK = 2500,
            BitRate_HighMotionK = 3200,
            Aduio_BitRateK = 128,
            BandWidthK = 2800,
            BufferSizeK = 4200,
            MaxRateK = 2996)]
        P720_HD = 4,

        [VideoResolutions(Width = 1280,
            Height = 720,
            BitRate_LowMotionK = 3500,
            BitRate_HighMotionK = 4400,
            Aduio_BitRateK = 128,
            BufferSizeK = 7500,
            MaxRateK = 5350)]
        P720_HD_60FPS = 5,

        [VideoResolutions(Width = 1920,
            Height = 1080,
            BitRate_LowMotionK = 4500,
            BitRate_HighMotionK = 5300,
            Aduio_BitRateK = 192,
            BandWidthK = 5000)]
        P1080_HD_FULL = 6,

        [VideoResolutions(Width = 1920,
            Height = 1080,
            BitRate_LowMotionK = 5800,
            BitRate_HighMotionK = 7400,
            Aduio_BitRateK = 192)]
        P1080_HD_FULL_60FPS = 7,

        [VideoResolutions(Width = 3840,
            Height = 2160,
            BitRate_LowMotionK = 14000,
            BitRate_HighMotionK = 18200,
            Aduio_BitRateK = 192)]
        FOUR_K = 8,

        [VideoResolutions(Width = 3840,
            Height = 2160,
            BitRate_LowMotionK = 23000,
            BitRate_HighMotionK = 29500,
            Aduio_BitRateK = 192)]
        FOUR_K_60FPS = 9
    }
}
#pragma warning restore RCS1197