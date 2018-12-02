using System;

namespace MediaToolkit.Model
{
    public class MediaFile
    {
        public MediaFile() { }

        public MediaFile(string filename)
        {
            this.Filename = filename;
            this.Metadata = new Metadata();
        }

        public string Filename { get; set; }

        public Metadata Metadata { get; internal set; }

        public override string ToString() => $"{this.Filename} {Environment.NewLine} {this.Metadata}";
    }
}
