using Domain.Entities;
using Persistence.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ImageService : IImageService
    {
        public ImageService()
        {
        }

        public Image ReadImage(string imageSrc)
        {
            return new Image
            (
                new FileStream(imageSrc, FileMode.Open),
                imageSrc
            );
        }

        public void SaveImage(Image image)
        {
            using FileStream destination = File.Create(image.Filename);
            image.ImageStream.CopyTo(destination);
        }
    }
}
