﻿using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories.Base;

namespace MyShop.Core.Interfaces.Repositories
{
    public interface IImageGalleryRepository : IBaseRepository<ImageGallery, string, EntitySortingEnum>
    {
    }
}