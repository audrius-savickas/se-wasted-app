using Domain.Entities;

namespace Persistence.Interfaces
{
    public interface IImageService
    {
        void SaveImage(Image image);
        Image ReadImage(string imageSrc);
    }
}
