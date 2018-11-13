using MediaToolkit.Model;
using System.Threading.Tasks;

namespace MediaToolkit.Web
{
    public class InternalMediaEngine : IMediaEngine
    {
        private readonly MediaEngine _engine;

        public InternalMediaEngine(MediaEngine engine)
        {
            this._engine = engine;
        }

        public Task<bool> IsValid(string physicalPath)
        {
            // To prevent deadlock, we should not block aspnet thread
            return Task.Run(() =>
            {
                var input = new MediaFile(physicalPath);
                this._engine.HealthCheck(input);

                return input.Metadata.ErrorCollection.Count == 0;
            });
        }
    }
}
