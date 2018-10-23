using System;

namespace MediaToolkit.Options
{
    public class Rectangle
    {
        public Rectangle()
        {
        }

        public Rectangle(int width, int height)
        {
            Guard.Positive(width, nameof(width));
            Guard.Positive(height, nameof(height));

            this.Width = width;
            this.Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public override string ToString() => $"{this.Width}x{this.Height}";
    }
}