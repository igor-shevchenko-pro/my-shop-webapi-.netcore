using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;

namespace MyShop.BL.Services
{
    public class ImageGalleryService : BaseService<ImageGalleryAddApiModel, ImageGalleryGetFullApiModel, ImageGalleryGetMinApiModel, ImageGallery, string, EntitySortingEnum>, IImageGalleryService
    {
        public ImageGalleryService(IImageGalleryRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
        }
    }
}