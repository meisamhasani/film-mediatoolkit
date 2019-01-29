using MediaToolkit.HLSOptions;
using MediaToolkit.Model;
using MediaToolkit.Options.GIF;
using System;
using System.IO;
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

        public async Task<Metadata> Metadata(string path)
        {
            Guard.NotNullOrEmpty(path, nameof(path));

            var fileInfo = new MediaFile(path);
            await Task.Run(() => _engine.GetMetadata(fileInfo));

            return fileInfo.Metadata;
        }

        public Task GenerateHLS(EngineParameters parameters)
        {
            Guard.NotNull(parameters, nameof(parameters));

            return Task.Run(() => _engine.GenerateHLS(parameters));
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

        public Task Thumbnail(ThumbnailOptions options)
        {
            Guard.NotNull(options, nameof(options));

            if (!Directory.Exists(options.OutputDirectory))
            {
                Directory.CreateDirectory(options.OutputDirectory);
            }

            return Task.Run(() => this._engine.GetThumbnail(options));
        }

        public Task Gif(string inputFile, string outputFile, GifGenerationOptions options = null)
        {
            Guard.NotNullOrEmpty(inputFile, nameof(inputFile));
            Guard.NotNullOrEmpty(outputFile, nameof(outputFile));

            var outputDirectory = Path.GetDirectoryName(outputFile);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var param = EngineParameters.GIF(
               new MediaFile(inputFile),
               new MediaFile(outputFile),
               options ?? GifGenerationOptions.Default());

            return Task.Run(() => _engine.GenerateGIF(param));
        }
    }
}
