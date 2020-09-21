using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories
{
    public class ImageGalleryRepository : BaseRepository<ImageGallery, string, EntitySortingEnum>, IImageGalleryRepository
    {
        public ImageGalleryRepository(IGenericRepository<ImageGallery, string> repository) : base(repository)
        {
        }
    }
}