using MediaToolkit.Options.GIF;

namespace MediaToolkit
{
    public class GIFParameters : EngineParameters
    {
        public GifGenerationOptions GifOptions { get; set; }

        public string Serialize() => this.GifOptions.Serialize();
    }
}