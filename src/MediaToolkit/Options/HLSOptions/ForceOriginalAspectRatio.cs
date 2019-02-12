#pragma warning disable RCS1197
// Hassan: Refer to https://docs.peer5.com/guides/production-ready-hls-vod/

using System.ComponentModel;

namespace MediaToolkit.HLSOptions
{
    public enum ForceOriginalAspectRatio
    {
        [Description("increase")]
        Increase,

        [Description("decrease")]
        Decrease
    }
}
#pragma warning restore RCS1197