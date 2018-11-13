using System.Threading.Tasks;

namespace MediaToolkit.Web
{
    public interface IMediaEngine
    {
        Task<bool> IsValid(string physicalPath);
        Task GenerateHLS(EngineParameters parameters);
    }
}
