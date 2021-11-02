using System;
using System.IO;

namespace Domain.Entities
{
    public class Image
    {
        public Stream ImageStream { get; set; }
        public string Filename { get; set; }

        public Image(Stream imageStream, string filename)
        {
            ImageStream = imageStream;
            Filename = filename;
        }
    }
}
