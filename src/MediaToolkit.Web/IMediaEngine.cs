using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using System.Threading.Tasks;

namespace MediaToolkit.Web
{
    public interface IMediaEngine
    {
        Task<Metadata> Metadata(string path);
        Task<bool> IsValid(string physicalPath);
        Task GenerateHLS(EngineParameters parameters);
        Task Thumbnail(ThumbnailOptions options);
    }
}
