using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options.GIF;
using MediaToolkit.Options.Storyboard;
using System.Threading.Tasks;

namespace MediaToolkit.Web
{
    public interface IMediaEngine
    {
        Task Storyboard(string inputFile, StoryBoardOptions options);
        Task<Metadata> Metadata(string path);
        Task<bool> IsValid(string physicalPath);
        Task GenerateHLS(EngineParameters parameters);
        Task Thumbnail(ThumbnailOptions options);
        Task Gif(string inputFile, string outputFile, GifGenerationOptions options = null);
    }
}
