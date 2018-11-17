#pragma warning disable RCS1197
// Hassan: Refer to https://docs.peer5.com/guides/production-ready-hls-vod/


namespace MediaToolkit.HLSOptions
{
    public enum VideoQualities
    {
        [VideoResolutions(Width = 426,
            Height = 240,
            BitRate_LowMotionK = 400,
            BitRate_HighMotionK = 600,
            Aduio_BitRateK = 64)]
        P240 = 1,

        [VideoResolutions(Width = 640,
            Height = 360,
            BitRate_LowMotionK = 700,
            BitRate_HighMotionK = 900,
            Aduio_BitRateK = 96)]
        P360 = 2,

        [VideoResolutions(Width = 854,
            Height = 480,
            BitRate_LowMotionK = 1250,
            BitRate_HighMotionK = 1600,
            Aduio_BitRateK = 128)]
        P480 = 3,

        [VideoResolutions(Width = 1280,
            Height = 720,
            BitRate_LowMotionK = 2500,
            BitRate_HighMotionK = 3200,
            Aduio_BitRateK = 128)]
        P720_HD = 4,

        [VideoResolutions(Width = 1280,
            Height = 720,
            BitRate_LowMotionK = 3500,
            BitRate_HighMotionK = 4400,
            Aduio_BitRateK = 128)]
        P720_HD_60FPS = 5,

        [VideoResolutions(Width = 1920,
            Height = 1080,
            BitRate_LowMotionK = 4500,
            BitRate_HighMotionK = 5300,
            Aduio_BitRateK = 192)]
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