using System.IO;

namespace MediaToolkit.HLSOptions
{
    public class HLSGeneratingOptions
    {
        public HLSGeneratingOptions(string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            this.OuputDirectory = outputDirectory;
        }

        public string OuputDirectory { get; set; }
        public int HLSTime { get; set; } = 8;
        public string HLSPlayListType { get; set; }
        public string HLSSegementFileName { get; set; }
        public string HLSPlayListFileName { get; set; }
        public int ConstantRateFactor { get; set; } = 20;
        public int KeyFramePerFrame { get; set; } = 48;
        public bool CreateKeyFrameOnScneneChange { get; set; } = false;
        public string DefaultParams => "-profile:v main -sc_threshold 0";
    }
}
